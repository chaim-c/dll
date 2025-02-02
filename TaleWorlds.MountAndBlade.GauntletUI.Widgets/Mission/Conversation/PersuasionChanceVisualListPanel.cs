using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.Conversation
{
	// Token: 0x020000F6 RID: 246
	public class PersuasionChanceVisualListPanel : ListPanel
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0002424C File Offset: 0x0002244C
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00024254 File Offset: 0x00022454
		public bool IsFailChance { get; set; }

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002425D File Offset: 0x0002245D
		public PersuasionChanceVisualListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00024266 File Offset: 0x00022466
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			base.IsVisible = (!this.IsFailChance && this.ChanceValue > 0);
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00024289 File Offset: 0x00022489
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00024291 File Offset: 0x00022491
		public int ChanceValue
		{
			get
			{
				return this._chanceValue;
			}
			set
			{
				if (this._chanceValue != value)
				{
					this._chanceValue = value;
					base.OnPropertyChanged(value, "ChanceValue");
				}
			}
		}

		// Token: 0x04000600 RID: 1536
		private int _chanceValue;
	}
}
