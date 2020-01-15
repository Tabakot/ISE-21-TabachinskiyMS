using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    /// <summary>
    /// Класс-ошибка "Если место, на которое хотим поставить самолет уже занято"
    /// </summary>
    public class DockOccupiedPlaceException : InvalidOperationException
    {
        public DockOccupiedPlaceException(int i) : base("Place " + i + " is already taken")
        { }
    }
}
