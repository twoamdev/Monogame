using Engine.Enum;
using Objects;
using Engine.Objects;
using Engine.States.Base;
using Engine.Input.Base;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Engine.Input;

namespace States
{
    public class GameplayState : BaseGameState
    {
        private const string bgImage = "assets/dev/testDungeonSheet/dungeon_sheet";
        private const string characterTexture = "assets/characters/genericTestMale/male_animation_walk_001/down_right_tween_down/male_animation_walk_001_down_right_tween_down.0";
        private CharacterSprite _characterSprite;


        public override void LoadContent()
        {
            _characterSprite = new CharacterSprite(LoadTexture(characterTexture));
            AddGameObject(new SplashImage(LoadTexture(bgImage)));
            AddGameObject(_characterSprite);
        }

        

        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
                {
                    if(cmd is GameplayInputCommand.GameExit)
                    {
                        NotifyEvent(Events.GAME_QUIT);
                    }
                    if(cmd is GameplayInputCommand.PlayerMove)
                    { 
                        var playerMoveCmd = (GameplayInputCommand.PlayerMove) cmd;
                        _characterSprite.Move(playerMoveCmd.GetDirection());
                    }
                }
            );

            
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }
}