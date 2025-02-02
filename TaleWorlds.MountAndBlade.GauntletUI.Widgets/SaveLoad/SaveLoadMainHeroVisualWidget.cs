using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.SaveLoad
{
	// Token: 0x02000055 RID: 85
	public class SaveLoadMainHeroVisualWidget : Widget
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000E5C3 File Offset: 0x0000C7C3
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000E5CB File Offset: 0x0000C7CB
		public Widget DefaultVisualWidget { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public SaveLoadHeroTableauWidget SaveLoadHeroTableau { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000E5E5 File Offset: 0x0000C7E5
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000E5ED File Offset: 0x0000C7ED
		public bool IsVisualDisabledForMemoryPurposes { get; set; }

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E5F6 File Offset: 0x0000C7F6
		public SaveLoadMainHeroVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E600 File Offset: 0x0000C800
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.DefaultVisualWidget != null)
			{
				if (this.IsVisualDisabledForMemoryPurposes)
				{
					this.DefaultVisualWidget.IsVisible = true;
					this.SaveLoadHeroTableau.IsVisible = false;
					return;
				}
				this.DefaultVisualWidget.IsVisible = (string.IsNullOrEmpty(this.SaveLoadHeroTableau.HeroVisualCode) || !this.SaveLoadHeroTableau.IsVersionCompatible);
				this.SaveLoadHeroTableau.IsVisible = !this.DefaultVisualWidget.IsVisible;
			}
		}
	}
}
