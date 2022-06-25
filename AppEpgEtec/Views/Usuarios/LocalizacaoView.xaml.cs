using AppEpgEtec.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEpgEtec.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalizacaoView : ContentPage //PERMANECE ESTE ERRO 
    {
        LocalizacaoViewModel viewModel;

        //LocalizacaoViewModel BindingContext { get; set; }//COLOQUEI PARA RETIRAR O ERRO DO "BINDING" E SAIU O ERRO

        public LocalizacaoView()
        {
            InitializeComponent();

            viewModel = new LocalizacaoViewModel();
            BindingContext = viewModel;

            LocalizacaoViewModel.MeuMapa = map;

            viewModel.InicializarMapa();
            viewModel.LocalizarEscola();
            /*viewModel.ExibirUsuariosNoMapa();*/
        }
    }
}