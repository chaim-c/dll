using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000030 RID: 48
	public class NavigationTargetSwitcher : Widget
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x000091CF File Offset: 0x000073CF
		public NavigationTargetSwitcher(UIContext context) : base(context)
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 0f;
			base.SuggestedWidth = 0f;
			base.IsVisible = false;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009203 File Offset: 0x00007403
		private void OnFromTargetNavigationIndexUpdated(PropertyOwnerObject propertyOwner, string propertyName, int value)
		{
			if (propertyName == "GamepadNavigationIndex" && this.ToTarget != null)
			{
				this.TransferGamepadNavigation();
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009220 File Offset: 0x00007420
		private void TransferGamepadNavigation()
		{
			if (!this._isTransferingNavigationIndices)
			{
				this._isTransferingNavigationIndices = true;
				int gamepadNavigationIndex = this.FromTarget.GamepadNavigationIndex;
				this.ToTarget.GamepadNavigationIndex = gamepadNavigationIndex;
				this.FromTarget.GamepadNavigationIndex = -1;
				if (this.FromTarget.OnGamepadNavigationFocusGained != null)
				{
					Widget toTarget = this.ToTarget;
					toTarget.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Combine(toTarget.OnGamepadNavigationFocusGained, this.FromTarget.OnGamepadNavigationFocusGained);
					this.FromTarget.OnGamepadNavigationFocusGained = null;
				}
				this._isTransferingNavigationIndices = false;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000092A6 File Offset: 0x000074A6
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x000092AE File Offset: 0x000074AE
		public Widget ToTarget
		{
			get
			{
				return this._toTarget;
			}
			set
			{
				if (value != this._toTarget)
				{
					this._toTarget = value;
					if (this._toTarget != null && this.FromTarget != null && this.FromTarget.GamepadNavigationIndex != -1)
					{
						this.TransferGamepadNavigation();
					}
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000092E4 File Offset: 0x000074E4
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x000092EC File Offset: 0x000074EC
		public Widget FromTarget
		{
			get
			{
				return this._fromTarget;
			}
			set
			{
				if (value != this._fromTarget)
				{
					if (this._fromTarget != null)
					{
						this._fromTarget.intPropertyChanged -= this.OnFromTargetNavigationIndexUpdated;
					}
					this._fromTarget = value;
					this._fromTarget.intPropertyChanged += this.OnFromTargetNavigationIndexUpdated;
				}
			}
		}

		// Token: 0x04000121 RID: 289
		private bool _isTransferingNavigationIndices;

		// Token: 0x04000122 RID: 290
		private Widget _toTarget;

		// Token: 0x04000123 RID: 291
		private Widget _fromTarget;
	}
}
