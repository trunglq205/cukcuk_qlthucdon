using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MISA.CUKCUK.Core.Interfaces.Service;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HobbysController : MISABaseController<Hobby>
    {
        #region Fields
        IHobbyRepository _hobbyRepository;
        IHobbyService _hobbyService;
        #endregion

        #region Constructor
        public HobbysController(IHobbyRepository hobbyRepository, IHobbyService hobbyService) : base(hobbyService, hobbyRepository)
        {
            _hobbyRepository = hobbyRepository;
            _hobbyService = hobbyService;
        }
        #endregion
    }
}
