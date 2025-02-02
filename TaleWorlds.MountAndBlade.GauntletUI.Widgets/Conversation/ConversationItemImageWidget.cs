using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000161 RID: 353
	public class ConversationItemImageWidget : ImageWidget
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00032AF1 File Offset: 0x00030CF1
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00032AF9 File Offset: 0x00030CF9
		public Brush NormalBrush { get; set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00032B02 File Offset: 0x00030D02
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00032B0A File Offset: 0x00030D0A
		public Brush SpecialBrush { get; set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00032B13 File Offset: 0x00030D13
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00032B1B File Offset: 0x00030D1B
		public bool IsSpecial { get; set; }

		// Token: 0x06001288 RID: 4744 RVA: 0x00032B24 File Offset: 0x00030D24
		public ConversationItemImageWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00032B2D File Offset: 0x00030D2D
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				base.Brush = (this.IsSpecial ? this.SpecialBrush : this.NormalBrush);
				this._isInitialized = true;
			}
		}

		// Token: 0x04000872 RID: 2162
		private bool _isInitialized;
	}
}
