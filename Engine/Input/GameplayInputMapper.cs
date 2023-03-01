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
                commands.Add(new GameplayInputCommand.PlayerMove(moveResult.direction));
            }
            


            

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) {
            var commands = new List<GameplayInputCommand>();
            
            

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
            if (state.IsButtonDown(Buttons.LeftStick))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.RUNNING));
            }

            //Walk again
            if (state.IsButtonUp(Buttons.LeftStick) && !state.IsButtonDown(Buttons.B))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.WALKING));
            }

            //Roll
            if (state.IsButtonDown(Buttons.B))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationStates.ROLLING));
            }



            return commands;
        }


        private (bool isKeyDown, Vector2 direction) GetMoveDirectionOnKeyDown(KeyboardState state)
        {

            if(state.IsKeyDown(Keys.W))
            {
                return (true, new Vector2(0,-1));
            }
            if (state.IsKeyDown(Keys.S))
            {
                return (true, new Vector2(0, 1));
            }
            if (state.IsKeyDown(Keys.D))
            {
                return (true, new Vector2(1, 0));
            }
            if (state.IsKeyDown(Keys.A))
            {
                return (true, new Vector2(-1, 0));
            }



            return (false, new Vector2(0,0));
            

        }
    }
}