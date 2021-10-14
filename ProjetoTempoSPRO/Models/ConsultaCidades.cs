using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTempoSPRO.Models
{
    public class ConsultaCidades
    {
     
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string nome { get; set; }
    }
}
