using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GuildOfHeavenXP.Dev;

namespace GuildOfHeaven
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //test
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spaceManPos = new Vector2(_graphics.PreferredBackBufferWidth / 2,
_graphics.PreferredBackBufferHeight / 2);
            spaceManSpeed = walkSpeed;
            currFrame = 0;
            startFrame = 0;
            endFrame = 60;
            direction = "down";
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //spaceman = Content.Load<Texture2D>(DevUtils.createTexturePath(walkAnimDirectory, walkLabel, direction, currFrame));
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
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


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(spaceman, spaceManPos, Color.White);
            //(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
            if (!isWalking)
            {
                if (flip)
                {
                    
                    Rectangle rect2 = new Rectangle(0, 0, 64, 64);
                    _spriteBatch.Draw(spaceman, spaceManPos, rect2, Color.White, 0,
                        new Vector2(16,16), 2, SpriteEffects.FlipHorizontally, 0);
                    /*
                     * (Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
{
                     */
                }
                else
                {
                    Rectangle rect = new Rectangle((int)spaceManPos.X-32, (int)spaceManPos.Y-32, 128, 128);
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
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

