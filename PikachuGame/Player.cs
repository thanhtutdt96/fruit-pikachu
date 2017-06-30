using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikachuGame
{
   public class Player
    {
        public string name;
        public int age;
        public int score;

        public Player(string name, int age, int score)
        {
            this.name = name;
            this.age = age;
            this.score = score;
        }
    }
}
