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
        public Ultimate ultimate { get; set; }
        public Archetipo archetipo { get; set; }
        public bool trasformazione { get; set; }
        public bool statoTras { get; set; } = false;


        public Pg(string nome, int forza, double hp, Skill skill, Ultimate ultimate, Archetipo archetipo, bool trasformazione)
        {
            this.nome = nome;
            this.forza = forza;
            this.hp = Math.Round(hp, 2);
            this.atkb = (double)(forza*5) / 50;
            this.skill = skill;
            this.ultimate = ultimate;
            this.skill.dmgskill = (double)(forza * 10) / 50;
            this.archetipo = archetipo;
            this.trasformazione = trasformazione;
        }

        public void usaUlti()
        {
            if (trasformazione && !statoTras)
            {
                forza += ((forza * 20) / 100);
                atkb = (double)(forza * 5) / 50;                                     //Ricalcolo dei danni delle abilità in relazione al cambio di forza della ulti se trasformazione == true. Dato che i danni erano valutati nel costruttore, non venivano aggiornati all'incremento della forza tramite la trasformazione
                skill.dmgskill = (double)(forza * 10) / 50;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{nome} ha usato {ultimate.nome} e ha incrementato la sua forza del 20%");
                Console.ResetColor();
                ultimate.carica -= ultimate.costoUso;
                Console.WriteLine($"La carica della ultimate è scesa a {ultimate.carica}");
                statoTras = true;                                           //Modifica dell'attributo statoTrasformazione per non far entrare piu nell'if una volta che la trasformazione è stata fatta.
            }
            else if(!trasformazione)
            {
                ultimate.dmgUlti = (double)(forza * 35) / 50;
                ultimate.carica -= ultimate.costoUso;
                Console.WriteLine($"La carica della ultimate è scesa a {ultimate.carica}");
            }
        }
    }
}
