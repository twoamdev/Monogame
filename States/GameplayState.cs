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
        
        private CharacterObject _mainCharacter;
        private ViewportCamera _camera;
        private bool playerCollided = false;


        public override void LoadContent()
        {
            var startPos = new Vector3(_viewportWidth / 2, (_viewportHeight / 2) + 70, 0);
            var startCamPos = new Vector3(startPos.X, startPos.Y, 0);
            _camera = new ViewportCamera(startCamPos, _viewportWidth, _viewportHeight);

            var mainPlayerSheetsNormal = LoadSheets(AssetLoadUtils.PlayerTextureAndDataPaths(CamLookAngle.NORMAL));
            var mainPlayerSheetsMid = LoadSheets(AssetLoadUtils.PlayerTextureAndDataPaths(CamLookAngle.MID));
            var mainPlayerSheetsLow = LoadSheets(AssetLoadUtils.PlayerTextureAndDataPaths(CamLookAngle.LOW));
            var mainPlayerSheetsGround = LoadSheets(AssetLoadUtils.PlayerTextureAndDataPaths(CamLookAngle.GROUND));
            var mainPlayerFrameManager = new CharacterFrameManager(
                mainPlayerSheetsNormal,
                _camera,
                mainPlayerSheetsMid,
                mainPlayerSheetsLow,
                mainPlayerSheetsGround
                );
            _mainCharacter = new CharacterObject(startPos, mainPlayerFrameManager);
            AddGameObject(_mainCharacter);

            float yOffset = 0;
            float xOffset = 0;
            for (int i = 0; i < 4; i++)
            {
                yOffset += ((float)i) * 200;

                for (int j = 0; j < 4; j++)
                {
                    xOffset += ((float)j) * 200;
                    var buildingSheetsNormal = LoadSheets(AssetLoadUtils.BuildingsTextureAndDataPaths(CamLookAngle.NORMAL), true);
                    var buildingSheetsMid = LoadSheets(AssetLoadUtils.BuildingsTextureAndDataPaths(CamLookAngle.MID), true);
                    var buildingSheetsLow = LoadSheets(AssetLoadUtils.BuildingsTextureAndDataPaths(CamLookAngle.LOW), true);
                    var buildingSheetsGround = LoadSheets(AssetLoadUtils.BuildingsTextureAndDataPaths(CamLookAngle.GROUND), true);



                    var frameManager = new PropFrameManager(buildingSheetsNormal, _camera, buildingSheetsMid, buildingSheetsLow, buildingSheetsGround);
                    var position = new Vector3((_viewportWidth / 2) + xOffset, (_viewportHeight / 2) + yOffset, 0);
                    var envObj = new EnvironmentObject(position, frameManager);
                    AddGameObject(envObj);
                    
                    
                }

                
                xOffset = 0;

            }


        }

        private List<SpriteSheet> LoadSheets(List<SpriteSheetPacket> paths, bool hasFrames=false)
        {
            
            var sheets = new List<SpriteSheet>();
            foreach (var path in paths)
            {
                if (hasFrames)
                {
                    var loadResult = LoadTextureSequence(path.TexturePath, 0, 31);
                    var metaData = new SpriteSheetData(path.MetaDataPath);
                    var sheet = new SpriteSheet(loadResult.Textures, metaData, path.AnimationState);
                    sheets.Add(sheet);
                }
                else
                {
                    var loadResult = LoadTexture(path.TexturePath);
                    var metaData = new SpriteSheetData(path.MetaDataPath);
                    var sheet = new SpriteSheet(loadResult.LoadedTexture, metaData, path.AnimationState);
                    sheets.Add(sheet);
                }
            }
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
                        Vector3 goalPosition = _mainCharacter.Move(playerMoveCmd.GetDirection());

                        //Check Collisions, and if valid, then move
                        foreach (var gameObject in GameObjects)
                        {
                            if (gameObject is EnvironmentObject)
                            {
                                var envObject = (EnvironmentObject)gameObject;
                                playerCollided = _mainCharacter.CompareColliders(goalPosition, envObject.GetColliders());
                                if (playerCollided)
                                {
                                    break;
                                }
                            }
                        }
                        if (!playerCollided)
                        {
                            _mainCharacter.Position = goalPosition;
                            _camera.CameraPosition = new Vector3(_mainCharacter.Position.X, _mainCharacter.Position.Y, 0);
                        } 
                    }
                    
                    if (cmd is GameplayInputCommand.ChangeAnimationState)
                    {
                        var changeStateCmd = (GameplayInputCommand.ChangeAnimationState)cmd;
                        _mainCharacter.ChangeState(changeStateCmd.State);
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
                            if (gameObject is CharacterObject)
                            {
                                var charObject = (CharacterObject)gameObject;
                                charObject.ShiftDrawDirection(1);
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
                            if (gameObject is CharacterObject)
                            {
                                var charObject = (CharacterObject)gameObject;
                                charObject.ShiftDrawDirection(-1);
                            }

                        }
                        _camera.RotateCameraRight();
                    }
                    if (cmd is GameplayInputCommand.CameraRotateUp)
                    {
                        _camera.RotateCameraUp();
                    }
                    if (cmd is GameplayInputCommand.CameraRotateDown)
                    {
                        _camera.RotateCameraDown();
                    }


                }
            );
        }

        public override void Update(GameTime gameTime)
        {
            _mainCharacter.UpdateCharacter();
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }

    
}