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
            

            spriteBatch.Draw(
                _frameManager.Texture,
                _frameManager.DestinationRectangle,
                _frameManager.SourceRectangle,
                Color.White);
        }
    }
}

