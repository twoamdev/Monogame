
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const float CHARACTER_SPEED = 1.0f;
        private CharacterFrameManager _frameManager;
        private int _shiftDrawAccumulator = 0;

        public CharacterObject(CharacterFrameManager manager, Vector2 startPosition)
        {
            _frameManager = manager;
            Position = startPosition;
        }

        public Vector2 Move(Vector2 direction)
        {
            var camDir = _frameManager.Camera.CameraDirection;
            int camMult = (int) camDir;
            
            var degreeRotation = (float)((360.0 / (float)32.0) * (float)camMult);
            var rotation = MathUtils.ToRadians(degreeRotation);

            var cosTheta = Math.Cos(rotation);
            var sinTheta = Math.Sin(rotation);
            double x = ((double)direction.X * cosTheta) - ((double)direction.Y * sinTheta);
            double y = ((double)direction.Y * cosTheta) + ((double)direction.X * sinTheta);
            direction = new Vector2((float)x, (float)y);

            var compareDirection = new Vector2(0, 1);
            cosTheta = Math.Cos(rotation);
            sinTheta = Math.Sin(rotation);
            x = ((double)compareDirection.X * cosTheta) - ((double)compareDirection.Y * sinTheta);
            y = ((double)compareDirection.Y * cosTheta) + ((double)compareDirection.X * sinTheta);
            compareDirection = new Vector2((float)x, (float)y);

            _frameManager.UpdateDrawFrameDirection(direction, compareDirection, (int) camDir);
            var speed = _frameManager.CurrentAnimationState == AnimationStates.RUNNING ? CHARACTER_SPEED * 1.75f : CHARACTER_SPEED;
            _shiftDrawAccumulator = 0;
            return new Vector2(Position.X + (speed * direction.X), Position.Y + (speed * direction.Y));  
        }

        public void ChangeState(AnimationStates state)
        {
            _frameManager.ChangeState(state);
        }

        public bool CompareColliders(Vector2 intendedPosition, List<BoundingBox> bboxes)
        {
            //TEMP, later change to imported bounding boxes
            float size = 15f;
            var topL = new Vector2(intendedPosition.X + (size / -2f), intendedPosition.Y + (size / -2f));
            var topR = new Vector2(intendedPosition.X + (size / 2f), intendedPosition.Y + (size / -2f));
            var botL = new Vector2(intendedPosition.X + (size / -2f), intendedPosition.Y + (size / 2f));
            var botR = new Vector2(intendedPosition.X + (size / 2f), intendedPosition.Y + (size / 2f));
            Vector3 min = new Vector3(topL.X, topL.Y, -1);
            Vector3 max = new Vector3(botR.X, botR.Y, 1);
            BoundingBox charBbox = new BoundingBox(min, max);

            foreach (var bbox in bboxes)
            {  
                if (charBbox.Intersects(bbox))
                {
                    return true;
                }
            }
            return false;
        }

        public void ShiftDrawDirection(int shiftAmount)
        {
            int NUM_OF_PLAYER_DIRECTIONS = 8;
            int NUM_OF_CAMERA_DIRECTIONS = 32;
            int padding = NUM_OF_CAMERA_DIRECTIONS / NUM_OF_PLAYER_DIRECTIONS;
            _shiftDrawAccumulator += shiftAmount;
            if (Math.Abs(_shiftDrawAccumulator) >= padding)
            {
                int currDir = (int)_frameManager.FrameDirection;
                currDir += shiftAmount;
                currDir = MathUtils.Mod(currDir, NUM_OF_PLAYER_DIRECTIONS);
                _frameManager.FrameDirection = (Directions)currDir;
                _shiftDrawAccumulator = 0;
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
           
            _frameManager.UpdateDrawRectangles(Position, true);
            zIndex = _frameManager.DrawDepth;
            _frameManager.UpdateCurrentFrame();

            
            drawBbox(spriteBatch);
            
            spriteBatch.Draw(_frameManager.Texture, _frameManager.ScreenPosition,
                _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor,
                new Vector2(1, 1),
                SpriteEffects.None, 0);

        }

        private void drawBbox(SpriteBatch spriteBatch)
        {
            float size = 15f;
            var topL = new Vector2(Position.X + (size/-2f), Position.Y + (size / -2f));
            var topR = new Vector2(Position.X + (size / 2f), Position.Y + (size / -2f));
            var botL = new Vector2(Position.X + (size / -2f), Position.Y + (size / 2f));
            var botR = new Vector2(Position.X + (size / 2f), Position.Y + (size / 2f));
            var sourceRect = new Rectangle(0, 0, 4,4);

            topL = _frameManager.Camera.ToCameraSpace(topL.X, topL.Y);
            topR = _frameManager.Camera.ToCameraSpace(topR.X, topR.Y);
            botL = _frameManager.Camera.ToCameraSpace(botL.X, botL.Y);
            botR = _frameManager.Camera.ToCameraSpace(botR.X, botR.Y);

            var color = Color.Red;
            color.A = 50;
            var t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            var bboxAnchor = new Vector2(0,0);
            t.SetData(new Color[] { color });
            spriteBatch.Draw(t, topL,
                                sourceRect,
                                Color.Gray, 0,
                                bboxAnchor,
                                new Vector2(1, 1),
                                _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(t, topR,
                                sourceRect,
                                Color.Gray, 0,
                                bboxAnchor,
                                new Vector2(1, 1),
                                _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(t, botL,
                                sourceRect,
                                Color.Gray, 0,
                                bboxAnchor,
                                new Vector2(1, 1),
                                _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(t, botR,
                                sourceRect,
                                Color.Gray, 0,
                                bboxAnchor,
                                new Vector2(1, 1),
                                _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        

    }
}