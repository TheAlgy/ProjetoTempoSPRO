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
        

        // GET: api/City/ListCities
        [HttpGet]
        [Route("ListCities")]
        public List<City> listCity() => _context.Cities.ToList();

        
        // POST: api/PostCities
        [HttpPost]
        [Route("PostCities")]

        public IActionResult PostCities([FromBody] City city)
        {
            var verificaCity = _context.Cities.FirstOrDefault(x => x.nome == city.nome);
            ConsultaCidades consultacidade = _context.PreviousCity.FirstOrDefault(x => x.nome == city.nome);
            if (consultacidade == null)
            {
                return NotFound("Nome de cidade incorreto. Por favor, preste atenção quanto a ortografia, os nomes de cidade devem começar com letra maiuscula.");
            }
            if (verificaCity == null)
            {
                City newCity = new City();
                newCity.nome = consultacidade.nome;
                _context.Cities.Add(newCity);
                _context.SaveChangesAsync();
                return Created("", newCity);
            }
            
            return NotFound("Oooooops, você já cadastrou essa cidade. Quem sabe você pode cadastrar uma nova!" );

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

        // GET: api/City/City/{city}
        [HttpGet]
        [Route("City/{id}")]
        public async Task<IActionResult> City(int id)
        {
                try
                {
                    City city = _context.Cities.Find(id);
                    if(city == null){

                    return NotFound("Cidade não encontrada");
                }

                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city.nome}&appid=bd6f5a541db04c91d1d95653e1d6c592&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                return Ok(new
                {
                    Cidade = city.nome,
                    TemperaturaMínima = rawWeather.Main.Temp_Min,
                    TemperaturaMáxima = rawWeather.Main.Temp_Max,
                    TemperaturaAtual = rawWeather.Main.Temp,
                    Humidade = rawWeather.Main.Humidity,
                    SensaçãoTérmica = rawWeather.Main.Feels_Like,
                    Descrição = string.Join(",", rawWeather.Weather.Select(x => x.Description))

                }) ;
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

    }

