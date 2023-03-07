using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Objects.Base;
using Engine.Utilities;
using Engine.Animation;
using Engine.Enum;

namespace Engine.Objects
{
	public class EnvironmentObject : BaseGameObject
	{
        private PropFrameManager _frameManager;
        private bool _isGroundObject;

        public EnvironmentObject(PropFrameManager manager, Vector2 startPos, bool isGround = false)
		{
            _frameManager = manager;
            Position = startPos;
            _frameManager.UpdateDrawRectangles(Position);
            _isGroundObject = isGround;
        }

        public void ShiftDrawAngle(int shiftAmt)
        {
            var currFrameDir = (int)_frameManager.FrameDirection + shiftAmt;
            currFrameDir = MathUtils.Mod(currFrameDir, 32);
            _frameManager.FrameDirection = (Directions)currFrameDir;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
         
            _frameManager.UpdateDrawRectangles(Position);
            zIndex = _isGroundObject ? _frameManager.DrawDepth - 10000f : _frameManager.DrawDepth;

            
            spriteBatch.Draw(_frameManager.Texture, _frameManager.ScreenPosition, _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor, new Vector2(1, 1), SpriteEffects.None, 0);
            drawBbox(spriteBatch);

        }

        private void drawBbox(SpriteBatch spriteBatch)
        {
            float size = 70f;
            var topL = new Vector2(Position.X + (size / -2f), Position.Y + (size / -2f));
            var topR = new Vector2(Position.X + (size / 2f), Position.Y + (size / -2f));
            var botL = new Vector2(Position.X + (size / -2f), Position.Y + (size / 2f));
            var botR = new Vector2(Position.X + (size / 2f), Position.Y + (size / 2f));
            var sourceRect = new Rectangle(0, 0, 5, 5);

            topL = _frameManager.Camera.ToCameraSpace(topL.X, topL.Y);
            topR = _frameManager.Camera.ToCameraSpace(topR.X, topR.Y);
            botL = _frameManager.Camera.ToCameraSpace(botL.X, botL.Y);
            botR = _frameManager.Camera.ToCameraSpace(botR.X, botR.Y);

            var color = Color.Red;
            color.A = 100;
            var t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            var bboxAnchor = new Vector2(0, 0);
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

