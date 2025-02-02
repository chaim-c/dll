using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000120 RID: 288
	public class DecisionSupporterGridWidget : GridWidget
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00029711 File Offset: 0x00027911
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00029719 File Offset: 0x00027919
		public int VisibleCount { get; set; } = 4;

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00029722 File Offset: 0x00027922
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0002972A File Offset: 0x0002792A
		public TextWidget MoreTextWidget { get; set; }

		// Token: 0x06000EEB RID: 3819 RVA: 0x00029733 File Offset: 0x00027933
		public DecisionSupporterGridWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00029743 File Offset: 0x00027943
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.IsVisible = (child.GetSiblingIndex() < this.VisibleCount);
			this.UpdateMoreText();
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00029768 File Offset: 0x00027968
		private void UpdateMoreText()
		{
			if (this.MoreTextWidget != null)
			{
				this.MoreTextWidget.IsVisible = (base.ChildCount > this.VisibleCount);
				if (this.MoreTextWidget.IsVisible)
				{
					this.MoreTextWidget.Text = "+" + (base.ChildCount - this.VisibleCount);
				}
			}
		}
	}
}
