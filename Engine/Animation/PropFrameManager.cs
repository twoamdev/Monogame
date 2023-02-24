using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Animation.Base;
using Engine.Utilities;
using Engine.Enum;

namespace Engine.Animation
{
	public class PropFrameManager : BaseFrameManager
	{
        private float _rotationDegrees = 0.0f;
        private float _prevRotationDegrees = 0.0f;
        private bool _negateDirection = false;
        private bool _directionRangeChanged = false;

		public PropFrameManager(SpriteSheet sheet)
		{
			SpriteSheets = new List<SpriteSheet> {sheet};
        }

        public void CalculateDirection(Vector2 direction)
        {
            float dotResult = Vector2.Dot(new Vector2(0, 1), direction);

            float value = MathUtils.Remap(dotResult, -1, 1, 0, 1);
            _negateDirection = direction.X < 0 ? true : false;

            _prevRotationDegrees = _rotationDegrees;

            if (_negateDirection)
            {
                _rotationDegrees -= 2;
                _rotationDegrees = MathUtils.Mod((int)_rotationDegrees, 360);

                
            }
            if (!_negateDirection)
            {
                _rotationDegrees += 2;
                _rotationDegrees = MathUtils.Mod((int)_rotationDegrees, 360);

            }
            
            
            int result = (int) MathUtils.Remap(_rotationDegrees, 0, 360, 0, 16);
            _directionRangeChanged = (int)FrameDirection != result ? true : false;
            FrameDirection = (Directions)result;


            
        }

        public Vector2 RotatePosition(Vector2 pivotPosition, Vector2 objectPosition)
        {
            if (!_directionRangeChanged)
            { return objectPosition; }

            //could be needing to plus or minus to the closest direciton
            var degreeRotation = (float)(360.0 / 16.0); //Math.Abs(_rotationDegrees - _prevRotationDegrees) + (float) (360.0/16.0);
            var rotation = _negateDirection ? MathUtils.ToRadians(degreeRotation) : MathUtils.ToRadians(degreeRotation * -1);
            var vectorA = new Vector2((float) Math.Cos(rotation), (float) Math.Sin(rotation));
            var vectorB = new Vector2((float) Math.Sin(rotation) * -1.0f, (float) Math.Cos(rotation));


            //Transform to pivot space
            var posToRotate = Vector2.Subtract(objectPosition, pivotPosition);
            //Rotate
            posToRotate = Vector2.Multiply(vectorA, posToRotate.X) + Vector2.Multiply(vectorB, posToRotate.Y);
            //Transform back to object space
            var updatedPosition = Vector2.Add(posToRotate, pivotPosition);

            return updatedPosition;
            
        }


    }
}

