
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
    public class CharacterObject : BaseGameObject
    {
        private const float CHARACTER_SPEED = 0.8f;
        private CharacterFrameManager _frameManager;

        public CharacterObject(CharacterFrameManager manager, Vector2 startPosition)
        {
            _frameManager = manager;
            Position = startPosition;
        }

        public Vector2 Move(Vector2 direction)
        {
            _frameManager.CalculateDirection(direction);
            var speed = _frameManager.CurrentAnimationState == AnimationStates.RUNNING ? CHARACTER_SPEED * 2.2f : CHARACTER_SPEED;

            return new Vector2(Position.X + (speed * direction.X), Position.Y + (speed * direction.Y));
        }

        public void ChangeState(AnimationStates state)
        {
            _frameManager.ChangeState(state);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            _frameManager.UpdateDrawRectangles(Position);
            _frameManager.UpdateCurrentFrame();

            if (_frameManager.DrawFlipped) {
                spriteBatch.Draw(_frameManager.Texture, _frameManager.DestinationRectangle, _frameManager.SourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(_frameManager.Texture, _frameManager.DestinationRectangle, _frameManager.SourceRectangle, Color.White);

            }
            
        }

        

    }
}