using System;
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

        public void CalculateDirection(Vector2 direction)
        {
            float dotResult = Vector2.Dot(new Vector2(0,1), direction);
            _characterMoved = true;

            double upperBound = 1.0;
            double lowerBound = 0;
            bool settingsAdjusted = false;
            for (int i = 0; i < 9; i++)
            {

                lowerBound = 1.0 - ((2.0 / 9.0) * (i + 1));
                if (dotResult <= upperBound && dotResult >= lowerBound)
                {
                    DrawFlipped = direction.X < 0 && (i != 0) && (i != 9) ? true : false;
                    FrameDirection = (Enum.Directions) i;
                    settingsAdjusted = true;
                }
                upperBound = lowerBound;
            }
            if (!settingsAdjusted)
            {
                FrameDirection = Enum.Directions.DIR_0;
                DrawFlipped = false;
            }

            //account for flipping up or down
            bool upOrDown = FrameDirection == Enum.Directions.DIR_0 ||
                FrameDirection == Enum.Directions.DIR_8 ? true : false;
            if(upOrDown && DrawFlipped)
            {
                DrawFlipped = false;
            }
            
        }
    }
}

