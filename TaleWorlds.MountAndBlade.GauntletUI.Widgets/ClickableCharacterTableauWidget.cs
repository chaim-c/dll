using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200000F RID: 15
	public class ClickableCharacterTableauWidget : CharacterTableauWidget
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00003ADD File Offset: 0x00001CDD
		public ClickableCharacterTableauWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003AF4 File Offset: 0x00001CF4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._isMouseDown && !this._isDragging && (this._mousePressPos - base.EventManager.MousePosition).LengthSquared >= this._dragThresholdSqr)
			{
				this._isDragging = true;
				base.SetTextureProviderProperty("CurrentlyRotating", true);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003B5B File Offset: 0x00001D5B
		protected override void OnMousePressed()
		{
			this._isMouseDown = true;
			this._mousePressPos = base.EventManager.MousePosition;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003B7A File Offset: 0x00001D7A
		protected override void OnMouseReleased()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", false);
			if (!this._isDragging)
			{
				base.EventFired("Click", Array.Empty<object>());
			}
			this._isDragging = false;
			this._isMouseDown = false;
		}

		// Token: 0x04000053 RID: 83
		private const float DragThreshold = 5f;

		// Token: 0x04000054 RID: 84
		private float _dragThresholdSqr = 25f;

		// Token: 0x04000055 RID: 85
		private bool _isMouseDown;

		// Token: 0x04000056 RID: 86
		private bool _isDragging;

		// Token: 0x04000057 RID: 87
		private Vec2 _mousePressPos;
	}
}
