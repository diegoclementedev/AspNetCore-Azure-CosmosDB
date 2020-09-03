using System;
using System.Threading.Tasks;
using CosmosDB.Domain;
using CosmosDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ClienteRepository Repository = new ClienteRepository();
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Repository.Get(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente model)
        {
            await Repository.Save(model);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Cliente model)
        {
            await Repository.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Repository.Remove(id);
            return Ok();
        }
    }
}
