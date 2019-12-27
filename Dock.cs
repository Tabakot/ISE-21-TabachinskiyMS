using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TechProgWin
{
    public class Dock<T, E> where T : class, ITransport where E : class, IEngine
    {
        private Dictionary<int, T> _places;

        private Stack<T> removedPlane;

        private int _maxCount;

        private int PictureWidth { get; set; }

        private int PictureHeight { get; set; }

        private const int _placeSizeWidth = 210;

        private const int _placeSizeHeight = 80;

        public Dock(int sizes, int pictureWidth, int pictureHeight)
        {
            _maxCount = sizes;
            _places = new Dictionary<int, T>();
            removedPlane = new Stack<T>();
            PictureWidth = pictureWidth;
            PictureHeight = pictureHeight;
        }

        public static int operator +(Dock<T, E> p, T plane)
        {
            if (p._places.Count == p._maxCount)
            {
                return -1;
            }

            for (int i = 0; i < p._maxCount; i++)
            {
                if (p.CheckFreePlace(i))
                {
                    p._places.Add(i, plane);
                    p._places[i].SetPosition(5 + i / 5 * _placeSizeWidth + 5,
                     i % 5 * _placeSizeHeight + 15, p.PictureWidth,
                    p.PictureHeight);
                    return i;
                }
            }
            return -1;
        }

        public static T operator -(Dock<T, E> p, int index)
        {
            if (!p.CheckFreePlace(index))
            {
                T plane = p._places[index];
                p.removedPlane.Push(plane);
                p._places.Remove(index);
                return plane;
            }
            return null;
        }

        private bool CheckFreePlace(int index)
        {
            return !_places.ContainsKey(index);
        }

        public T GetPlaneByKey(int key)
        {
            return _places.ContainsKey(key) ? _places[key] : null;
        }

        public void Draw(Graphics g)
        {
            DrawMarking(g);
            var keys = _places.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                _places[keys[i]].DrawPlane(g);
            }
        }

        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.AntiqueWhite, 3);
            pen.DashStyle = DashStyle.Dash;
            Brush whiteDock = new SolidBrush(Color.AntiqueWhite);
            Brush brush = new SolidBrush(Color.DodgerBlue);
            g.FillRectangle(brush, 0, 0, PictureWidth, PictureHeight);

            g.DrawRectangle(pen, 0, 0, (_maxCount / 5) * _placeSizeWidth, 480);
            for (int i = 0; i < _maxCount / 5; i++)
            {
                for (int j = 0; j < 6; ++j)
                {
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight,
                    i * _placeSizeWidth + 110, j * _placeSizeHeight);
                    g.FillEllipse(whiteDock, i * _placeSizeWidth + 100, j * _placeSizeHeight - 7, 14, 14);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth, 400);

            }
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public T this[int ind]
        {
            get
            {
                if (_places.ContainsKey(ind))
                {
                    return _places[ind];
                }
                return null;
            }
            set
            {
                if (CheckFreePlace(ind))
                {
                    _places.Add(ind, value);
                    _places[ind].SetPosition(5 + ind / 5 * _placeSizeWidth + 5, ind % 5
                    * _placeSizeHeight + 15, PictureWidth, PictureHeight);
                }
            }
        }
    }
}