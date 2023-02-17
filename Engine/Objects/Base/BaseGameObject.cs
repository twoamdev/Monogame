using Engine.Enum;
using Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Engine.Objects.Base
{
    public class BaseGameObject
    {
        protected SpriteSheet _sheet;
        protected Vector2 _position = Vector2.One;

        public int zIndex;

        //public int Width { get { return _sheet.TextureWidth; } }
        //public int Height { get { return _sheet.TextureHeight; } }
        //public int SpriteWidth { get { return _sheet.SpriteWidth; } }
        //public int SpriteHeight { get { return _sheet.SpriteHeight; } }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public SpriteSheet Sheet
        {
            get { return _sheet; }
            set { _sheet = value; }
        }



        public virtual void OnNotify(Events eventType, object argument = null) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            var sourceRect = new Rectangle(0, (int)Enum.Directions.DIR_0_DOWN, _sheet.SpriteWidth, _sheet.SpriteHeight);
            var destRect = new Rectangle((int)Position.X, (int)Position.Y, _sheet.SpriteWidth, _sheet.SpriteHeight);

            spriteBatch.Draw(_sheet.Texture, destRect, sourceRect, Color.White);
        }
    }
}