using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_JUMPFORCE
{
    public class Ultimate
    {
        public string nome { get; set; }
        public int costoUso { get; set; }
        public int carica { get; set; }
        public double dmgUlti { get; set; } = 0;
        
        public Ultimate(string nome, int costoUso)
        {
            this.nome = nome;
            this.costoUso = costoUso;
        }
    }
}
