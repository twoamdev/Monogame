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


        public override void LoadContent()
        {
            var startPos = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            var startCamPos = new Vector2(startPos.X, startPos.Y);
            _camera = new ViewportCamera(startCamPos, _viewportWidth, _viewportHeight);

            var mainPlayerSheets = LoadSheets(AssetLoadUtils.PlayerTextureAndDataPaths());
            var mainPlayerFrameManager = new CharacterFrameManager(mainPlayerSheets, _camera);
            _mainCharacter = new CharacterObject(mainPlayerFrameManager, startPos);
            AddGameObject(_mainCharacter);

            var buildingSheets = LoadSheets(AssetLoadUtils.BuildingsTextureAndDataPaths(), true);
            var yOffset = 0;
            foreach(var sheet in buildingSheets)
            {
                var frameManager = new PropFrameManager(sheet, _camera);
                
                var position = new Vector2(_viewportWidth / 2, (_viewportHeight / 2) + yOffset);
                var envObj = new EnvironmentObject(frameManager, position);
                AddGameObject(envObj);
                yOffset += (int) frameManager.FrameSize.Y * 2;
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
                    var sheet = new SpriteSheet(loadResult.HasErrorTexture,
                    loadResult.Textures, metaData, path.AnimationState);
                    sheets.Add(sheet);
                }
                else
                {
                    var loadResult = LoadTexture(path.TexturePath);
                    var metaData = new SpriteSheetData(path.MetaDataPath);
                    var sheet = new SpriteSheet(loadResult.IsErrorTexture,
                    loadResult.LoadedTexture, metaData, path.AnimationState);
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
                        _mainCharacter.Move(playerMoveCmd.GetDirection());
                        _camera.CameraPosition = new Vector2(_mainCharacter.Position.X, _mainCharacter.Position.Y);


                    }
                    if(cmd is GameplayInputCommand.ChangeAnimationState)
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

                }
            );

            
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }

    
}