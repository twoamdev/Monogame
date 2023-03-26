using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Engine.Enum;
using Engine.Objects.Base;
using Engine.Input.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Engine.States.Base
{
    public abstract class BaseGameState
    {
        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();
        private const string missingTexture = "assets/ui/icons/errorTexture";
        private ContentManager _contentManager;
        private GraphicsDeviceManager _graphicsManager;
        protected int _viewportHeight;
        protected int _viewportWidth;
        protected InputManager InputManager { get; set; }

        protected abstract void SetInputManager();

        public void Initialize(GraphicsDeviceManager graphicsManager, ContentManager contentManager, int viewportWidth, int viewportHeight)
        {
            _graphicsManager = graphicsManager;
            _contentManager = contentManager;
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;

            SetInputManager();
        }

        public abstract void LoadContent();

        public void UnloadContent(ContentManager contentManager)
        {
            _contentManager.Unload();
        }

        public abstract void HandleInput();

        public Effect LoadMotionBlurEffect()
        {
            var hlslShaderFile = "/Users/bennelson/Documents/Git_Repositories/GuildOfHeaven/GuildOfHeaven/Content/shaders/motionblur.mgfx";
            byte[] bytecode = File.ReadAllBytes(hlslShaderFile);
            return new Effect(_graphicsManager.GraphicsDevice, bytecode);
        }



        protected (bool IsErrorTexture, Texture2D LoadedTexture) LoadTexture(string textureName)
        {
            try
            {
                var texture = _contentManager.Load<Texture2D>(textureName);
                return (false, texture);
            }
            catch(Exception)
            {
                var errorTexture = _contentManager.Load<Texture2D>(missingTexture);
                return (true, errorTexture);
            } 
        }

        protected (bool HasErrorTexture, List<Texture2D> Textures) LoadTextureSequence(string textureNamePrefix, int frameStart, int frameEnd)
        {
            var textures = new List<Texture2D>();
            bool didError = false;
            for(int i = frameStart; i < frameEnd +1; i++)
            {
                string textureName = textureNamePrefix + "." + i.ToString();
                try
                {
                    var texture = _contentManager.Load<Texture2D>(textureName);
                    textures.Add(texture);
                }
                catch (Exception)
                {
                    var errorTexture = _contentManager.Load<Texture2D>(missingTexture);
                    textures.Add(errorTexture);
                    didError = true;
                }

            }
            return (didError, textures);

        }
        

       

        public event EventHandler<BaseGameState> OnStateSwitched;

        public event EventHandler<Events> OnEventNotification;

        protected void NotifyEvent(Events eventType, object argument = null)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnNotify(eventType);
            }
        }

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected List<BaseGameObject> GameObjects
        {
            get { return _gameObjects; }
        }

        public abstract void Update(GameTime gameTime);

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects.OrderBy(a => a.zIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }
    }
}

