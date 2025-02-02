using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DE RID: 478
	public class MBWindowManager
	{
		// Token: 0x06001ADB RID: 6875 RVA: 0x0005D326 File Offset: 0x0005B526
		public static float WorldToScreen(Camera camera, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w)
		{
			return MBAPI.IMBWindowManager.WorldToScreen(camera.Pointer, worldSpacePosition, ref screenX, ref screenY, ref w);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0005D340 File Offset: 0x0005B540
		public static float WorldToScreenInsideUsableArea(Camera camera, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w)
		{
			float result = MBAPI.IMBWindowManager.WorldToScreen(camera.Pointer, worldSpacePosition, ref screenX, ref screenY, ref w);
			screenX -= (Screen.RealScreenResolutionWidth - ScreenManager.UsableArea.X * Screen.RealScreenResolutionWidth) / 2f;
			screenY -= (Screen.RealScreenResolutionHeight - ScreenManager.UsableArea.Y * Screen.RealScreenResolutionHeight) / 2f;
			return result;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0005D3AA File Offset: 0x0005B5AA
		public static float WorldToScreenWithFixedZ(Camera camera, Vec3 cameraPosition, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w)
		{
			return MBAPI.IMBWindowManager.WorldToScreenWithFixedZ(camera.Pointer, cameraPosition, worldSpacePosition, ref screenX, ref screenY, ref w);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0005D3C3 File Offset: 0x0005B5C3
		public static void ScreenToWorld(Camera camera, float screenX, float screenY, float w, ref Vec3 worldSpacePosition)
		{
			MBAPI.IMBWindowManager.ScreenToWorld(camera.Pointer, screenX, screenY, w, ref worldSpacePosition);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0005D3DA File Offset: 0x0005B5DA
		public static void PreDisplay()
		{
			MBAPI.IMBWindowManager.PreDisplay();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0005D3E6 File Offset: 0x0005B5E6
		public static void DontChangeCursorPos()
		{
			MBAPI.IMBWindowManager.DontChangeCursorPos();
		}
	}
}
