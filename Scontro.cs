using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Vlc.DotNet.Core;
using LibVLCSharp.Shared;
using Microsoft.SqlServer.Server;
using System.Media;

namespace ConsoleApp_JUMPFORCE
{
   public class Scontro
    {
        List<Pg> pgl = new List<Pg>();
        Random sc = new Random();
        
        public void popola()
        {
            Pg goku = new Pg("Goku", 95, 300, new Skill("Kamehameha", 22), new Ultimate("Super Sayan 3° Lvl", 1), Archetipo.Sayan, true);
            Pg gon = new Pg("Gon", 77, 300, new Skill("Sasso, Carta, Sasso", 16), new Ultimate("Adult Gon", 3), Archetipo.Umano, true);
            Pg asta = new Pg("Asta", 70, 300, new Skill("Zetten", 15), new Ultimate("Devil Possessed", 3), Archetipo.Demone, true);
            Pg naruto = new Pg("Naruto", 90, 300, new Skill("Rasengan", 21), new Ultimate("Kurama", 1), Archetipo.Ninja, true);
            Pg kakashi = new Pg("Kakashi", 70, 300, new Skill("Mille Falchi", 15), new Ultimate("Sharingan", 3), Archetipo.Ninja, false);
            Pg vegeta = new Pg("Vegeta", 93, 300, new Skill("Final Cannon", 22), new Ultimate("Super Sayan 3° Lvl", 1), Archetipo.Sayan, true);
            Pg luffy = new Pg("Luffy", 89, 300, new Skill("Red Hawk", 20), new Ultimate("Gear Fourth", 1), Archetipo.Pirata, true);
            Pg killua = new Pg("Killua", 73, 300, new Skill("Speed of Lightning", 15), new Ultimate("Divine Speed", 3), Archetipo.Umano, false);
            Pg lightyagami = new Pg("Light Yagami", 70, 300, new Skill("Ryuk", 15), new Ultimate("Death Note", 3), Archetipo.Demone, false);
            
            pgl.Add(goku);
            pgl.Add(gon);
            pgl.Add(asta);
            pgl.Add(naruto);
            pgl.Add(kakashi);
            pgl.Add(vegeta);
            pgl.Add(luffy);
            pgl.Add(killua);
            pgl.Add(lightyagami);
            
            Pg barbanera = new Pg("BarbaNera", 85, 300, pgl[sc.Next(pgl.Count)].skill, new Ultimate("Dark Vortex", 3), Archetipo.Pirata, false);
            pgl.Add(barbanera);
        }

