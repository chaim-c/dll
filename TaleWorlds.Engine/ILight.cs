using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200002B RID: 43
	[ApplicationInterfaceBase]
	internal interface ILight
	{
		// Token: 0x06000401 RID: 1025
		[EngineMethod("create_point_light", false)]
		Light CreatePointLight(float lightRadius);

		// Token: 0x06000402 RID: 1026
		[EngineMethod("set_radius", false)]
		void SetRadius(UIntPtr lightpointer, float radius);

		// Token: 0x06000403 RID: 1027
		[EngineMethod("set_light_flicker", false)]
		void SetLightFlicker(UIntPtr lightpointer, float magnitude, float interval);

		// Token: 0x06000404 RID: 1028
		[EngineMethod("enable_shadow", false)]
		void EnableShadow(UIntPtr lightpointer, bool shadowEnabled);

		// Token: 0x06000405 RID: 1029
		[EngineMethod("is_shadow_enabled", false)]
		bool IsShadowEnabled(UIntPtr lightpointer);

		// Token: 0x06000406 RID: 1030
		[EngineMethod("set_volumetric_properties", false)]
		void SetVolumetricProperties(UIntPtr lightpointer, bool volumelightenabled, float volumeparameter);

		// Token: 0x06000407 RID: 1031
		[EngineMethod("set_visibility", false)]
		void SetVisibility(UIntPtr lightpointer, bool value);

		// Token: 0x06000408 RID: 1032
		[EngineMethod("get_radius", false)]
		float GetRadius(UIntPtr lightpointer);

		// Token: 0x06000409 RID: 1033
		[EngineMethod("set_shadows", false)]
		void SetShadows(UIntPtr lightPointer, int shadowType);

		// Token: 0x0600040A RID: 1034
		[EngineMethod("set_light_color", false)]
		void SetLightColor(UIntPtr lightpointer, Vec3 color);

		// Token: 0x0600040B RID: 1035
		[EngineMethod("get_light_color", false)]
		Vec3 GetLightColor(UIntPtr lightpointer);

		// Token: 0x0600040C RID: 1036
		[EngineMethod("set_intensity", false)]
		void SetIntensity(UIntPtr lightPointer, float value);

		// Token: 0x0600040D RID: 1037
		[EngineMethod("get_intensity", false)]
		float GetIntensity(UIntPtr lightPointer);

		// Token: 0x0600040E RID: 1038
		[EngineMethod("release", false)]
		void Release(UIntPtr lightpointer);

		// Token: 0x0600040F RID: 1039
		[EngineMethod("set_frame", false)]
		void SetFrame(UIntPtr lightPointer, ref MatrixFrame frame);

		// Token: 0x06000410 RID: 1040
		[EngineMethod("get_frame", false)]
		void GetFrame(UIntPtr lightPointer, out MatrixFrame result);
	}
}
