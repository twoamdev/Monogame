using Engine.Enum;
using Engine.Utilities;
using Engine.Animation.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Engine.Objects.Base
{
    public class BaseGameObject
    {
        protected BaseFrameManager _baseFrameManager;
        protected Vector3 _position = Vector3.One;
        public float zIndex;
        //aBoundingBox
        

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void OnNotify(Events eventType, object argument = null) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_baseFrameManager.Texture, _baseFrameManager.DestinationRectangle, _baseFrameManager.SourceRectangle, Color.White);
        }
    }
}