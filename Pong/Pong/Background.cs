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
    class Background
    {
        #region Properties

        private Texture2D background;
        private Rectangle backgroundRect;
        private Texture2D arrow;
        private Rectangle arrowRect;
        private Texture2D SecondScreen;
        private MenuState menuState;
        private bool isPressed = false;
        public Level level;
        public Screen screen;

        #endregion

        public void Update(ContentManager Content, GraphicsDeviceManager graphics, ref int? playerLives, ref int p1Score, Paddle paddle, Ball ball)
        {
            KeyboardState keystate = Keyboard.GetState();

            if (isPressed && keystate.IsKeyUp(Keys.Up) && keystate.IsKeyUp(Keys.Down) && keystate.IsKeyUp(Keys.Enter) && keystate.IsKeyUp(Keys.P))
            {
                isPressed = false;
            }

            switch (screen)
            {
                #region ScreenStart
                case Screen.Start:
                    if (keystate.IsKeyDown(Keys.Down) && !isPressed)
                    {
                        if (menuState == MenuState.Options)
                            menuState = MenuState.Credits;
                        if (menuState == MenuState.Play)
                            menuState = MenuState.Options;
                        isPressed = true;
                    }

                    if (keystate.IsKeyDown(Keys.Up) && !isPressed)
                    {
                        if (menuState == MenuState.Options)
                            menuState = MenuState.Play;
                        if (menuState == MenuState.Credits)
                            menuState = MenuState.Options;
                        isPressed = true;
                    }
                    #region Menu States
                    switch (menuState)
                    {
                        case MenuState.Play:
                            arrowRect.X = 450;
                            arrowRect.Y = 220;
                            if (keystate.IsKeyDown(Keys.Enter) && !isPressed)
                            {
                                LoadPlayContent(Content);
                            }

                            break;
                        case MenuState.Options:
                            arrowRect.X = 419;
                            arrowRect.Y = 340;
                            if (keystate.IsKeyDown(Keys.Enter) && !isPressed)
                                screen = Screen.Options;
                            break;
                        case MenuState.Credits:
                            arrowRect.X = 425;
                            arrowRect.Y = 460;
                            if (keystate.IsKeyDown(Keys.Enter) && !isPressed)
                                screen = Screen.Credits;
                            break;
                    }
                    #endregion
                    break;
                #endregion
                #region ScreenPause
                case Screen.Pause:
                    if (keystate.IsKeyDown(Keys.P) && !isPressed)
                    {
                        isPressed = true;
                        screen = Screen.Play;

                    }
                    if (keystate.IsKeyDown(Keys.Back))
                    {
                        screen = Screen.Start;
                        LoadStartContent(Content, graphics, ref playerLives, paddle, ball);
                    }
                    break;
                #endregion
                #region ScreenOver
                case Screen.Over:
                    LoadOverContent(Content);
                    if (keystate.IsKeyDown(Keys.Enter) && !isPressed)
                    {
                        LoadStartContent(Content, graphics, ref playerLives);
                        isPressed = true;
                    }
                    break;
                #endregion
                #region ScreenPlay
                case Screen.Play:
                    if (playerLives < 1)
                    {
                        screen = Screen.Over;
                        LoadOverContent(Content);
                        playerLives = 50;
                        p1Score = 0;
                    }
                    if (keystate.IsKeyDown(Keys.P) && !isPressed)
                    {
                        isPressed = true;
                        LoadPauseContent(Content);
                    }
                    #region LevelLogic
                    if (p1Score >= 50 && level == Level.One)
                        level = Level.Two;
                    if (p1Score >= 100 && level == Level.Two)
                        level = Level.Three;
                    if (p1Score >= 150 && level == Level.Three)
                        level = Level.Four;
                    if (p1Score >= 200 && level == Level.Four)
                        level = Level.Five;
                    if (p1Score >= 250 && level == Level.Five)
                        level = Level.Six;
                    if (p1Score >= 300 && level == Level.Six)
                        level = Level.Seven;
                    if (p1Score >= 350 && level == Level.Seven)
                        level = Level.Eight;
                    if (p1Score >= 400 && level == Level.Eight)
                        level = Level.Nine;
                    if (p1Score >= 450 && level == Level.Nine)
                        level = Level.Ten;
                    if (p1Score >= 500 && level == Level.Ten)
                        level = Level.Eleven;
                    if (p1Score >= 550 && level == Level.Eleven)
                        level = Level.Twelve;
                    if (p1Score >= 600 && level == Level.Twelve)
                        level = Level.Thirteen;
                    if (p1Score >= 650 && level == Level.Thirteen)
                        level = Level.Fourteen;
                    if (p1Score >= 700 && level == Level.Fourteen)
                        level = Level.Fifteen;
                    #endregion
                    LoadPlayContent(Content);
                    break;
                #endregion
                #region ScreenCredits
                case Screen.Credits:
                    LoadCreditsContent(Content);
                    if (keystate.IsKeyDown(Keys.Back))
                    {
                        screen = Screen.Start;
                        p1Score = 0;
                        LoadStartContent(Content, graphics, ref playerLives, paddle, ball);
                    }
                    break;
                #endregion
            }
        }

        #region LoadContents

        #region LoadStartContentsx2
        public void LoadStartContent(ContentManager Content, GraphicsDeviceManager graphics, ref int? playerLives, Paddle paddle, Ball ball)
        {
            background = Content.Load<Texture2D>("startBackground");
            arrow = Content.Load<Texture2D>("arrow");
            backgroundRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            arrowRect = new Rectangle(450, 225, arrow.Width, arrow.Height);
            screen = Screen.Start;
            playerLives = 5;
            paddle.AIpaddlePos.Y = graphics.GraphicsDevice.Viewport.Height / 2 - paddle.AIpaddle.Height / 2;
            paddle.paddlePos.Y = graphics.GraphicsDevice.Viewport.Height / 2 - paddle.AIpaddle.Height / 2;
            ball.gameStarted = false;
            ball.ballPos.X = graphics.GraphicsDevice.Viewport.Width / 2 - ball.ball.Width / 2;
            ball.ballPos.Y = graphics.GraphicsDevice.Viewport.Height / 2 - ball.ball.Height / 2;
            level = Level.One;
        }

        public void LoadStartContent(ContentManager Content, GraphicsDeviceManager graphics, ref int? playerLives)
        {
            background = Content.Load<Texture2D>("startBackground");
            arrow = Content.Load<Texture2D>("arrow");
            backgroundRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            arrowRect = new Rectangle(450, 225, arrow.Width, arrow.Height);
            screen = Screen.Start;
            playerLives = 5;
        }
        #endregion

        private void LoadPlayContent(ContentManager Content)
        {
            screen = Screen.Play;
            switch (level)
            #region levels
            {
                case Level.One:
                    background = Content.Load<Texture2D>("levelOneBackground");
                    break;
                case Level.Two:
                    background = Content.Load<Texture2D>("levelTwoBackground");
                    break;
                case Level.Three:
                    background = Content.Load<Texture2D>("levelThreeBackground");
                    break;
                case Level.Four:
                    background = Content.Load<Texture2D>("levelFourBackground");
                    break;
                case Level.Five:
                    background = Content.Load<Texture2D>("levelFiveBackground");
                    break;
                case Level.Six:
                    background = Content.Load<Texture2D>("levelSixBackground");
                    break;
                case Level.Seven:
                    background = Content.Load<Texture2D>("levelSevenBackground");
                    break;
                case Level.Eight:
                    background = Content.Load<Texture2D>("levelEightBackground");
                    break;
                case Level.Nine:
                    background = Content.Load<Texture2D>("levelNineBackground");
                    break;
                case Level.Ten:
                    background = Content.Load<Texture2D>("levelTenBackground");
                    break;
                case Level.Eleven:
                    background = Content.Load<Texture2D>("levelElevenBackground");
                    break;
                case Level.Twelve:
                    background = Content.Load<Texture2D>("levelTwelveBackground");
                    break;
                case Level.Thirteen:
                    background = Content.Load<Texture2D>("levelThirteenBackground");
                    break;
                case Level.Fourteen:
                    background = Content.Load<Texture2D>("levelFourteenBackground");
                    break;
                case Level.Fifteen:
                    background = Content.Load<Texture2D>("levelFifteenBackground");
                    break;
            }
            #endregion
        }

        private void LoadOverContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("gameOverBackground");
            screen = Screen.Over;
        }

        private void LoadPauseContent(ContentManager Content)
        {
            SecondScreen = Content.Load<Texture2D>("Pause");
            screen = Screen.Pause;
        }

        private void LoadOptionsContent(ContentManager Content)
        {
            SecondScreen = Content.Load<Texture2D>("Options");
            screen = Screen.Options;
        }

        private void LoadCreditsContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Credits");
            screen = Screen.Credits;
        }
        #endregion

        public void Draw(SpriteBatch spritebatch)
        {
            switch (screen)
            {
                case Screen.Start:
                    spritebatch.Draw(background, backgroundRect, Color.White);
                    spritebatch.Draw(arrow, arrowRect, Color.White);
                    break;
                case Screen.Pause:
                    spritebatch.Draw(background, backgroundRect, Color.White);
                    spritebatch.Draw(SecondScreen, backgroundRect, Color.White);
                    break;
                default:
                    spritebatch.Draw(background, backgroundRect, Color.White);
                    break;
            }
        }

        private void BackgroundParralax(GameTime gameTime)
        {

        }
    }
}
