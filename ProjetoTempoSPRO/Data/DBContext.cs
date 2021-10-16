using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjetoTempoSPRO.Models;

namespace ProjetoTempoSPRO.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            SeedData();
        }


        public DbSet<City> Cities { get; set; }
        public DbSet<CityReference> PreviousCity { get; set; }


        private void SeedData()
        {
            if (!PreviousCity.Any()) {

                string json = new WebClient().DownloadString("https://servicodados.ibge.gov.br/api/v1/localidades/municipios");

                List<CityReference> cidades = JsonConvert.DeserializeObject<List<CityReference>>(json);

                foreach (CityReference cidade in cidades)
                {
                    CityReference city = new CityReference();
                    city.nome = cidade.nome;
                    PreviousCity.Add(city);
                    SaveChanges();
                }
            }
        }
    }
}