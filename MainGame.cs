using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GuildOfHeavenXP.Dev;

using Engine.Enum;
using Engine.Objects.Base;
using Engine.States;
using Engine.States.Base;
using States;
using Objects;
using System;

namespace GuildOfHeaven
{
    public class MainGame : Game
    {
        private BaseGameState _currentGameState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;
        private const int DESIGNED_RESOLUTION_WIDTH = 1920/2;
        private const int DESIGNED_RESOLUTION_HEIGHT = 1080/2;

        private const float DESIGNED_RESOLUTION_ASPECT_RATIO = DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;


        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //test
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = (int) (DESIGNED_RESOLUTION_WIDTH * 1.25f);// 1024;
            graphics.PreferredBackBufferHeight = (int) (DESIGNED_RESOLUTION_HEIGHT * 1.25f);// 768;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(graphics.GraphicsDevice,
                DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT,
                false,
                SurfaceFormat.Color, DepthFormat.None, 0,
                RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();
            

            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            var variance = 0.0;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SwitchGameState(new SplashState());
        }


        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            _currentGameState?.UnloadContent(Content);
            _currentGameState = gameState;
            _currentGameState.Initialize(Content,DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT);
            _currentGameState.LoadContent();
            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
        }

        private void _currentGameState_OnEventNotification(object sender, Events e)
        {
            switch (e)
            {
                case Events.GAME_QUIT:
                    Exit();
                    break;
            }
        }

        protected override void UnloadContent()
        {
            _currentGameState?.UnloadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.HandleInput();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            // Render to the Render Target
            GraphicsDevice.SetRenderTarget(_renderTarget);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentGameState.Render(_spriteBatch);

            _spriteBatch.End();

            
            // Now render the scaled content
            graphics.GraphicsDevice.SetRenderTarget(null);

            
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
