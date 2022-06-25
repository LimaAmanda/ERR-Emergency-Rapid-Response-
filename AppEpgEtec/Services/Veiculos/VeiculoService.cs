using AppEpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppEpgEtec.Services.Veiculos
{
    public class VeiculoService : Request
    {
        private readonly Request _request = null;
        private string _token;
        private const string ApiUrlBase = "https://bsite.net/TrabalhoTCC/Veiculos";

        public VeiculoService(string token)
        {
            _request = new Request();
            _token = token;
        }

        //public int Id { get; internal set; }

        public async Task<Veiculo>  PostVeiculoAsync(Veiculo v)
        {
            return await _request.PostAsync(ApiUrlBase, v, _token);
        }

        public async Task<ObservableCollection<Veiculo>> GetVeiculos()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Veiculo> ListaVeiculos = await
            _request.GetAsync<ObservableCollection<Models.Veiculo>>(ApiUrlBase + urlComplementar, _token);
            return ListaVeiculos;
        }
        public async Task<ObservableCollection<Veiculo>> GetVeiculoAsync(int veiculoId)
        {
            ObservableCollection<Models.Veiculo> ListarVeiculos = await
                _request.GetAsync<ObservableCollection<Models.Veiculo>>(ApiUrlBase, _token);

            var veiculoFiltrado = ListarVeiculos.Where(v => v.Id == veiculoId);
            return new ObservableCollection<Veiculo>(veiculoFiltrado);
        }
        public async Task<Veiculo> PutVeiculoAsync(Veiculo v)
        {
            var result = await _request.PutAsync(ApiUrlBase, v, _token);
            return result;
        }
        public async Task<Veiculo> DeleteVeiculoAsync(int veiculoId)
        {
            string urlComplementar = string.Format("/{0}", veiculoId);
            await _request.DeleteAsync(ApiUrlBase + urlComplementar, _token);
            return new Veiculo() {Id = veiculoId };
        }

       
        
        
        

        
        


    }
}
