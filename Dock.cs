using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TechProgWin;
public class Dock<T> where T : class, ITransport
{
    private T[] _places;

    private int PictureWidth { get; set; }

    private int PictureHeight { get; set; }

    private const int _placeSizeWidth = 210;

    private const int _placeSizeHeight = 80;

    public Dock(int sizes, int pictureWidth, int pictureHeight)
    {
        _places = new T[sizes];
        PictureWidth = pictureWidth;
        PictureHeight = pictureHeight;
        for (int i = 0; i < _places.Length; i++)
        {
            _places[i] = null;
        }
    }

    public static int operator +(Dock<T> p, T plane)
    {
        for (int i = 0; i < p._places.Length; i++)
        {
            if (p.CheckFreePlace(i))
            {
                p._places[i] = plane;
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
        if (index < 0 || index > p._places.Length)
        {
            return null;
        }

        if (!p.CheckFreePlace(index))
        {
            T plane = p._places[index];
            p._places[index] = null;
            return plane;
        }
        return null;
    }

    private bool CheckFreePlace(int index)
    {
        return _places[index] == null;
    }

    public void Draw(Graphics g)
    {
        DrawMarking(g);
        for (int i = 0; i < _places.Length; i++)
        {
            if (!CheckFreePlace(i))
            {
                _places[i].DrawPlane(g);
            }
        }
    }
    
    private void DrawMarking(Graphics g)
    {
        Pen pen = new Pen(Color.AntiqueWhite, 3);
        pen.DashStyle = DashStyle.Dash;
        Brush whiteDock = new SolidBrush(Color.AntiqueWhite);
        Brush brush = new SolidBrush(Color.DodgerBlue);
        g.FillRectangle(brush, 0, 0, PictureWidth, PictureHeight);

        g.DrawRectangle(pen, 0, 0, (_places.Length / 5) * _placeSizeWidth, 480);
        for (int i = 0; i < _places.Length / 5; i++)
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
}
