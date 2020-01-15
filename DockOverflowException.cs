using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    /// <summary>
    /// Класс-ошибка "Если на парковке уже заняты все места"
    /// </summary>
    public class DockOverflowException : Exception
    {
        public DockOverflowException() : base("No free places")
        { }
    }
}
