using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utilities;

namespace Engine.Animation.Base
{
	public class BaseFrameManager
	{
        protected List<SpriteSheet> _spriteSheets;
        protected SpriteSheet _currentSheet;
		protected Rectangle _destRectangle;
		protected Rectangle _sourceRectangle;
		protected bool _drawFlipped;

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

        public List<SpriteSheet> SpriteSheets
        {
            get { return _spriteSheets; }
            set {
                _spriteSheets = value;
                if(_spriteSheets.Count > 0 && _currentSheet == null)
                {
                    _currentSheet = _spriteSheets[0];
                }
            }
        }


        public SpriteSheet CurrentSheet
        {
            get { return _currentSheet; }
            set { _currentSheet = value; }
        }



    }
}

