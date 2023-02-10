using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GuildOfHeavenXP.Dev;

using Engine.Enum;
using Engine.Objects;
using Engine.States;
using Engine.States.Base;
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
        private const int DESIGNED_RESOLUTION_WIDTH = 1920;
        private const int DESIGNED_RESOLUTION_HEIGHT = 1080;
        private const float DESIGNED_RESOLUTION_ASPECT_RATIO =
            DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;

 

        Texture2D spaceman;
        Vector2 spaceManPos;
        float spaceManSpeed;
        readonly string walkAnimDirectory = "assets/characters/genericTestMale/male_animation_walk_001/";
        readonly string runAnimDirectory = "assets/characters/genericTestMale/male_animation_run_001/";
        readonly string carAnimDirector = "assets/vehicles/genericCarTest/car_animation_001/";
        readonly string walkLabel = "male_animation_walk_001_";
        readonly string runLabel = "male_animation_run_001_";
        readonly string carLabel = "car_animation_go_001_";
        string direction;
        string texturePath;
        readonly float runSpeed = 150f;
        readonly float walkSpeed = 50f;
        readonly float carSpeed = 150f;
        bool isWalking = true;
        int currFrame;
        int startFrame;
        int endFrame;
        bool flip = false;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            //test
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spaceManPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            spaceManSpeed = walkSpeed;
            currFrame = 0;
            startFrame = 0;
            endFrame = 60;
            direction = "down";


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
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;
            Rectangle scaleRectangle;

            if(actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
            {
                var currentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                var barHeight = (Window.ClientBounds.Height - currentHeight) / 2;
                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, currentHeight);
            }
            else
            {
                var currentWidth = (int)(Window.ClientBounds.Height + DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
                var barWidth = (Window.ClientBounds.Width - currentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, currentWidth, Window.ClientBounds.Height);
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
            _currentGameState.LoadContent(Content);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

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


            //Render the scaled content
            graphics.GraphicsDevice.SetRenderTarget(null);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);

            /*
            if (!isWalking)
            {
                if (flip)
                {

                    Rectangle rect2 = new Rectangle(0, 0, 64, 64);
                    _spriteBatch.Draw(spaceman, spaceManPos, rect2, Color.White, 0,
                        new Vector2(16, 16), 2, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    Rectangle rect = new Rectangle((int)spaceManPos.X - 32, (int)spaceManPos.Y - 32, 128, 128);
                    Rectangle rect2 = new Rectangle(0, 0, 64, 64);
                    _spriteBatch.Draw(spaceman, rect, rect2, Color.White);
                }
            }
            else
            {
                if (flip)
                {
                    _spriteBatch.Draw(spaceman, spaceManPos, null, Color.White, 0,
                        new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    _spriteBatch.Draw(spaceman, spaceManPos, Color.White);
                }
            }
            */

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}



/*
 * // TODO: Add your update logic here
            
            var kstate = Keyboard.GetState();
            var keyCount = kstate.GetPressedKeyCount();
            double diagSpeedMult = 1.4142;
            if (kstate.IsKeyDown(Keys.Space))
            {
                isWalking = false;
                endFrame = 5;// 37;
            }
            else
            {
                isWalking = true;
                endFrame = 60;
            }

            if ( keyCount > 0)
            {
                currFrame = DevUtils.updateFrame(currFrame, startFrame, endFrame);
                spaceManSpeed = isWalking ? walkSpeed : carSpeed; //runSpeed;
            }

            //UP
            if (kstate.IsKeyDown(Keys.W) && !kstate.IsKeyDown(Keys.D) && !kstate.IsKeyDown(Keys.S) && !kstate.IsKeyDown(Keys.A))
            {

                spaceManPos.Y -= spaceManSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "up";
                flip = false;


            }

            //UP RIGHT
            if (kstate.IsKeyDown(Keys.W) && kstate.IsKeyDown(Keys.D))
            {
                spaceManPos.Y -= (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spaceManPos.X += (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "up_right_tween_mid";
                flip = false;


            }

            //RIGHT
            if (kstate.IsKeyDown(Keys.D) && !kstate.IsKeyDown(Keys.S) && !kstate.IsKeyDown(Keys.W) && !kstate.IsKeyDown(Keys.A))
            {
                spaceManPos.X += spaceManSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "right";
                flip = false;

            }

            //DOWN RIGHT
            if (kstate.IsKeyDown(Keys.S) && kstate.IsKeyDown(Keys.D))
            {
                spaceManPos.Y += (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spaceManPos.X += (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "down_right_tween_mid";
                flip = false;
            }

            

            //DOWN
            if (kstate.IsKeyDown(Keys.S) && !kstate.IsKeyDown(Keys.D) && !kstate.IsKeyDown(Keys.W) && !kstate.IsKeyDown(Keys.A))
            {
                spaceManPos.Y += spaceManSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "down";
                flip = false;
            }

            //DOWN LEFT
            if (kstate.IsKeyDown(Keys.S) && kstate.IsKeyDown(Keys.A))
            {
                spaceManPos.Y += (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spaceManPos.X -= (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "down_right_tween_mid";
                flip = true;
            }



            //LEFT
            if (kstate.IsKeyDown(Keys.A) && !kstate.IsKeyDown(Keys.D) && !kstate.IsKeyDown(Keys.W) && !kstate.IsKeyDown(Keys.S))
            {
                
                spaceManPos.X -= spaceManSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "right";
                flip = true;

            }

            //UP LEFT
            if (kstate.IsKeyDown(Keys.A) && kstate.IsKeyDown(Keys.W))
            {

                spaceManPos.X -= (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                spaceManPos.Y -= (spaceManSpeed / ((float)diagSpeedMult)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction = "up_right_tween_mid";
                flip = true;

            }



            if (isWalking) {
                spaceman = Content.Load<Texture2D>(DevUtils.createTexturePath(walkAnimDirectory, walkLabel, direction, currFrame));
            }
            else
            {
                //spaceman = Content.Load<Texture2D>(DevUtils.createTexturePath(runAnimDirectory, runLabel, direction, currFrame));
                spaceman = Content.Load<Texture2D>(DevUtils.createTexturePath(carAnimDirector, carLabel, direction, currFrame));
            }
 */