using Newtonsoft.Json;
using SolariPDV.Logic;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Page.Pedido
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardapioPage : INotifyPropertyChanged
    {
        public ObservableCollection<CardapioModel> LstCardapio { get; set; }
        public ObservableCollection<FichaModel> LstFicha { get; set; }

        ObservableCollection<CardapioCateg> LstCategoria = new ObservableCollection<CardapioCateg>();

        public static long nqtCarac = 1;
        public static CardapioPage current;

        public CardapioPage ()
		{            
			InitializeComponent();
            BindingContext = this;
            lstViewCardapio.ItemTapped += LstViewCardapio_ItemTapped;
            FABButton.Clicked += FABButton_Clicked;
            current = this;
            IniciarCardapio();
		}

        private async void FABButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FinalizaPedidoPage(), true);
        }

        private void LstViewCardapio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lstViewCardapio.SelectedItem = null;
            if (e.Item == null) return;

            if (nqtCarac > 1)
            {
                nqtCarac -= 1;
                if (nqtCarac == 1) lblMetade.IsVisible = false;
                InicioPedido.Pedido[0][InicioPedido.Pedido[0].Count - 1].DS_MATERIAL += " / " + ((CardapioProd)e.Item).DS_MATERIAL;
            }
            else
            {
                Navigation.PushAsync(new AdicionarItemPage((CardapioProd)e.Item), true);
            }
        }

        internal void MostrarLabel()
        {
            lblMetade.IsVisible = true;
        }

        private async void IniciarCardapio()
        {
            try
            {
                buscarCardapioAsync();
            }
            catch
            {
                await DisplayAlert("Ops", "Não foi possível iniciar o cardapio", "Ok");
            }
        }

        private async Task buscarFichaAsync()
        {
            try
            {
                var logic = new FichaLogic();
                var ficha = await logic.GetFicha(0);

                LstFicha = JsonConvert.DeserializeObject<ObservableCollection<FichaModel>>(ficha);
            }
            catch
            {
                await DisplayAlert("Ops", "Não foi possível buscar a ficha", "Ok");
            }
        }

        private async void buscarCardapioAsync()
        {
            try
            {
                IsBusy = true;
                await buscarFichaAsync();

                var logic = new CardapioLogic();
                var cardapio = await logic.GetCardapio("");

                LstCardapio = JsonConvert.DeserializeObject<ObservableCollection<CardapioModel>>(cardapio);
                ConverteCardapioCategorias();

                lstViewCardapio.ItemsSource = LstCategoria;
                IsBusy = false;

            }
            catch
            {
                await DisplayAlert("Ops", "Não foi possível buscar o cardapio", "Ok");
            }
        }
        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ObservableCollection<CardapioCateg> LstAux = new ObservableCollection<CardapioCateg>();
                var texto = CardapioSearchBar.Text;
                if (String.IsNullOrEmpty(texto))
                {
                    LstAux = LstCategoria;
                }
                else
                {
                    foreach (var categ in LstCategoria)
                    {
                        foreach (var prod in categ)
                        {
                            if (prod.DS_MATERIAL.ToLower().Contains(texto.ToLower()))
                            {
                                if (LstAux.Count == 0)
                                {
                                    LstAux.Add(new CardapioCateg() { ID_CATEGORIA = categ.ID_CATEGORIA, DS_CATEGORIA = categ.DS_CATEGORIA });
                                }
                                if (LstAux[LstAux.Count - 1].DS_CATEGORIA != categ.DS_CATEGORIA)
                                {
                                    LstAux.Add(new CardapioCateg() { ID_CATEGORIA = categ.ID_CATEGORIA, DS_CATEGORIA = categ.DS_CATEGORIA });
                                }
                                LstAux[LstAux.Count - 1].Add(new CardapioProd()
                                {
                                    ID_MATERIAL = prod.ID_MATERIAL,
                                    DS_MATERIAL = prod.DS_MATERIAL,
                                });
                            }
                        }
                    }
                }

                lstViewCardapio.ItemsSource = LstAux;
            }
            catch
            {
                await DisplayAlert("Ops", "Não foi possível buscar o produto", "Ok");
            }
        }

        private async void ConverteCardapioCategorias()
        {
            try
            {
                LstCategoria = new ObservableCollection<CardapioCateg>();
                int c = -1;
                int p = -1;
                foreach (var it in LstCardapio)
                {
                    if (it.FL_ADICIONAL == "F")
                    {
                        if (!LstCategoria.Any(cat => cat.ID_CATEGORIA == it.ID_CATEGORIA))
                        {
                            LstCategoria.Add(new CardapioCateg() { ID_CATEGORIA = it.ID_CATEGORIA, DS_CATEGORIA = it.DS_CATEGORIA });
                            p = -1;
                            c++;
                        }
                        if (!LstCategoria[c].Any(pro => pro.ID_MATERIAL == it.ID_MATERIAL))
                        {
                            var sds_ficha = "";
                            foreach (var ficha in LstFicha)
                            {
                                if (ficha.ID_MATERIAL == it.ID_MATERIAL)
                                {
                                    if (sds_ficha == "")
                                        sds_ficha = ficha.DS_MATERIAL;
                                    else if (!sds_ficha.Contains(ficha.DS_MATERIAL))
                                        sds_ficha = sds_ficha + ", " + ficha.DS_MATERIAL;
                                }
                            }

                            LstCategoria[c].Add(new CardapioProd()
                            {
                                ID_MATERIAL = it.ID_MATERIAL,
                                DS_MATERIAL = it.DS_MATERIAL,
                                DS_FICHA = sds_ficha
                            });
                            p++;
                        }
                        if (!LstCategoria[c][p].Any(t => t.ID_TAMANHO == it.ID_TAMANHO))
                        {
                            if (String.IsNullOrEmpty(it.DS_TAMANHO))
                            {
                                it.DS_TAMANHO = "Único";
                                LstCategoria[c][p].VL_UNITARIO = it.VL_UNITARIO;
                            }
                            else
                                LstCategoria[c][p].VL_UNITARIO = null;

                            LstCategoria[c][p].Add(new TamanhoProd()
                            {
                                ID_TAMANHO = it.ID_TAMANHO,
                                VL_UNITARIO = it.VL_UNITARIO,
                                DS_TAMANHO = it.DS_TAMANHO
                            });
                        }
                    }
                }
            }
            catch
            {
                await DisplayAlert("Ops", "Não foi possível converter os dados", "Ok");
            }

        }
    }
}