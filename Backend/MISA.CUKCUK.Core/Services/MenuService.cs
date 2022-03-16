using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MISA.CUKCUK.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Services
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        #region Fields
        IMenuRepository _menuRepository;
        #endregion

        #region Constructor
        public MenuService(IMenuRepository menuRepository) : base(menuRepository)
        {
            _menuRepository = menuRepository;
        }


        #endregion

        public override int InsertService(Menu menu)
        {
            base.ValidateData(menu, menu.MenuID);
            var res = _menuRepository.Insert(menu);
            return res;
        }

        public override int UpdateService(Menu menu, Guid menuID)
        {
            base.ValidateData(menu, menu.MenuID);
            var res = _menuRepository.Update(menu, menuID);
            return res;
        }

        public string GetMenuCode(string menuName)
        {
            string menuCode = String.Empty;
            if (menuName != null)
            {
                string[] arr = menuName.Trim().Split(' ');
                foreach (string item in arr)
                {
                    menuCode += item.Substring(0, 1).ToUpper();
                }
                if (_menuRepository.CheckMenuCode(menuCode))
                {
                    menuCode = String.Empty;
                    foreach (string item in arr)
                    {
                        menuCode += item.ToUpper();
                    }
                }
            }
            else
            {
                return "";
            }
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = menuCode.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
