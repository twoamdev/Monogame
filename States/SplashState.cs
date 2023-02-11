using Objects;
using Engine.States.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Input;
using Engine.Input.Base;

namespace States
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            // TODO: Add Content Loading
            AddGameObject(new SplashImage(LoadTexture("assets/ui/backgrounds/testSplashBG")));
        }

        public override void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SwitchState(new GameplayState());
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}