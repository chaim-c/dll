using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000AF RID: 175
	public class MultiplayerLobbyArmoryCosmeticItemButtonWidget : ButtonWidget
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x0001A77B File Offset: 0x0001897B
		public MultiplayerLobbyArmoryCosmeticItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001A784 File Offset: 0x00018984
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.EventManager.HoveredView == this && Input.IsKeyPressed(InputKey.ControllerRUp))
			{
				this.OnMouseAlternatePressed();
				return;
			}
			if (base.EventManager.HoveredView == this && Input.IsKeyReleased(InputKey.ControllerRUp))
			{
				this.OnMouseAlternateReleased();
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001A7DC File Offset: 0x000189DC
		private void UpdateSelectableState()
		{
			this._selectableTimer = 0f;
			base.IsDisabled = !this.IsSelectable;
			this._animationStartAlpha = (this.IsSelectable ? this.NonSelectableStateAlpha : this.SelectableStateAlpha);
			this._animationTargetAlpha = (this.IsSelectable ? this.SelectableStateAlpha : this.NonSelectableStateAlpha);
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.AnimateSelectableState), 1);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001A854 File Offset: 0x00018A54
		private void AnimateSelectableState(float dt)
		{
			this._selectableTimer += dt;
			float amount;
			if (this._selectableTimer < this.SelectableStateAnimationDuration)
			{
				amount = this._selectableTimer / this.SelectableStateAnimationDuration;
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.AnimateSelectableState), 1);
			}
			else
			{
				amount = 1f;
			}
			float num = MathF.Lerp(this._animationStartAlpha, this._animationTargetAlpha, amount, 1E-05f);
			base.IsVisible = (num != 0f);
			this.SetGlobalAlphaRecursively(num);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001A8DC File Offset: 0x00018ADC
		protected override void OnClick()
		{
			base.OnClick();
			if (this.IsUnlocked)
			{
				this.HandleSoundEvent();
				return;
			}
			base.EventFired("Obtain", Array.Empty<object>());
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001A903 File Offset: 0x00018B03
		protected override void OnAlternateClick()
		{
			base.OnAlternateClick();
			this.HandleSoundEvent();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001A914 File Offset: 0x00018B14
		private void HandleSoundEvent()
		{
			int itemType = this.ItemType;
			switch (itemType)
			{
			case 12:
				base.EventFired("WearHelmet", Array.Empty<object>());
				return;
			case 13:
				base.EventFired("WearArmorBig", Array.Empty<object>());
				return;
			case 14:
			case 15:
				break;
			default:
				if (itemType != 22)
				{
					base.EventFired("WearGeneric", Array.Empty<object>());
					return;
				}
				break;
			}
			base.EventFired("WearArmorSmall", Array.Empty<object>());
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001A98B File Offset: 0x00018B8B
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001A993 File Offset: 0x00018B93
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (value != this._itemType)
				{
					this._itemType = value;
					base.OnPropertyChanged(value, "ItemType");
				}
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001A9B1 File Offset: 0x00018BB1
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0001A9B9 File Offset: 0x00018BB9
		public bool IsUnlocked
		{
			get
			{
				return this._isUnlocked;
			}
			set
			{
				if (value != this._isUnlocked)
				{
					this._isUnlocked = value;
					base.OnPropertyChanged(value, "IsUnlocked");
				}
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0001A9D7 File Offset: 0x00018BD7
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0001A9DF File Offset: 0x00018BDF
		public float SelectableStateAnimationDuration
		{
			get
			{
				return this._selectableStateAnimationDuration;
			}
			set
			{
				if (value != this._selectableStateAnimationDuration)
				{
					this._selectableStateAnimationDuration = value;
					base.OnPropertyChanged(value, "SelectableStateAnimationDuration");
				}
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001A9FD File Offset: 0x00018BFD
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0001AA05 File Offset: 0x00018C05
		public float SelectableStateAlpha
		{
			get
			{
				return this._selectableStateAlpha;
			}
			set
			{
				if (value != this._selectableStateAlpha)
				{
					this._selectableStateAlpha = value;
					base.OnPropertyChanged(value, "SelectableStateAlpha");
				}
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001AA23 File Offset: 0x00018C23
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0001AA2B File Offset: 0x00018C2B
		public float NonSelectableStateAlpha
		{
			get
			{
				return this._nonSelectableStateAlpha;
			}
			set
			{
				if (value != this._nonSelectableStateAlpha)
				{
					this._nonSelectableStateAlpha = value;
					base.OnPropertyChanged(value, "NonSelectableStateAlpha");
				}
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001AA49 File Offset: 0x00018C49
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0001AA51 File Offset: 0x00018C51
		public bool IsSelectable
		{
			get
			{
				return this._isSelectable;
			}
			set
			{
				if (value != this._isSelectable)
				{
					this._isSelectable = value;
					base.OnPropertyChanged(value, "IsSelectable");
					this.UpdateSelectableState();
				}
			}
		}

		// Token: 0x0400043C RID: 1084
		private float _selectableTimer;

		// Token: 0x0400043D RID: 1085
		private float _animationTargetAlpha;

		// Token: 0x0400043E RID: 1086
		private float _animationStartAlpha;

		// Token: 0x0400043F RID: 1087
		private int _itemType;

		// Token: 0x04000440 RID: 1088
		private bool _isUnlocked;

		// Token: 0x04000441 RID: 1089
		private float _selectableStateAnimationDuration;

		// Token: 0x04000442 RID: 1090
		private float _selectableStateAlpha;

		// Token: 0x04000443 RID: 1091
		private float _nonSelectableStateAlpha;

		// Token: 0x04000444 RID: 1092
		private bool _isSelectable;
	}
}
