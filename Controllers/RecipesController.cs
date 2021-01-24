using Frank_Workshop.Models;
using Microsoft.AspNetCore.Http;
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
        public ActionResult<ICollection<Object>> Get(Guid requester)
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
                    Author = newRecipe.Author,
                    Content = newRecipe.Content,
                    Category = newRecipe.Category,
                    IsPrivate = newRecipe.IsPrivate,
                    IsPremium = newRecipe.IsPrivate,
                    DateCreated = DateTime.Now,
                    DateLastUpdated = DateTime.Now
                };

                _context.Recipe.Add(recipe);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest("Invalid model");
           
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Recipe recipe)
        {
            if(id == recipe.Id)
            {
                var currentRecipe = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);

                if(currentRecipe == null)
                {
                    return NotFound("Recipe not found");
                }

                if(currentRecipe.Author != recipe.Author)
                {
                    return Unauthorized();
                }

                currentRecipe.Content = recipe.Content;
                currentRecipe.Category = recipe.Category;
                currentRecipe.IsPremium = recipe.IsPremium;
                currentRecipe.IsPrivate = recipe.IsPrivate;
                currentRecipe.DateLastUpdated = DateTime.Now;

  
                await _context.SaveChangesAsync();
                return Ok();

            }

            return BadRequest("Recipe does not exist");
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, Guid Author)
        {
            var recipeToDelete = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);

            if (recipeToDelete == null)
            {
                return NotFound("Recipe not found");
            }

            if (recipeToDelete.Author != Author)
            {
                return Unauthorized();
            }

            _context.Recipe.Remove(recipeToDelete);
            return Ok();
        }
    }
}
