using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Enum;
using Engine.Animation.Base;
using Engine.Animation;
using Engine.Utilities;

namespace Engine.Utilities
{
	public class SpriteSheet
	{
		private Texture2D _texture;
        private List<Texture2D> _sequenceTextures;
		private SpriteSheetData _sheetMetaData;
        private AnimationState _animationState;
        private bool _isSequence = false;
      

        public SpriteSheet(
            List<Texture2D> textures,
            SpriteSheetData metaData,
            AnimationState animationState = AnimationState.STATIC)
        {
            _texture = textures[0];
            _isSequence = true;
            _sequenceTextures = textures;
            _sheetMetaData = metaData;
            _animationState = animationState;
        }

        public SpriteSheet(
            Texture2D texture,
            SpriteSheetData metaData,
            AnimationState animationState = AnimationState.STATIC)
		{
			_texture = texture;
			_sheetMetaData = metaData;
            _animationState = animationState;
        }

        public SpriteSheet(Texture2D texture, AnimationState animationState = AnimationState.STATIC)
        {
            _texture = texture;
            _sheetMetaData = new SpriteSheetData(_texture.Width, _texture.Height);
            _animationState = animationState;
        }

        

        public int TextureWidth { get { return _texture.Width; } }
        public int TextureHeight { get { return _texture.Height; } }
        public Texture2D Texture { get { return _texture; } }

        public bool IsSequence { get { return _isSequence; } }

        public AnimationState AnimationState { get { return _animationState; } }

        public void UpdateSequenceTexture(Directions direction)
        {
            int frame = (int) direction;
            _texture = _sequenceTextures[frame];
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

        public List<BoundingBox> BoundingBoxes(int currentFrame, Directions direction)
        {
            return _sheetMetaData.GetBoundingBoxes(currentFrame, direction);
        }
    }
}

