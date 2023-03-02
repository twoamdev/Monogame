using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Animation.Base;
using Engine.Utilities;
using Engine.Enum;
using Engine.Objects;

namespace Engine.Animation
{
	public class CharacterFrameManager : BaseFrameManager
	{
        private bool _isAnimationPlaying = false;
        private bool _characterMoved = false;

        public CharacterFrameManager(List<SpriteSheet> sheets, ViewportCamera camera) : base(camera)
		{
            SpriteSheets = sheets;
           
        }

        public void UpdateCurrentFrame()
        {
            if (_characterMoved || _isAnimationPlaying)
            {
                CurrentFrame += FrameDuration;
                if (CurrentFrame >= FrameCount)
                {
                    _isAnimationPlaying = false;
                }
                CurrentFrame %= FrameCount;
                _characterMoved = false;
            }
        }

        public void ChangeState(AnimationStates state)
        {
            if (_isAnimationPlaying) {
                return;
            }

            bool wasUpdated = UpdateCurrentSpriteSheet(state);
            if (wasUpdated && AnimationTriggered)
            {
                _isAnimationPlaying = true;
                CurrentFrame = 0.0;
            }
        }

        public void CalculateDirection(Vector2 direction, Vector2 screenDownDirection)
        {
            
            float dotResult = Vector2.Dot(screenDownDirection, direction);
            _characterMoved = true;
            dotResult += 1;
            int dir = (int) MathUtils.Remap(dotResult, 0, 2, 0, 8);
            dir = 8 - dir;

            if(MathUtils.Mod(dir, 2) != 0)
            {
                dir -= 1;
            }

            FrameDirection = (Directions)dir;

            
        }
    }
}

