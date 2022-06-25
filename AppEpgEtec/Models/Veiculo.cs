using System;
using System.Collections.Generic;
using System.Text;

namespace AppEpgEtec.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        public int IdMarca { get; set; }
        public string Modelo { get; set; }

        public ClasseEnum ClasseEnum { get; set; }  
    }
}
