using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001BF RID: 447
	public static class DebugExtensions
	{
		// Token: 0x060019B7 RID: 6583 RVA: 0x0005B8CA File Offset: 0x00059ACA
		public static void RenderDebugCircleOnTerrain(Scene scene, MatrixFrame frame, float radius, uint color, bool depthCheck = true, bool isDotted = false)
		{
			MBAPI.IMBDebugExtensions.RenderDebugCircleOnTerrain(scene.Pointer, ref frame, radius, color, depthCheck, isDotted);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		public static void RenderDebugArcOnTerrain(Scene scene, MatrixFrame frame, float radius, float beginAngle, float endAngle, uint color, bool depthCheck = true, bool isDotted = false)
		{
			MBAPI.IMBDebugExtensions.RenderDebugArcOnTerrain(scene.Pointer, ref frame, radius, beginAngle, endAngle, color, depthCheck, isDotted);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0005B910 File Offset: 0x00059B10
		public static void RenderDebugLineOnTerrain(Scene scene, Vec3 position, Vec3 direction, uint color, bool depthCheck = true, float time = 0f, bool isDotted = false, float pointDensity = 1f)
		{
			MBAPI.IMBDebugExtensions.RenderDebugLineOnTerrain(scene.Pointer, position, direction, color, depthCheck, time, isDotted, pointDensity);
		}
	}
}
