using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AppEpgEtec.Models;
using System.Linq;


namespace AppEpgEtec.Services.Marcas
{
    class MarcaService : Request
    {
        private readonly Request _request = null;
        private string _token;
        private const string ApiUrlBase = "https://bsite.net/TrabalhoTCC/Marcas";

        public MarcaService(string token)
        {
            _token = token;
            _request = new Request();
        }

        public async Task<ObservableCollection<Marca>> GetMarcasAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Marca> listaMarcas = await
                _request.GetAsync<ObservableCollection<Models.Marca>>(ApiUrlBase + urlComplementar, _token);

            return listaMarcas;
        }
        public async Task<Marca> GetMarcaAsync(int marcaId)
        {
            string urlComplementar = string.Format("/{0}", marcaId);

            var marca = await
                _request.GetAsync<Models.Marca>(ApiUrlBase + urlComplementar, _token);

            return marca;
        }

        public async Task<Marca> PostMarcaAsync(Marca a)
        {
            return await _request.PostAsync(ApiUrlBase, a, _token);
        }
        public async Task<Marca> PutMarcaAsync(Marca a)
        {
            var result = await _request.PutAsync(ApiUrlBase, a, _token);
            return result;
        }
        public async Task<Marca> DeleteMarcaAsync(int MarcaId)
        {
            string urlComplementar = string.Format("/{0}", MarcaId);
            await _request.DeleteAsync(ApiUrlBase + urlComplementar, _token);
            return new Marca(); //{ Id = MarcaId };
        }
    }
}

