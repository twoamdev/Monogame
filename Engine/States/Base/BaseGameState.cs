using System;
using System.Collections.Generic;
using System.Linq;

using Engine.Enum;
using Engine.Objects.Base;
using Engine.Input.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Engine.States.Base
{
    public abstract class BaseGameState
    {
        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();
        private const string missingTexture = "assets/ui/icons/errorTexture";
        private ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;
        protected InputManager InputManager { get; set; }

        protected abstract void SetInputManager();

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
        {
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

        protected Texture2D LoadTexture(string textureName)
        {
            try
            {
                var texture = _contentManager.Load<Texture2D>(textureName);
                return texture;
            }
            catch(Exception)
            {
                var errorTexture = _contentManager.Load<Texture2D>(missingTexture);
                return errorTexture;
            } 
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

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects.OrderBy(a => a.zIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }
    }
}

