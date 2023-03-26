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
        private bool _characterMoved = false;
        private CharacterAnimationStateManager _animationState;

        public CharacterFrameManager(List<SpriteSheet> sheets, ViewportCamera camera, List<SpriteSheet> topSheets, List<SpriteSheet> highSheets, List<SpriteSheet> lowSheets) : base(sheets, camera, topSheets, highSheets, lowSheets)
        {
            SpriteSheets = sheets;
            _animationState = new CharacterAnimationStateManager(SpriteSheetAnimationState);
            _spriteSheetsHighCam = highSheets;
            _spriteSheetsLowCam = lowSheets;
            _spriteSheetsTopCam = topSheets;
        }

        public void UpdateCurrentFrame()
        {
            if (_characterMoved || _animationState.IsPlaying || _animationState.IsLooping)
            {
                CurrentFrame += _animationState.FrameDuration();

                if (CurrentFrame >= FrameCount)
                {
                    _animationState.StoppedPlaying();
                }

                CurrentFrame = MathUtils.Mod(CurrentFrame, (double)FrameCount);
                CurrentFrame = CurrentFrame >= (double)FrameCount ? 0.0 : CurrentFrame;
                _characterMoved = false;

            }
        }

        public void ChangeState(AnimationState state)
        {
            if (_animationState.IsPlaying || state == _animationState.AnimationState) {
                return;
            }

            (var canUpdate, state) = _animationState.ValidateStateChange(state);

            if (canUpdate && UpdateCurrentSpriteSheet(state))
            {
                _animationState.UpdateAnimationState(state);
                if (_animationState.PreviousState == AnimationState.IDLE
                    || _animationState.PlaysOnceOnChange
                    || _animationState.PlaysOnLoop)
                {
                    CurrentFrame = 0.0;
                }
            }
        }

        public AnimationState AnimationState
        {
            get { return _animationState.AnimationState; }
        }

        public AnimationState PreviousAnimationState
        {
            get { return _animationState.PreviousState; }
        }




        public void UpdateDrawFrameDirection(Vector2 moveDirection, Vector2 screenDownDirection, int camDir)
        {
            _characterMoved = true;

           
            float NUM_OF_DIRECTIONS = 8;
            float SQRT_OF_3_OVER_2 = 0.86602540378f;
            Vector2 normalizedMoveDirection = Vector2.Normalize(moveDirection);
            float dotProduct = Vector2.Dot(screenDownDirection, normalizedMoveDirection);
            float cirleSlice = 1.0f / (NUM_OF_DIRECTIONS/2.0f);
            float halfOfCircleSlice = (cirleSlice / 2.0f);
            var crossProduct = Vector3.Cross(new Vector3(screenDownDirection.X, screenDownDirection.Y, 0),
                new Vector3(normalizedMoveDirection.X, normalizedMoveDirection.Y, 0));
            bool flipDirection = crossProduct.Z < 0 ? true : false;

            if (dotProduct >= SQRT_OF_3_OVER_2)
            {
                FrameDirection = Directions.DIR_0;
            }
            if (dotProduct < SQRT_OF_3_OVER_2 && dotProduct > (SQRT_OF_3_OVER_2 - cirleSlice))
            {
                FrameDirection = flipDirection ? Directions.DIR_1 : Directions.DIR_7;
            }
            if (dotProduct < (cirleSlice / 2.0f) && dotProduct > (cirleSlice / -2.0f))
            {
                FrameDirection = flipDirection ? Directions.DIR_2 : Directions.DIR_6;
            }
            if (dotProduct > -SQRT_OF_3_OVER_2 && dotProduct < (-SQRT_OF_3_OVER_2 + cirleSlice))
            {
                FrameDirection = flipDirection ? Directions.DIR_3 : Directions.DIR_5;
            }
            if (dotProduct <= -SQRT_OF_3_OVER_2)
            {
                FrameDirection = Directions.DIR_4;
            }

        }
    }
}

