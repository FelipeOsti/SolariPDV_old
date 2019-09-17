using SolariPDV.Logic;
using SolariPDV.Page.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Page.Config
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigServer : ContentPage
    {
        public ConfigServer()
        {
            InitializeComponent();
            Title = "Configurações";

            sdsServidor.Text = App.current.sdsServidorApp;
            nnrPorta.Text = App.current.nnrPorta.ToString();
        }

        private void BtConfirma_Clicked(object sender, EventArgs e)
        {
            App.current.sdsServidorApp = sdsServidor.Text;
            App.current.nnrPorta = int.Parse(nnrPorta.Text);

            RealizaLogin();
        }

        private async void RealizaLogin()
        {
            try
            {
                App.current.sdsSenha = entrySenha.Text;
                App.current.sdsUsuario = entryUsuario.Text;

                var loginLogic = new LoginLogic();
                var bboOk = await loginLogic.VerificaLogin(entryUsuario.Text);
                if (bboOk)
                {
                    MenuPage.Current.AfterLogin();
                }
                else
                    throw new Exception();
            }catch{
                await DisplayAlert("Ops", "Usuário ou senha inválido", "Ok");
            }
        }
    }
}