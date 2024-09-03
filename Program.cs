using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_JUMPFORCE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\ereng\\source\\repos\\ConsoleApp_JUMPFORCE\\mains.wav";
            SoundPlayer player = new SoundPlayer(filePath);
            player.PlayLooping();
            Scontro sc1 = new Scontro();
            sc1.popola();
            sc1.genera();
            Console.ReadKey();
        }


    }
  
}
