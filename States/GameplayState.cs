using Engine.Enum;
using Objects;
using Engine.Objects;
using Engine.States.Base;
using Engine.Input.Base;
using Engine.Utilities;



using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Engine.Input;

namespace States
{
    public class GameplayState : BaseGameState
    {
        private const string bgImage = "assets/dev/testDungeonSheet/dungeon_sheet";

        private const string rollTexture = "assets/characters/genericMaleJake/maleJake_ROLL_spriteSheet";
        private const string walkTexture = "assets/characters/genericMaleJake/maleJake_WALK_spriteSheet";
        private const string runTexture = "assets/characters/genericMaleJake/maleJake_RUN_spriteSheet";
        private CharacterSprite _characterSprite;
        private EnvironmentBackground _levelBackground;


        public override void LoadContent()
        {
            List<SpriteSheet> sheets = new List<SpriteSheet>();
            sheets.Add(new SpriteSheet(LoadTexture(walkTexture), 40,40, AnimationStates.WALKING));
            sheets.Add(new SpriteSheet(LoadTexture(runTexture), 40, 40, AnimationStates.RUNNING));
            sheets.Add(new SpriteSheet(LoadTexture(rollTexture), 40, 40, AnimationStates.ROLLING));

            _characterSprite = new CharacterSprite(sheets);
            _levelBackground = new EnvironmentBackground(new SpriteSheet(LoadTexture(bgImage),384,160));
            //AddGameObject(_levelBackground);
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
                    if(cmd is GameplayInputCommand.ChangeAnimationState)
                    {
                        var changeStateCmd = (GameplayInputCommand.ChangeAnimationState)cmd;
                        _characterSprite.ChangeState(changeStateCmd.State);
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