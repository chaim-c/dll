using System;
using TaleWorlds.Engine.InputSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.Engine
{
	// Token: 0x02000041 RID: 65
	public static class EngineController
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x000032BC File Offset: 0x000014BC
		[EngineCallback]
		internal static void Initialize()
		{
			IInputContext debugInput = null;
			Input.Initialize(new EngineInputManager(), debugInput);
			Common.PlatformFileHelper = new PlatformFileHelperPC(Utilities.GetApplicationName());
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000032E5 File Offset: 0x000014E5
		[EngineCallback]
		internal static void OnConfigChange()
		{
			NativeConfig.OnConfigChanged();
			if (EngineController.ConfigChange != null)
			{
				EngineController.ConfigChange();
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060005AF RID: 1455 RVA: 0x00003300 File Offset: 0x00001500
		// (remove) Token: 0x060005B0 RID: 1456 RVA: 0x00003334 File Offset: 0x00001534
		public static event Action ConfigChange;

		// Token: 0x060005B1 RID: 1457 RVA: 0x00003367 File Offset: 0x00001567
		[EngineCallback]
		internal static void OnConstrainedStateChange(bool isConstrained)
		{
			Action<bool> onConstrainedStateChanged = EngineController.OnConstrainedStateChanged;
			if (onConstrainedStateChanged == null)
			{
				return;
			}
			onConstrainedStateChanged(isConstrained);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060005B2 RID: 1458 RVA: 0x0000337C File Offset: 0x0000157C
		// (remove) Token: 0x060005B3 RID: 1459 RVA: 0x000033B0 File Offset: 0x000015B0
		public static event Action<bool> OnConstrainedStateChanged;

		// Token: 0x060005B4 RID: 1460 RVA: 0x000033E3 File Offset: 0x000015E3
		internal static void OnApplicationTick(float dt)
		{
			Input.Update();
			Screen.Update();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000033F0 File Offset: 0x000015F0
		[EngineCallback]
		public static string GetVersionStr()
		{
			return ApplicationVersion.FromParametersFile(null).ToString();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00003414 File Offset: 0x00001614
		[EngineCallback]
		public static string GetApplicationPlatformName()
		{
			return ApplicationPlatform.CurrentPlatform.ToString();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00003434 File Offset: 0x00001634
		[EngineCallback]
		public static string GetModulesVersionStr()
		{
			string text = "";
			foreach (ModuleInfo moduleInfo in ModuleHelper.GetModules())
			{
				text = string.Concat(new object[]
				{
					text,
					moduleInfo.Name,
					"#",
					moduleInfo.Version,
					"\n"
				});
			}
			return text;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000034B8 File Offset: 0x000016B8
		[EngineCallback]
		internal static void OnControllerDisconnection()
		{
			ScreenManager.OnControllerDisconnect();
		}
	}
}
