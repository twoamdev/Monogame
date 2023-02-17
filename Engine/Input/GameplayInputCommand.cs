using Engine.Input.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Input
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public class GameExit : GameplayInputCommand { }
        public class PlayerMove : GameplayInputCommand
        {
            private Vector2 _direction;

            public PlayerMove(Vector2 direction)
            {
                _direction = direction;
            }

            public Vector2 GetDirection()
            {
                return _direction;
            }
        }
        public class ChangeAnimationState : GameplayInputCommand { }


    }
}