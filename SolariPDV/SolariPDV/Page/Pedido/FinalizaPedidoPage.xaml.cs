using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Page.Pedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizaPedidoPage : ContentPage
    {
        public FinalizaPedidoPage()
        {
            InitializeComponent();
            Title = "Finalização do Pedido";
            lstViewPedido.ItemsSource = InicioPedido.Pedido;
            btFinalizar.Clicked += BtFinalizar_Clicked;
        }

        private async void BtFinalizar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}