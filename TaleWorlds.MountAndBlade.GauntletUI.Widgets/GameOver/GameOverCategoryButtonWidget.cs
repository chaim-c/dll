using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GameOver
{
	// Token: 0x02000143 RID: 323
	public class GameOverCategoryButtonWidget : ButtonWidget
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0003000D File Offset: 0x0002E20D
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00030015 File Offset: 0x0002E215
		public string CategoryID { get; set; }

		// Token: 0x06001126 RID: 4390 RVA: 0x0003001E File Offset: 0x0002E21E
		public GameOverCategoryButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00030027 File Offset: 0x0002E227
		protected override void HandleClick()
		{
			this.HandleSoundEvent();
			base.HandleClick();
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00030035 File Offset: 0x0002E235
		private void HandleSoundEvent()
		{
			base.EventFired(this.CategoryID, Array.Empty<object>());
		}
	}
}
