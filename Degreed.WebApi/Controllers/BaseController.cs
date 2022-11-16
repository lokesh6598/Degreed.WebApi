using Degreed.WebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Degreed.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        #region Public Methods
		/// <summary>
		/// This method will return the standard json response
		/// </summary>
		/// <param name="statuscode"></param>
		/// <param name="message"></param>
		/// <returns></returns>
        public static JsonResponse JsonResult(int statuscode, string message)
		{
			return new JsonResponse()
			{
				StatusCode = statuscode,
				Message = message
			};
		}

		/// <summary>
		/// This method will return the standard json reponse with view model
		/// </summary>
		/// <param name="statuscode"></param>
		/// <param name="message"></param>
		/// <param name="results"></param>
		/// <returns></returns>
		public static JsonResponse JsonResult(int statuscode, string message, object results = null)
		{
			return new JsonResponse()
			{
				StatusCode = statuscode,
				Message = message,
				Result = results
			};
		}
        #endregion
    }
}
