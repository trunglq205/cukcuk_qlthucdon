using MISA.CUKCUK.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Service
{
    public interface IMenuService : IBaseService<Menu>
    {
        string GetMenuCode(string menuName);
    }
}
