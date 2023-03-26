using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utilities;
using Engine.Enum;
using Engine.Objects;

namespace Engine.Animation.Base
{
	public class BaseFrameManager
	{
        protected List<SpriteSheet> _spriteSheets;
        private List<SpriteSheet> _spriteSheetsNormalCam;
        private List<SpriteSheet> _spriteSheetsTopCam;
        private List<SpriteSheet> _spriteSheetsHighCam;
        private List<SpriteSheet> _spriteSheetsLowCam;
        

        protected ViewportCamera _camera;
        protected float _drawDepth;
		protected Rectangle _destRectangle;
		protected Rectangle _sourceRectangle;
        protected bool _drawFlipped = false;
        protected Directions _currentDirection = Directions.DIR_0;
        private double _currentFrame = 0.0;
        private Vector2 _screenPosition = Vector2.One;
        private Vector2 _previousScreenPosition = Vector2.One;

        public BaseFrameManager(List<SpriteSheet> sheets, ViewportCamera camera, List<SpriteSheet> topSheets, List<SpriteSheet> highSheets, List<SpriteSheet> lowSheets)
        {
            _camera = camera;
            _spriteSheetsTopCam = topSheets;
            _spriteSheetsHighCam = highSheets;
            _spriteSheetsNormalCam = sheets;
            _spriteSheetsLowCam = lowSheets;
        }

        public ViewportCamera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        public double CurrentFrame
        {
            get { return _currentFrame; }
            set { _currentFrame = value; }
        }

        public Rectangle DestinationRectangle {
            get { return _destRectangle; }
            set { _destRectangle = value; }
        }

        public Rectangle SourceRectangle {
            get { return _sourceRectangle; }
            set { _sourceRectangle = value; }
        }

        public Vector2 ScreenPosition
        {
            get { return _screenPosition; }
            set {
                _previousScreenPosition = new Vector2(_screenPosition.X, _screenPosition.Y);
                _screenPosition = value; }
        }

        public Vector2 PrevScreenPosition
        {
            get { return _previousScreenPosition; }
        }

        public bool DrawFlipped
        {
            get { return _drawFlipped; }
            set { _drawFlipped = value; }
        }

        public Directions FrameDirection
        {
            get { return _currentDirection; }
            set { _currentDirection = value; }
        }

        public float DrawDepth
        {
            get { return _drawDepth; }
            set { _drawDepth = value; }
        }

        public List<SpriteSheet> SpriteSheets
        {
            set { _spriteSheets = value; }
        }

        public void UpdateSpriteSheetBasedOnCameraAngle(AnimationState currentAnimationState = AnimationState.STATIC)
        {
            float switchValue = Camera.CameraTopAngle;
            switch (switchValue)
            {
                case -45f:
                    _spriteSheets = _spriteSheetsNormalCam;
                    SelectSpriteSheetBasedOnAnimationState(currentAnimationState);
                    break;
                case -10f:
                    _spriteSheets = _spriteSheetsTopCam;
                    SelectSpriteSheetBasedOnAnimationState(currentAnimationState);
                    break;
                case -27.5f:
                    _spriteSheets = _spriteSheetsHighCam;
                    SelectSpriteSheetBasedOnAnimationState(currentAnimationState);
                    break;
                case -62.5f:
                    _spriteSheets = _spriteSheetsLowCam;
                    SelectSpriteSheetBasedOnAnimationState(currentAnimationState);
                    break;
                default:
                    break;
            }
        }

        public void UpdateDrawRectangles(Vector3 position, bool trackWithCamera = false)
        {

            float height = position.Z;
            SourceRectangle = new Rectangle((int)FrameSourcePos.X, (int)FrameSourcePos.Y, (int)FrameSize.X, (int)FrameSize.Y);
            position = Camera.ToCameraSpace(position.X, position.Y, position.Z);
            DrawDepth = position.Y - FrameAnchor.Y;
            var screenYPosition = Camera.ScreenYPositionFromZComponent(position.Y, height);
            ScreenPosition = new Vector2(position.X,screenYPosition);
            DestinationRectangle = new Rectangle((int)(position.X - FrameAnchor.X), (int)(position.Y - FrameAnchor.Y),
                (int)FrameSize.X, (int)FrameSize.Y);
       
        }


        public bool SelectSpriteSheetBasedOnAnimationState(AnimationState state)
        {
            for (int i = 0; i < _spriteSheets.Count; i++)
            {
                if (_spriteSheets[i].AnimationState == state)
                {
                    if(i != 0)
                    {
                        var sheet = _spriteSheets[i];
                        _spriteSheets.RemoveAt(i);
                        _spriteSheets.Insert(0, sheet);
                    }
                    return true;
                }
            }

            return false;
        }

        private SpriteSheet CurrentSpriteSheet
        {
            get { return _spriteSheets[0]; }
        }


        protected AnimationState SpriteSheetAnimationState
        {
            get { return CurrentSpriteSheet.AnimationState; }
        }

        public Texture2D Texture
        {
            get {
                if (CurrentSpriteSheet.IsSequence)
                {
                    //update
                    CurrentSpriteSheet.UpdateSequenceTexture(_currentDirection);
                }
                return CurrentSpriteSheet.Texture; }
        }

        public int FrameCount
        {
            get { return CurrentSpriteSheet.FrameCount(FrameDirection); }
        }

        public Vector2 FrameSourcePos
        {
            get { return CurrentSpriteSheet.FrameSourcePosition((int) CurrentFrame, FrameDirection); }
        }

        public Vector2 FrameSize
        {
            get { return CurrentSpriteSheet.FrameSize((int)CurrentFrame, FrameDirection); }
        }

        public Vector2 FrameAnchor
        {
            get { return CurrentSpriteSheet.FrameAnchor((int)CurrentFrame, FrameDirection); }
        }

        public List<BoundingBox> BoundingBoxes
        {
            get { return CurrentSpriteSheet.BoundingBoxes((int)CurrentFrame, FrameDirection); }
        }
    }
}

