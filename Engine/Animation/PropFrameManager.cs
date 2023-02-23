using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Animation.Base;
using Engine.Utilities;
using Engine.Enum;

namespace Engine.Animation
{
	public class PropFrameManager : BaseFrameManager
	{
		public PropFrameManager(SpriteSheet sheet)
		{
			SpriteSheets = new List<SpriteSheet> {sheet};
        }

        
    }
}

