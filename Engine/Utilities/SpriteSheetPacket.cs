using System;
using Engine.Enum;
namespace Engine.Utilities
{
	public class SpriteSheetPacket
	{
		private string _texturePath;
		private string _metaDataPath;
		private AnimationStates _state;

		public SpriteSheetPacket(string texturePath, string metaDataPath, AnimationStates state)
		{
			_texturePath = texturePath;
			_metaDataPath = metaDataPath;
			_state = state;
		}

		public string TexturePath
		{
			get { return _texturePath; }
		}

        public string MetaDataPath
        {
            get { return _metaDataPath; }
        }

        public AnimationStates AnimationState
        {
            get { return _state; }
        }
    }
}

