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
    public partial class CadastroVeiculoView : ContentPage
    {
        private CadastroVeiculoViewModel cadViewModel;
        public CadastroVeiculoView()
        {
            InitializeComponent();

            cadViewModel = new CadastroVeiculoViewModel();
            BindingContext = cadViewModel;
            Title = "Novo Veiculo";
        }
    }
}