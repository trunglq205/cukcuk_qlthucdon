using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Exception;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceHobbysController : ControllerBase
    {
        #region Fields
        IServiceHobbyRepository _serviceHobbyRepository;
        /// <summary>
        /// Bắt lỗi
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="userMessage"></param>
        /// <returns></returns>
        protected object CatchMISAValidateException(Exception ex, string userMessage)
        {
            var response = new
            {
                devMsg = ex.Message,
                userMsg = userMessage,
                data = ex.InnerException
            };
            return response;
        }
        #endregion

        #region Constructor
        public ServiceHobbysController(IServiceHobbyRepository serviceHobbyRepository)
        {
            _serviceHobbyRepository = serviceHobbyRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách sở thích phục vụ theo ID thực đơn
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        /// created by LQTrung (26/02/2022)
        [HttpGet("{menuID}")]
        public IActionResult GetServiceHobbyByMenuID(Guid menuID)
        {
            try
            {
                var data = _serviceHobbyRepository.GetByMenuID(menuID);
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

        /// <summary>
        /// Thêm mới sở thích phục vụ cho thực đơn
        /// </summary>
        /// <param name="serviceHobby"></param>
        /// <returns></returns>
        /// created by LQTrung (26/02/2022)
        [HttpPost]
        public IActionResult Post(ServiceHobby serviceHobby)
        {
            try
            {
                var data = _serviceHobbyRepository.Insert(serviceHobby);
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

        /// <summary>
        /// Xóa sở thích phục vụ
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="hobbyID"></param>
        /// <returns></returns>
        /// created by LQTrung (26/02/2022)
        [HttpDelete("{menuID}/{hobbyID}")]
        public IActionResult Delete(Guid menuID, Guid hobbyID)
        {
            try
            {
                var data = _serviceHobbyRepository.Delete(menuID,hobbyID);
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

        #endregion
    }
}
