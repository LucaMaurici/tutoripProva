using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tutoripProva.Models;
using tutoripProva.REST;
using Xamarin.Forms;

namespace tutoripProva
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;
        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        private async void bt_search_Clicked(object sender, EventArgs e)
        {
            Elenco elenco = await _restService.GetStudentsDataAsync(GenerateRequestUri(Constants.TutoripEndPoint));
            BindingContext = elenco;

            for(int i = 0; i<elenco.utenti.Length; i++)
            {
                Content = new Label
                {
                    Text = elenco.utenti[i].cognome
                };
            }
        }

        private string GenerateRequestUri(string endpoint)
        {
            String requestUri = endpoint;
            requestUri += "/utente/read.php";

            return requestUri;
        }
    }
}
