using AppCustoViagem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCustoViagem
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public partial cla MainPage()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;
        }

        private async void btnPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                int qntPedagios = PropriedadesApp.ListaPedagios.Count;

                PropriedadesApp.ListaPedagios.Add(new Pedagio
                {
                    NroPedagia = qntPedagios + 1,
                    Valor = Convert.ToDecimal(etyValorP.Text)
                });
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "Ok");
            }
        }

        private async void btnListaPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                decimal valor_total_pedagios = PropriedadesApp.ListaPedagios.Sum(item => item.Valor);

                if (string.IsNullOrEmpty(etyDistancia.Text))
                    throw new Exception("Por favor, preencha a distancia.");
                if (string.IsNullOrEmpty(etyConsumo.Text))
                    throw new Exception("Por favor, preencha o consumo do veiculo.");
                if (string.IsNullOrEmpty(etyValorC.Text))
                    throw new Exception("Por favor, preencha o valordo combustível");

                decimal consumo = Convert.ToDecimal(etyConsumo.Text);
                decimal preco_combustivel = Convert.ToDecimal(etyValorC.Text);
                decimal distancia = Convert.ToDecimal(etyDistancia.Text);
            }
            catch(Exception ex)
            {

            }
        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
