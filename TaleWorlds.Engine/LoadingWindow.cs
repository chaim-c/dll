using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000053 RID: 83
	public static class LoadingWindow
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000053D4 File Offset: 0x000035D4
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x000053DB File Offset: 0x000035DB
		public static bool IsLoadingWindowActive { get; private set; }

		// Token: 0x06000711 RID: 1809 RVA: 0x000053E3 File Offset: 0x000035E3
		public static void Initialize(ILoadingWindowManager loadingWindowManager)
		{
			LoadingWindow._loadingWindowManager = loadingWindowManager;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000053EB File Offset: 0x000035EB
		public static void Destroy()
		{
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			LoadingWindow._loadingWindowManager = null;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000053FF File Offset: 0x000035FF
		public static void DisableGlobalLoadingWindow()
		{
			if (LoadingWindow._loadingWindowManager == null)
			{
				return;
			}
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow._loadingWindowManager.DisableLoadingWindow();
				Utilities.DisableGlobalLoadingWindow();
				Utilities.OnLoadingWindowDisabled();
			}
			LoadingWindow.IsLoadingWindowActive = false;
			Utilities.DebugSetGlobalLoadingWindowState(false);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00005430 File Offset: 0x00003630
		public static bool GetGlobalLoadingWindowState()
		{
			return LoadingWindow.IsLoadingWindowActive;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00005437 File Offset: 0x00003637
		public static void EnableGlobalLoadingWindow()
		{
			if (LoadingWindow._loadingWindowManager == null)
			{
				return;
			}
			LoadingWindow.IsLoadingWindowActive = true;
			Utilities.DebugSetGlobalLoadingWindowState(true);
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow._loadingWindowManager.EnableLoadingWindow();
				Utilities.OnLoadingWindowEnabled();
			}
		}

		// Token: 0x040000AA RID: 170
		private static ILoadingWindowManager _loadingWindowManager;
	}
}
