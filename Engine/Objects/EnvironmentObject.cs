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
            _frameManager.UpdateObjectPosition(Position);
        }



        public override void Render(SpriteBatch spriteBatch)
        {
            //_frameManager.UpdateDrawPosition(Position);
            //_frameManager.UpdateCurrentFrame();

            
            spriteBatch.Draw(
                _frameManager.Texture,
                _frameManager.DestinationRectangle,
                _frameManager.SourceRectangle,
                Color.White);
        }




    }
}

