using LondonAPI.Models;
using LondonAPI.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LondonAPI.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion(version:"1.0")]
    public class RootController : ControllerBase
    {
        public RootController() { }

        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Rooms = Link.ToCollection(nameof(RoomsController.GetAllRooms)),
                Info = Link.To(nameof(InfoController.GetInfo))
                //href = Link.To(nameof(GetRoot), null),
                //rooms = new
                //{
                //    href = Link.To(nameof(RoomsController.GetRooms))
                  
                //},
                //info = new
                //{
                //    href = Link.To(nameof(InfoController.GetInfo))
                //}
            };

            return Ok(response);
        }
    }
}
