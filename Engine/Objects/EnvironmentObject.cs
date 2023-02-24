using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Objects.Base;
using Engine.Utilities;
using Engine.Animation;

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

        public void CameraMove(Vector2 direction, Vector2 pivotPosition)
        {
            _frameManager.CalculateDirection(direction);
            Position = _frameManager.RotatePosition(pivotPosition, Position);
            //_frameManager.UpdateDrawRectangles(Position);

        }

        public void OffsetPosition(Vector2 offsetPos)
        {
            Position = Vector2.Add(Position, offsetPos);
            
        }


        public override void Render(SpriteBatch spriteBatch)
        {
            //_frameManager.UpdateDrawPosition(Position);
            //_frameManager.UpdateCurrentFrame();
            _frameManager.UpdateDrawRectangles(Position);


            spriteBatch.Draw(
                _frameManager.Texture,
                _frameManager.DestinationRectangle,
                _frameManager.SourceRectangle,
                Color.White);
        }




    }
}

