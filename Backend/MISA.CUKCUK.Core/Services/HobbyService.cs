using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MISA.CUKCUK.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Services
{
    public class HobbyService : BaseService<Hobby>, IHobbyService
    {
        #region Fields
        IHobbyRepository _hobbyRepository;
        #endregion

        #region Constructor
        public HobbyService(IHobbyRepository hobbyRepository) : base(hobbyRepository)
        {
            _hobbyRepository = hobbyRepository;
        }
        #endregion
    }
}
