using Engine.Enum;
using Objects;
using Engine.Objects;
using Engine.States.Base;
using Engine.Input.Base;
using Engine.Utilities;
using Engine.Input;
using Engine.Animation;



using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace States
{
    public class GameplayState : BaseGameState
    {
        private const string metaDataRoot = "/Users/bennelson/Documents/Git_Repositories/GuildOfHeaven/GuildOfHeaven/Content/";
        private const string bgImage = "assets/dev/testDungeonSheet/dungeon_sheet";
        private const string rollTexture = "assets/characters/genericMaleJake/maleJake_ROLL_spriteSheet";
        private const string rollData = "assets/characters/genericMaleJake/maleJake_ROLL_metaData.json";
        private const string walkTexture = "assets/characters/genericMaleJake/maleJake_WALK_spriteSheet";
        private const string walkData = "assets/characters/genericMaleJake/maleJake_WALK_metaData.json";
        private const string runTexture = "assets/characters/genericMaleJake/maleJake_RUN_spriteSheet";
        private const string runData = "assets/characters/genericMaleJake/maleJake_RUN_metaData.json";
        private CharacterObject _characterSprite;
        private EnvironmentBackground _levelBackground;


        public override void LoadContent()
        {
            var sheets = LoadSheets();
            var frameManager = new CharacterFrameManager(sheets);
            
            var startPos = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _characterSprite = new CharacterObject(frameManager, startPos);
            
            //AddGameObject(_levelBackground);
            AddGameObject(_characterSprite);
        }

        

        private List<SpriteSheet> LoadSheets()
        {
            var sheets = new List<SpriteSheet>();
            var walkResult = LoadTexture(walkTexture);
            var walkMetaData = new SpriteSheetData(metaDataRoot + walkData);
            var runResult = LoadTexture(runTexture);
            var runMetaData = new SpriteSheetData(metaDataRoot + runData);
            var rollResult = LoadTexture(rollTexture);
            var rollMetaData = new SpriteSheetData(metaDataRoot + rollData);


            var walkingSheet = new SpriteSheet(walkResult.IsErrorTexture,
                walkResult.LoadedTexture, walkMetaData, AnimationStates.WALKING);
            var runningSheet = new SpriteSheet(runResult.IsErrorTexture,
                runResult.LoadedTexture, runMetaData, AnimationStates.RUNNING);
            var rollingSheet = new SpriteSheet(rollResult.IsErrorTexture,
                rollResult.LoadedTexture, rollMetaData, AnimationStates.ROLLING);

            sheets.Add(walkingSheet);
            sheets.Add(runningSheet);
            sheets.Add(rollingSheet);

            return sheets;
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