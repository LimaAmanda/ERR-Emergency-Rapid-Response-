
using AppEpgEtec.ViewModels.Veiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEpgEtec.Views.Veiculos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemView : ContentPage
    {
        private ListagemVeiculoViewModel viewModel;
    
        public ListagemView()
        {
            InitializeComponent();
            viewModel = new ListagemVeiculoViewModel();
            BindingContext = viewModel;
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = viewModel.ObterVeiculos();
        }
    }
}
