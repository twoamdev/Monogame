using Engine.Input.Base;
using Engine.Enum;
using Engine.Utilities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Engine.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        private int _cameraRelease = 0;
        private int _cameraTopAngleRelease = 0;
        private KeyboardState _keyboardState;
        private GamePadState _gamePadState;
       

        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            _keyboardState = state;
            return ProcessInputs();
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) {
            _gamePadState = state;
            return ProcessInputs();
        }

        private List<GameplayInputCommand> ProcessInputs()
        {
            
            var commands = new List<GameplayInputCommand>();
            

            if (_keyboardState.IsKeyDown(Keys.Escape) || _gamePadState.IsButtonDown(Buttons.Start))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }


            //Player Move
            var move = MoveDirectionFromStates();
            if (move.isKeyDown)
            {
                if (_keyboardState.IsKeyDown(Keys.LeftShift) || _gamePadState.IsButtonDown(Buttons.RightTrigger))
                {
                    commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationState.RUNNING));
                }
                else
                {
                    commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationState.WALKING));
                }

                commands.Add(new GameplayInputCommand.PlayerMove(move.direction));
            }

            //Idle
            if (!move.isKeyDown)
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationState.IDLE));
            }

            //Roll
            if (_keyboardState.IsKeyDown(Keys.R) || _gamePadState.IsButtonDown(Buttons.B))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationState.ROLLING));
            }

            //Jump
            if (_keyboardState.IsKeyDown(Keys.Space) || _gamePadState.IsButtonDown(Buttons.A))
            {
                commands.Add(new GameplayInputCommand.ChangeAnimationState(AnimationState.JUMPING));
            }

            //Move Camera Right
            if ((_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.RightThumbstickLeft))
                && (_cameraRelease == 0))
            {
                commands.Add(new GameplayInputCommand.CameraRotateRight());
            }

            //Move Camera Left
            if ((_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.RightThumbstickRight))
                && (_cameraRelease == 0))
            {
                commands.Add(new GameplayInputCommand.CameraRotateLeft());
            }

            //Move Camera up
            if ((_keyboardState.IsKeyDown(Keys.Up) || _gamePadState.IsButtonDown(Buttons.RightThumbstickUp))
                && (_cameraTopAngleRelease == 0))
            {
                commands.Add(new GameplayInputCommand.CameraRotateDown());
            }

            //Move Camera down 
            if ((_keyboardState.IsKeyDown(Keys.Down) || _gamePadState.IsButtonDown(Buttons.RightThumbstickDown))
                && (_cameraTopAngleRelease == 0))
            {
                commands.Add(new GameplayInputCommand.CameraRotateUp());
            }

            //if holding down camera
            if (_keyboardState.IsKeyDown(Keys.Right)
                || _keyboardState.IsKeyDown(Keys.Left)
                || _keyboardState.IsKeyDown(Keys.Up)
                || _keyboardState.IsKeyDown(Keys.Down)
                || _gamePadState.IsButtonDown(Buttons.RightThumbstickRight)
                || _gamePadState.IsButtonDown(Buttons.RightThumbstickLeft)
                || _gamePadState.IsButtonDown(Buttons.RightThumbstickUp)
                || _gamePadState.IsButtonDown(Buttons.RightThumbstickDown))
            {
                _cameraRelease++;
                _cameraTopAngleRelease++;
                _cameraRelease = MathUtils.Mod(_cameraRelease, 16);
                _cameraTopAngleRelease = MathUtils.Mod(_cameraTopAngleRelease, 32);
            }

            if ( (_keyboardState.IsKeyUp(Keys.Right) && _keyboardState.IsKeyUp(Keys.Left)) &&
                (_keyboardState.IsKeyUp(Keys.Down) && _keyboardState.IsKeyUp(Keys.Up)) &&
                (_gamePadState.IsButtonUp(Buttons.RightThumbstickRight) && _gamePadState.IsButtonUp(Buttons.RightThumbstickLeft) &&
                 _gamePadState.IsButtonUp(Buttons.RightThumbstickUp) && _gamePadState.IsButtonUp(Buttons.RightThumbstickDown)
                ))
            {
                _cameraRelease = 0;
                _cameraTopAngleRelease = 0;
            }




            return commands;
        }


        private (bool isKeyDown, Vector2 direction) MoveDirectionFromStates()
        {
            if (_gamePadState.IsButtonDown((Buttons.LeftThumbstickUp)) ||
                _gamePadState.IsButtonDown((Buttons.LeftThumbstickDown)) ||
                _gamePadState.IsButtonDown((Buttons.LeftThumbstickLeft)) ||
                _gamePadState.IsButtonDown((Buttons.LeftThumbstickRight)) )
            {
                return MoveDirectionFromThumbstick();
            }

            float SQRT_OF_2 = 1.41421356237f;
            bool keyIsDown = false;
            Vector2 direction = new Vector2(0, 0);

            bool isUp = _keyboardState.IsKeyDown(Keys.W)
                || _gamePadState.IsButtonDown(Buttons.DPadUp);
            bool isLeft = _keyboardState.IsKeyDown(Keys.A)
                || _gamePadState.IsButtonDown(Buttons.DPadLeft);
            bool isDown = _keyboardState.IsKeyDown(Keys.S)
                || _gamePadState.IsButtonDown(Buttons.DPadDown);
            bool isRight = _keyboardState.IsKeyDown(Keys.D)
                || _gamePadState.IsButtonDown(Buttons.DPadRight);


            //is UP
            if (isUp && !isLeft && !isDown && !isRight)
            {
                keyIsDown = true;
                direction.X = 0;
                direction.Y = -1;

            }
            //is UP LEFT
            if (isUp && isLeft && !isDown && !isRight)
            {
                keyIsDown = true;
                direction.X = -0.5f * SQRT_OF_2;
                direction.Y = -0.5f * SQRT_OF_2;
            }
            //is UP RIGHT
            if (isUp && isRight && !isDown && !isLeft)
            {
                keyIsDown = true;
                direction.X = 0.5f * SQRT_OF_2;
                direction.Y = -0.5f * SQRT_OF_2;
            }
            //is RIGHT
            if (isLeft && !isUp && !isDown && !isRight)
            {
                keyIsDown = true;
                direction.X = -1;
                direction.Y = 0;
            }
            //is DOWN LEFT
            if (isLeft && isDown && !isUp && !isRight)
            {
                keyIsDown = true;
                direction.X = -0.5f * SQRT_OF_2;
                direction.Y = 0.5f * SQRT_OF_2;
            }
            //is DOWN
            if (isDown && !isUp && !isLeft && !isRight)
            {
                keyIsDown = true;
                direction.X = 0;
                direction.Y = 1;
            }
            //is DOWN RIGHT
            if (isDown && isRight && !isLeft && !isUp)
            {
                keyIsDown = true;
                direction.X = 0.5f * SQRT_OF_2;
                direction.Y = 0.5f * SQRT_OF_2;
            }
            //is DOWN
            if (isRight && !isUp && !isLeft && !isDown)
            {
                keyIsDown = true;
                direction.X = 1;
                direction.Y = 0;
            }
            return (keyIsDown, direction);
        }

        private (bool isKeyDown, Vector2 direction) MoveDirectionFromThumbstick()
        {
            var x = _gamePadState.ThumbSticks.Left.X;
            var y = -1 * _gamePadState.ThumbSticks.Left.Y;
            var scale = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            var direction = new Vector2(x, y);
            direction = Vector2.Normalize(direction);
            direction = Vector2.Multiply(direction, (float) scale);

            return (true, direction);
        }

    }

}