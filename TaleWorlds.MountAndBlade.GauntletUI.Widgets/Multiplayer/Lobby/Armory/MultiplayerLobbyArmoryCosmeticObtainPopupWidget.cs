using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B0 RID: 176
	public class MultiplayerLobbyArmoryCosmeticObtainPopupWidget : Widget
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0001AA75 File Offset: 0x00018C75
		public MultiplayerLobbyArmoryCosmeticObtainPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001AA88 File Offset: 0x00018C88
		private void OnObtainStateChanged(int newState)
		{
			if (newState == 0)
			{
				this.ItemPreviewListPanel.IsVisible = true;
				this.ActionButtonWidget.IsEnabled = true;
				this.CancelButtonWidget.IsEnabled = true;
				this.ResultSuccessfulIconWidget.IsVisible = false;
				this.ResultFailedIconWidget.IsVisible = false;
				this.ResultTextWidget.IsVisible = false;
				this.LoadingAnimationWidget.IsVisible = false;
				return;
			}
			if (newState == 1)
			{
				this.LoadingAnimationWidget.IsVisible = true;
				this.CancelButtonWidget.IsEnabled = false;
				this.ActionButtonWidget.IsEnabled = false;
				this.ItemPreviewListPanel.IsVisible = false;
				this.ResultSuccessfulIconWidget.IsVisible = false;
				this.ResultFailedIconWidget.IsVisible = false;
				this.ResultTextWidget.IsVisible = false;
				return;
			}
			if (newState == 2 || newState == 3)
			{
				this.CancelButtonWidget.IsEnabled = true;
				this.ActionButtonWidget.IsEnabled = true;
				this.ResultTextWidget.IsVisible = true;
				if (newState == 2)
				{
					this.ResultSuccessfulIconWidget.IsVisible = true;
				}
				else
				{
					this.ResultFailedIconWidget.IsVisible = true;
				}
				this.ItemPreviewListPanel.IsVisible = false;
				this.LoadingAnimationWidget.IsVisible = false;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001ABA8 File Offset: 0x00018DA8
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		[Editor(false)]
		public int ObtainState
		{
			get
			{
				return this._obtainState;
			}
			set
			{
				if (value != this._obtainState)
				{
					this._obtainState = value;
					base.OnPropertyChanged(value, "ObtainState");
					this.OnObtainStateChanged(value);
				}
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001ABD5 File Offset: 0x00018DD5
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0001ABDD File Offset: 0x00018DDD
		[Editor(false)]
		public ButtonWidget CancelButtonWidget
		{
			get
			{
				return this._cancelButtonWidget;
			}
			set
			{
				if (value != this._cancelButtonWidget)
				{
					this._cancelButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "CancelButtonWidget");
				}
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001ABFB File Offset: 0x00018DFB
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0001AC03 File Offset: 0x00018E03
		[Editor(false)]
		public ListPanel ItemPreviewListPanel
		{
			get
			{
				return this._itemPreviewListPanel;
			}
			set
			{
				if (value != this._itemPreviewListPanel)
				{
					this._itemPreviewListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "ItemPreviewListPanel");
				}
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001AC21 File Offset: 0x00018E21
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0001AC29 File Offset: 0x00018E29
		[Editor(false)]
		public ButtonWidget ActionButtonWidget
		{
			get
			{
				return this._actionButtonWidget;
			}
			set
			{
				if (value != this._actionButtonWidget)
				{
					this._actionButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ActionButtonWidget");
				}
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001AC47 File Offset: 0x00018E47
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0001AC4F File Offset: 0x00018E4F
		[Editor(false)]
		public Widget ResultSuccessfulIconWidget
		{
			get
			{
				return this._resultSuccessfulIconWidget;
			}
			set
			{
				if (value != this._resultSuccessfulIconWidget)
				{
					this._resultSuccessfulIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "ResultSuccessfulIconWidget");
				}
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001AC6D File Offset: 0x00018E6D
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0001AC75 File Offset: 0x00018E75
		[Editor(false)]
		public Widget ResultFailedIconWidget
		{
			get
			{
				return this._resultFailedIconWidget;
			}
			set
			{
				if (value != this._resultFailedIconWidget)
				{
					this._resultFailedIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "ResultFailedIconWidget");
				}
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001AC93 File Offset: 0x00018E93
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0001AC9B File Offset: 0x00018E9B
		[Editor(false)]
		public TextWidget ResultTextWidget
		{
			get
			{
				return this._resultTextWidget;
			}
			set
			{
				if (value != this._resultTextWidget)
				{
					this._resultTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "ResultTextWidget");
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001ACB9 File Offset: 0x00018EB9
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0001ACC1 File Offset: 0x00018EC1
		[Editor(false)]
		public Widget LoadingAnimationWidget
		{
			get
			{
				return this._loadingAnimationWidget;
			}
			set
			{
				if (value != this._loadingAnimationWidget)
				{
					this._loadingAnimationWidget = value;
					base.OnPropertyChanged<Widget>(value, "LoadingAnimationWidget");
				}
			}
		}

		// Token: 0x04000445 RID: 1093
		private int _obtainState = -1;

		// Token: 0x04000446 RID: 1094
		private ButtonWidget _cancelButtonWidget;

		// Token: 0x04000447 RID: 1095
		private ListPanel _itemPreviewListPanel;

		// Token: 0x04000448 RID: 1096
		private ButtonWidget _actionButtonWidget;

		// Token: 0x04000449 RID: 1097
		private Widget _resultSuccessfulIconWidget;

		// Token: 0x0400044A RID: 1098
		private Widget _resultFailedIconWidget;

		// Token: 0x0400044B RID: 1099
		private TextWidget _resultTextWidget;

		// Token: 0x0400044C RID: 1100
		private Widget _loadingAnimationWidget;
	}
}
