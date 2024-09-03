using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_JUMPFORCE
{
    public class Skill
    {

        public string nome { get; set; }
        public int costoMana { get; set; }
        public double dmgskill { get; set; } = 0;

        public Skill(string nome, int costoMana)
        {
            this.nome = nome;
            this.costoMana = costoMana;
        }
    }
}
