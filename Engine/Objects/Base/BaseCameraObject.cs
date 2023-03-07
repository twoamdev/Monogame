using Engine.Enum;
using Engine.Utilities;
using Engine.Animation.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Engine.Objects.Base
{
	public class BaseCameraObject
	{
		protected Vector2 _cameraPosition;
        protected Directions _currentCameraDirection;
        private bool _rotatePositive = false;
		protected int _viewWidth;
		protected int _viewHeight;
        private bool _rotateCalled = false;

		public Vector2 CameraPosition
		{
			get { return _cameraPosition; }
			set { _cameraPosition = value; }
		}

        public Directions CameraDirection
        {
            get { return _currentCameraDirection; }
            set { _currentCameraDirection = value; }
        }

        public bool RotateWasCalled
        {
            get { return _rotateCalled; }
        }

        public int ViewWidth
		{
            get { return _viewWidth; }
            set { _viewWidth = value; }
        }

        public int ViewHeight
        {
            get { return _viewHeight; }
            set { _viewHeight = value; }
        }

        public void RotateCameraRight()
        {
            ShiftDirection(false);
        }

        public void RotateCameraLeft()
        {
            ShiftDirection(true);
        }

        private void ShiftDirection(bool shiftPositive)
        {
            int currDir = (int)CameraDirection;
            int newDir = shiftPositive ? currDir + 1 : currDir -1;
            newDir = MathUtils.Mod(newDir, 32);
            CameraDirection = (Directions)newDir;
            _rotatePositive = shiftPositive;
            _rotateCalled = true;
           
        }

        private Vector2 RotateCameraByDegrees(Vector2 position)
        {

            var camOrigin = new Vector2(ViewWidth / 2, ViewHeight / 2);
            var diff = Vector2.Subtract(CameraPosition, camOrigin);
            position = Vector2.Subtract(position, diff);

            int camMult = (int) CameraDirection;
            var degreeRotation = (float)((360.0 / (float)32.0) * (float) -camMult);
            var rotation = MathUtils.ToRadians(degreeRotation);



            //Rotate camera
            var offset = new Vector2(ViewWidth / 2, ViewHeight / 2);
            position = Vector2.Subtract(position, offset);
            var cosTheta = Math.Cos(rotation);
            var sinTheta = Math.Sin(rotation);
            double x = ((double)position.X * cosTheta) - ((double)position.Y * sinTheta);
            double y = ((double)position.Y * cosTheta) + ((double)position.X * sinTheta);
            position = new Vector2((float)x, (float)y);
            position = Vector2.Add(position, offset);

            //Rotate to isometric space
            offset = new Vector2(0, ViewHeight / 2);
            var isoPosition = new Vector2(0, position.Y);
            isoPosition = Vector2.Subtract(isoPosition, offset);
            rotation = MathUtils.ToRadians(45.0f);
            cosTheta = Math.Cos(rotation);
            sinTheta = Math.Sin(rotation);
            double z = ((double)0 * cosTheta) - ((double)isoPosition.Y * sinTheta);
            y = ((double)isoPosition.Y * cosTheta) + ((double)0 * sinTheta);
            isoPosition = new Vector2(0, (float)y);
            isoPosition = Vector2.Add(isoPosition, offset);
            position.Y = isoPosition.Y;


            _rotateCalled = false;
            return position;
        }

        public Vector2 ToCameraSpace(float positionX, float positionY)
        {
            return RotateCameraByDegrees(new Vector2(positionX, positionY));
        }
    }
}

