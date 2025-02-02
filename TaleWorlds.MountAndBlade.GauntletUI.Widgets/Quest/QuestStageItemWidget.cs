using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Quest
{
	// Token: 0x0200005A RID: 90
	public class QuestStageItemWidget : Widget
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		public QuestStageItemWidget(UIContext context) : base(context)
		{
			this._firstFrame = true;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			this._previousHoverBegan = this._hoverBegan;
			if (!this._firstFrame && this.IsNew)
			{
				bool flag = this.IsMouseOverWidget();
				if (flag && !this._hoverBegan)
				{
					this._hoverBegan = true;
				}
				else if (!flag && this._hoverBegan)
				{
					this._hoverBegan = false;
				}
			}
			this._firstFrame = false;
			if (this._previousHoverBegan && !this._hoverBegan)
			{
				base.EventFired("ResetGlow", Array.Empty<object>());
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000ED70 File Offset: 0x0000CF70
		private bool IsMouseOverWidget()
		{
			Vector2 globalPosition = base.GlobalPosition;
			return this.IsBetween(base.EventManager.MousePosition.X, globalPosition.X, globalPosition.X + base.Size.X) && this.IsBetween(base.EventManager.MousePosition.Y, globalPosition.Y, globalPosition.Y + base.Size.Y);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		private bool IsBetween(float number, float min, float max)
		{
			return number >= min && number <= max;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000EDF3 File Offset: 0x0000CFF3
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0000EDFB File Offset: 0x0000CFFB
		[Editor(false)]
		public bool IsNew
		{
			get
			{
				return this._isNew;
			}
			set
			{
				if (this._isNew != value)
				{
					this._isNew = value;
					base.OnPropertyChanged(value, "IsNew");
				}
			}
		}

		// Token: 0x04000215 RID: 533
		private bool _firstFrame;

		// Token: 0x04000216 RID: 534
		private bool _previousHoverBegan;

		// Token: 0x04000217 RID: 535
		private bool _hoverBegan;

		// Token: 0x04000218 RID: 536
		private bool _isNew;
	}
}
