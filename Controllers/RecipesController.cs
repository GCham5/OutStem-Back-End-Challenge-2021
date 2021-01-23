using Frank_Workshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (ModelState.IsValid)
            {
                Recipe recipe = new Recipe
                {
                    Id = new Random().Next(100),
                    Author = Guid.NewGuid(),
                    Content = newRecipe.Content,
                    Category = newRecipe.Category,
                    IsPrivate = newRecipe.IsPrivate,
                    IsPremium = newRecipe.IsPrivate,
                };
                _context.Recipe.Add(recipe);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Invalid model");
           
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Recipe recipe)
        {
            if(id == recipe.Id)
            {
                _context.Entry(recipe).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var currentRecipe = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);
                    if ( currentRecipe == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return BadRequest("Recipe does not exist");
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
