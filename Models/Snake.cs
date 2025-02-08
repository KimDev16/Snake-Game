using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace Snake_game.Models
{
    public class Snake
    {

        public List<Tails>  tailsSnake = new List<Tails>();
        private GraphicsDeviceManager _graphics;
        GraphicsDevice GraphicsDevice;
        ContentManager Content;

        String DirectionSnake;
        String lastDirection;
        public Snake(GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            this.GraphicsDevice = GraphicsDevice;
            this.Content = Content;
            this.DirectionSnake = "R";
            this.lastDirection = "R";
            this._graphics = _graphics;
            Random random = new Random();
            int x = (int)random.Next(0, (int)(Globals.width / Globals.fruitSnakeWidth)) * Globals.fruitSnakeWidth;
            int y = Globals.width / 2 + (Globals.width / 2)% Globals.fruitSnakeWidth;
            Debug.WriteLine("x="+ x + "y" + y);

            this.tailsSnake.Add(new Tails(new System.Numerics.Vector2(x, y)));
            this.tailsSnake.Add(new Tails(new System.Numerics.Vector2(x - Globals.fruitSnakeWidth,y)));
            this.tailsSnake.Add(new Tails(new System.Numerics.Vector2(x - Globals.fruitSnakeWidth * 2,y)));
            //this.tailsSnake.Add(new Tails(new System.Numerics.Vector2(Globals.width / 2 - Globals.fruitSnakeWidth, Globals.height / 2)));
            //this.tailsSnake.Add(new Tails(new System.Numerics.Vector2(Globals.width / 2 - Globals.fruitSnakeWidth*2, Globals.height / 2)));
            snakeTexture();
        }
        public void snakeTexture()
        {
            switch(lastDirection){
                case "R":
                    this.tailsSnake[0].tailTexture= this.Content.Load<Texture2D>("head_right");
                    break;
                case "L":
                    this.tailsSnake[0].tailTexture = this.Content.Load<Texture2D>("head_left");
                    break;
                case "U":
                    this.tailsSnake[0].tailTexture = this.Content.Load<Texture2D>("head_up");
                    break;
                case "D":
                    this.tailsSnake[0].tailTexture = this.Content.Load<Texture2D>("head_down");
                    break;
            }
            for (int i = 1; i < tailsSnake.Count; i++)
            {
                this.tailsSnake[i].tailTexture = this.Content.Load<Texture2D>("body_horizontal");

            }
        }
        public void addNewTail()
        {
            tailsSnake.Add(new Tails(new System.Numerics.Vector2(tailsSnake[tailsSnake.Count - 1].rect.X, tailsSnake[tailsSnake.Count - 1].rect.Y) ));
            snakeTexture();
        }
        public void update(GameTime gameTime)
        {   
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                if(DirectionSnake!="D" && lastDirection !="D")
                 DirectionSnake = "U";

            }
            if (kstate.IsKeyDown(Keys.Right) )
            {
                if (DirectionSnake != "L" && lastDirection != "L")

                    DirectionSnake = "R";

            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                if (DirectionSnake != "U" && lastDirection != "U")

                    DirectionSnake = "D";

            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                if (DirectionSnake != "R" && lastDirection != "R")

                    DirectionSnake = "L";

            }
        }
        public void move(GameTime gameTime)
        {
            for (int i = tailsSnake.Count - 1; i > 0; i--)
            {
                tailsSnake[i].rect.X = tailsSnake[i - 1].rect.X;
                tailsSnake[i].rect.Y = tailsSnake[i - 1].rect.Y;
            }
            tailsSnake[0].update(DirectionSnake, gameTime);
            lastDirection = DirectionSnake;
            snakeTexture();


        }
        public void draw()
        {
            for (int i = tailsSnake.Count - 1; i >= 0; i--)
            {
                tailsSnake[i].draw();
            }
        }  
        public bool endGame()
        {
            //verify the border
            if (tailsSnake[0].rect.X > Globals.width || tailsSnake[0].rect.X < 0 ||  tailsSnake[0].rect.Y> Globals.height || tailsSnake[0].rect.Y < 0)
                 return true;
            //verify touch tail
            for (int i = 1;i<tailsSnake.Count; i++) {
                    if (tailsSnake[0].rect.Intersects(tailsSnake[i].rect))
                    {
                        return true;
                    }                    
            }
            return false;
        }
    }
}
