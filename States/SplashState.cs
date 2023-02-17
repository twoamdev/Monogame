using Objects;
using Engine.States.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Input;
using Engine.Input.Base;
using Engine.Enum;
using Engine.Objects;
using Engine.Utilities;

namespace States
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            // TODO: Add Content Loading
            SpriteSheet sheet = new SpriteSheet(LoadTexture("assets/ui/backgrounds/testSplashBG"), 120, 60);
            AddGameObject(new SplashImage(sheet));
        }

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
            }
            );

        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}