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

        public EnvironmentObject(PropFrameManager manager, Vector2 startPos)
		{
            _frameManager = manager;
            Position = startPos;
            _frameManager.UpdateDrawRectangles(Position);
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
            zIndex = _frameManager.DrawDepth;

            spriteBatch.Draw(
                _frameManager.Texture,
                _frameManager.DestinationRectangle,
                _frameManager.SourceRectangle,
                Color.White);
        }
    }
}

