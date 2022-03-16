using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Exception;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MISA.CUKCUK.Core.Interfaces.Service;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenusController : MISABaseController<Menu>
    {
        #region Fields
        IMenuRepository _menuRepository;
        IMenuService _menuService;
        #endregion

        #region Constructor
        public MenusController(IMenuRepository menuRepository, IMenuService menuService) : base(menuService, menuRepository)
        {
            _menuRepository = menuRepository;
            _menuService = menuService;
        }
        #endregion

        [HttpPost]
        public override IActionResult Post(Menu menu)
        {
            try
            {
                var data = _menuService.InsertService(menu);
                return StatusCode(201, data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resource.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }


        [HttpGet("get-menucode/{menuName}")]
        public IActionResult GetMenuCodeByMenuName(string menuName)
        {
            try
            {
                var data = _menuService.GetMenuCode(menuName);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resource.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }

        [HttpPost("filter")]
        public IActionResult Filter(FilterEntity filter)
        {
            try
            {
                var data = _menuRepository.Filter(filter);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resource.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }
    }
}
