using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x0200009B RID: 155
	public class MultiplayerLobbyGameTypeCardButtonWidget : ButtonWidget
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x00018826 File Offset: 0x00016A26
		public MultiplayerLobbyGameTypeCardButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00018830 File Offset: 0x00016A30
		protected override void RefreshState()
		{
			base.RefreshState();
			if (!base.OverrideDefaultStateSwitchingEnabled)
			{
				if (base.IsDisabled)
				{
					this.SetState(base.IsSelected ? "SelectedDisabled" : "Disabled");
				}
				else if (base.IsSelected)
				{
					this.SetState("Selected");
				}
				else if (base.IsPressed)
				{
					this.SetState("Pressed");
				}
				else if (base.IsHovered)
				{
					this.SetState("Hovered");
				}
				else
				{
					this.SetState("Default");
				}
			}
			if (base.UpdateChildrenStates)
			{
				for (int i = 0; i < base.ChildCount; i++)
				{
					base.GetChild(i).SetState(base.CurrentState);
				}
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000188E4 File Offset: 0x00016AE4
		private void UpdateGameTypeImage()
		{
			if (this.GameTypeImageWidget == null || string.IsNullOrEmpty(this.GameTypeId))
			{
				return;
			}
			Sprite sprite = base.Context.SpriteData.GetSprite("MPLobby\\Matchmaking\\GameTypeCards\\" + this.GameTypeId);
			foreach (Style style in this.GameTypeImageWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Sprite = sprite;
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00018990 File Offset: 0x00016B90
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x00018998 File Offset: 0x00016B98
		[Editor(false)]
		public string GameTypeId
		{
			get
			{
				return this._gameTypeId;
			}
			set
			{
				if (this._gameTypeId != value)
				{
					this._gameTypeId = value;
					base.OnPropertyChanged<string>(value, "GameTypeId");
					this.UpdateGameTypeImage();
				}
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x000189C1 File Offset: 0x00016BC1
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x000189C9 File Offset: 0x00016BC9
		[Editor(false)]
		public BrushWidget GameTypeImageWidget
		{
			get
			{
				return this._gameTypeImageWidget;
			}
			set
			{
				if (this._gameTypeImageWidget != value)
				{
					this._gameTypeImageWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "GameTypeImageWidget");
					this.UpdateGameTypeImage();
				}
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x000189ED File Offset: 0x00016BED
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x000189F5 File Offset: 0x00016BF5
		[Editor(false)]
		public Widget CheckboxWidget
		{
			get
			{
				return this._checkboxWidget;
			}
			set
			{
				if (this._checkboxWidget != value)
				{
					this._checkboxWidget = value;
					base.OnPropertyChanged<Widget>(value, "CheckboxWidget");
				}
			}
		}

		// Token: 0x040003DB RID: 987
		private string _gameTypeId;

		// Token: 0x040003DC RID: 988
		private BrushWidget _gameTypeImageWidget;

		// Token: 0x040003DD RID: 989
		private Widget _checkboxWidget;
	}
}
