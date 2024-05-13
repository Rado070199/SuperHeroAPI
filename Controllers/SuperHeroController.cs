using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;
using System.Runtime.InteropServices;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroesAsync()
        {
            var heroes = await _dataContext.SuperHeros.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var heroe = await _dataContext.SuperHeros.FindAsync(id);

            if (heroe is null) 
            {
                return NotFound("Hero not found.");
            }

            return Ok(heroe);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeros.Add(hero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero ubdateHero)
        {
            var dbHero = await _dataContext.SuperHeros.FindAsync(ubdateHero.Id);

            if (dbHero is null)
                return NotFound("Hero not found");

            dbHero.Name = ubdateHero.Name;
            dbHero.FirstName = ubdateHero.FirstName;
            dbHero.LastName = ubdateHero.LastName;
            dbHero.Place = ubdateHero.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            
            var dbHero = await _dataContext.SuperHeros.FindAsync(id);

            if (dbHero is null)
                return NotFound("Hero not found");

            _dataContext.SuperHeros.Remove(dbHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }
    }
}
