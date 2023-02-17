using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Enum;

namespace Engine.Utilities
{
	public class SpriteSheet
	{
		private Texture2D _texture;
		private int _spriteWidth;
		private int _spriteHeight;
		private AnimationStates _animationState;

		public SpriteSheet(Texture2D texture, int spriteWidth, int spriteHeight, AnimationStates animationState = AnimationStates.STATIC)
		{
			_texture = texture;
			_spriteWidth = spriteWidth;
			_spriteHeight = spriteHeight;
			_animationState = animationState;
		}

        public int TextureWidth { get { return _texture.Width; } }
        public int TextureHeight { get { return _texture.Height; } }
        public int SpriteWidth { get { return _spriteWidth; } }
        public int SpriteHeight { get { return _spriteHeight; } }
        public Texture2D Texture { get { return _texture; } }
		public AnimationStates AnimationState { get { return _animationState; } }



    }
}

