using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000174 RID: 372
	public class CharacterDeveloperPerkSelectionWidget : Widget
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x00034BD4 File Offset: 0x00032DD4
		public CharacterDeveloperPerkSelectionWidget(UIContext context) : base(context)
		{
			base.IsVisible = false;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00034BF0 File Offset: 0x00032DF0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsVisible && this._latestMouseUpWidgetWhenActivated != base.EventManager.LatestMouseUpWidget && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget))
			{
				this.Deactivate();
			}
			this.UpdatePosition();
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00034C40 File Offset: 0x00032E40
		private void UpdatePosition()
		{
			if (base.IsVisible && this._latestMouseUpWidgetWhenActivated != null)
			{
				float num = this._latestMouseUpWidgetWhenActivated.GlobalPosition.X + this._latestMouseUpWidgetWhenActivated.Size.X + this._distBetweenPerkItemsMultiplier * 2f * base._scaleToUse;
				float num2 = 0f;
				if (base.GetChild(0).ChildCount > 1)
				{
					PerkItemButtonWidget perkItemButtonWidget;
					if ((perkItemButtonWidget = (this._latestMouseUpWidgetWhenActivated as PerkItemButtonWidget)) != null)
					{
						if (perkItemButtonWidget.AlternativeType == 1)
						{
							num2 = this._latestMouseUpWidgetWhenActivated.GlobalPosition.Y + (this._latestMouseUpWidgetWhenActivated.Size.Y - 4f * base._scaleToUse) - base.Size.Y / 2f;
						}
						else if (perkItemButtonWidget.AlternativeType == 2)
						{
							num2 = this._latestMouseUpWidgetWhenActivated.GlobalPosition.Y - base.Size.Y / 2f;
						}
					}
				}
				else
				{
					num2 = this._latestMouseUpWidgetWhenActivated.GlobalPosition.Y + this._latestMouseUpWidgetWhenActivated.Size.Y / 2f - base.Size.Y / 2f;
				}
				base.ScaledPositionXOffset = MathF.Clamp(num - base.EventManager.LeftUsableAreaStart, 0f, base.EventManager.PageSize.X - base.Size.X);
				base.ScaledPositionYOffset = MathF.Clamp(num2 - base.EventManager.TopUsableAreaStart, 0f, base.EventManager.PageSize.Y - base.Size.Y);
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00034DE9 File Offset: 0x00032FE9
		private void Activate()
		{
			if (this._latestMouseUpWidgetWhenActivated == null)
			{
				this._latestMouseUpWidgetWhenActivated = base.EventManager.LatestMouseDownWidget;
			}
			base.IsVisible = true;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00034E0B File Offset: 0x0003300B
		private void Deactivate()
		{
			base.EventFired("Deactivate", Array.Empty<object>());
			base.IsVisible = false;
			this._latestMouseUpWidgetWhenActivated = null;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00034E2B File Offset: 0x0003302B
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x00034E33 File Offset: 0x00033033
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

		// Token: 0x040008C3 RID: 2243
		private float _distBetweenPerkItemsMultiplier = 16f;

		// Token: 0x040008C4 RID: 2244
		private Widget _latestMouseUpWidgetWhenActivated;

		// Token: 0x040008C5 RID: 2245
		private bool _isActive;
	}
}
