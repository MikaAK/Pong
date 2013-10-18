using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace Pong
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Properties
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Variables For Update
        int player1Score = 0;
        int? playerLives = 5;
        Screen screen;
        Level level;

        //Score Stuff
        SpriteFont font;
        Vector2 fontPos;

        //sprites
        Paddle paddle;
        Ball ball;

        //Backgrounds
        Background background;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1080;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            paddle = new Paddle();
            ball = new Ball();
            background = new Background();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            paddle.LoadContent(Content, graphics);
            ball.LoadContent(Content, graphics);
            font = Content.Load<SpriteFont>("Font");
            fontPos = Vector2.Zero;
            background.LoadStartContent(Content, graphics, ref playerLives, paddle, ball);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();

            if (screen == Screen.Play)
            {
                paddle.Update(graphics, ball, level);
                ball.Update(graphics, ref player1Score, ref playerLives, paddle);
            }

            if (screen == Screen.Credits)
            {
                paddle.UpdateAIvsAI(graphics, ball, level);
                ball.Update(graphics, ref player1Score, ref playerLives, paddle);
                playerLives = null;
            }

            background.Update(Content, graphics, ref playerLives, ref player1Score, paddle, ball);
            screen = background.screen;
            level = background.level;
            Methods.level = level;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            background.Draw(spriteBatch);

            if (screen == Screen.Play)
            {
                paddle.Draw(spriteBatch);
                ball.Draw(spriteBatch);
                spriteBatch.DrawString(font,
                                       "Score: " + player1Score.ToString() + "\nLives: " + playerLives.ToString(),
                                       fontPos,
                                       Color.FromNonPremultiplied(217, 56, 196, 300));
            }
            if (screen == Screen.Credits)
            {
                paddle.Draw(spriteBatch);
                ball.Draw(spriteBatch);                
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
