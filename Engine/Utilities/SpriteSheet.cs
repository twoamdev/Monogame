using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Enum;
using Engine.Animation.Base;
using Engine.Utilities;

namespace Engine.Utilities
{
	public class SpriteSheet
	{
		private Texture2D _texture;
		private SpriteSheetData _sheetMetaData;
		private AnimationStates _stateId;
		private bool _statePlaysOnChange;


		public SpriteSheet(bool isErrorTexture, Texture2D texture,
            SpriteSheetData metaData, AnimationStates stateId = AnimationStates.STATIC)
		{
			_texture = texture;
			_sheetMetaData = metaData;
            _stateId = stateId;
            if (stateId == AnimationStates.ROLLING)
            {
                _statePlaysOnChange = true;
            }
            else
            {
                _statePlaysOnChange = false;
            }

        }

        public SpriteSheet(bool isErrorTexture, Texture2D texture, AnimationStates stateId = AnimationStates.STATIC)
        {
            _texture = texture;
            _sheetMetaData = new SpriteSheetData(_texture.Width, _texture.Height);
            _stateId = stateId;
            if (stateId == AnimationStates.ROLLING)
            {
                _statePlaysOnChange = true;
            }
            else
            {
                _statePlaysOnChange = false;
            }

        }

        public int TextureWidth { get { return _texture.Width; } }
        public int TextureHeight { get { return _texture.Height; } }
        public Texture2D Texture { get { return _texture; } }

		public AnimationStates AnimationState { get { return _stateId; } }
		public bool PlaysOnChange
		{
			get { return _statePlaysOnChange; }
		}

        public double FrameDuration
        {
            get {
                if(AnimationState == AnimationStates.ROLLING) { return (18.0/ 60.0); }
                
                return (15.0 / 60.0);
            }
        }

        public int FrameCount(Directions direction)
        {
            return _sheetMetaData.FrameCount(direction); 
        }

        public Vector2 FrameSourcePosition(int currentFrame, Directions direction)
        {
            return _sheetMetaData.GetSourcePosition(currentFrame, direction);
        }

        public Vector2 FrameSize(int currentFrame, Directions direction)
        {
            return _sheetMetaData.GetCurrentFrameSize(currentFrame, direction);
        }

        public Vector2 FrameAnchor(int currentFrame, Directions direction)
        {
            return _sheetMetaData.GetAnchorPosition(currentFrame, direction);
        }
    }
}

