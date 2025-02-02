using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000C RID: 12
	public struct DependentVersionMissmatchItem
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003251 File Offset: 0x00001451
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003259 File Offset: 0x00001459
		public string MissmatchedModuleId { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003262 File Offset: 0x00001462
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000326A File Offset: 0x0000146A
		public List<Tuple<DependedModule, ApplicationVersion>> MissmatchedDependencies { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00003273 File Offset: 0x00001473
		public DependentVersionMissmatchItem(string missmatchedModuleId, List<Tuple<DependedModule, ApplicationVersion>> missmatchedDependencies)
		{
			this.MissmatchedModuleId = missmatchedModuleId;
			this.MissmatchedDependencies = missmatchedDependencies;
		}
	}
}
