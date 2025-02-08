using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game.Models
{
    public class Tails
    {
        public  Texture2D tailTexture;

        public Rectangle rect;
        private int moveAmount =(int) Globals.fruitSnakeWidth; // Move 10 pixels each time
        public Tails (System.Numerics.Vector2 positionTail)
        {
            rect = new Rectangle(((int)positionTail.X), (int) positionTail.Y, Globals.fruitSnakeWidth, Globals.fruitSnakeWidth);
        }
        public void update(String Direction, GameTime gameTime)
        {   
                // Move the rectangle by 10 pixels
               // rect.Y -= moveAmount;
                switch (Direction)
                {
                    case "U":
                        this.rect.Y -= moveAmount;
                        
                        break;
                    case "R":
                        this.rect.X += moveAmount;
                        break;
                    case "D":
                        this.rect.Y += moveAmount;
                        break;
                    case "L":
                        this.rect.X -= moveAmount;
                        break;
                }
               
                // Reset the timer
        }
        public void draw()
        {
            Globals._spriteBatch.Draw(tailTexture, rect, Color.White);
        }

    }
}
