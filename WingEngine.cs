using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    class WingEngine : IEngine
    {
        public float posx;
        public float posy;

        public WingEngine(float _startPosX, float _startPosY)
        {
            posx = _startPosX;
            posy = _startPosY;

        }

        public void Engine(Graphics g, Pen pen, Brush brush, float posx, float posy)
        {
            Point engineP1 = new Point((int)posx, ((int)posy + 10));
            Point engineP2 = new Point((int)posx + 8, (int)posy + 1);
            Point engineP3 = new Point((int)posx + 16, (int)posy);
            Point engineP4 = new Point((int)posx + 16, (int)posy + 10);
            Point engineP5 = new Point((int)posx + 7, (int)posy + 10);
            Point engineP6 = new Point((int)posx, (int)posy + 10);

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
            g.DrawEllipse(pen, (int)posx + 5, (int)posy, 10, 9);
            g.FillEllipse(brush, (int)posx + 5, (int)posy, 10, 9);
            g.FillPolygon(brush, engine);

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
