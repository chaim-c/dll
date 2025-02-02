using System;
using System.Collections.Generic;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.Localization
{
	// Token: 0x0200000B RID: 11
	public class SaveableLocalizationTypeDefiner : SaveableTypeDefiner
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000042AF File Offset: 0x000024AF
		public SaveableLocalizationTypeDefiner() : base(20000)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000042BC File Offset: 0x000024BC
		protected override void DefineClassTypes()
		{
			base.AddClassDefinition(typeof(TextObject), 1, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000042D0 File Offset: 0x000024D0
		protected override void DefineContainerDefinitions()
		{
			base.ConstructContainerDefinition(typeof(Dictionary<string, TextObject>));
		}
	}
}
