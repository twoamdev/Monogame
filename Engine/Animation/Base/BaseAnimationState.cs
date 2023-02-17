using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Enum;

namespace Engine.Animation.Base
{
	public abstract class BaseAnimationState
	{
        private AnimationStates _state = AnimationStates.STATIC;

        public AnimationStates AnimationState
        {
            get { return _state; }
            set { _state = value; }
        }
        /*
        protected void LoadAnimationFrames(Texture2D textureSheet)
        {

        }
        */
    }
}

