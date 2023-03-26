using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Objects.Base;
using Engine.Animation.Base;
using Engine.Utilities;
using Engine.Animation;
using Engine.Enum;

namespace Engine.Objects
{
	public class EnvironmentObject : BaseGameObject
	{
        private PropFrameManager _frameManager;
        private bool _isGroundObject;
        private Effect _motionBlurEffect;

        public EnvironmentObject(Vector3 startPos, PropFrameManager manager, Effect motionblur, bool isGround = false)
		{
            _frameManager = manager;
            _motionBlurEffect = motionblur;
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

        public List<BoundingBox> GetColliders()
        {
            var bboxes = _frameManager.BoundingBoxes;
            var updatedBboxes = new List<BoundingBox>();
            foreach(var bbox in bboxes)
            {
                Vector3 min = new Vector3(bbox.Min.X + Position.X, bbox.Min.Y + Position.Y, bbox.Min.Z + 0);
                Vector3 max = new Vector3(bbox.Max.X + Position.X, bbox.Max.Y + Position.Y, bbox.Max.Z + 0);
                var newBbox = new BoundingBox(min, max);
                updatedBboxes.Add(newBbox);
            }
            return updatedBboxes;
        }

        public override void Render(SpriteBatch spriteBatch)
        {

            _frameManager.UpdateSpriteSheetBasedOnCameraAngle();
            _frameManager.UpdateDrawRectangles(Position);
            zIndex = _isGroundObject ? _frameManager.DrawDepth - 10000f : _frameManager.DrawDepth;
            zIndex = UpdateDrawDepth();

            Vector2 currP = _frameManager.ScreenPosition;
            Vector2 prevP = _frameManager.PrevScreenPosition;
            var direction = Vector2.Normalize(Vector2.Subtract(currP, prevP));
            float mag = Vector2.Distance(currP, prevP);
            Vector2 hey = Vector2.Multiply(prevP, mag * 0.5f);

            if (Vector2.Distance(Vector2.Zero, direction) > 0)
            {
               // _motionBlurEffect.Parameters["blurDirection"].SetValue(new Vector2(direction.X, direction.Y));
                //_motionBlurEffect.Parameters["blurSpeed"].SetValue(0.035f * mag);
                
                spriteBatch.Draw(_frameManager.Texture, _frameManager.PrevScreenPosition, _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor, new Vector2(1, 1), SpriteEffects.None, 0);
                spriteBatch.Draw(_frameManager.Texture, hey, _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor, new Vector2(1, 1), SpriteEffects.None, 0);
                spriteBatch.Draw(_frameManager.Texture, _frameManager.ScreenPosition, _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor, new Vector2(1, 1), SpriteEffects.None, 0);


            }
            else
            {
                spriteBatch.Draw(_frameManager.Texture, _frameManager.ScreenPosition, _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor, new Vector2(1, 1), SpriteEffects.None, 0);

            }


            

        }

        private float UpdateDrawDepth()
        {
            var count = _frameManager.BoundingBoxes.Count;
            float bestDepth = -100000f;
            for (int i = 0; i < count; i++)
            {
                var bbox = _frameManager.BoundingBoxes[i];
                var topL = new Vector3(bbox.Min.X + Position.X, bbox.Min.Y + Position.Y, 0);
                var topR = new Vector3(bbox.Max.X + Position.X, bbox.Min.Y + Position.Y, 0);
                var botL = new Vector3(bbox.Min.X + Position.X, bbox.Max.Y + Position.Y, 0);
                var botR = new Vector3(bbox.Max.X + Position.X, bbox.Max.Y + Position.Y, 0);
                var sourceRect = new Rectangle(0, 0, 5, 5);

                topL = _frameManager.Camera.ToCameraSpace(topL.X, topL.Y, 0);
                topR = _frameManager.Camera.ToCameraSpace(topR.X, topR.Y, 0);
                botL = _frameManager.Camera.ToCameraSpace(botL.X, botL.Y, 0);
                botR = _frameManager.Camera.ToCameraSpace(botR.X, botR.Y, 0);

                float newDepth = Math.Max(botR.Y, Math.Max(botL.Y, Math.Max(topL.Y, topR.Y)));
                bestDepth = newDepth > bestDepth ? newDepth : bestDepth;
            }
            return bestDepth;
        }

        private void drawBbox(SpriteBatch spriteBatch)
        {
            var count = _frameManager.BoundingBoxes.Count;
            for (int i = 0; i < count; i++)
            {
                var bbox = _frameManager.BoundingBoxes[i];
                var topL = new Vector3(bbox.Min.X + Position.X, bbox.Min.Y + Position.Y,0);
                var topR = new Vector3(bbox.Max.X + Position.X, bbox.Min.Y + Position.Y,0);
                var botL = new Vector3(bbox.Min.X + Position.X, bbox.Max.Y + Position.Y,0);
                var botR = new Vector3(bbox.Max.X + Position.X, bbox.Max.Y + Position.Y,0);
                var sourceRect = new Rectangle(0, 0, 5, 5);

                topL = _frameManager.Camera.ToCameraSpace(topL.X, topL.Y, 0);
                topR = _frameManager.Camera.ToCameraSpace(topR.X, topR.Y, 0);
                botL = _frameManager.Camera.ToCameraSpace(botL.X, botL.Y, 0);
                botR = _frameManager.Camera.ToCameraSpace(botR.X, botR.Y, 0);

                var color = Color.Red;
                color.A = 100;
                var t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                var bboxAnchor = new Vector2(0, 0);
                t.SetData(new Color[] { color });
                spriteBatch.Draw(t, new Vector2(topL.X, topL.Y),
                                    sourceRect,
                                    Color.Gray, 0,
                                    bboxAnchor,
                                    new Vector2(1, 1),
                                    _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                spriteBatch.Draw(t, new Vector2(topR.X, topR.Y),
                                    sourceRect,
                                    Color.Gray, 0,
                                    bboxAnchor,
                                    new Vector2(1, 1),
                                    _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                spriteBatch.Draw(t, new Vector2(botL.X, botL.Y),
                                    sourceRect,
                                    Color.Gray, 0,
                                    bboxAnchor,
                                    new Vector2(1, 1),
                                    _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                spriteBatch.Draw(t, new Vector2(botR.X, botR.Y),
                                    sourceRect,
                                    Color.Gray, 0,
                                    bboxAnchor,
                                    new Vector2(1, 1),
                                    _frameManager.DrawFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
        }

    }
}

