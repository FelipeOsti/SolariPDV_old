using SolariPDV.Models;
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
	public partial class IdentificacaoPage : ContentPage
	{
		public IdentificacaoPage ()
		{
			InitializeComponent ();
            Title = "Identificação";
		}

        private async void BtIniciar_Clicked(object sender, EventArgs e)
        {
            InicioPedido.Pedido.Add(new PedidoModel()
            {
                ID_PEDIDO = 1,
                DT_PEDIDO = DateTime.Now,
                DS_CLIENTE = sdsNome.Text,
                DS_TELEFONE = nnrTelefone.Text,
                DS_MESA = nnrMesa.Text
            });

            await DisplayAlert("Ola, "+sdsNome.Text,"Selecione os produtos desejados para seu pedido.","Ok");
            await Navigation.PushAsync(new CardapioPage());
        }
    }
}