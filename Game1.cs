using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Collision_With_Rectangles
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        KeyboardState keyboardState;
        MouseState mouseState;


        Texture2D pacUpTexture;
        Texture2D pacDownTexture;
        Texture2D pacLeftTexture;
        Texture2D pacRightTexture;
        Texture2D currentpacTexture; // stores the current texture

        Rectangle pacrect;
        //add-ins
        Texture2D exitTexture;
        Rectangle exitRect;

        Texture2D barrierTexture;
        List<Rectangle> barriers;

        Texture2D heartTexture;
        Rectangle heart1, heart2, heart3;

        SoundEffect coinget;
        SoundEffect pacDeath;

        Texture2D coinTexture;
        List<Rectangle> coins;

        int lives = 3;
        

        int pacspeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            pacspeed = 3;
            pacrect = new Rectangle(10, 10, 60, 60);
            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(0, 250, 350, 75));
            barriers.Add(new Rectangle(450, 250, 350, 75));
            coins = new List<Rectangle>();
            coins.Add(new Rectangle(400, 50, coinTexture.Width, coinTexture.Height));
            coins.Add(new Rectangle(475, 50, coinTexture.Width, coinTexture.Height));
            coins.Add(new Rectangle(200, 300, coinTexture.Width, coinTexture.Height));
            coins.Add(new Rectangle(400, 300, coinTexture.Width, coinTexture.Height));


            
            heart1 = new Rectangle(650, 10, 50,50);
            heart2 = new Rectangle(700, 10, 50, 50);
            heart3 = new Rectangle(750, 10, 50, 50);


            exitRect = new Rectangle(700, 380, 100, 100);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pacDownTexture = Content.Load<Texture2D>("PacDown");
            pacUpTexture = Content.Load<Texture2D>("PacUp");
            pacLeftTexture = Content.Load<Texture2D>("PacLeft");
            pacRightTexture = Content.Load<Texture2D>("PacRight");
            currentpacTexture = pacRightTexture;

            // add ins
            exitTexture = Content.Load<Texture2D>("hobbit_door");
            barrierTexture = Content.Load<Texture2D>("rock_barrier");
            coinTexture = Content.Load<Texture2D>("coin");
            heartTexture = Content.Load<Texture2D>("minecraft_heart");
            coinget = Content.Load<SoundEffect>("Mario Coin Sound - Sound Effect (HD)");
            pacDeath = Content.Load<SoundEffect>("Pacman Death");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
                        
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                currentpacTexture = pacUpTexture;
                pacrect.Y -= pacspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                currentpacTexture = pacDownTexture;
                pacrect.Y += pacspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                currentpacTexture = pacLeftTexture;
                pacrect.X -= pacspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                currentpacTexture = pacRightTexture;
                pacrect.X += pacspeed;
            }
           

            if (pacrect.Left < 0)
            {
                pacrect.X = 0;
            }
            if (pacrect.Top < 0)
            {
                pacrect.Y = 0;
            }

            if (pacrect.Bottom >= _graphics.PreferredBackBufferHeight)
                pacrect.Y = _graphics.PreferredBackBufferHeight - 60;
            if (pacrect.Right >= _graphics.PreferredBackBufferWidth)
                pacrect.X = _graphics.PreferredBackBufferWidth - 60;


            for (int i = 0; i < coins.Count; i++)
            {
                if (pacrect.Intersects(coins[i]))
                {
                    coins.RemoveAt(i);
                    i--;
                    coinget.Play();
                }
            }

            foreach (Rectangle barrier in barriers)
                if (pacrect.Intersects(barrier))
                {
                    pacrect.X = 10;
                    pacrect.Y = 10;
                    lives = lives - 1;
                    pacDeath.Play();
                }
            if (lives == 3) 
            {
                
            }
            if (lives == 2)
            {
                heart1.Location = new Point(1000, 600);
            }
            if (lives == 1)
            {
                heart2.Location = new Point(1000, 600);
            }
            if (lives == 0)
                Exit();


            if (exitRect.Contains(pacrect) && mouseState.LeftButton == ButtonState.Pressed )
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // Draw Pacman

            _spriteBatch.Draw(currentpacTexture, pacrect, Color.White);
            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(barrierTexture, barrier, Color.White);
            _spriteBatch.Draw(exitTexture, exitRect, Color.White);
            _spriteBatch.Draw(currentpacTexture, pacrect, Color.White);
            foreach (Rectangle coin in coins)
                _spriteBatch.Draw(coinTexture, coin, Color.White);
                
            _spriteBatch.Draw(heartTexture, heart1, Color.White);
            _spriteBatch.Draw(heartTexture, heart2, Color.White);
            _spriteBatch.Draw(heartTexture, heart3, Color.White);


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
