using System;
using Engine.Enum;
namespace Engine.Utilities
{
	public class SpriteSheetPacket
	{
		private string _texturePath;
		private string _metaDataPath;
		private AnimationState _state;

		public SpriteSheetPacket(string texturePath, string metaDataPath, AnimationState state)
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

        public AnimationState AnimationState
        {
            get { return _state; }
        }
    }
}

