using Engine.Input.Base;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Engine.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();
            

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }


            var moveResult = IsKeyboardMoveKeyDown(state);
            if (moveResult.Item1)
            {
                commands.Add(new GameplayInputCommand.PlayerMove(moveResult.Item2));
            }

            

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) {
            var commands = new List<GameplayInputCommand>();
            
            if(state.IsButtonDown(Buttons.LeftThumbstickDown) || state.IsButtonDown(Buttons.LeftThumbstickUp) ||
                state.IsButtonDown(Buttons.LeftThumbstickLeft) || state.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                Vector2 direction = new Vector2(state.ThumbSticks.Left.X, -1 * state.ThumbSticks.Left.Y);
                commands.Add(new GameplayInputCommand.PlayerMove(direction));
            }

            return commands;
        }


        private (bool, Vector2) IsKeyboardMoveKeyDown(KeyboardState state)
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