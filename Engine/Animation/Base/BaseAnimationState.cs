using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Enum;

namespace Engine.Animation.Base
{
	public class BaseAnimationState
	{
        private AnimationStates _state;
        private bool _isPlaying;

        public BaseAnimationState()
        {
            _state = AnimationStates.STATIC;
            _isPlaying = false;
        }

        public AnimationStates State
        {
            get { return _state; }
            set { _state = value; }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { _isPlaying = value; }
        }

        
       
    }
}

