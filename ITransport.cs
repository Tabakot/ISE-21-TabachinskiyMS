using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    public interface ITransport
    {
        void SetPosition(int x, int y, int width, int height);

        void MoveTransport(Direction direction);

        void DrawPlane(Graphics g);

        /// <summary>
        /// Смена основного цвета автомобиля
        /// </summary>
        /// <param name="color"></param>
        void SetMainColor(Color color);
    }
}
