using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C9 RID: 201
	public class MultiplayerClassLoadoutTroopTupleVisualWidget : Widget
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0001DAB1 File Offset: 0x0001BCB1
		public MultiplayerClassLoadoutTroopTupleVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001DABC File Offset: 0x0001BCBC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				base.Sprite = base.Context.SpriteData.GetSprite("MPClassLoadout\\TroopTupleImages\\" + this.TroopTypeCode + "1");
				base.Sprite = base.Sprite;
				base.SuggestedWidth = (float)base.Sprite.Width;
				base.SuggestedHeight = (float)base.Sprite.Height;
				this._initialized = true;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001DB3A File Offset: 0x0001BD3A
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0001DB42 File Offset: 0x0001BD42
		public string FactionCode
		{
			get
			{
				return this._factionCode;
			}
			set
			{
				if (value != this._factionCode)
				{
					this._factionCode = value;
					base.OnPropertyChanged<string>(value, "FactionCode");
				}
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001DB65 File Offset: 0x0001BD65
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0001DB6D File Offset: 0x0001BD6D
		public string TroopTypeCode
		{
			get
			{
				return this._troopTypeCode;
			}
			set
			{
				if (value != this._troopTypeCode)
				{
					this._troopTypeCode = value;
					base.OnPropertyChanged<string>(value, "TroopTypeCode");
				}
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001DB90 File Offset: 0x0001BD90
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001DB98 File Offset: 0x0001BD98
		public bool UseSecondary
		{
			get
			{
				return this._useSecondary;
			}
			set
			{
				if (value != this._useSecondary)
				{
					this._useSecondary = value;
					base.OnPropertyChanged(value, "UseSecondary");
				}
			}
		}

		// Token: 0x040004CA RID: 1226
		private bool _initialized;

		// Token: 0x040004CB RID: 1227
		private string _factionCode;

		// Token: 0x040004CC RID: 1228
		private string _troopTypeCode;

		// Token: 0x040004CD RID: 1229
		private bool _useSecondary;
	}
}
