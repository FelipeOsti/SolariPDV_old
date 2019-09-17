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
    public partial class InicioPedido : ContentPage
    {
        public static ObservableCollection<PedidoModel> Pedido;

        public InicioPedido()
        {
            InitializeComponent();
            Title = "Seja bem vindo!";
        }

        private void AnimationViewHand_OnClick(object sender, EventArgs e)
        {
            AbrirCardapio();
        }

        private async void AbrirCardapio()
        {
            Pedido = new ObservableCollection<PedidoModel>();
            await Navigation.PushAsync(new IdentificacaoPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AbrirCardapio();
        }
    }
}