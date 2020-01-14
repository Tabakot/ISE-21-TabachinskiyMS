using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    class FireEngine : IEngine
    {
        public float posx;
        public float posy;

        public FireEngine(float _startPosX, float _startPosY)
        {
            posx = _startPosX;
            posy = _startPosY;

        }

        public void Fire(Graphics g, float posx, float posy)
        {
            Point fireP1 = new Point((int)posx, ((int)posy + 3));
            Point fireP2 = new Point((int)posx - 1, (int)posy + 2);
            Point fireP3 = new Point((int)posx - 3, (int)posy + 1);
            Point fireP4 = new Point((int)posx - 6, (int)posy + 1);
            Point fireP5 = new Point((int)posx - 5, (int)posy + 2);
            Point fireP6 = new Point((int)posx - 10, (int)posy + 2);
            Point fireP7 = new Point((int)posx - 9, ((int)posy + 3));
            Point fireP8 = new Point((int)posx - 10, (int)posy + 3);
            Point fireP9 = new Point((int)posx - 15, (int)posy + 7);
            Point fireP10 = new Point((int)posx - 13, (int)posy + 7);
            Point fireP11 = new Point((int)posx - 11, (int)posy + 6);
            Point fireP12 = new Point((int)posx - 9, (int)posy + 7);
            Point fireP13 = new Point((int)posx - 10, (int)posy + 8);
            Point fireP14 = new Point((int)posx - 8, (int)posy + 8);
            Point fireP15 = new Point((int)posx - 6, (int)posy + 9);
            Point fireP16 = new Point((int)posx - 5, ((int)posy + 9));
            Point fireP17 = new Point((int)posx - 6, (int)posy + 10);
            Point fireP18 = new Point((int)posx - 3, (int)posy + 10);
            Point fireP19 = new Point((int)posx - 1, (int)posy + 6);
            Point fireP20 = new Point((int)posx, (int)posy + 4);

            Point[] fire =
            {
                fireP1,
                fireP2,
                fireP3,
                fireP4,
                fireP5,
                fireP6,
                fireP7,
                fireP8,
                fireP9,
                fireP10,
                fireP11,
                fireP12,
                fireP13,
                fireP14,
                fireP15,
                fireP16,
                fireP17,
                fireP18,
                fireP19,
                fireP20
            };
            Brush brush = new SolidBrush(Color.Red);
            g.FillPolygon(brush, fire);
        }

        public void Engine(Graphics g, Pen pen, Brush brush, float posx, float posy)
        {
            Point engineP1 = new Point((int)posx, ((int)posy + 3));
            Point engineP2 = new Point((int)posx + 7, (int)posy);
            Point engineP3 = new Point((int)posx + 11, (int)posy);
            Point engineP4 = new Point((int)posx + 11, (int)posy + 10);
            Point engineP5 = new Point((int)posx + 7, (int)posy + 10);
            Point engineP6 = new Point((int)posx, (int)posy + 7);

            Point[] engine =
            {
                 engineP1,
                 engineP2,
                 engineP3,
                 engineP4,
                 engineP5,
                 engineP6,
             };
            g.DrawPolygon(pen, engine);
            g.FillPolygon(brush, engine);
            Fire(g, posx, posy);

        }

        public void DrawEngine(CountEngine count, Graphics g, Brush color)
        {
            Pen pen = new Pen(Color.Black, 2);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            switch ((int)count)
            {
                case 4:
                    Engine(g, pen, color, posx + 17, posy + 18);
                    Engine(g, pen, color, posx + 60, posy + 18);
                    break;
                case 5:
                    Engine(g, pen, color, posx + 50, posy - 8);
                    Engine(g, pen, color, posx + 17, posy + 18);
                    Engine(g, pen, color, posx + 60, posy + 18);
                    break;
                case 6:
                    Engine(g, pen, color, posx - 5, posy + 6);
                    Engine(g, pen, color, posx + 17, posy + 18);
                    Engine(g, pen, color, posx + 60, posy + 18);
                    break;

            }

        }
    }
}
