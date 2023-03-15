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
        protected Vector3 _cameraPosition;
        protected Directions _currentCameraDirection;
        private float _currentUpDownRotation = -45f;
        private bool _rotatePositive = false;
        protected int _viewWidth;
        protected int _viewHeight;
        private bool _rotateCalled = false;

        public Vector3 CameraPosition
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

        public void RotateCameraUp()
        {
           //-45
           //-56.25
           //-67.5
           //-78.75
           if(_currentUpDownRotation != -45f)
            {
                _currentUpDownRotation += 11.25f;
           
            }
            Debug.WriteLine(string.Format("rot up: {0}", _currentUpDownRotation));
        }

        public float CameraTopAngle
        {
            get { return _currentUpDownRotation; }
        }

        public void RotateCameraDown()
        {
            if(_currentUpDownRotation != -67.5)//-78.75f)
            {
                _currentUpDownRotation -= 11.25f;
            }
            Debug.WriteLine(string.Format("rot down: {0}", _currentUpDownRotation));
        }

        private void ShiftDirection(bool shiftPositive)
        {
            int currDir = (int)CameraDirection;
            int newDir = shiftPositive ? currDir + 1 : currDir - 1;
            newDir = MathUtils.Mod(newDir, 32);
            CameraDirection = (Directions)newDir;
            _rotatePositive = shiftPositive;
            _rotateCalled = true;

        }

        private Vector3 RotateCameraByDegrees(Vector3 position)
        {

            var camOrigin = new Vector3(ViewWidth / 2, ViewHeight / 2, 0);
            var diff = Vector3.Subtract(CameraPosition, camOrigin);
            position = Vector3.Subtract(position, diff);

            int camMult = (int)CameraDirection;
            var degreeRotation = (float)((360.0 / (float)32.0) * (float)-camMult);
            var rotation = MathUtils.ToRadians(degreeRotation);



            //Rotate camera top down, looking at XY plane
            var offset = new Vector3(ViewWidth / 2, ViewHeight / 2, 0);
            position = Vector3.Subtract(position, offset);
            var cosTheta = Math.Cos(rotation);
            var sinTheta = Math.Sin(rotation);
            double x = ((double)position.X * cosTheta) - ((double)position.Y * sinTheta);
            double y = ((double)position.Y * cosTheta) + ((double)position.X * sinTheta);
            position = new Vector3((float)x, (float)y, 0);
            position = Vector3.Add(position, offset);

            //Rotate to isometric space, rotating about the world "X" axis
            offset = new Vector3(0, ViewHeight / 2, 0);
            var isoPosition = new Vector3(0, position.Y, position.Z);
            isoPosition = Vector3.Subtract(isoPosition, offset);
            rotation = MathUtils.ToRadians(-1 * _currentUpDownRotation);
            cosTheta = Math.Cos(rotation);
            sinTheta = Math.Sin(rotation);
            double z = ((double)isoPosition.Z * cosTheta) - ((double)isoPosition.Y * sinTheta);
            y = ((double)isoPosition.Y * cosTheta) + ((double)isoPosition.Z * sinTheta);
            isoPosition = new Vector3(0, (float)y, (float)z);
            isoPosition = Vector3.Add(isoPosition, offset);
            position.Y = isoPosition.Y;
            position.Z = isoPosition.Z;


            _rotateCalled = false;
            return position;
        }

        public Vector3 ToCameraSpace(float positionX, float positionY, float positionZ)
        {
            return RotateCameraByDegrees(new Vector3(positionX, positionY, positionZ));
        }

        public float ScreenYPositionFromZComponent(float cameraSpaceY, float height)
        {
            
            //double x = Math.Sqrt((2.0 * Math.Pow(height, 2)) / 2.0);
            
            float degrees = -45.0f;
            float radians = degrees * ((float) (Math.PI / 180.0));
            double yOffset = height * Math.Cos(degrees);
            
            float result = cameraSpaceY - (float) yOffset;
            return result;
        }
    }
}

