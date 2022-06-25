using AppEpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppEpgEtec.Services.Usuarios;
using AppEpgEtec.Views;
using Plugin.Geolocator;
namespace AppEpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {

        private UsuarioService uService;
        private Usuario Usuario;

        public ICommand EntrarCommand { get; set; }

        public void RegistrarCommands()
        {
            EntrarCommand = new Command(async () => { await ConsultarUsuario(); });
        }



        public UsuarioViewModel()
        {
            this.Usuario = new Usuario();
            uService = new UsuarioService();
            RegistrarCommands();

        }

        public async Task ConsultarUsuario()  // Método para buscar um usuário
        {
            try
            {
                Usuario u = null;
                u = await uService.PostLoginUsuarioAsync(Usuario);

                if (!String.IsNullOrEmpty(u.Token))
                {
                    Application.Current.Properties["UsuarioId"] = u.Id;
                    Application.Current.Properties["UsuarioUsername"] = u.Username;
                    Application.Current.Properties["UsuarioPerfil"] = u.Perfil;
                    Application.Current.Properties["UsuarioToken"] = u.Token;

                    /*Acesso acesso = new Acesso();
                    acesso.Login = u.Username;
                    acesso.Dispositivo = $"{DeviceInfo.Manufacturer} - {DeviceInfo.Name}";
                    acesso.Plataforma = $"{DeviceInfo.Platform} Versão {DeviceInfo.VersionString}";
                    acesso.DtAcesso = Datetime.Now;
                    await App.Database.Insert(acesso);*/

                    /*UsuarioService uServiceLoc = new UsuarioService(u.Token);
                    Usuario uLoc = await uServiceLoc.GetUsuarioAsync(u.Username);
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                    uLoc.Latitude = string.Format("{0:0.0000000}", position.Latitude);
                    uLoc.Longitude = string.Format("{0:0.0000000 }", position.Longitude);

                    await uServiceLoc.PutAtualizarLocalizacaoAsync(uLoc);*/

                    String mensagem = string.Format("Bem- vindo {0}", u.Username);
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new FlyoutMenu();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos: (", "Ok");
                }

            }

            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }
        #region View Login
        public String Login
        {
            get { return this.Usuario.Username; }
            set
            {
                this.Usuario.Username = value;
                onPropertyChanged();
            }
        }

        public String Senha
        {
            get { return this.Usuario.PasswordString; }
            set
            {
                this.Usuario.PasswordString = value;
                onPropertyChanged();
            }
        }
        #endregion


        
       

        




        


    }
}

