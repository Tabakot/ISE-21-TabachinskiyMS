using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin   
{
    public class Plane : Vehicle
    {
        protected const int planeWidth = 100;

        protected const int planeHeight = 60;

        public Plane(int maxSpeed, float weight, Color mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
        }

        public Plane(string info)
        {
            string[] strs = info.Split(';');
            if (strs.Length == 3)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
            }
        }


        public override void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - planeWidth)
                    {
                        _startPosX += step;
                    }
                    break;

                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;

                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;

                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - planeHeight)  
                   {
                        _startPosY += step;
                    }
                    break;
            }
        }
        public override void DrawPlane(Graphics g)
        {
            g.SmoothingMode =
      System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Black, 2);
            g.DrawRectangle(pen, _startPosX - 5, _startPosY - 6, 10, 13);
            g.DrawEllipse(pen, _startPosX - 5, _startPosY - 6, 15, 23);

            Brush blackBrush = new SolidBrush(Color.Black);

            Point body1 = new Point((int)_startPosX - 5, ((int)_startPosY + 7));
            Point body2 = new Point((int)_startPosX, (int)_startPosY + 17);
            Point body3 = new Point((int)_startPosX + 40, (int)_startPosY + 30);
            Point body4 = new Point((int)_startPosX + 80, (int)_startPosY + 30);
            Point body5 = new Point((int)_startPosX + 90, (int)_startPosY + 25);
            Point body6 = new Point((int)_startPosX + 90, (int)_startPosY + 15);
            Point body7 = new Point((int)_startPosX + 80, (int)_startPosY + 13);
            Point body8 = new Point((int)_startPosX + 70, (int)_startPosY + 3);
            Point body9 = new Point((int)_startPosX + 50, (int)_startPosY + 3);
            Point body10 = new Point((int)_startPosX + 35, (int)_startPosY + 13);

            Point[] body =
             {
                 body1,
                 body2,
                 body3,
                 body4,
                 body5,
                 body6,
                 body7,
                 body8,
                 body9,
                 body10,
             };

           

            Point frontLine1 = new Point((int)_startPosX + 80, (int)_startPosY + 30);
            Point frontLine2 = new Point((int)_startPosX + 90, (int)_startPosY + 25);
            Point frontLine3 = new Point((int)_startPosX + 90, (int)_startPosY + 15);
            Point frontLine4 = new Point((int)_startPosX + 40, (int)_startPosY + 30);

            Point[] frontLine =
            {
                 frontLine1,
                 frontLine2,
                 frontLine3,
                 frontLine4,
             };

            Point planeWindow1 = new Point((int)_startPosX + 78, (int)_startPosY + 13);
            Point planeWindow2 = new Point((int)_startPosX + 68, (int)_startPosY + 5);
            Point planeWindow3 = new Point((int)_startPosX + 52, (int)_startPosY + 5);
            Point planeWindow4 = new Point((int)_startPosX + 37, (int)_startPosY + 13);

            Point[] planeWindow =
            {
                 planeWindow1,
                 planeWindow2,
                 planeWindow3,
                 planeWindow4,
             };


            g.DrawPolygon(pen, body);

            Brush bodyColor = new SolidBrush(MainColor);
            g.FillPolygon(bodyColor, body);

            g.FillRectangle(bodyColor, _startPosX - 5, _startPosY - 6, 10, 13);
            g.FillEllipse(bodyColor, _startPosX - 5, _startPosY - 6, 15, 23);

            Brush frontLineColor = new SolidBrush(Color.IndianRed);
            g.FillPolygon(frontLineColor, frontLine);

            Brush windowColor = new SolidBrush(Color.RoyalBlue);
            g.FillPolygon(windowColor, planeWindow);
            g.FillRectangle(bodyColor, _startPosX + 59, _startPosY + 5, 2, 8);
        }

        public override string ToString()
        {
            return MaxSpeed + ";" + Weight + ";" + MainColor.Name;
        }

    }
}
