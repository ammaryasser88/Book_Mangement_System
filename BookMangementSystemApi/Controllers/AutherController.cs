using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMangementSystemApi.Controllers
{
    [Route("api/auther")]
    [ApiController]
    public class AutherController : ControllerBase
    {
        private readonly IAutherService _autherService;
        public AutherController(IAutherService autherService)
        {
            _autherService = autherService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutherRequest autherRequest)
        {
            var auther = await _autherService.AddAuther(autherRequest);
            return Ok(auther);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var authers = await _autherService.GetAllAuthers();
            return Ok(authers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var auther =   await _autherService.GetAutherById(id);
            return Ok(auther);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,AutherRequest autherRequest)
        {
            var autherUpdate = await _autherService.UpdateAuther(id,autherRequest);
            return Ok("Auther Has Been Successfully Updated");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            await _autherService.DeleteAuther(id);
            return Ok("The Auther Has Been Successfully Deleted");
        }
    }
}
