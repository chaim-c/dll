using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000171 RID: 369
	public class CharacterDeveloperAttributeInspectionPopupWidget : Widget
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x00034589 File Offset: 0x00032789
		public CharacterDeveloperAttributeInspectionPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00034594 File Offset: 0x00032794
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.ParentWidget.IsVisible && this._latestMouseUpWidgetWhenActivated != base.EventManager.LatestMouseUpWidget && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget))
			{
				this.Deactivate();
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000345E1 File Offset: 0x000327E1
		private void Activate()
		{
			this._latestMouseUpWidgetWhenActivated = base.EventManager.LatestMouseDownWidget;
			base.ParentWidget.IsVisible = true;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00034600 File Offset: 0x00032800
		private void Deactivate()
		{
			base.EventFired("Deactivate", Array.Empty<object>());
			base.ParentWidget.IsVisible = false;
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0003461E File Offset: 0x0003281E
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x00034626 File Offset: 0x00032826
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this._isActive != value)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
					if (this._isActive)
					{
						this.Activate();
						return;
					}
					this.Deactivate();
				}
			}
		}

		// Token: 0x040008B7 RID: 2231
		private Widget _latestMouseUpWidgetWhenActivated;

		// Token: 0x040008B8 RID: 2232
		private bool _isActive;
	}
}
