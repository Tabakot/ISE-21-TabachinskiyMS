using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TechProgWin
{
    public class Dock<T> : IEnumerator<T>, IEnumerable<T>, IComparable<Dock<T>> where T : class, ITransport
    {
        private Dictionary<int, T> _places;

        private int _maxCount;

        private int PictureWidth { get; set; }

        private int PictureHeight { get; set; }

        private const int _placeSizeWidth = 210;

        private const int _placeSizeHeight = 80; 

        private int _currentIndex;

        /// <summary>
        /// Получить порядковое место на парковке
        /// </summary>
        public int GetKey
        {
            get
            {
                return _places.Keys.ToList()[_currentIndex];
            }
        }

        public Dock(int sizes, int pictureWidth, int pictureHeight)
        {
            _maxCount = sizes;
            _places = new Dictionary<int, T>();
            _currentIndex = -1;
            PictureWidth = pictureWidth;
            PictureHeight = pictureHeight;
        }


        public static int operator +(Dock<T> p, T plane)
        {
            if (p._places.Count == p._maxCount)
            {
                throw new DockOverflowException();
            }

            if (p._places.ContainsValue(plane))
            {
                throw new DockAlreadyHaveException();
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

        public static T operator -(Dock<T> p, int index)
        {
            if (!p.CheckFreePlace(index))
            {
                T plane = p._places[index];
                p._places.Remove(index);
                return plane;
            }
            throw new DockNotFoundException(index);
        }

        private bool CheckFreePlace(int index)
        {
            return !_places.ContainsKey(index);
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
                throw new DockNotFoundException(ind);
            }
            set
            {
                if (CheckFreePlace(ind))
                {
                    _places.Add(ind, value);
                    _places[ind].SetPosition(5 + ind / 5 * _placeSizeWidth + 5, ind % 5
                    * _placeSizeHeight + 15, PictureWidth, PictureHeight);
                }
                else
                {
                    throw new DockOccupiedPlaceException(ind);
                }

            }
        }

        /// <summary>
        /// Метод интерфейса IEnumerator для получения текущего элемента
        /// </summary>
        public T Current
        {
            get
            {
                return _places[_places.Keys.ToList()[_currentIndex]];
            }
        }

        /// <summary>
        /// Метод интерфейса IEnumerator для получения текущего элемента
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        /// <summary>
        /// Метод интерфейса IEnumerator, вызываемый при удалении объекта
        /// </summary>
        public void Dispose()
        {
            _places.Clear();
        }

        /// <summary>
        /// Метод интерфейса IEnumerator для перехода к следующему элементу или началу коллекции
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (_currentIndex + 1 >= _places.Count)
            {
                Reset();
                return false;
            }
            _currentIndex++;
            return true;
        }

        /// <summary>
        /// Метод интерфейса IEnumerator для сброса и возврата к началу коллекции
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }

        /// <summary>
        /// Метод интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Метод интерфейса IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Метод интерфейса IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Dock<T> other)
        {
            if (_places.Count > other._places.Count)
            {
                return -1;
            }
            else if (_places.Count < other._places.Count)
            {
                return 1;
            }
            else if (_places.Count > 0)
            {
                var thisKeys = _places.Keys.ToList();
                var otherKeys = other._places.Keys.ToList();
                for (int i = 0; i < _places.Count; ++i)
                {
                    if (_places[thisKeys[i]] is Plane && other._places[thisKeys[i]] is
                   Seaplane)
                    {
                        return 1;
                    }
                    if (_places[thisKeys[i]] is Seaplane && other._places[thisKeys[i]]
                    is Plane)
                    {
                        return -1;
                    }
                    if (_places[thisKeys[i]] is Plane && other._places[thisKeys[i]] is
                    Plane)
                    {
                        return (_places[thisKeys[i]] is
                       Plane).CompareTo(other._places[thisKeys[i]] is Plane);
                    }
                    if (_places[thisKeys[i]] is Seaplane && other._places[thisKeys[i]]
                    is Seaplane)
                    {
                        return (_places[thisKeys[i]] is
                       Seaplane).CompareTo(other._places[thisKeys[i]] is Seaplane);
                    }
                }
            }
            return 0;
        }

    }
}