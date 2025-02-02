using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Clan
{
	// Token: 0x0200016B RID: 363
	public class ClanPartyRoleSelectionToggleWidget : ButtonWidget
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x000337A4 File Offset: 0x000319A4
		public ClanPartyRoleSelectionToggleWidget(UIContext context) : base(context)
		{
			this.ClickEventHandlers.Add(new Action<Widget>(this.OnClick));
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000337C8 File Offset: 0x000319C8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			ClanPartyRoleSelectionPopupWidget popup = this.Popup;
			if (((popup != null) ? popup.ActiveToggleWidget : null) == this && MathF.Abs(this.Popup.Size.Y - this._lastPopupSizeY) > 1E-05f)
			{
				this.UpdatePopupPosition();
				this._lastPopupSizeY = this.Popup.Size.Y;
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00033830 File Offset: 0x00031A30
		protected virtual void OnClick(Widget widget)
		{
			if (this.Popup != null)
			{
				if (this.Popup.ActiveToggleWidget == this)
				{
					this.ClosePopup();
					return;
				}
				this.OpenPopup();
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00033855 File Offset: 0x00031A55
		private void OpenPopup()
		{
			this.Popup.ActiveToggleWidget = this;
			this.Popup.IsVisible = true;
			this.UpdatePopupPosition();
			this._lastPopupSizeY = this.Popup.Size.Y;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0003388B File Offset: 0x00031A8B
		private void ClosePopup()
		{
			this.Popup.ActiveToggleWidget = null;
			this.Popup.IsVisible = false;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000338A8 File Offset: 0x00031AA8
		private void UpdatePopupPosition()
		{
			this.Popup.ScaledPositionYOffset += base.GlobalPosition.Y - this.Popup.GlobalPosition.Y - this.Popup.Size.Y + 47f * base._scaleToUse;
			this.Popup.ScaledPositionXOffset += base.GlobalPosition.X - this.Popup.GlobalPosition.X + 80f * base._scaleToUse;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0003393C File Offset: 0x00031B3C
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x00033944 File Offset: 0x00031B44
		[Editor(false)]
		public ClanPartyRoleSelectionPopupWidget Popup
		{
			get
			{
				return this._popup;
			}
			set
			{
				if (this._popup != value)
				{
					this._popup = value;
					base.OnPropertyChanged<ClanPartyRoleSelectionPopupWidget>(value, "Popup");
					this._popup.AddToggleWidget(this);
				}
			}
		}

		// Token: 0x04000892 RID: 2194
		private float _lastPopupSizeY;

		// Token: 0x04000893 RID: 2195
		private ClanPartyRoleSelectionPopupWidget _popup;
	}
}
