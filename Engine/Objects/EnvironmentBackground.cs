using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Objects.Base;
using Engine.Utilities;

namespace Engine.Objects
{
	public class EnvironmentBackground : BaseGameObject
	{
		private float SCROLL_SPEED = 0.0f;
		public EnvironmentBackground(SpriteSheet sheet)
		{
            /*
			_sheet = sheet;
			_position = new Vector2(0, 0);
            */
		}

        
        public override void Render(SpriteBatch spriteBatch)
        {
            /*
            var viewport = spriteBatch.GraphicsDevice.Viewport;

            
            var sourceRectangle = new Rectangle(0, 0, Sheet.TextureWidth, Sheet.TextureHeight);

            // tile the textures to fill the screen. Add an extra row of tiles above the viewport so they can scroll into view and not leave a gap
            for (int nbVertical = -1; nbVertical < viewport.Height / Sheet.TextureHeight + 1; nbVertical++)
            {
                var y = (int)_position.Y + nbVertical * Sheet.TextureHeight;
                for (int nbHorizontal = 0; nbHorizontal < viewport.Width / Sheet.TextureWidth + 1; nbHorizontal++)
                {
                    var x = (int)_position.X + nbHorizontal * Sheet.TextureWidth;
                    var destinationRectangle = new Rectangle(x, y, Sheet.TextureWidth, Sheet.TextureHeight);
                    spriteBatch.Draw(Sheet.Texture, destinationRectangle, sourceRectangle, Color.White);
                }
            }

            _position.Y = (int)(_position.Y + SCROLL_SPEED) % Sheet.TextureHeight;
            */
        }
        
    }
}

