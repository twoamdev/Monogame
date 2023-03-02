using Engine.Enum;
using Objects;
using Engine.Objects;
using Engine.States.Base;
using Engine.Input.Base;
using Engine.Utilities;
using Engine.Input;
using Engine.Animation;


using System.Diagnostics;
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
        private const string building_ALT_Texture = "assets/buildings/test/buildingA_ALT_STATIC_spriteSheet";
        private const string building_ALT_Data = "assets/buildings/test/buildingA_ALT_STATIC_metaData.json";
        private const string buildingBTexture = "assets/buildings/test/buildingB_STATIC_spriteSheet";
        private const string buildingBData = "assets/buildings/test/buildingB_STATIC_metaData.json";
        private const string buildingCTexture = "assets/buildings/test/buildingC_STATIC_spriteSheet";
        private const string buildingCData = "assets/buildings/test/buildingC_STATIC_metaData.json";
        private const string hqTexturePrefix = "assets/buildings/test/testSequence/guildHQ_STATIC_spriteSheet.";
        private const string hqBuildingData = "assets/buildings/test/testSequence/guildHQ_STATIC_metaData.json";

        private const string groundTexture = "assets/tiles/ground/groundTile_STATIC_spriteSheet";
        private const string groundData = "assets/tiles/ground/groundTile_STATIC_metaData.json";

        private CharacterObject _characterSprite;
        private EnvironmentObject _b1;
        private EnvironmentObject _b2;
        private EnvironmentObject _b3;
        private EnvironmentObject _b4;
        private EnvironmentObject _b5;
        private EnvironmentObject _b6;
        private EnvironmentObject _b7;
        private EnvironmentObject _b8;
        private EnvironmentObject _hq;
        
        private ViewportCamera _camera;


        public override void LoadContent()
        {
            var sheets = LoadCharacterSheets();
            var startPos = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            var startCamPos = new Vector2(startPos.X , startPos.Y);
            _camera = new ViewportCamera(startCamPos, _viewportWidth, _viewportHeight);
            var frameManager = new CharacterFrameManager(sheets, _camera);
            _characterSprite = new CharacterObject(frameManager, startPos);

            var hqResult = LoadTextureSequence(hqTexturePrefix, 0, 31);
            var hqData = new SpriteSheetData(metaDataRoot + hqBuildingData);
            var hqSheet = new SpriteSheet(hqResult.HasErrorTexture,
                hqResult.Textures, hqData);
            var hqM = new PropFrameManager(hqSheet, _camera);
            var hqPos = new Vector2(-100, -100);
            _hq = new EnvironmentObject(hqM, hqPos);

            float offset = 100f;
            
            var buildingResult = LoadTexture(building_ALT_Texture);
            var buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            var buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            var propFrameManager = new PropFrameManager(buildingSheet, _camera);
            var buildingPos = new Vector2((_viewportWidth / 2) + offset, _viewportHeight / 2);
            _b1 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2) - offset, _viewportHeight / 2);
            _b2 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2), (_viewportHeight / 2) + offset);
            _b3 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2), (_viewportHeight / 2) - offset);
            _b4 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2) + (offset*2), _viewportHeight / 2);
            _b5 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2) - (offset*2), _viewportHeight / 2);
            _b6 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2), (_viewportHeight / 2) + (offset*2));
            _b7 = new EnvironmentObject(propFrameManager, buildingPos);

            buildingResult = LoadTexture(building_ALT_Texture);
            buildingMetaData = new SpriteSheetData(metaDataRoot + building_ALT_Data);
            buildingSheet = new SpriteSheet(buildingResult.IsErrorTexture,
                buildingResult.LoadedTexture, buildingMetaData);
            propFrameManager = new PropFrameManager(buildingSheet, _camera);
            buildingPos = new Vector2((_viewportWidth / 2), (_viewportHeight / 2) - (offset*2));
            _b8 = new EnvironmentObject(propFrameManager, buildingPos);



            AddGameObject(_hq);
            AddGameObject(_b1);
            AddGameObject(_b2);
            AddGameObject(_b3);
            AddGameObject(_b4);
            AddGameObject(_b5);
            AddGameObject(_b6);
            AddGameObject(_b7);
            AddGameObject(_b8);
            AddGameObject(_characterSprite);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    float tileOffset = 38;
                    var tileResult = LoadTexture(groundTexture);
                    var tileData = new SpriteSheetData(metaDataRoot + groundData);
                    var tileSheet = new SpriteSheet(tileResult.IsErrorTexture,
                        tileResult.LoadedTexture, tileData);
                    var tileManager = new PropFrameManager(tileSheet, _camera);
                    var tilePos = new Vector2(i * tileOffset, j * tileOffset);
                    bool isGround = true;
                    var tilePiece = new EnvironmentObject(tileManager, tilePos, isGround);
                   // AddGameObject(tilePiece);
                }
            }
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
                        _characterSprite.Move(playerMoveCmd.GetDirection());
                        _camera.CameraPosition = new Vector2(_characterSprite.Position.X, _characterSprite.Position.Y);


                    }
                    if(cmd is GameplayInputCommand.ChangeAnimationState)
                    {
                        var changeStateCmd = (GameplayInputCommand.ChangeAnimationState)cmd;
                        _characterSprite.ChangeState(changeStateCmd.State);
                    }
                    if (cmd is GameplayInputCommand.CameraRotateLeft)
                    {
                        foreach(var gameObject in GameObjects)
                        {
                            if (gameObject is EnvironmentObject)
                            {
                                var envObject = (EnvironmentObject)gameObject;
                                envObject.ShiftDrawAngle(1);
                            }
                        }
                        _camera.RotateCameraLeft();
                    }
                    if (cmd is GameplayInputCommand.CameraRotateRight)
                    {
                        foreach (var gameObject in GameObjects)
                        {
                            if (gameObject is EnvironmentObject)
                            {
                                var envObject = (EnvironmentObject)gameObject;
                                envObject.ShiftDrawAngle(-1);
                            }
                        }
                        _camera.RotateCameraRight();
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