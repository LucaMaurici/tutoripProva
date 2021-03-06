﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace tutoripProva.Models
{
    public class Utente
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("nome")]
        public String nome { get; set; }

        [JsonProperty("cognome")]
        public String cognome { get; set; }

        [JsonProperty("tipo")]
        public String tipo { get; set; }

        public override string ToString()
        {
            return nome + " " + cognome;
        }

    }
}
