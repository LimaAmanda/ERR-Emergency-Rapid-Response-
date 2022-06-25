using AppEpgEtec.Models;
using AppEpgEtec.Services.Veiculos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEpgEtec.ViewModels.Veiculos
{
    public class ListagemVeiculoViewModel : BaseViewModel
    {
        private VeiculoService vService;

        public ObservableCollection<Veiculo> Veiculos { get; set; }

        public ListagemVeiculoViewModel()
        {
            string Token = Application.Current.Properties["UsuarioToken"].ToString();
            vService = new VeiculoService(Token);
            Veiculos = new ObservableCollection<Veiculo>();
           _= ObterVeiculos();

            NovoVeiculo = new Command(async () => { await ExibirCadastroVeiculo(); });
            
            //TODO: Criar método Remover veículo --> RemoverVeiculoCommand =
              //new Command<Veiculo>(async (Veiculo v) => { await RemoverVeiculo(v); });
        }
        public ICommand NovoVeiculo { get; } // using system.windows.input
        public ICommand RemoverVeiculoCommand { get; }



        public async Task ObterVeiculos()
        {
            try
            {
                Veiculos = await vService.GetVeiculos();
                onPropertyChanged(nameof(Veiculos));

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes:" + ex.InnerException, "Ok");


            }
        }


        public async Task ExibirCadastroVeiculo()
        {
            try
            {
                await Shell.Current.GoToAsync("cadVeiculoView");

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }
        }

        private Veiculo veiculoSelecionado; //ctrl + r,e

        public Veiculo VeiculoSelecionado
        {
            get { return veiculoSelecionado; }
            set
            {
                if (value != null)
                {
                    VeiculoSelecionado = value;

                    Shell.Current
                        .GoToAsync($"cadVeiculoView?pId={VeiculoSelecionado.Id}");
                }
            }
        }
    }
}
   
