using Newtonsoft.Json;
using SolariPDV.Logic;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Page.Pedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionarItemPage : ContentPage
    {

        CardapioProd itemCardapio;

        public AdicionarItemPage(CardapioProd item)
        {
            InitializeComponent();
            Title = "Item do Pedido";
            lstViewTamanho.ItemsSource = item;
            itemCardapio = item;

            btAdicionar.Clicked += BtAdicionar_Clicked;

            CarregarInformacoes();
        }

        private async void BtAdicionar_Clicked(object sender, EventArgs e)
        {
            var tamanho = ((TamanhoProd)lstViewTamanho.SelectedItem);
            InicioPedido.Pedido[0].Add(new ItemPedidoModel()
            {
                DS_MATERIAL = itemCardapio.DS_MATERIAL,
                DS_TAMANHO = tamanho.DS_TAMANHO,
                ID_ITEM = InicioPedido.Pedido.Count + 1,
                ID_MATERIAL = itemCardapio.ID_MATERIAL,
                ID_TAMANHO = tamanho.ID_TAMANHO,
                QT_PEDIDO = 1,
                VL_UNITARIO = tamanho.VL_UNITARIO
            });

            if (lstViewCarac.ItemsSource != null)
            {
                var carac = ((CaracModel)lstViewCarac.SelectedItem);
                CardapioPage.nqtCarac = carac.QT_QUANTIDADE;
                CardapioPage.current.MostrarLabel();
            }
            else
                CardapioPage.nqtCarac = 1;

            await Navigation.PopAsync();
        }

        private async void CarregarInformacoes()
        {
            await GetCarac();
        }

        private async Task GetCarac()
        {
            var logic = new CaracLogic();
            var carac = await logic.GetCarac(itemCardapio.ID_MATERIAL);

            var LstCarac = JsonConvert.DeserializeObject<ObservableCollection<CaracModel>>(carac);
            lstViewCarac.ItemsSource = LstCarac;
        }
    }
}