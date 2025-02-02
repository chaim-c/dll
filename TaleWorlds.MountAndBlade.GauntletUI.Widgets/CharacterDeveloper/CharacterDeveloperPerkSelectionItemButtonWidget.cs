using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000173 RID: 371
	public class CharacterDeveloperPerkSelectionItemButtonWidget : ButtonWidget
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00034B58 File Offset: 0x00032D58
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00034B60 File Offset: 0x00032D60
		public Widget PerkSelectionIndicatorWidget { get; set; }

		// Token: 0x0600133A RID: 4922 RVA: 0x00034B69 File Offset: 0x00032D69
		public CharacterDeveloperPerkSelectionItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00034B74 File Offset: 0x00032D74
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.PerkSelectionIndicatorWidget != null)
			{
				if (base.ParentWidget.ChildCount == 1)
				{
					this.PerkSelectionIndicatorWidget.VerticalAlignment = VerticalAlignment.Center;
					return;
				}
				this.PerkSelectionIndicatorWidget.VerticalAlignment = ((base.GetSiblingIndex() % 2 == 0) ? VerticalAlignment.Bottom : VerticalAlignment.Top);
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00034BC4 File Offset: 0x00032DC4
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00034BCC File Offset: 0x00032DCC
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
		}
	}
}
