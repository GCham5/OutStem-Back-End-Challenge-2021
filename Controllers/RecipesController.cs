using Frank_Workshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public async Task<ActionResult<Object>> Get([FromBody] Requester requester)
        {

            var recipes = _context.Recipe;
            List<Recipe> recipesToSend = new List<Recipe>();

            if (recipes == null)
            {
                return BadRequest("Could not find any recipes");
            }

            foreach (var recipe in recipes)
            {
                if (!recipe.IsDeleted)
                {
                    if (recipe.IsPrivate)
                    {
                        if (recipe.Author == requester.Id)
                        {
                            recipesToSend.Add(recipe);
                        }
                    }
                    else if (recipe.IsPremium)
                    {
                        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == requester.Id);
                        if (user.IsPremium)
                        {
                            recipesToSend.Add(recipe);
                        }
                    }
                    else
                    {
                        recipesToSend.Add(recipe);
                    }
                }

            }

            return Ok(recipesToSend);
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id, [FromBody] Requester requester)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == requester.Id);
            var recipe = await _context.Recipe.FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return BadRequest("Recipe not found");
            }

            if (recipe.IsDeleted)
            {
                return BadRequest("Recipe unavailable");
            }

            if (recipe.IsPrivate)
            {
                if (recipe.Author == requester.Id)
                {
                    return Ok(recipe);
                }
            }

            if (recipe.IsPremium)
            {
                if (user.IsPremium)
                {
                    return Ok(recipe);
                }
            }

            return BadRequest("Recipe unavailable");
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
                    IsPremium = newRecipe.IsPremium,
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

            var currentRecipe = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);

            if (currentRecipe == null)
            {
                return NotFound("Recipe not found");
            }

            if (currentRecipe.Author != recipe.Author)
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

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, [FromBody] Requester requester)
        {
            var recipeToDelete = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);

            if (recipeToDelete == null)
            {
                return NotFound("Recipe not found");
            }

            if (recipeToDelete.Author != requester.Id)
            {
                return Unauthorized();
            }

            recipeToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
