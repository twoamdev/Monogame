using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Enum;

namespace Engine.Animation.Base
{
	public class BaseAnimationStateManager
	{
		private AnimationState _animationState;
        

        public BaseAnimationStateManager(AnimationState animationState)
		{
			_animationState = animationState;
		}

		public AnimationState AnimationState
		{
			get { return _animationState; }
			set { _animationState = value; }
		}

		public virtual double FrameDuration()
		{
			return 0.0;
		}
    }
}

