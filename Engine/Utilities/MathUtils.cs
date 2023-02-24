using System;
namespace Engine.Utilities
{
	public static class MathUtils
	{
		public static float Remap(float value, float sourceMin, float sourceMax, float destinationMin, float destinationMax)
        {
            return ((value - sourceMin) / ((destinationMin - sourceMin) * (destinationMax - sourceMax) + sourceMax)) * destinationMax;
        }

        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static float ToRadians(float degrees)
        {
            return degrees * (float) (Math.PI / 180.0);
        }
    }
}

