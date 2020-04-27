using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tutoripProva.Models;
using tutoripProva.REST;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;

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
            //BindingContext = elenco;
            Users.ItemsSource = elenco.utenti;
        }

        private string GenerateRequestUri(string endpoint)
        {
            String requestUri = endpoint;
            requestUri += "/utente/read.php";
            return requestUri;
        }

        private async void Users_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Utente u = (Utente)e.Item;
            //DisplayAlert("User " + u.id, "Selected", "ok");
            Utente u = (Utente)e.Item;
            Console.WriteLine(u.id);
            await _restService.DeleteElements(u.id);
        }

        private async void bt_add_Clicked(object sender, EventArgs e)
        {
            Utente utente = new Utente(); 
            utente.cognome = "Prova";
            utente.nome = "prova";
            utente.tipo = "0";
            Console.WriteLine(await _restService.SaveElements(utente, GenerateAddUri(Constants.TutoripEndPoint), true));

        }

        private string GenerateAddUri(string endpoint)
        {
            String requestUri = endpoint;
            requestUri += "/utente/create.php/";
            return requestUri;
        }

        /*
        private async void Users_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Utente u = (Utente) e.SelectedItem;
            Console.WriteLine(u.id);
            await _restService.DeleteElements(u.id);
        }
        */
    }
}
