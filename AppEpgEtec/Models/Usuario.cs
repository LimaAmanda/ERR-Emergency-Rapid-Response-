using System;
using System.Collections.Generic;
using System.Text;

namespace AppEpgEtec.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordString { get; set; }
        public string Perfil { get; set; }
        public string Token { get; set; }
        public byte[] Foto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
