using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    /// <summary>
    /// Класс-ошибка "Если не найден автомобиль по определенному месту"
    /// </summary>
    public class DockNotFoundException : Exception
    {
        public DockNotFoundException(int i) : base("Not found on place "
       + i)
        { }
    }

}
