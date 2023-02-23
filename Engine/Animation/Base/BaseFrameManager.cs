using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utilities;
using Engine.Enum;

namespace Engine.Animation.Base
{
	public class BaseFrameManager
	{
        protected List<SpriteSheet> _spriteSheets;
		protected Rectangle _destRectangle;
		protected Rectangle _sourceRectangle;
        protected bool _drawFlipped = false;
        protected Directions _currentDirection = Directions.DIR_0;
        private double _currentFrame = 0.0;

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

        public List<SpriteSheet> SpriteSheets
        {
            set { _spriteSheets = value; }
        }

        public void UpdateObjectPosition(Vector2 position)
        {
            SourceRectangle = new Rectangle((int)FrameSourcePos.X, (int)FrameSourcePos.Y, (int)FrameSize.X, (int)FrameSize.Y);
            DestinationRectangle = new Rectangle((int)(position.X - FrameAnchor.X), (int)(position.Y - FrameAnchor.Y), (int)FrameSize.X, (int)FrameSize.Y);
        }

        public bool UpdateCurrentSpriteSheet(AnimationStates state)
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

        public bool AnimationTriggered
        {
            get{ return CurrentSpriteSheet.PlaysOnChange; }
        }

        public Texture2D Texture
        {
            get { return CurrentSpriteSheet.Texture; }
        }

        public double FrameDuration
        {
            get { return CurrentSpriteSheet.FrameDuration; }
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
    }
}

