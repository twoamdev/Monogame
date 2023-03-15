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
        

        public PropFrameManager(List<SpriteSheet> sheets, ViewportCamera camera, List<SpriteSheet> midSheets, List<SpriteSheet> lowSheets, List<SpriteSheet> groundSheets) : base(sheets, camera ,midSheets, lowSheets, groundSheets)
		{
            SpriteSheets = sheets;
            _spriteSheetsMidCam = midSheets;
            _spriteSheetsLowCam = lowSheets;
            _spriteSheetsGroundCam = groundSheets;
        }

        

    }
}

