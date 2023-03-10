using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Engine.Enum;
using Newtonsoft.Json.Linq;

namespace Engine.Utilities
{
	public static class AssetLoadUtils
	{
        private static string _jsonRootDir = "/Users/bennelson/Documents/Git_Repositories/GuildOfHeaven/GuildOfHeaven/Content/";

        private static string _mainPlayerDirExtension = "assets/character/test/";
        private static string _mainPlayerAssetLabel = "joe";
        private static List<AnimationStates> _mainPlayerAnimationStates = new List<AnimationStates>()
        {
            AnimationStates.IDLE, AnimationStates.WALKING, AnimationStates.RUNNING,
            AnimationStates.ROLLING, AnimationStates.JUMPING, AnimationStates.FALLING,
            AnimationStates.LANDING
        };

        private static string _buildingsDirExtension = "assets/building/test/";
        private static List<string> _buildingLabels = new List<string>()
        {
            "buildingA", "buildingB", "buildingC"
            
        };


        public static List<SpriteSheetPacket> PlayerTextureAndDataPaths()
        {
            List<SpriteSheetPacket> packets = new List<SpriteSheetPacket>();
            foreach(AnimationStates state in _mainPlayerAnimationStates)
            {
                string stateLiteralName = AnimationStates.GetName(typeof(AnimationStates), state);
                string texturePath =  _mainPlayerDirExtension + _mainPlayerAssetLabel + "/" + stateLiteralName + "/" + _mainPlayerAssetLabel + "_" + stateLiteralName + "_" + "spriteSheet";
                string metaDataPath = _jsonRootDir + _mainPlayerDirExtension + _mainPlayerAssetLabel + "/" + stateLiteralName + "/" + _mainPlayerAssetLabel + "_" + stateLiteralName + "_" + "metaData.json";
                var packet = new SpriteSheetPacket(texturePath, metaDataPath, state);
                packets.Add(packet);
            }
            return packets;
        }

        public static List<SpriteSheetPacket> BuildingsTextureAndDataPaths()
        {
            List<SpriteSheetPacket> packets = new List<SpriteSheetPacket>();

            string stateLiteralName = AnimationStates.GetName(typeof(AnimationStates), AnimationStates.STATIC);
            foreach (string label in _buildingLabels) { 
                string texturePath = _buildingsDirExtension + label + "/" + label + "_" + stateLiteralName + "_" + "spriteSheet";
                string metaDataPath = _jsonRootDir + _buildingsDirExtension + label + "/"  + label + "_" + stateLiteralName + "_" + "metaData.json";
                var packet = new SpriteSheetPacket(texturePath, metaDataPath, AnimationStates.STATIC);
                packets.Add(packet);
            }
            return packets;
        }

    }
}

