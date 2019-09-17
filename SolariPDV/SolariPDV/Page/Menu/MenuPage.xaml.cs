using SolariPDV.Logic;
using SolariPDV.Page.Config;
using SolariPDV.Page.Estoque;
using SolariPDV.Page.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Page.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : Xamarin.Forms.TabbedPage
    {
        public static MenuPage Current;

        public MenuPage()
        {
            InitializeComponent();
            Current = this;

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            this.Children.Add(new ConfigServer() { Title = "Entrar" });

            this.SelectedItem = 1;
        }

        public void AfterLogin()
        {
            if (this.Children.Count == 1)
            {
                this.Children.Add(new EstoquePage() { Title = "Estoque" });
                this.Children.Add(new NavigationPage(new InicioPedido() { Title = "Iniciar Pedido" }) { Title = "Pedido" });
                this.Children.RemoveAt(0);
            }
        }
    }
}