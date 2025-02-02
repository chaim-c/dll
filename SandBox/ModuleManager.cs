using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;

namespace SandBox
{
	// Token: 0x02000022 RID: 34
	public class ModuleManager : IModuleManager
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006C32 File Offset: 0x00004E32
		public string[] ModuleNames
		{
			get
			{
				return Utilities.GetModulesNames();
			}
		}
	}
}
