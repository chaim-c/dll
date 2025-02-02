using System;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000013 RID: 19
	public class KeybindingPopup
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000615F File Offset: 0x0000435F
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00006167 File Offset: 0x00004367
		public bool IsActive { get; private set; }

		// Token: 0x06000093 RID: 147 RVA: 0x00006170 File Offset: 0x00004370
		public KeybindingPopup(Action<Key> onDone, ScreenBase targetScreen)
		{
			this._onDone = onDone;
			this._messageStr = new BindingListStringItem(new TextObject("{=hvaDkG4w}Press any key.", null).ToString());
			this._targetScreen = targetScreen;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000061A4 File Offset: 0x000043A4
		public void Tick()
		{
			if (!this.IsActive)
			{
				return;
			}
			if (!this._isActiveFirstFrame)
			{
				InputKey firstKeyReleasedInRange = (InputKey)Input.GetFirstKeyReleasedInRange(0);
				if (firstKeyReleasedInRange != InputKey.Invalid)
				{
					this._onDone(new Key(firstKeyReleasedInRange));
					return;
				}
			}
			else
			{
				this._isActiveFirstFrame = false;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000061E8 File Offset: 0x000043E8
		public void OnToggle(bool isActive)
		{
			if (this.IsActive != isActive)
			{
				this.IsActive = isActive;
				if (this.IsActive)
				{
					this._gauntletLayer = new GauntletLayer(4005, "GauntletLayer", false);
					ScreenManager.TrySetFocus(this._gauntletLayer);
					this._gauntletLayer.IsFocusLayer = true;
					this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
					ScreenManager.SetSuspendLayer(this._gauntletLayer, false);
					this._movie = this._gauntletLayer.LoadMovie("KeybindingPopup", this._messageStr);
					this._targetScreen.AddLayer(this._gauntletLayer);
					this._isActiveFirstFrame = true;
					return;
				}
				ScreenManager.TryLoseFocus(this._gauntletLayer);
				this._gauntletLayer.IsFocusLayer = false;
				this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
				ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
				if (this._movie != null)
				{
					this._gauntletLayer.ReleaseMovie(this._movie);
					this._movie = null;
				}
				this._targetScreen.RemoveLayer(this._gauntletLayer);
				this._gauntletLayer = null;
			}
		}

		// Token: 0x0400006D RID: 109
		private bool _isActiveFirstFrame;

		// Token: 0x0400006E RID: 110
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400006F RID: 111
		private IGauntletMovie _movie;

		// Token: 0x04000070 RID: 112
		private BindingListStringItem _messageStr;

		// Token: 0x04000071 RID: 113
		private ScreenBase _targetScreen;

		// Token: 0x04000072 RID: 114
		private Action<Key> _onDone;
	}
}
