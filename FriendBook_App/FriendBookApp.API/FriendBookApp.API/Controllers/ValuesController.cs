using System.Threading.Tasks;
using FriendBookApp.API.Data.Interfaces;
using FriendBookApp.API.Dto;
using FriendBookApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriendBookApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private IValueRepository _valueRepository;

        public ValuesController(IValueRepository ValueRepository)
        {
            _valueRepository = ValueRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
           var values = await _valueRepository.GetValues();
           return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _valueRepository.GetValue(id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post(Value value)
        {
            //Value value = new Value { Name = valueDto.Value };
            _valueRepository.SaveValue(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
