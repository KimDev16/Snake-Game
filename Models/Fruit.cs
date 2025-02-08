using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using System.Numerics;
using Microsoft.Xna.Framework.Content;
namespace Snake_game.Models
{
    public class Fruit
    {
        public  Texture2D fruitTexture;
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice GraphicsDevice;

        private System.Numerics.Vector2 position;
        private int fruitfruitSnakeWidth=Globals.fruitSnakeWidth;
        public static Rectangle rect;
        ContentManager Content;


        public Fruit(GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content) {
            this.Content=Content;
            this._graphics = _graphics;
            this.GraphicsDevice = GraphicsDevice;
            Random random = new Random();

            int x = (int)random.Next(0, (int)(Globals.width/Globals.fruitSnakeWidth))* Globals.fruitSnakeWidth;
            int y = (int)random.Next(0, (int)(Globals.height/Globals.fruitSnakeWidth)) * Globals.fruitSnakeWidth;
            rect = new Rectangle(x, y, Globals.fruitSnakeWidth, Globals.fruitSnakeWidth);
            FruitTexture();
        }
       public void FruitTexture()
        {
            this.fruitTexture = new Texture2D(GraphicsDevice, 1, 1);
            this.fruitTexture.SetData<Color>(new Color[] { Color.Red });
        }
        public void update()
        {
            Random random = new Random();
            int x = (int)random.Next(0, (int)(Globals.width / Globals.fruitSnakeWidth) )*Globals.fruitSnakeWidth;
            int y = (int)random.Next(0, (int)(Globals.height / Globals.fruitSnakeWidth) )*Globals.fruitSnakeWidth;
            rect.X =x;
            rect.Y = y;

        }
        public void draw()
        {
            Globals._spriteBatch.Draw(fruitTexture, rect, Color.White);
        }
    }
}
