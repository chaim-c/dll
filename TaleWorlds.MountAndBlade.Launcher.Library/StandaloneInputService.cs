using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.TwoDimension.Standalone;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000009 RID: 9
	public class StandaloneInputService : IInputService
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002C94 File Offset: 0x00000E94
		public StandaloneInputService(GraphicsForm graphicsForm)
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C9C File Offset: 0x00000E9C
		bool IInputService.MouseEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C9F File Offset: 0x00000E9F
		bool IInputService.KeyboardEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002CA2 File Offset: 0x00000EA2
		bool IInputService.GamepadEnabled
		{
			get
			{
				return false;
			}
		}
	}
}
