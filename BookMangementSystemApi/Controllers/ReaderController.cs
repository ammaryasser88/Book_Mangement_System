using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMangementSystemApi.Controllers
{
    [Route("api/reader")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _readerService;
        
        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReaderRequest readerRequest)
        {
            var reader = await _readerService.AddReader(readerRequest);
            return Ok(reader);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var readers = await _readerService.GetAllReaders();
            return Ok(readers);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reader = await _readerService.GetReaderById(id);
            return Ok(reader);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ReaderRequest readerRequest)
        {
            var readerUpdated = await _readerService.UpdateReader(id, readerRequest);
            return Ok(readerUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _readerService.DeleteReader(id);
            return Ok("The Reader Has Been Successfully Deleted");
        }

    }
}
