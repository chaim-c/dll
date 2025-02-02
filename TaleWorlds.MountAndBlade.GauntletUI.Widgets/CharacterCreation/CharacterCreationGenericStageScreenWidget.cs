using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterCreation
{
	// Token: 0x0200017B RID: 379
	public class CharacterCreationGenericStageScreenWidget : Widget
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x000358B3 File Offset: 0x00033AB3
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x000358BB File Offset: 0x00033ABB
		public ButtonWidget NextButton { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x000358C4 File Offset: 0x00033AC4
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x000358CC File Offset: 0x00033ACC
		public ButtonWidget PreviousButton { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x000358D5 File Offset: 0x00033AD5
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x000358DD File Offset: 0x00033ADD
		public ListPanel ItemList { get; set; }

		// Token: 0x06001394 RID: 5012 RVA: 0x000358E6 File Offset: 0x00033AE6
		public CharacterCreationGenericStageScreenWidget(UIContext context) : base(context)
		{
		}
	}
}
