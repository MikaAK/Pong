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
    class Paddle
    {
        #region Properties
        public Texture2D paddle;
        public Vector2 paddlePos;
        private float playerSpeed;
        public Texture2D AIpaddle;
        public Vector2 AIpaddlePos;
        private float AISpeed;
        public bool isRunning = false;
#endregion

        public void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            paddle = Content.Load<Texture2D>("Paddle");
            AIpaddle = Content.Load<Texture2D>("AIPaddle");
            paddlePos = new Vector2(0, graphics.GraphicsDevice.Viewport.Height / 3 + paddle.Height);
            AIpaddlePos = new Vector2(graphics.GraphicsDevice.Viewport.Width - AIpaddle.Width, graphics.GraphicsDevice.Viewport.Height / 3 + paddle.Height);
        }

        public void Update(GraphicsDeviceManager graphics, Ball ball, Level level)
        {
            UpdatePlayer(graphics);
            UpdateAI(graphics, ball, level);
        }

        public void UpdateAIvsAI(GraphicsDeviceManager graphics, Ball ball, Level level)
        {
            isRunning = true;
            UpdateAI(graphics, ball, level);
            UpdatePlayerAsAI(graphics, ball);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddle, paddlePos, Color.White);
            spriteBatch.Draw(AIpaddle, AIpaddlePos, Color.White);
        }

        private void UpdatePlayer(GraphicsDeviceManager graphics)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space))
                isRunning = true;

            if (keyState.IsKeyDown(Keys.W))
            {
                if (paddlePos.Y <= 0)
                    paddlePos.Y = 0;
                else
                    paddlePos.Y -= playerSpeed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                if (paddlePos.Y >= graphics.GraphicsDevice.Viewport.Height - paddle.Height)
                    paddlePos.Y = graphics.GraphicsDevice.Viewport.Height - paddle.Height;
                else
                    paddlePos.Y += playerSpeed;
            }
        }

        private void UpdateAI(GraphicsDeviceManager graphics, Ball ball, Level level)
        {
            int maxX = graphics.GraphicsDevice.Viewport.Width / 3 - ball.ball.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - AIpaddle.Height;
            if (isRunning)
            {
                if (AIpaddlePos.Y <= ball.ballPos.Y && ball.goingRight())
                    AIpaddlePos.Y += AISpeed;
                if (AIpaddlePos.Y >= ball.ballPos.Y && ball.goingRight())
                    AIpaddlePos.Y -= AISpeed;
            }
            if (AIpaddlePos.Y >= maxY)
                AIpaddlePos.Y = maxY;
            if (AIpaddlePos.Y <= 0)
                AIpaddlePos.Y = 0;
            if (!isRunning)
            {
                AIpaddlePos = new Vector2(graphics.GraphicsDevice.Viewport.Width - AIpaddle.Width, graphics.GraphicsDevice.Viewport.Height / 3 + AIpaddle.Height);
            }

            switch (level)
            #region Levels
            {
                case Level.One:
                    AISpeed = 5.5f;
                    playerSpeed = 6f;                    
                    break;
                case Level.Two:
                    AISpeed = 5.8f;
                    playerSpeed = 6.3f;
                    break;
                case Level.Three:      
                    AISpeed = 6f;
                    playerSpeed = 6.5f;
                    break;
                case Level.Four:
                    AISpeed = 6.4f;
                    playerSpeed = 6.7f;                    
                    break;
                case Level.Five:                         
                    AISpeed = 6.6f;
                    playerSpeed = 6.9f;  
                    break;
                case Level.Six:
                    AISpeed = 6.9f;
                    playerSpeed = 7.1f;                 
                    break;
                case Level.Seven:                        
                    AISpeed = 7.1f;
                    playerSpeed = 7.2f;     
                    break;
                case Level.Eight:
                    AISpeed = 7.3f;
                    playerSpeed = 7.4f;                     
                    break;
                case Level.Nine:
                    AISpeed = 7.4f;
                    playerSpeed = 7.5f;             
                    break;
                case Level.Ten:
                    AISpeed = 7.5f;
                    playerSpeed = 7.6f;            
                    break;
                case Level.Eleven:
                    AISpeed = 7.6f;
                    playerSpeed = 7.7f;               
                    break;
                case Level.Twelve:
                    AISpeed = 7.7f;
                    playerSpeed = 7.8f;               
                    break;
                case Level.Thirteen:
                    AISpeed = 7.8f;
                    playerSpeed = 7.9f;                 
                    break;
                case Level.Fourteen:
                    AISpeed = 7.9f;
                    playerSpeed = 8f;                 
                    break;
                case Level.Fifteen:
                    AISpeed = 8.5f;
                    playerSpeed = 9f;                
                    break;            
            }
            #endregion
        }

        private void UpdatePlayerAsAI(GraphicsDeviceManager graphics, Ball ball)
        {
            int maxX = graphics.GraphicsDevice.Viewport.Width / 3 - ball.ball.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - AIpaddle.Height;
            if (isRunning)
            {
                if (paddlePos.Y <= ball.ballPos.Y && ball.goingLeft())
                    paddlePos.Y += AISpeed;
                if (paddlePos.Y >= ball.ballPos.Y && ball.goingLeft())
                    paddlePos.Y -= AISpeed;
            }
            if (paddlePos.Y >= maxY)
                paddlePos.Y = maxY;
            if (paddlePos.Y <= 0)
                paddlePos.Y = 0;
            if (!isRunning)
            {
                paddlePos = new Vector2(graphics.GraphicsDevice.Viewport.Width - AIpaddle.Width, graphics.GraphicsDevice.Viewport.Height / 3 + AIpaddle.Height);
            }
        }
    }
}
