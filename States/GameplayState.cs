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

        private const string buildingATexture = "assets/buildings/test/buildingA_STATIC_spriteSheet";
        private const string buildingAData = "assets/buildings/test/buildingA_STATIC_metaData.json";
        private const string buildingBTexture = "assets/buildings/test/buildingB_STATIC_spriteSheet";
        private const string buildingBData = "assets/buildings/test/buildingB_STATIC_metaData.json";
        private const string buildingCTexture = "assets/buildings/test/buildingC_STATIC_spriteSheet";
        private const string buildingCData = "assets/buildings/test/buildingC_STATIC_metaData.json";

        private CharacterObject _characterSprite;
        private EnvironmentObject _buildingASprite;
        private EnvironmentObject _buildingBSprite;
        private EnvironmentObject _buildingCSprite;
        private Vector2 _viewportCenter;
        private Vector2 _viewportCenterPrev;


        public override void LoadContent()
        {
            var sheets = LoadCharacterSheets();
            var frameManager = new CharacterFrameManager(sheets);
            var startPos = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _characterSprite = new CharacterObject(frameManager, startPos);

            var buildingResult = LoadTexture(buildingATexture);
            var buildingMetaData = new SpriteSheetData(metaDataRoot + buildingAData);
            var buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            var propFrameManager = new PropFrameManager(buildingSheet);
            var buildingPos = new Vector2(_viewportWidth / 4, _viewportHeight / 4);
            _buildingASprite = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(buildingBTexture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + buildingBData);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet);
            buildingPos = new Vector2(_viewportWidth / 1.5f, _viewportHeight / 1.5f);
            _buildingBSprite = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(buildingCTexture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + buildingCData);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet);
            buildingPos = new Vector2(_viewportWidth / 4.0f, _viewportHeight / 1.5f);
            _buildingCSprite = new EnvironmentObject(propFrameManager, buildingPos);


            AddGameObject(_buildingASprite);
            AddGameObject(_buildingBSprite);
            AddGameObject(_buildingCSprite);
            AddGameObject(_characterSprite);

            _viewportCenter = new Vector2(startPos.X, startPos.Y);
            _viewportCenterPrev = new Vector2(startPos.X, startPos.Y);
        }

        

        private List<SpriteSheet> LoadCharacterSheets()
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
                        
                        var pos = _characterSprite.Move(playerMoveCmd.GetDirection());
                        
                        var offsetPos = Vector2.Subtract(_viewportCenter, pos);
                        _buildingASprite.OffsetPosition(offsetPos);
                        _buildingBSprite.OffsetPosition(offsetPos);
                        _buildingCSprite.OffsetPosition(offsetPos);

                    }
                    if(cmd is GameplayInputCommand.ChangeAnimationState)
                    {
                        var changeStateCmd = (GameplayInputCommand.ChangeAnimationState)cmd;
                        _characterSprite.ChangeState(changeStateCmd.State);
                    }
                    if (cmd is GameplayInputCommand.CameraMove)
                    {
                        var cameraMoveCmd = (GameplayInputCommand.CameraMove) cmd;
                        var pivotPos = _viewportCenter;
                        _buildingASprite.CameraMove(cameraMoveCmd.GetDirection(), pivotPos);
                        _buildingBSprite.CameraMove(cameraMoveCmd.GetDirection(), pivotPos);
                        _buildingCSprite.CameraMove(cameraMoveCmd.GetDirection(), pivotPos);
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