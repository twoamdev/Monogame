
using System;
using Engine.Objects.Base;
using Engine.Animation;
using Engine.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects
{
    public class CharacterSprite : BaseGameObject
    {
        private const float CHARACTER_SPEED = 1.25f;
        private int _spriteWidth;
        private int _spriteHeight;
        private CharacterAnimationState _animationState = new CharacterAnimationState();
        private CharacterFrameManager _frameManager;

        public CharacterSprite(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            _texture = texture;
            _spriteWidth = spriteWidth;
            _spriteHeight = spriteHeight;
            _frameManager = new CharacterFrameManager(_spriteWidth, _spriteHeight, Position);

        }

        public void Move(Vector2 direction)
        {
            _frameManager.CalculateDirection(direction);
            
            Position = new Vector2(Position.X + (CHARACTER_SPEED * direction.X),
                Position.Y + (CHARACTER_SPEED * direction.Y));
            _frameManager.UpdatePosition(Position);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            
            if (_frameManager.DrawFlipped) {
                spriteBatch.Draw(_texture, _frameManager.DestinationRectangle, _frameManager.SourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(_texture, _frameManager.DestinationRectangle, _frameManager.SourceRectangle, Color.White);

            }
            
        }

        

    }
}