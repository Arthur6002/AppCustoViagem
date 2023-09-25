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
        private App PropriedadesApp;

        public MainPage()
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
                    Valor = Convert.ToInt16(etyValorP.Text)
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
                await Navigation.PushAsync(new ListaPedagios());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "Ok");
            }
        }
 
        private async void btnLimpar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await DisplayAlert("Tem certeza?", "Limpar todos os dados?", "Sim", "Não");

                if (confirmar)
                {
                    etyConsumo.Text = "";
                    etyDestino.Text = string.Empty;
                    etyDistancia.Text = "";
                    etyOrigem.Text = "";
                    etyValorC.Text = "";
                    etyValorP.Text = "";

                    PropriedadesApp.ListaPedagios.Clear();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private async void btnCalcular_Clicked(object sender, EventArgs e)
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
                //consumo do carro
                decimal consumo_carro = (distancia / consumo) * preco_combustivel;
                //Custo total, com os pedagios
                decimal custo_total = consumo_carro + valor_total_pedagios;

                string origem = etyOrigem.Text;
                string destino = etyDestino.Text;

                string mensagem = string.Format("Viagem de {0} a {1} custara {2}", origem, destino, custo_total.ToString("C"));

                await DisplayAlert("resultado Final", mensagem, "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "Ok");
            }
        }
    }
}
