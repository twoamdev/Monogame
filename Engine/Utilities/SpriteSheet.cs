using System;
using System.Collections.Generic;
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
        private List<Texture2D> _sequenceTextures;
        private bool _isSequence = false;
		private SpriteSheetData _sheetMetaData;
		private AnimationStates _stateId;
		private bool _statePlaysOnChange;
        private bool _stateAnimationLoops;

        public SpriteSheet(bool hasErrorTexture, List<Texture2D> textures,
            SpriteSheetData metaData, AnimationStates stateId = AnimationStates.STATIC)
        {
            _texture = textures[0];
            _isSequence = true;
            _sequenceTextures = textures;
            _sheetMetaData = metaData;
            _stateId = stateId;
            InitializeVariables();

        }

        public SpriteSheet(bool isErrorTexture, Texture2D texture,
            SpriteSheetData metaData, AnimationStates stateId = AnimationStates.STATIC)
		{
			_texture = texture;
			_sheetMetaData = metaData;
            _stateId = stateId;
            InitializeVariables();

        }

        public SpriteSheet(bool isErrorTexture, Texture2D texture, AnimationStates stateId = AnimationStates.STATIC)
        {
            _texture = texture;
            _sheetMetaData = new SpriteSheetData(_texture.Width, _texture.Height);
            _stateId = stateId;
            InitializeVariables();

        }

        private void InitializeVariables()
        {
            _statePlaysOnChange = _stateId == AnimationStates.ROLLING ? true : false;
            _stateAnimationLoops = _stateId == AnimationStates.IDLE ? true : false;

        }

        public int TextureWidth { get { return _texture.Width; } }
        public int TextureHeight { get { return _texture.Height; } }
        public Texture2D Texture { get { return _texture; } }

        public bool IsSequence { get { return _isSequence; } }

        public AnimationStates AnimationState { get { return _stateId; } }

        public void UpdateSequenceTexture(Directions direction)
        {
            int frame = (int) direction;
            _texture = _sequenceTextures[frame];
        }

        public bool PlaysOnChange
		{
			get { return _statePlaysOnChange; }
		}

        public bool StateLoopsTheAnimation
        {
            get { return _stateAnimationLoops; }
        }

        public double FrameDuration
        {
            get {
                if(AnimationState == AnimationStates.ROLLING) { return (18.0/ 60.0); }
                if (AnimationState == AnimationStates.WALKING) { return (20.0 / 60.0); }
                if (AnimationState == AnimationStates.RUNNING) { return (25.0 / 60.0); }
                if (AnimationState == AnimationStates.IDLE) { return (10.0 / 60.0); }
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

        public List<BoundingBox> BoundingBoxes(int currentFrame, Directions direction)
        {
            return _sheetMetaData.GetBoundingBoxes(currentFrame, direction);
        }
    }
}

