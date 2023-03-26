using Engine.Enum;
using Engine.Utilities;
using Engine.Animation.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Engine.Objects.Base
{
    public class BaseGameObject
    {
        private Vector3 _position = Vector3.One;
        private Vector3 _previousPosition = Vector3.One;
        public float zIndex = 0;

        public Vector3 Position
        {
            get { return _position; }
            set {
                _previousPosition.X = _position.X;
                _previousPosition.Y = _position.Y;
                _previousPosition.Z = _position.Z;
                _position = value;
            }
        }

        public Vector3 PreviousPosition
        {
            get { return _previousPosition; }
        }

        protected void HeightAdjust(float adjustValue)
        {
            _position.Z += adjustValue;
        }

        public virtual void OnNotify(Events eventType, object argument = null) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {

        }
    }
}