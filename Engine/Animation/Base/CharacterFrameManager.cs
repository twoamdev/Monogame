using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Animation.Base;
namespace Engine.Animation
{
	public class CharacterFrameManager : BaseFrameManager
	{
        private Vector2 Y_POSITIVE_DIRECTION = new Vector2(0, 1);
        private Enum.Directions _drawDirection;
        private int _frameWidth;
        private int _frameHeight;
        private double _currentFrame;

        public CharacterFrameManager(int frameWidth, int frameHeight, Vector2 characterPosition)
		{
            _drawDirection = Enum.Directions.DIR_0_DOWN;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _currentFrame = 0;
            DrawFlipped = false;
            
            SourceRectangle = new Rectangle((int)_currentFrame * frameWidth, (int) _drawDirection * frameHeight, frameWidth, frameHeight);
            DestinationRectangle = new Rectangle((int)characterPosition.X, (int)characterPosition.Y, frameWidth, frameHeight);
        }

        public void CalculateDirection(Vector2 direction)
        {
            float dotResult = Vector2.Dot(Y_POSITIVE_DIRECTION, direction);

            double upperBound = 1.0;
            double lowerBound = 0;
            bool settingsAdjusted = false;
            for (int i = 0; i < 9; i++)
            {

                lowerBound = 1.0 - ((2.0 / 9.0) * (i + 1));
                if (dotResult <= upperBound && dotResult >= lowerBound)
                {
                    DrawFlipped = direction.X < 0 && (i != 0) && (i != 9) ? true : false;
                    _drawDirection = (Enum.Directions) i;
                    settingsAdjusted = true;
                }
                upperBound = lowerBound;
            }
            if (!settingsAdjusted)
            {
                _drawDirection = Enum.Directions.DIR_0_DOWN;
                DrawFlipped = false;
            }

            //account for flipping up or down
            bool upOrDown = _drawDirection == Enum.Directions.DIR_0_DOWN ||
                _drawDirection == Enum.Directions.DIR_8_UP ? true : false;
            if(upOrDown && DrawFlipped)
            {
                DrawFlipped = false;
            }
            
        }

        public void UpdatePosition(Vector2 position)
        {
            _currentFrame += (15.0 / 60.0);
            _currentFrame = _currentFrame % 14;
            SourceRectangle = new Rectangle((int)_currentFrame * _frameWidth, (int)_drawDirection * _frameHeight, _frameWidth, _frameHeight);
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, _frameWidth, _frameHeight);
        }

    }
}

