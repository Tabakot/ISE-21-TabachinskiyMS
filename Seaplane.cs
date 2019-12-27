using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TechProgWin
{
    public class Seaplane : Plane
    {      
        public float PropellerWidth;

        public Color DopColor { private set; get; }

        public bool Wheels { private set; get; }

        public bool PlaneFloat { private set; get; }

        public bool HiddenPropeller { private set; get; }


        public Seaplane(int maxSpeed, float weight, float propellerWidth, Color mainColor, Color dopColor,
bool wheels, bool planeFloat, bool hiddenPropeller) : base (maxSpeed, weight, mainColor)

        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            PropellerWidth = propellerWidth;
            MainColor = mainColor;
            DopColor = dopColor;
            Wheels = wheels;
            PlaneFloat = planeFloat;
            HiddenPropeller = hiddenPropeller;
        }

       




        public override void DrawPlane(Graphics g)
        {
            g.SmoothingMode =
       System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Black, 2);

            Brush blackBrush = new SolidBrush(Color.Black);
            if (!HiddenPropeller)
            {
                g.FillEllipse(blackBrush, _startPosX + 88, _startPosY + 17, 7, 6);
                g.DrawLine(pen, _startPosX + 95, _startPosY + 20 - PropellerWidth, _startPosX + 95, _startPosY + 20 + PropellerWidth);
            }

            base.DrawPlane(g);
            Brush bodyColor = new SolidBrush(MainColor);

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


            Brush dopColor = new SolidBrush(DopColor);
            g.FillPolygon(dopColor, bodyLine);


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
        }

    }
}
