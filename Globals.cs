using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game
{
    public class Globals
    {
        public static SpriteBatch _spriteBatch;
        public static int width = 300;
        public static int height = 300;
        public static int fruitSnakeWidth = 30;
        public static String[] Direction = { "U", "R", "B", "L" };
        private static float _speedGame=10; // Base speed of the snake


    }
}
