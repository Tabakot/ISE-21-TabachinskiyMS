using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TechProgWin
{
    public class Seaplane : Plane, IComparable<Seaplane>, IEquatable<Seaplane>
    {
        public float PropellerWidth;

        public Color DopColor { private set; get; }

        public bool Wheels { private set; get; }

        public bool PlaneFloat { private set; get; }

        public bool HiddenPropeller { private set; get; }

        public CountEngine Count { private set; get; }

        public int Type;


        public Seaplane(int maxSpeed, float weight, Color mainColor, Color dopColor, float propellerWidth,
bool wheels, bool planeFloat, bool hiddenPropeller, CountEngine countEngine) : base (maxSpeed, weight, mainColor)

        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            PropellerWidth = propellerWidth;
            Wheels = wheels;
            PlaneFloat = planeFloat;
            HiddenPropeller = hiddenPropeller;
            Count = countEngine;

            Type = 3;
        }


        public Seaplane(string info) : base(info)
        {
            string[] strs = info.Split(';');
            if (strs.Length == 10)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
                DopColor = Color.FromName(strs[3]);
                PropellerWidth = Convert.ToInt32(strs[4]);
                Wheels = Convert.ToBoolean(strs[5]);
                PlaneFloat = Convert.ToBoolean(strs[6]);
                HiddenPropeller = Convert.ToBoolean(strs[7]);
                if (strs[8] == "Four")
                {
                    Count = CountEngine.Four;
                }
                else if (strs[8] == "Five")
                {
                    Count = CountEngine.Five;
                }
                else if (strs[8] == "Six")
                {
                    Count = CountEngine.Six;
                }
                Type = Convert.ToInt32(strs[9]);
            }
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


            IEngine engine;

            switch (Type)
            {
                case 0:
                    engine = new WingEngine(_startPosX, _startPosY);
                    break;
                case 1:
                    engine = new FireEngine(_startPosX, _startPosY);
                    break;
                default:
                    engine = new DefaultEngine(_startPosX, _startPosY);
                    break;
            }
            engine.DrawEngine(Count, g, dopColor);
        }

        public void SetDopColor(Color color)
        {
            DopColor = color;
        }

        public override string ToString()
        {
            return base.ToString() + ";" + DopColor.Name + ";" + PropellerWidth + ";" + Wheels + ";" +
           PlaneFloat + ";" + HiddenPropeller + ";" + Count + ";" + Type;
        }

        public void SetEngineType(int type)
        {
            Type = type;
        }

        /// <summary>
        /// Метод интерфейса IComparable для класса Seaplane
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Seaplane other)
        {
            var res = (this is Plane).CompareTo(other is Plane);
            if (res != 0)
            {
                return res;
            }
            if (DopColor != other.DopColor)
            {
                DopColor.Name.CompareTo(other.DopColor.Name);
            }
            if (PropellerWidth != other.PropellerWidth)
            {
                return PropellerWidth.CompareTo(other.PropellerWidth);
            }
            if (Wheels != other.Wheels)
            {
                return Wheels.CompareTo(other.Wheels);
            }
            if (PlaneFloat != other.PlaneFloat)
            {
                return PlaneFloat.CompareTo(other.PlaneFloat);
            }
            if (HiddenPropeller != other.HiddenPropeller)
            {
                return HiddenPropeller.CompareTo(other.HiddenPropeller);
            }
            return 0;
        }

        /// <summary>
        /// Метод интерфейса IEquatable для класса Seaplane
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Seaplane other)
        {
            var res = (this as Plane).Equals(other as Plane);
            if (!res)
            {
                return res;
            }
            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }
            if (DopColor != other.DopColor)
            {
                return false;
            }
            if (PropellerWidth != other.PropellerWidth)
            {
                return false;
            }
            if (Wheels != other.Wheels)
            {
                return false;
            }
            if (PlaneFloat != other.PlaneFloat)
            {
                return false;
            }
            if (HiddenPropeller != other.HiddenPropeller)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Перегрузка метода от object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Seaplane planeObj))
            {
                return false;
            }
            else
            {
                return Equals(planeObj);
            }
        }

        /// <summary>
        /// Перегрузка метода от object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}