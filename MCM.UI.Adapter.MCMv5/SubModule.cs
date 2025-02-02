using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Internal.Extensions;
using MCM.UI.Adapter.MCMv5.Properties;
using MCM.UI.Adapter.MCMv5.Providers;
using TaleWorlds.MountAndBlade;

namespace MCM.UI.Adapter.MCMv5
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SubModule : MBSubModuleBase
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021D4 File Offset: 0x000003D4
		private static void OnAfterSetInitialModuleScreenAsRootScreen()
		{
			IEnumerable<IExternalSettingsProvider> service = GenericServiceProvider.GetService<IEnumerable<IExternalSettingsProvider>>();
			IEnumerable<IExternalSettingsProviderHasInitialize> enumerable = ((service != null) ? service.OfType<IExternalSettingsProviderHasInitialize>() : null) ?? Enumerable.Empty<IExternalSettingsProviderHasInitialize>();
			foreach (IExternalSettingsProviderHasInitialize hasInitialize in enumerable)
			{
				hasInitialize.Initialize();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000223C File Offset: 0x0000043C
		private Harmony Harmony { get; } = new Harmony("MCM.UI.Adapter.MCMv5");

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002244 File Offset: 0x00000444
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000224C File Offset: 0x0000044C
		private bool ServiceRegistrationWasCalled { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002258 File Offset: 0x00000458
		public void OnServiceRegistration()
		{
			this.ServiceRegistrationWasCalled = true;
			IGenericServiceContainer services = this.GetServiceContainer();
			bool flag = services != null;
			if (flag)
			{
				services.AddSettingsPropertyDiscoverer<MCMv5AttributeSettingsPropertyDiscoverer>();
				services.AddSettingsPropertyDiscoverer<MCMv5FluentSettingsPropertyDiscoverer>();
				services.AddExternalSettingsProvider<MCMv5ExternalSettingsProvider>();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002294 File Offset: 0x00000494
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			bool flag = !this.ServiceRegistrationWasCalled;
			if (flag)
			{
				this.OnServiceRegistration();
			}
			this.Harmony.Patch(AccessTools2.Method(typeof(Module), "SetInitialModuleScreenAsRootScreen", null, null, true), null, new HarmonyMethod(AccessTools2.Method(typeof(SubModule), "OnAfterSetInitialModuleScreenAsRootScreen", null, null, true)), null, null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002300 File Offset: 0x00000500
		protected override void OnSubModuleUnloaded()
		{
			base.OnSubModuleUnloaded();
			this.Harmony.Unpatch(AccessTools2.Method(typeof(Module), "SetInitialModuleScreenAsRootScreen", null, null, true), AccessTools2.Method(typeof(SubModule), "OnAfterSetInitialModuleScreenAsRootScreen", null, null, true));
		}
	}
}
