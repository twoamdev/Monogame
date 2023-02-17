
using System;
using System.Collections.Generic;
using Engine.Objects.Base;
using Engine.Animation;
using Engine.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utilities;

namespace Engine.Objects
{
    public class CharacterSprite : BaseGameObject
    {
        private const float CHARACTER_SPEED = 1.25f;
        
        private CharacterAnimationState _animationState = new CharacterAnimationState();
        private CharacterFrameManager _frameManager;

        public CharacterSprite(List<SpriteSheet> spriteSheets)
        {
            _frameManager = new CharacterFrameManager(Position, spriteSheets);
        }

        public void Move(Vector2 direction)
        {
            _frameManager.CalculateDirection(direction);
            
            Position = new Vector2(Position.X + (CHARACTER_SPEED * direction.X),
                Position.Y + (CHARACTER_SPEED * direction.Y));
            _frameManager.UpdatePosition(Position);
        }

        public void ChangeState(AnimationStates state)
        {
            _frameManager.ChangeState(state);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            
            if (_frameManager.DrawFlipped) {
                spriteBatch.Draw(_frameManager.CurrentSheet.Texture, _frameManager.DestinationRectangle, _frameManager.SourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(_frameManager.CurrentSheet.Texture, _frameManager.DestinationRectangle, _frameManager.SourceRectangle, Color.White);

            }
            
        }

        

    }
}