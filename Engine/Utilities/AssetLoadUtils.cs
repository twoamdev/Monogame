using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Engine.Enum;
using Newtonsoft.Json.Linq;

namespace Engine.Utilities
{
	public static class AssetLoadUtils
	{
        private static string _jsonRootDir = "/Users/bennelson/Documents/Git_Repositories/GuildOfHeaven/GuildOfHeaven/Content/";

        private static string _mainPlayerDirExtension = "assets/character/test/";
        private static string _mainPlayerAssetLabel = "joe";
        private static List<AnimationState> _mainPlayerAnimationStates = new List<AnimationState>()
        {
            AnimationState.IDLE, AnimationState.WALKING, AnimationState.RUNNING,
            AnimationState.ROLLING, AnimationState.JUMPING, AnimationState.FALLING,
            AnimationState.LANDING
        };

        private static string _buildingsDirExtension = "assets/building/test/";
        private static List<string> _buildingLabels = new List<string>()
        {
            "buildingA"
            
        };


        public static List<SpriteSheetPacket> PlayerTextureAndDataPaths(string angle)
        {
            List<SpriteSheetPacket> packets = new List<SpriteSheetPacket>();
            

            foreach(AnimationState state in _mainPlayerAnimationStates)
            {
                string stateLiteralName = AnimationState.GetName(typeof(AnimationState), state) + "-" + angle;
                string texturePath =  _mainPlayerDirExtension + _mainPlayerAssetLabel + "/" + stateLiteralName + "/" + _mainPlayerAssetLabel + "_" + stateLiteralName + "_" + "spriteSheet";
                string metaDataPath = _jsonRootDir + _mainPlayerDirExtension + _mainPlayerAssetLabel + "/" + stateLiteralName + "/" + _mainPlayerAssetLabel + "_" + stateLiteralName + "_" + "metaData.json";
                var packet = new SpriteSheetPacket(texturePath, metaDataPath, state);
                packets.Add(packet);
            }
            return packets;
        }

        public static List<SpriteSheetPacket> BuildingsTextureAndDataPaths(string angle)
        {
            List<SpriteSheetPacket> packets = new List<SpriteSheetPacket>();

            string stateLiteralName = AnimationState.GetName(typeof(AnimationState), AnimationState.STATIC) + "-" + angle;
            foreach (string label in _buildingLabels) { 
                string texturePath = _buildingsDirExtension + label + "/" + stateLiteralName + "/" + label + "_" + stateLiteralName + "_" + "spriteSheet";
                string metaDataPath = _jsonRootDir + _buildingsDirExtension + label + "/" + stateLiteralName + "/" + label + "_" + stateLiteralName + "_" + "metaData.json";
                var packet = new SpriteSheetPacket(texturePath, metaDataPath, AnimationState.STATIC);
                packets.Add(packet);
            }
            return packets;
        }

    }
}

