using AppEpgEtec.Models;
using AppEpgEtec.Services.Marcas;
using AppEpgEtec.Services.Veiculos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppEpgEtec.ViewModels.Veiculos
{
    [QueryProperty("veiculoSekecionadoId", "pId")]
    public class CadastroVeiculoViewModel : BaseViewModel
    {
        private VeiculoService vService;
        private MarcaService aService;//CRIADO PARA CARREGAR MARCA
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        
        //Você não terá botão para carregar isso, pois a tela carregará preenchida com as marcas
        //public ICommand CarregarCommand { get; set; }//CRIADO PARA CARREGAR MARCA

        public CadastroVeiculoViewModel()
        {
            string token = Application.Current.Properties["UsuarioToken"].ToString();
            vService = new VeiculoService(token);
            aService = new MarcaService(token);

            CarregarMarcas();

            SalvarCommand = new Command(async () => await SalvarVeiculo());
            CancelarCommand = new Command(async => CancelarCadastro());
            
        }
        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        private int id;
        private string nome;
        private int idMarca;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                onPropertyChanged();
            }
        }

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                onPropertyChanged();
            }
        }

        public int IdMarca
        {
            get => idMarca;
            set
            {
                idMarca = value;
                onPropertyChanged();
            }
        }

        

        public async Task SalvarVeiculo()
        {
            try
            {
                if(string.IsNullOrEmpty(this.Nome))
                    throw new Exception("Digite o nome do veículo.");

                if (marcaSelecionada == null)
                    throw new Exception("Selecione a marca.");

                Veiculo model = new Veiculo()
                {
                    Modelo = this.Nome,
                    IdMarca = this.IdMarca
                };

                if (model.Id == 0)
                    await vService.PostVeiculoAsync(model);
                else
                    await vService.PutVeiculoAsync(model);

                await Application.Current.MainPage
                 .DisplayAlert("Mensagem", "Dados salvos com sucesso! ", "Ok");

                await Shell.Current.GoToAsync(".."); //Remove a página atual da pinha de páginas

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }       

        public async void CarregarVeiculo()
        {
            try
            {
                
                Veiculo v = new Veiculo();//TODO: Arrumar código -->  await vService.ge GetVeiculoAsync(int.Parse(veiculoSelecionadoId));                
                this.nome = v.Modelo;
                this.Id = v.Id;
                this.IdMarca = v.IdMarca;

                //Abaixo está a parte onde você pega a marca selecionada para exibir na seleção
                this.MarcaSelecionada =
                    Marcas.FirstOrDefault(x => x.IdMarca == v.IdMarca);                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }
        }

        private string veiculoSelecionadoId; // ctrl + r,e

        public string VeiculoSelecionadoId
        {
            set
            {
                if (value == null)
                {
                    veiculoSelecionadoId = Uri.UnescapeDataString(value);
                    CarregarVeiculo();
                }
            }
        }

        // Removendo um veiculo
        public async Task RemoverVeiculo(Veiculo v)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", $"Confirma a remoção de {v.Modelo}?", "Sim", "Não"))
                {
                    await vService.DeleteVeiculoAsync(v.Id);

                    await Application.Current.MainPage.DisplayAlert("Mensagem",
                        "Veiculo removido com sucesso!", "OK");

                    await Shell.Current.GoToAsync(".."); //Remove a página atual da pinha de páginas
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        public ObservableCollection<Marca> Marcas { get; set; }

        public async void CarregarMarcas()//CRIADO PARA CARREGAR MARCASSSSSSSSS
        {
            try
            {
                Marcas = await aService.GetMarcasAsync();
                onPropertyChanged(nameof(Marcas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("ops", ex.Message, "Ok");
            }
        }

        //Você não precisa disso
        //private string VeiculoId;//CRIADO PARA CARREGAR MARCA

        private Marca marcaSelecionada;//CTRL + R,E
        public Marca MarcaSelecionada
        {
            set
            {
                if (value != null)
                {
                    marcaSelecionada = value;
                    this.IdMarca = marcaSelecionada.IdMarca;
                    //onPropertyChanged();
                }
            }
        }



       


        



    }
}







