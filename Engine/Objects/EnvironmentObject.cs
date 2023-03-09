using System;
using System.Collections.Generic;
using System.Diagnostics;
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
         
            _frameManager.UpdateDrawRectangles(Position);
            zIndex = _isGroundObject ? _frameManager.DrawDepth - 10000f : _frameManager.DrawDepth;
            zIndex = UpdateDrawDepth();

            
            spriteBatch.Draw(_frameManager.Texture, _frameManager.ScreenPosition, _frameManager.SourceRectangle, Color.White, 0, _frameManager.FrameAnchor, new Vector2(1, 1), SpriteEffects.None, 0);
            //drawBbox(spriteBatch);

        }

        private float UpdateDrawDepth()
        {
            var count = _frameManager.BoundingBoxes.Count;
            float bestDepth = -100000f;
            for (int i = 0; i < count; i++)
            {
                var bbox = _frameManager.BoundingBoxes[i];
                var topL = new Vector2(bbox.Min.X + Position.X, bbox.Min.Y + Position.Y);
                var topR = new Vector2(bbox.Max.X + Position.X, bbox.Min.Y + Position.Y);
                var botL = new Vector2(bbox.Min.X + Position.X, bbox.Max.Y + Position.Y);
                var botR = new Vector2(bbox.Max.X + Position.X, bbox.Max.Y + Position.Y);
                var sourceRect = new Rectangle(0, 0, 5, 5);

                topL = _frameManager.Camera.ToCameraSpace(topL.X, topL.Y);
                topR = _frameManager.Camera.ToCameraSpace(topR.X, topR.Y);
                botL = _frameManager.Camera.ToCameraSpace(botL.X, botL.Y);
                botR = _frameManager.Camera.ToCameraSpace(botR.X, botR.Y);

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
                var topL = new Vector2(bbox.Min.X + Position.X, bbox.Min.Y + Position.Y);
                var topR = new Vector2(bbox.Max.X + Position.X, bbox.Min.Y + Position.Y);
                var botL = new Vector2(bbox.Min.X + Position.X, bbox.Max.Y + Position.Y);
                var botR = new Vector2(bbox.Max.X + Position.X, bbox.Max.Y + Position.Y);
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
}

