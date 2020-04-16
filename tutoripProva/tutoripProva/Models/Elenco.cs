using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace tutoripProva.Models
{
    public class Elenco
    {
        [JsonProperty("Elenco")]
        public Studente[] studenti { get; set; }
    }
}