        public void genera()
        {
            int nturno = 1;
            List<Pg> turno = new List<Pg>();                            //Creazione Lista per fight 1v1
            Pg pg1 = pgl[sc.Next(pgl.Count)];                           //Primo pg pescato random dalla lista di tutti i pg
            Pg pg2;
            
            do
            {
                pg2 = pgl[sc.Next(pgl.Count)];                          //Secondo pg pescato random dalla lista di tutti i pg (non uguale al primo pg)
            } while (pg2.nome == pg1.nome);

            turno.Add(pg1);                                             //Aggiunta primo pg alla lista del fight 1v1
            turno.Add(pg2);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Scontro generato: {pg1.nome} vs {pg2.nome}");
            controlloArchetipo(pg1 , pg2);
            Console.WriteLine("Lo scontro inizierà a breve");
            Thread.Sleep(2500);
            Console.WriteLine("\n");
            Console.ResetColor();

            do                                                           //Inizio del fight
            {
                Pg attaccante = turno[sc.Next(turno.Count)];             //Scelta random dell'attaccante all'interno del turno
                Pg difensore;

                do
                {
                    difensore = turno[sc.Next(turno.Count)];
                } while (difensore.nome == attaccante.nome);

                if (pg1.hp <= 0 || pg2.hp <= 0)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Turno {nturno}\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Attacca: {attaccante.nome}");
                Console.ResetColor();

                for (int i = 0, j = 1; i < turno.Count; i++, j--)                                                           //Ciclo per il primo attacco del turno
                {
                    if(difensore.nome.Equals(turno[i].nome) && turno[j].ultimate.carica == turno[j].ultimate.costoUso)      //Condizione per usare l'ultimate
                    {
                        turno[j].usaUlti();
                        if (!turno[j].trasformazione)
                        {
                            turno[i].hp -= Math.Round(turno[j].ultimate.dmgUlti, 2);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{turno[j].nome} ha usato {turno[j].ultimate.nome} e ha inflitto {turno[j].ultimate.dmgUlti} a {turno[i].nome}");
                            Console.ResetColor();
                        }
                    }
                    if (difensore.nome.Equals(turno[i].nome) && turno[j].mana >= turno[j].skill.costoMana)                  //Condizione per usare la skill
                    {
                        turno[i].hp -= Math.Round(attaccante.skill.dmgskill, 2);
                            if ((attaccante.nome.Equals(turno[j].nome)))
                            {
                                turno[j].mana -= turno[j].skill.costoMana;
                                Console.WriteLine($"{attaccante.nome} ha usato {turno[j].skill.costoMana} di mana");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine($"{attaccante.nome} ha usato la skill {attaccante.skill.nome} e ha inflitto {attaccante.skill.dmgskill} a {difensore.nome}");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine($"{attaccante.nome} dopo il cast ha {turno[j].mana} mana..");
                                turno[j].ultimate.carica++;
                                Console.WriteLine($"{turno[j].nome} ha {turno[j].ultimate.carica} punti ultimate su {turno[j].ultimate.costoUso}");
                                Console.WriteLine($"{turno[i].nome} ha {Math.Round(turno[i].hp , 2)} HP");
                                Console.WriteLine();
                                Thread.Sleep(500);
                            }
                        break;
                    }
                    if (difensore.nome.Equals(turno[i].nome))                                                                  //Condizione per usare l'attacco base
                    {
                        turno[i].hp -= Math.Round(attaccante.atkb, 2);
                        Console.WriteLine($"{attaccante.nome} ha inflitto {attaccante.atkb} a {difensore.nome}");
                        if ((attaccante.nome.Equals(turno[j].nome)))
                        {
                            turno[j].mana += 5;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{turno[j].nome} ha {turno[j].mana} di mana a seguito di un attacco!");
                            Console.ResetColor();
                            Console.WriteLine($"{turno[i].nome} ha {Math.Round(turno[i].hp , 2)} HP");
                            Console.WriteLine();
                            Thread.Sleep(500);
                        }
                    }
                }

                if(pg1.hp <= 0 || pg2.hp <= 0)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Attacca: {difensore.nome}");
                Console.ResetColor();

                for (int i = 0, j = 1; i < turno.Count; i++, j--)                                                            //Ciclo per il secondo attacco del turno
                {
                    if (attaccante.nome.Equals(turno[i].nome) && turno[j].ultimate.carica == turno[j].ultimate.costoUso)     //Condizione per usare l'ultimate
                    {
                        turno[j].usaUlti();

                        if (!turno[j].trasformazione)
                        {
                            turno[i].hp -= Math.Round(turno[j].ultimate.dmgUlti, 2);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{turno[j].nome} ha usato {turno[j].ultimate.nome} e ha inflitto {turno[j].ultimate.dmgUlti} a {turno[i].nome}");
                            Console.ResetColor();
                        }
                    }
                    if (attaccante.nome.Equals(turno[i].nome) && turno[j].mana >= turno[j].skill.costoMana)                   //Condizione per usare la skill
                    {
                        turno[i].hp -= Math.Round(difensore.skill.dmgskill, 2);
                        
                        if ((difensore.nome.Equals(turno[j].nome)))
                        {
                            turno[j].mana -= turno[j].skill.costoMana;
                            Console.WriteLine($"{turno[j].nome} ha usato {turno[j].skill.costoMana} di mana");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{difensore.nome} ha usato la skill {difensore.skill.nome} e ha inflitto {difensore.skill.dmgskill} a {attaccante.nome}");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine($"{turno[j].nome} dopo il cast ha {turno[j].mana} mana..");
                            turno[j].ultimate.carica++;
                            Console.WriteLine($"{turno[j].nome} ha {turno[j].ultimate.carica} punti ultimate su {turno[j].ultimate.costoUso}");
                            Console.WriteLine($"{turno[i].nome} ha {Math.Round(turno[i].hp, 2)} HP");
                            Console.WriteLine();
                            Thread.Sleep(500);
                        }
                        break;
                    }
                        
                        
                    if (attaccante.nome.Equals(turno[i].nome) && turno[j].mana < turno[j].skill.costoMana)      //Condizione per usare l'attacco base
                    {
                        turno[i].hp -= Math.Round(difensore.atkb, 2);
                        Console.WriteLine($"{difensore.nome} ha inflitto {difensore.atkb} a {attaccante.nome}");
                       
                        if ((difensore.nome.Equals(turno[j].nome)))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            turno[j].mana += 5;
                            Console.WriteLine($"{turno[j].nome} ha {turno[j].mana} di mana a seguito di un attacco!");
                            Console.ResetColor();
                            Console.WriteLine($"{turno[i].nome} ha {Math.Round(turno[i].hp, 2)} HP");
                            Console.WriteLine("_____________________________________________________________");
                            Console.WriteLine();
                            Thread.Sleep(500);
                        }
                    }
                }
                nturno++;
                                                                                                                  //Fine del turno
            }while(pg1.hp > 0|| pg2.hp > 0);
              
