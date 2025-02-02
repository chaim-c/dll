using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000081 RID: 129
	public static class Screen
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0000AB93 File Offset: 0x00008D93
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0000AB9A File Offset: 0x00008D9A
		public static float RealScreenResolutionWidth { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0000ABA9 File Offset: 0x00008DA9
		public static float RealScreenResolutionHeight { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		public static Vec2 RealScreenResolution
		{
			get
			{
				return new Vec2(Screen.RealScreenResolutionWidth, Screen.RealScreenResolutionHeight);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0000ABC2 File Offset: 0x00008DC2
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0000ABC9 File Offset: 0x00008DC9
		public static float AspectRatio { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public static Vec2 DesktopResolution { get; private set; }

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		internal static void Update()
		{
			Screen.RealScreenResolutionWidth = EngineApplicationInterface.IScreen.GetRealScreenResolutionWidth();
			Screen.RealScreenResolutionHeight = EngineApplicationInterface.IScreen.GetRealScreenResolutionHeight();
			Screen.AspectRatio = EngineApplicationInterface.IScreen.GetAspectRatio();
			Screen.DesktopResolution = new Vec2(EngineApplicationInterface.IScreen.GetDesktopWidth(), EngineApplicationInterface.IScreen.GetDesktopHeight());
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000AC38 File Offset: 0x00008E38
		public static bool GetMouseVisible()
		{
			return EngineApplicationInterface.IScreen.GetMouseVisible();
		}
	}
}
