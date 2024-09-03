using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_JUMPFORCE
{
    public class Pg
    {
        
        public string nome { get; set; }
        public int forza { get; set; } 
        public double hp { get; set; }
        public double atkb { get; set; }
        public int mana { get; set; } = 0;

        public Skill skill { get; set; }

        public string elemento;


        public Pg(string nome, int forza, double hp, Skill skill)
        {
            this.nome = nome;
            this.forza = forza;
            this.hp = hp;
            this.atkb = (double)(forza*5) / 50;
            this.skill = skill;
            this.skill.dmgskill = (forza * 10) / 50;
        }

    }
}
