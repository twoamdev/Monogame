
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
        private const float CHARACTER_SPEED = 1.5f;
        private CharacterFrameManager _frameManager;

        public CharacterObject(CharacterFrameManager manager, Vector2 startPosition)
        {
            _frameManager = manager;
            Position = startPosition;
        }

        public void Move(Vector2 direction)
        {
            var camDir = _frameManager.Camera.CameraDirection;
            int camMult = (int) camDir;
            
            var degreeRotation = (float)((360.0 / (float)32.0) * (float)camMult);
            var rotation = MathUtils.ToRadians(degreeRotation);

            //check input direction before cam rotation to see if sprite should draw flipped.
            _frameManager.DrawFlipped = direction.X < 0 ? true : false;
            
            var cosTheta = Math.Cos(rotation);
            var sinTheta = Math.Sin(rotation);
            double x = ((double)direction.X * cosTheta) - ((double)direction.Y * sinTheta);
            double y = ((double)direction.Y * cosTheta) + ((double)direction.X * sinTheta);
            direction = new Vector2((float)x, (float)y);

            var compareDirection = new Vector2(0, 1);
            x = ((double)compareDirection.X * cosTheta) - ((double)compareDirection.Y * sinTheta);
            y = ((double)compareDirection.Y * cosTheta) + ((double)compareDirection.X * sinTheta);
            compareDirection = new Vector2((float)x, (float)y);

            _frameManager.CalculateDirection(direction, compareDirection);
            var speed = _frameManager.CurrentAnimationState == AnimationStates.RUNNING ? CHARACTER_SPEED + 1.2f : CHARACTER_SPEED;
            Position = new Vector2(Position.X + (speed * direction.X), Position.Y + (speed * direction.Y));
        }

        public void ChangeState(AnimationStates state)
        {
            _frameManager.ChangeState(state);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
           
            _frameManager.UpdateDrawRectangles(Position, true);
            zIndex = _frameManager.DrawDepth;
            _frameManager.UpdateCurrentFrame();


            spriteBatch.Draw(_frameManager.Texture, _frameManager.ScreenPosition,
                _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor,
                new Vector2(1, 1), _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

        }

        

    }
}