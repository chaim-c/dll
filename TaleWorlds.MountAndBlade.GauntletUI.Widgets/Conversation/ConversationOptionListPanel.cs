using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000163 RID: 355
	public class ConversationOptionListPanel : ListPanel
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00032BE3 File Offset: 0x00030DE3
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x00032BEB File Offset: 0x00030DEB
		public ButtonWidget OptionButtonWidget { get; set; }

		// Token: 0x06001293 RID: 4755 RVA: 0x00032BF4 File Offset: 0x00030DF4
		public ConversationOptionListPanel(UIContext context) : base(context)
		{
		}
	}
}
