using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapConversation
{
	// Token: 0x02000116 RID: 278
	public class MapConversationScreenButtonWidget : ButtonWidget
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00028971 File Offset: 0x00026B71
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00028979 File Offset: 0x00026B79
		public Widget ConversationParent { get; set; }

		// Token: 0x06000E98 RID: 3736 RVA: 0x00028982 File Offset: 0x00026B82
		public MapConversationScreenButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0002898B File Offset: 0x00026B8B
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00028993 File Offset: 0x00026B93
		public bool IsBarterActive
		{
			get
			{
				return this._isBarterActive;
			}
			set
			{
				if (this._isBarterActive != value)
				{
					this._isBarterActive = value;
					this.ConversationParent.IsVisible = !this.IsBarterActive;
				}
			}
		}

		// Token: 0x040006B6 RID: 1718
		private bool _isBarterActive;
	}
}
