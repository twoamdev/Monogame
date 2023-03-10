using System;

using Engine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects
{
	public class ViewportCamera : BaseCameraObject
	{
		
		public ViewportCamera(Vector3 cameraPosition, int viewportWidth, int viewportHeight)
		{
			CameraPosition = cameraPosition;
			ViewWidth = viewportWidth;
			ViewHeight = viewportHeight;
		}
	}
}

