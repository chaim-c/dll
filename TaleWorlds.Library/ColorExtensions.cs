using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000020 RID: 32
	public static class ColorExtensions
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000044E0 File Offset: 0x000026E0
		public static Color AddFactorInHSB(this Color rgbColor, float hueDifference, float saturationDifference, float brighnessDifference)
		{
			Vec3 vec = MBMath.RGBtoHSB(rgbColor);
			vec.x = (vec.x + hueDifference + 360f) % 360f;
			vec.y = MBMath.ClampFloat(vec.y + saturationDifference, 0f, 1f);
			vec.z = MBMath.ClampFloat(vec.z + brighnessDifference, 0f, 1f);
			return MBMath.HSBtoRGB(vec.x, vec.y, vec.z, rgbColor.Alpha);
		}
	}
}
