using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    class Ball
    {
        #region Properties
        public Texture2D ball;
        public Vector2 ballPos;
        public bool gameStarted = false;
        private Vector2 velocity = Methods.StartDirection();
        #endregion

        public bool goingLeft()
        {
            bool left = false;
            if (velocity.X < 0)
                left = true;
            return left;
        }

        public bool goingRight()
        {
            bool right = false;
            if (velocity.X > 0)
                right = true;
            return right;
        }

        public void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            ball = Content.Load<Texture2D>("Ball");
            ballPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, ballPos, Color.White);
        }

        public void Update(GraphicsDeviceManager graphics,ref int player1Score, ref int? playerlives, Paddle paddle)
        {
            int maxX = graphics.GraphicsDevice.Viewport.Width - ball.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - ball.Height;
            double angleNormal = Math.Atan2(velocity.Y, velocity.X);
            double angleBallMovment = Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double reflectionAngle = angleNormal - (angleBallMovment - angleNormal);
            Vector2 centerScreen = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - ball.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 - ball.Height  / 2);
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space))
            {
                gameStarted = true;
                paddle.isRunning = true;
            }

            if (playerlives == null)
            {
                gameStarted = true;
                paddle.isRunning = true;
            }

            if (gameStarted)
                ballPos += velocity;
            if (ballPos.X >= maxX)
            {
                ballPos = centerScreen;
                gameStarted = false;
                paddle.isRunning = false;
                velocity = Methods.StartDirection();
                player1Score += 10;
            }
            if (ballPos.X <= 0)
            {
                ballPos = centerScreen;
                gameStarted = false;
                paddle.isRunning = false;
                playerlives--;
                velocity = Methods.StartDirection();
            }
            if (ballPos.Y >= maxY || ballPos.Y <= 0)
            {
                velocity.Y = -velocity.Y;
            }
            if ((ballPos.Y >= paddle.paddlePos.Y
                && ballPos.Y <= paddle.paddlePos.Y + paddle.paddle.Height)
                && ballPos.X <= paddle.paddle.Width)
            {
                velocity.X = Methods.randomXSpeed(this.goingRight());
            }
            if ((ballPos.Y >= paddle.AIpaddlePos.Y
                && ballPos.Y <= paddle.AIpaddlePos.Y + paddle.AIpaddle.Height)
                && ballPos.X + ball.Width >= graphics.GraphicsDevice.Viewport.Width - paddle.paddle.Width)
                velocity.X = Methods.randomXSpeed(this.goingRight());
        }
    }
}
