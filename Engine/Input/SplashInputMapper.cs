using Engine.Input.Base;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Engine.Input
{
    public class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            return commands;
        }


        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) {

            var commands = new List<SplashInputCommand>();
            
            if (state.IsButtonDown(Buttons.A))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            return commands;
        }
        
         

    }
}