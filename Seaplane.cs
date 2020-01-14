using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TechProgWin
{
    public class Seaplane
    {
        public static float _startPosX;

        public static float _startPosY;

        private int _pictureWidth;

        private int _pictureHeight;

        private const int planeWidth = 100;

        private const int planeHeight = 60;

        public СountEngine Engines { private set; get; }

        public float PropellerWidth;

        public int MaxSpeed { private set; get; }

        public float Weight { private set; get; }
        
        public Color MainColor { private set; get; }

        public Color DopColor { private set; get; }

        public bool Wheels { private set; get; }

        public bool PlaneFloat { private set; get; }

        public bool HiddenPropeller { private set; get; }

        public bool HiddenEngines { private set; get; }


        public Seaplane(int maxSpeed, float weight, float propellerWidth, Color mainColor, Color dopColor,
bool wheels, bool planeFloat, bool hiddenPropeller,bool hiddenEngines, СountEngine engine)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            PropellerWidth = propellerWidth;
            MainColor = mainColor;
            DopColor = dopColor;
            Wheels = wheels;
            PlaneFloat = planeFloat;
            HiddenPropeller = hiddenPropeller;
            HiddenEngines = hiddenEngines;
            Engines = engine;
        }

        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }

        public void MoveTransport(Direction direction)
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




        public void DrawPlane(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Black, 2);
            g.DrawRectangle(pen, _startPosX - 5, _startPosY - 6, 10, 13);
            g.DrawEllipse(pen, _startPosX - 5, _startPosY - 6, 15, 23);

            Brush blackBrush = new SolidBrush(Color.Black);
            if (!HiddenPropeller)
            {
                g.FillEllipse(blackBrush, _startPosX + 88, _startPosY + 17, 7, 6);
                g.DrawLine(pen, _startPosX + 95, _startPosY + 20 - PropellerWidth, _startPosX + 95, _startPosY + 20 + PropellerWidth);
            }

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

            Point bodyLine1 = new Point((int)_startPosX - 5, ((int)_startPosY + 7));
            Point bodyLine2 = new Point((int)_startPosX, (int)_startPosY + 17);
            Point bodyLine3 = new Point((int)_startPosX + 40, (int)_startPosY + 30);
            Point bodyLine4 = new Point((int)_startPosX + 80, (int)_startPosY + 30);

            Point[] bodyLine =
            {
                 bodyLine1,
                 bodyLine2,
                 bodyLine3,
                 bodyLine4,
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

            if (Wheels)
            {
                if (!PlaneFloat)
                {
                    g.DrawLine(pen, _startPosX + 40, _startPosY + 30, _startPosX + 45, _startPosY + 45);
                    g.DrawLine(pen, _startPosX + 55, _startPosY + 30, _startPosX + 50, _startPosY + 45);
                    g.DrawLine(pen, _startPosX + 65, _startPosY + 30, _startPosX + 70, _startPosY + 45);
                    g.DrawLine(pen, _startPosX + 80, _startPosY + 30, _startPosX + 75, _startPosY + 45);
                }

                Brush grayColor = new SolidBrush(Color.SlateGray);
                g.FillEllipse(blackBrush, _startPosX + 40, _startPosY + 43, 15, 15);
                g.FillEllipse(blackBrush, _startPosX + 65, _startPosY + 43, 15, 15);
                g.FillEllipse(grayColor, _startPosX + 43, _startPosY + 46, 9, 9);
                g.FillEllipse(grayColor, _startPosX + 68, _startPosY + 46, 9, 9);
                g.FillEllipse(blackBrush, _startPosX + 46, _startPosY + 49, 3, 3);
                g.FillEllipse(blackBrush, _startPosX + 71, _startPosY + 49, 3, 3);
            }


            if (PlaneFloat)
            {
                g.DrawLine(pen, _startPosX + 40, _startPosY + 30, _startPosX + 45, _startPosY + 45);
                g.DrawLine(pen, _startPosX + 55, _startPosY + 30, _startPosX + 50, _startPosY + 45);
                g.DrawLine(pen, _startPosX + 65, _startPosY + 30, _startPosX + 70, _startPosY + 45);
                g.DrawLine(pen, _startPosX + 80, _startPosY + 30, _startPosX + 75, _startPosY + 45);

                Point float1 = new Point((int)_startPosX + 25, ((int)_startPosY + 45));
                Point float2 = new Point((int)_startPosX + 30, (int)_startPosY + 50);
                Point float3 = new Point((int)_startPosX + 60, (int)_startPosY + 55);
                Point float4 = new Point((int)_startPosX + 80, (int)_startPosY + 55);
                Point float5 = new Point((int)_startPosX + 90, (int)_startPosY + 50);
                Point float6 = new Point((int)_startPosX + 95, (int)_startPosY + 45);

                Point[] planeFloat =
                {
                 float1,
                 float2,
                 float3,
                 float4,
                 float5,
                 float6,
             };
                g.DrawPolygon(pen, planeFloat);
                g.FillPolygon(bodyColor, planeFloat);
            }

            g.FillRectangle(bodyColor, _startPosX - 5, _startPosY - 6, 10, 13);
            g.FillEllipse(bodyColor, _startPosX - 5, _startPosY - 6, 15, 23);

            Brush frontLineColor = new SolidBrush(Color.IndianRed);
            g.FillPolygon(frontLineColor, frontLine);

            Brush dopColor = new SolidBrush(DopColor);
            g.FillPolygon(dopColor, bodyLine);

            Brush windowColor = new SolidBrush(Color.RoyalBlue);
            g.FillPolygon(windowColor, planeWindow);
            g.FillRectangle(bodyColor, _startPosX + 59, _startPosY + 5, 2, 8);

            if (!HiddenEngines)
            {
                SeaplaneEngine engine = new SeaplaneEngine(Engines, _startPosX, _startPosY);
                engine.DrawEngine(g);
            }
        }

    }
}
