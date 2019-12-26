using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    public interface IEngine
    {
        void DrawEngine(CountEngine countEngine, Graphics g, Brush color);
    }
}
