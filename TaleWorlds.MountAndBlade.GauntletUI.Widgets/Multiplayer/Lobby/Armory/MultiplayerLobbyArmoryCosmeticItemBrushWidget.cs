using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000AE RID: 174
	public class MultiplayerLobbyArmoryCosmeticItemBrushWidget : BrushWidget
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0001A68D File Offset: 0x0001888D
		public MultiplayerLobbyArmoryCosmeticItemBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001A696 File Offset: 0x00018896
		public override void SetState(string stateName)
		{
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001A698 File Offset: 0x00018898
		private void OnUsageChanged()
		{
			base.SetState(this.IsUsed ? "Selected" : "Default");
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001A6B4 File Offset: 0x000188B4
		private void OnRarityChanged()
		{
			switch (this.Rarity)
			{
			case 0:
			case 1:
				base.Brush = base.Context.GetBrush("MPLobby.Armory.CosmeticButton.Common");
				return;
			case 2:
				base.Brush = base.Context.GetBrush("MPLobby.Armory.CosmeticButton.Rare");
				return;
			case 3:
				base.Brush = base.Context.GetBrush("MPLobby.Armory.CosmeticButton.Unique");
				return;
			default:
				return;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001A723 File Offset: 0x00018923
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001A72B File Offset: 0x0001892B
		[Editor(false)]
		public bool IsUsed
		{
			get
			{
				return this._isUsed;
			}
			set
			{
				if (value != this._isUsed)
				{
					this._isUsed = value;
					base.OnPropertyChanged(value, "IsUsed");
					this.OnUsageChanged();
				}
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001A74F File Offset: 0x0001894F
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001A757 File Offset: 0x00018957
		[Editor(false)]
		public int Rarity
		{
			get
			{
				return this._rarity;
			}
			set
			{
				if (value != this._rarity)
				{
					this._rarity = value;
					base.OnPropertyChanged(value, "Rarity");
					this.OnRarityChanged();
				}
			}
		}

		// Token: 0x04000439 RID: 1081
		private const string BaseBrushName = "MPLobby.Armory.CosmeticButton";

		// Token: 0x0400043A RID: 1082
		private bool _isUsed;

		// Token: 0x0400043B RID: 1083
		private int _rarity;
	}
}
