using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    /// <summary>
    /// Класс-ошибка "Если не найден самолет по определенному месту"
    /// </summary>
    public class DockNotFoundException : ArgumentNullException
    {
        public DockNotFoundException(int i) : base("Not found on place "
       + i)
        { }
    }

}
