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
    static class Methods
    {
        public static Level level;

        public static Vector2 StartDirection()
        {
            Vector2 velocity = new Vector2();
            Random randNumb = new Random();
            int rand = randNumb.Next(0, 200);
            if (rand >= 0)
            {
                if (rand <= 100)
                    velocity.X = randNumb.Next(4, 6);
                else
                    velocity.X = randNumb.Next(-6, -4);
            }
            if (rand <= 200)
            {
                if (rand <= 100)
                    velocity.Y = randNumb.Next(4, 6);
                else
                    velocity.Y = randNumb.Next(-6, -4);
            }
            return velocity;
        }

        public static int randomXSpeed(bool isGoingRight)
        {
            int velocity = 0;
            Random randnumb = new Random();
            double rand = randnumb.NextDouble();

            switch (level)
            #region Levels
            {
                case Level.One:
                    if (rand < 0.225234)
                        velocity = -5;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -6;
                    if (rand > 0.414822 && rand < 0.870000)
                        velocity = -7;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -8;                    
                    break;
                case Level.Two:
                    if (rand < 0.225234)
                        velocity = -5;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -8;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -7;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -8;    
                    break;
                case Level.Three:
                    if (rand < 0.225234)
                        velocity = -6;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -7;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -8;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -9;    
                    break;
                case Level.Four:
                    if (rand < 0.225234)
                        velocity = -7;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -8;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -9;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -10;    
                    break;
                case Level.Five:
                    if (rand < 0.225234)
                        velocity = -8;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -9;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -10;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -11;    
                    break;
                case Level.Six:
                    if (rand < 0.225234)
                        velocity = -8;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -7;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -11;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -10;    
                    break;
                case Level.Seven:
                    if (rand < 0.225234)
                        velocity = -11;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -12;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -9;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -7;    
                    break;
                case Level.Eight: 
                    if (rand < 0.225234)
                        velocity = -10;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -12;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -11;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -8;    
                    break;
                case Level.Nine:
                    if (rand < 0.225234)
                        velocity = -12;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -11;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -9;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -7;    
                    break;
                case Level.Ten:
                    if (rand < 0.225234)
                        velocity = -8;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -14;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -13;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -11;    
                    break;
                case Level.Eleven:
                    if (rand < 0.225234)
                        velocity = -11;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -10;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -11;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -12;    
                    break;
                case Level.Twelve:
                    if (rand < 0.225234)
                        velocity = -12;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -12;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -10;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -10;    
                    break;
                case Level.Thirteen:
                    if (rand < 0.225234)
                        velocity = -11;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -13;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -10;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -11;    
                    break;
                case Level.Fourteen:
                    if (rand < 0.225234)
                        velocity = -10;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -10;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -11;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -12;    
                    break;
                case Level.Fifteen:
                    if (rand < 0.225234)
                        velocity = -15;
                    if (rand > 0.225234 && rand < 0.414823)
                        velocity = -13;
                    if (rand > 0.414823 && rand < 0.870000)
                        velocity = -12;
                    if (rand > 0.870000 && rand <= 1)
                        velocity = -13;    
                    break;            
            }
            #endregion
            if (!isGoingRight)
                velocity *= -1;
            return velocity;
        }
    }
}
