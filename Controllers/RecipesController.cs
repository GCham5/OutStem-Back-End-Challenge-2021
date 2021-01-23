using Frank_Workshop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Frank_Workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public ActionResult<ICollection<Object>> Get()
        {
            var recipes = _context.Recipe;
            return Ok(recipes);
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecipesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Recipe newRecipe) 
        {
            _context.Recipe.Add(newRecipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
