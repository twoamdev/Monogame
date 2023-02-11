
using Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects
{
    public class CharacterSprite : BaseGameObject
    {
        private const float CHARACTER_SPEED = 1.0f;

        public CharacterSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Move(Vector2 direction)
        {
            Position = new Vector2(Position.X + (CHARACTER_SPEED * direction.X),
                Position.Y + (CHARACTER_SPEED * direction.Y));
        }

        
    }
}