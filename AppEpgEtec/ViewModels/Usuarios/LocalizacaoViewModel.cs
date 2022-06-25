using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AppEpgEtec.Models;
using AppEpgEtec.Services.Usuarios;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Map = Xamarin.Forms.GoogleMaps.Map;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace AppEpgEtec.ViewModels.Usuarios
{
    public class LocalizacaoViewModel
    {
        public static Map MeuMapa;

        private UsuarioService uService;
        public LocalizacaoViewModel()
        {
            MeuMapa = new Map();
            string token = Application.Current.Properties["UsuarioToken"].ToString();
            uService = new UsuarioService(token);            
        }


        public async void InicializarMapa()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
               
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current
                        .ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await Application.Current.MainPage
                            .DisplayAlert("Atenção", "É necessário ativar a alocalização", "Ok");
                    }
                    status = await CrossPermissions.Current
                        .RequestPermissionAsync<LocationPermission>();

                }
                if (status == PermissionStatus.Granted)
                {
                    MeuMapa.MyLocationEnabled = true;
                    MeuMapa.UiSettings.MyLocationButtonEnabled = true;
                }
                else if (status != PermissionStatus.Unknown)
                    throw new Exception("Localização negada");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Erro", ex.Message, "Ok");

            }
        }
        public async void LocalizarEscola()
        {
            try
            {
                var status = await CrossPermissions.Current
                    .CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current
                        .ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await Application.Current.MainPage
                            .DisplayAlert("Atenção", "É necessário ativar a alocalização", "Ok");
                    }
                    status = await CrossPermissions.Current
                        .RequestPermissionAsync<LocationPermission>();

                }
                if (status == PermissionStatus.Granted)
                {
                    Pin pinEtec = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Etec Horacio",
                        Address = "Rua alcantara, 113, Vila Guilherme",
                        Position = new Position(-23.5200241d, -46.596498d),
                        Tag = "IdEtec",
                    };
                    MeuMapa.Pins.Add(pinEtec);
                    MeuMapa.MoveToRegion(MapSpan
                        .FromCenterAndRadius(pinEtec.Position, Distance.FromMeters(500)));
                }
                else if (status != PermissionStatus.Unknown)
                    throw new Exception("Localização negada");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Erro", ex.Message, "Ok");

            }
        }

        public async void ExibirUsuariosNoMapa()
        {
            try
            {
                ObservableCollection<Usuario> ocUsuarios = await uService.GetUsuariosAsync();

                List<Usuario> listaUsuarios = new List<Usuario>(ocUsuarios);
                foreach (Usuario u in listaUsuarios)
                {
                    double latitude = double.Parse(u.Latitude);
                    double logitude = double.Parse(u.Longitude);

                    Pin pinAtual = new Pin()
                    {
                        Type = PinType.Place,
                        Label = u.Username,
                        Position = new Position(latitude, logitude),
                    };
                    MeuMapa.Pins.Add(pinAtual);

                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", ex.Message, "OK");

                throw;
            }
        }







    }
}