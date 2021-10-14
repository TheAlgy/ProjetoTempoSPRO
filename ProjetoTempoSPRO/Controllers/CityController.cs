using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using Newtonsoft.Json;
using ProjetoTempoSPRO.Data;
using ProjetoTempoSPRO.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace ProjetoTempoSPRO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {

        HttpClient client = new HttpClient();
        private readonly DBContext _context;

        public CityController(DBContext context)
        {
            _context = context;
        }


        // GET: api/City/GetCityById
        [HttpGet]
        [Route("GetCityById/{id}")]
        public IActionResult GetCityById([FromRoute] int id)
        {
            var cidade = _context.Cities.Find(id);


            if (cidade == null)
            {
                return NotFound("Cidade não encontrada!");
            }

            return Ok(cidade);
        }


        // GET: api/City/ListCities
        [HttpGet]
        [Route("ListCities")]
        public List<City> listCity() => _context.Cities.ToList();

        
        // POST: api/PostCities
        [HttpPost]
        [Route("PostCities")]

        public IActionResult PostCities([FromBody] City city)
        {
            _context.Cities.Add(city);
            _context.SaveChangesAsync();

            return Created("", city);
        }


        // DELETE: api/DeleteCitiesById
        [HttpDelete]
        [Route("DeleteCitiesById/{id}")]

        public IActionResult DeleteCitiesById([FromRoute] int id)
        {
            var city = _context.Cities.Find(id);
            if (city == null)
            {
                return NotFound("Cidade não encontrada!");
            }

            _context.Cities.Remove(city);

            return Ok(_context.Cities.ToList());
        }


        // PUT: api/PutCityById
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("PutCityById/{id}")]
        public IActionResult PutCityById([FromRoute] int id, [FromBody] City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("Save")]

        public List<ConsultaCidades> GetProdutosAsync()
        {

             return _context.PreviousCity.ToList();
        }
    }  
} 