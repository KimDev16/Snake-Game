using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake_game.Models;
using System;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace Snake_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        Fruit fruit;
        Snake snake;
        private float elapsedTime = 0f; // Timer to track time
        private  float moveInterval = 0.5f; // Move every 1seconds
        private int _score;
        private GameState currentState = GameState.Playing;
        private SpriteFont font;
        Texture2D GrassTexture;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.width;
            _graphics.PreferredBackBufferHeight = Globals.height;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            _score = 1;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fruit=new Fruit(_graphics, GraphicsDevice, Content);
            snake=new Snake(_graphics, GraphicsDevice, Content);

            //Inisialise
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Globals._spriteBatch = new SpriteBatch(GraphicsDevice);
            //fruit  texture
            fruit.FruitTexture();
            //snake Texture
            snake.snakeTexture();
            //font and bakground board
            font = Content.Load<SpriteFont>("font"); 
            GrassTexture = Content.Load<Texture2D>("Grass_17-128x128"); 

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (currentState == GameState.Playing)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                snake.update(gameTime);
                if (elapsedTime >= moveInterval)
                {
                    snake.move(gameTime);
                    if (snake.endGame())
                    {
                        currentState = GameState.GameOver;
                    }
                    else
                    {
                        if (snake.tailsSnake[0].rect.Intersects(Fruit.rect))
                        {
                            _score++;
                            fruit.update();

                            // Level up every 5 points
                            snake.addNewTail();
                            if (_score % 3 == 0 && moveInterval > 0.20)
                            {
                                moveInterval -= 0.1f;

                            }
                        }
                    }
                    elapsedTime -= moveInterval;

                }
            }
            else 
            if (currentState == GameState.GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R)) // Press "R" to restart
                {
                    RestartGame();
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals._spriteBatch.Begin();
            Globals._spriteBatch.Draw(GrassTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            if (currentState == GameState.Playing)
            {
                fruit.draw();
                snake.draw();
            }
            else if (currentState == GameState.GameOver)
            {
                // Draw Game Over screen
                Globals._spriteBatch.DrawString(font, "Game Over!", new Vector2(0, 40), Color.Red);
                Globals._spriteBatch.DrawString(font, $"Your final score: {_score}", new Vector2(0, 60), Color.White);
                Globals._spriteBatch.DrawString(font, "Press 'R' to restart", new Vector2(0, 80), Color.Yellow);
                Globals._spriteBatch.DrawString(font, "Press 'Exit' to exit the game", new Vector2(0, 100), Color.Yellow);
            }
            Globals._spriteBatch.End();
            base.Draw(gameTime);
        }
        private void RestartGame()
        {
            // Reset game state
            currentState = GameState.Playing;

            // Reset snake
            snake.tailsSnake.Clear();
            snake.tailsSnake.Add(new Tails(new System.Numerics.Vector2(Globals.width/2, Globals.height / 2))); // Initial head
            snake.snakeTexture();
            // Reset other variables
            _score = 1;
            // Reset food position (if random logic exists)
            fruit.update();
        }
    }
}
