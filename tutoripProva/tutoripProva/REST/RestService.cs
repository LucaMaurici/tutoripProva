using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using tutoripProva.Models;

namespace tutoripProva.REST
{
    class RestService
    {
        private static readonly HttpClient _client;

        static RestService()
        {
            _client = new HttpClient();
        }

        public async Task<Elenco> GetStudentsDataAsync(string uri)
        {
            Elenco utenti = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    utenti = JsonConvert.DeserializeObject<Elenco>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return utenti;
        }

        public async Task<String> SaveElements(Utente user, String uri, bool isNewItem=false)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (isNewItem) //??
            {
                response = await _client.PostAsync(uri, content);
                Console.WriteLine(response.StatusCode);
            }
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved."); //Non utile
            }

            return json;
        }

        public async Task UpdateElements(Utente user, String uri, bool isNewItem = false)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem) //??
            {
                response = await _client.PutAsync(uri, content);
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved."); //Non utile
            }
        }

        //versione update e save tutta in uno
        public async Task SaveOrUpdateElements(Utente user, String uri, bool isNewItem = false)
        {   
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        /*
        public async Task DeleteElements(int id)
        {
            var uri = new Uri(string.Format(Constants.TutoripEndPoint+ "/utente/delete.php", id));
            try
            {
                var response = await _client.DeleteAsync(uri);
                Console.WriteLine(response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        */

        private class Identificativo
        {
            [JsonProperty("id")]
            int id;
            public Identificativo(int id)
            {
               
                this.id = id;
            }
        }

        public async Task<String> DeleteElements(int id)
        {
            Uri uri = new Uri(string.Format(Constants.TutoripEndPoint + "/utente/delete.php"));
            var json = JsonConvert.SerializeObject(new Identificativo(id));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            Console.WriteLine("Risposta: " + response.StatusCode);

            return json;
        }

    }
}
