using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgWin
{
    /// <summary>
    /// Класс-ошибка "The same plane is already here"
    /// </summary>
    public class DockAlreadyHaveException : Exception
    {
        public DockAlreadyHaveException() : base("The same plane is already here")
        { }
    }

}
