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
            var loadResult = LoadTexture("assets/ui/backgrounds/testSplashBG");
            SpriteSheet sheet = new SpriteSheet(loadResult.LoadedTexture);
            //AddGameObject(new SplashImage(sheet));
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

        public override void Update(GameTime gameTime)
        {

        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}