            if(pg1.hp <= 0 && pg2.hp > 0)
            {
                string filePath = "C:\\Users\\ereng\\source\\repos\\ConsoleApp_JUMPFORCE\\win.wav";
                SoundPlayer player = new SoundPlayer(filePath);
                player.Play();
               
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Il giocatore {pg2.nome} ha vinto lo scontro!");
                Console.ResetColor();
            }else if(pg1.hp > 0 && pg2.hp <= 0)
            {
                string filePath = "C:\\Users\\ereng\\source\\repos\\ConsoleApp_JUMPFORCE\\win.wav";
                SoundPlayer player = new SoundPlayer(filePath);
                player.Play();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Il giocatore {pg1.nome} ha vinto lo scontro!");
                Console.ResetColor();
            }
        }

        public void controlloArchetipo(Pg pg1, Pg pg2)
        {
            if(pg1.archetipo == Archetipo.Sayan && pg2.archetipo == Archetipo.Pirata)                                //Sayan fa piu danno su Pirata
            {
                pg1.forza += (pg1.forza * 10) / 100;
                pg1.atkb = (double)(pg1.forza * 5) / 50;
                pg1.skill.dmgskill = (double)(pg1.forza * 10) / 50;
                Console.WriteLine($"{pg1.nome} è di archetipo {pg1.archetipo} e guadagna un buff alla forza del 10% contro {pg2.nome} di archetipo {pg2.archetipo}");
            }
            
            else if(pg2.archetipo == Archetipo.Sayan && pg1.archetipo == Archetipo.Pirata)
            
            {
                Console.WriteLine($"{pg2.nome} è di archetipo {pg2.archetipo} e guadagna un buff alla forza del 10% contro {pg1.nome} di archetipo {pg1.archetipo}");
                pg2.forza += (pg2.forza * 10) / 100;
                pg2.atkb = (double)(pg2.forza * 5) / 50;
                pg2.skill.dmgskill = (double)(pg2.forza * 10) / 50;
            }

            if (pg1.archetipo == Archetipo.Demone && pg2.archetipo == Archetipo.Sayan)                              //Demone fa piu danno su Sayan
            {
                pg1.forza += (pg1.forza * 10) / 100;
                pg1.atkb = (double)(pg1.forza * 5) / 50;
                pg1.skill.dmgskill = (double)(pg1.forza * 10) / 50;

                Console.WriteLine($"{pg1.nome} è di archetipo {pg1.archetipo} e guadagna un buff alla forza del 10% contro {pg2.nome} di archetipo {pg2.archetipo}");
            }
            
            else if (pg2.archetipo == Archetipo.Demone && pg1.archetipo == Archetipo.Sayan)
            {
                pg2.forza += (pg2.forza * 10) / 100;
                pg2.atkb = (double)(pg2.forza * 5) / 50;
                pg2.skill.dmgskill = (double)(pg2.forza * 10) / 50;

                Console.WriteLine($"{pg2.nome} è di archetipo {pg2.archetipo} e guadagna un buff alla forza del 10% contro {pg1.nome} di archetipo {pg1.archetipo}");
            }

            if (pg1.archetipo == Archetipo.Pirata && pg2.archetipo == Archetipo.Ninja)                              //Pirata fa piu danno su Ninja
            {
                pg1.forza += (pg1.forza * 10) / 100;
                pg1.atkb = (double)(pg1.forza * 5) / 50;
                pg1.skill.dmgskill = (double)(pg1.forza * 10) / 50;

                Console.WriteLine($"{pg1.nome} è di archetipo {pg1.archetipo} e guadagna un buff alla forza del 10% contro {pg2.nome} di archetipo {pg2.archetipo}");
            }
            
            else if (pg2.archetipo == Archetipo.Pirata && pg1.archetipo == Archetipo.Ninja)
            
            {
                pg2.forza += (pg2.forza * 10) / 100;
                pg2.atkb = (double)(pg2.forza * 5) / 50;
                pg2.skill.dmgskill = (double)(pg2.forza * 10) / 50;

                Console.WriteLine($"{pg2.nome} è di archetipo {pg2.archetipo} e guadagna un buff alla forza del 10% contro {pg1.nome} di archetipo {pg1.archetipo}");
            }

            if (pg1.archetipo == Archetipo.Ninja && pg2.archetipo == Archetipo.Demone)                              //Ninja fa piu danno su Demone
            {
                pg1.forza += (pg1.forza * 10) / 100;
                pg1.atkb = (double)(pg1.forza * 5) / 50;
                pg1.skill.dmgskill = (double)(pg1.forza * 10) / 50;

                Console.WriteLine($"{pg1.nome} è di archetipo {pg1.archetipo} e guadagna un buff alla forza del 10% contro {pg2.nome} di archetipo {pg2.archetipo}");
            }
            
            else if (pg2.archetipo == Archetipo.Ninja && pg1.archetipo == Archetipo.Demone)
            
            {
                pg2.forza += (pg2.forza * 10) / 100;
                pg2.atkb = (double)(pg2.forza * 5) / 50;
                pg2.skill.dmgskill = (double)(pg2.forza * 10) / 50;

                Console.WriteLine($"{pg2.nome} è di archetipo {pg2.archetipo} e guadagna un buff alla forza del 10% contro {pg1.nome} di archetipo {pg1.archetipo}");
            }

            if(pg1.archetipo == Archetipo.Umano)                                                                    //Umano non ha particolari buff o debuff
            {
                
                Console.WriteLine($"{pg1.nome} è di archetipo {pg1.archetipo} e non ha particolari buff alla forza ma non subisce danni maggiorati da nessun archetipo");
            }
            
            else if(pg2.archetipo == Archetipo.Umano)
            
            {
                Console.WriteLine($"{pg2.nome} è di archetipo {pg2.archetipo} e non ha particolari buff alla forza ma non subisce danni maggiorati da nessun archetipo");
            }
        }
   }             
}