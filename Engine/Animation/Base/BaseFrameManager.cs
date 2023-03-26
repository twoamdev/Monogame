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
        public List<SpriteSheet> _spriteSheetsNormalCam;
        public List<SpriteSheet> _spriteSheetsTopCam;
        public List<SpriteSheet> _spriteSheetsHighCam;
        public List<SpriteSheet> _spriteSheetsLowCam;
        

        protected ViewportCamera _camera;
        protected float _drawDepth;
		protected Rectangle _destRectangle;
		protected Rectangle _sourceRectangle;
        protected bool _drawFlipped = false;
        protected Directions _currentDirection = Directions.DIR_0;
        private double _currentFrame = 0.0;
        protected Vector2 _screenPosition;

        public BaseFrameManager(List<SpriteSheet> sheets, ViewportCamera camera, List<SpriteSheet> topSheets, List<SpriteSheet> highSheets, List<SpriteSheet> lowSheets)
        {
            _camera = camera;
            _spriteSheetsTopCam = topSheets;
            _spriteSheetsHighCam = highSheets;
            _spriteSheetsLowCam = lowSheets;
            _spriteSheetsNormalCam = sheets;
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
            set { _screenPosition = value; }
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

        public void UpdateDrawRectangles(Vector3 position, bool trackWithCamera = false)
        {
            //-10 -- top
            //-27.5 -- high
            //-45   -- normal
            //-62.5 -- low
            float angle = Camera.CameraTopAngle;
            if(angle == -45)
            {
                CurrentFrame = CurrentFrame >= FrameCount ? 0.0 : CurrentFrame;
                _spriteSheets = _spriteSheetsNormalCam;

            }
            if (angle == -10)
            {
                CurrentFrame = CurrentFrame >= FrameCount ? 0.0 : CurrentFrame;
                _spriteSheets = _spriteSheetsTopCam;
            }
            if (angle == -27.5)
            {
                CurrentFrame = CurrentFrame >= FrameCount ? 0.0 : CurrentFrame;
                _spriteSheets = _spriteSheetsHighCam;
            }
            if (angle == -62.5)
            {
                CurrentFrame = CurrentFrame >= FrameCount ? 0.0 : CurrentFrame;
                _spriteSheets = _spriteSheetsLowCam;
            }

            float height = position.Z;
            SourceRectangle = new Rectangle((int)FrameSourcePos.X, (int)FrameSourcePos.Y, (int)FrameSize.X, (int)FrameSize.Y);
            //transform 2D world position to the screen camera space
            position = Camera.ToCameraSpace(position.X, position.Y, position.Z);
            DrawDepth = position.Y - FrameAnchor.Y;
            var screenYPosition = Camera.ScreenYPositionFromZComponent(position.Y, height);
            ScreenPosition = new Vector2(position.X,screenYPosition);
            DestinationRectangle = new Rectangle((int)(position.X - FrameAnchor.X), (int)(position.Y - FrameAnchor.Y),
                (int)FrameSize.X, (int)FrameSize.Y);
       
        }


        public bool UpdateCurrentSpriteSheet(AnimationState state)
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

