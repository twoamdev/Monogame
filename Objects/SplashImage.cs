using Engine.Objects.Base;
using Engine.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace Objects
{
    public class SplashImage : BaseGameObject
    {
        public SplashImage(SpriteSheet sheet)
        {
            _sheet = sheet;
        }
    }
}
