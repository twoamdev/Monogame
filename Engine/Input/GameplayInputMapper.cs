using Engine.Input.Base;
using Engine.Enum;
using Engine.Utilities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Engine.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        private int _camMoveReleased = 0;
        private int _camMoveKeyboardReleased = 0;

        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();
            

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }


            var moveResult = GetMoveDirectionOnKeyDown(state);
            if (moveResult.isKeyDown)
            {
                if (state.IsKeyDown(Keys.LeftShift))
                {
                    commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.RUNNING));
                }
                else
                {
                    commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.WALKING));
                }
                
                commands.Add(new GameplayInputCommand.PlayerMove(moveResult.direction));
            }

            //IDLE again
            if(!moveResult.isKeyDown)
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.IDLE));
            }

            //Roll
            if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.ROLLING));
            }

            //Move Camera Right
            if (state.IsKeyDown(Keys.Left) && (_camMoveKeyboardReleased == 0))
            {
                
                commands.Add(new GameplayInputCommand.CameraRotateRight());
                

            }

            //Move Camera Left
            if (state.IsKeyDown(Keys.Right) && (_camMoveKeyboardReleased == 0))
            {
                
                commands.Add(new GameplayInputCommand.CameraRotateLeft());
     

            }

            //if holding down
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.Left))
            {
                _camMoveKeyboardReleased++;
                _camMoveKeyboardReleased = MathUtils.Mod(_camMoveKeyboardReleased, 4);
            }

            if (state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.Left))
            {
                _camMoveKeyboardReleased = 0;
            }




            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) {
            var commands = new List<GameplayInputCommand>();
            
            
            /*
            //Move Player
            if(state.IsButtonDown(Buttons.LeftThumbstickDown) || state.IsButtonDown(Buttons.LeftThumbstickUp) ||
                state.IsButtonDown(Buttons.LeftThumbstickLeft) || state.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                Vector2 direction = new Vector2(state.ThumbSticks.Left.X, -1 * state.ThumbSticks.Left.Y);
                commands.Add(new GameplayInputCommand.PlayerMove(direction));
            }

            //Move Camera Right
            if (state.IsButtonDown(Buttons.RightThumbstickLeft) && (_camMoveReleased == 0))
            {   
                commands.Add(new GameplayInputCommand.CameraRotateRight());
                
            }

            //Move Camera Left
            if (state.IsButtonDown(Buttons.RightThumbstickRight) && (_camMoveReleased == 0))
            {
                commands.Add(new GameplayInputCommand.CameraRotateLeft());
            }

            //if holding down
            if (state.IsButtonDown(Buttons.RightThumbstickRight) || state.IsButtonDown(Buttons.RightThumbstickLeft))
            {
                _camMoveReleased++;
                _camMoveReleased = MathUtils.Mod(_camMoveReleased, 8);
            }
            
            if (state.IsButtonUp(Buttons.RightThumbstickRight) && state.IsButtonUp(Buttons.RightThumbstickLeft))
            {
                _camMoveReleased = 0;
            }

            //Run
            if (state.IsButtonDown(Buttons.RightTrigger))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.RUNNING));
            }

            //Walk again
            if (state.IsButtonUp(Buttons.RightTrigger) && !state.IsButtonDown(Buttons.B))
            {
               // commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.IDLE));
            }

            //Roll
            if (state.IsButtonDown(Buttons.B))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.ROLLING));
            }

            */

            return commands;
        }


        private (bool isKeyDown, Vector2 direction) GetMoveDirectionOnKeyDown(KeyboardState state)
        {
            float SQRT_OF_2 = 1.41421356237f;
            bool keyIsDown = false;
            Vector2 direction = new Vector2(0, 0);

            bool isW = state.IsKeyDown(Keys.W);
            bool isA = state.IsKeyDown(Keys.A);
            bool isS = state.IsKeyDown(Keys.S);
            bool isD = state.IsKeyDown(Keys.D);


            //is W
            if (isW && !isA && !isS && !isD)
            {
                keyIsDown = true;
                direction.X = 0;
                direction.Y = -1;

            }
            //is W and is A
            if (isW && isA && !isS && !isD)
            {
                keyIsDown = true;
                direction.X = -0.5f * SQRT_OF_2;
                direction.Y = -0.5f * SQRT_OF_2;
            }
            //is W and is D
            if (isW && isD && !isS && !isA)
            {
                keyIsDown = true;
                direction.X = 0.5f * SQRT_OF_2;
                direction.Y = -0.5f * SQRT_OF_2;
            }
            //is A
            if (isA && !isW && !isS && !isD)
            {
                keyIsDown = true;
                direction.X = -1;
                direction.Y = 0;
            }
            //is A is S
            if (isA && isS && !isW && !isD)
            {
                keyIsDown = true;
                direction.X = -0.5f * SQRT_OF_2;
                direction.Y = 0.5f * SQRT_OF_2;
            }
            //is S
            if (isS && !isW && !isA && !isD)
            {
                keyIsDown = true;
                direction.X = 0;
                direction.Y = 1;
            }
            //is S and is D
            if (isS && isD && !isA && !isW)
            {
                keyIsDown = true;
                direction.X = 0.5f * SQRT_OF_2;
                direction.Y = 0.5f * SQRT_OF_2;
            }
            //is D
            if (isD && !isW && !isA && !isS)
            {
                keyIsDown = true;
                direction.X = 1;
                direction.Y = 0;
            }
            return (keyIsDown, direction);
        }
    }
}