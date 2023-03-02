using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Animation.Base;
using Engine.Utilities;
using Engine.Enum;
using Engine.Objects;

namespace Engine.Animation
{
	public class PropFrameManager : BaseFrameManager
	{
        private float _rotationDegrees = 0.0f;
        private bool _negateDirection = false;
        private bool _directionRangeChanged = false;
        private const int CAMERA_DIRECTIONS = 32;
        

        public PropFrameManager(SpriteSheet sheet, ViewportCamera camera) : base(camera)
		{
			SpriteSheets = new List<SpriteSheet> {sheet};
        }

        

    }
}

