using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    public enum countEngine
    {
        Four,
        Five,
        Six
    }

    class SeaplaneEngine
    {
        public countEngine engineCount { private get; set; }
        float posx = Seaplane._startPosX;
        float posy = Seaplane._startPosY;

        public SeaplaneEngine(countEngine numberOfEngine, float _startPosX, float _startPosY)
        {
            engineCount = numberOfEngine;
            posx = _startPosX;
            posy = _startPosY;

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

        }

        public void DrawEngine(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            Brush white = new SolidBrush(Color.LightGray);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            switch (engineCount)
            {
                case countEngine.Four:
                    Engine(g, pen, white, posx + 17, posy + 18);
                    Engine(g, pen, white, posx + 60, posy + 18);
                    break;
                case countEngine.Five:
                    Engine(g, pen, white, posx + 50, posy - 8);
                    Engine(g, pen, white, posx + 17, posy + 18);
                    Engine(g, pen, white, posx + 60, posy + 18);
                    break;
                case countEngine.Six:
                    Engine(g, pen, white, posx - 5, posy + 6);
                    Engine(g, pen, white, posx + 17, posy + 18);
                    Engine(g, pen, white, posx + 60, posy + 18);
                    break;

            }

        }
    }
}
