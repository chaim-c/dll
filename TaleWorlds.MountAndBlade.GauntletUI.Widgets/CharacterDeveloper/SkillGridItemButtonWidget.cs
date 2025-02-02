using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000178 RID: 376
	public class SkillGridItemButtonWidget : ButtonWidget
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00035680 File Offset: 0x00033880
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x00035688 File Offset: 0x00033888
		public Brush CannotLearnBrush { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00035691 File Offset: 0x00033891
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00035699 File Offset: 0x00033899
		public Brush CanLearnBrush { get; set; }

		// Token: 0x0600137E RID: 4990 RVA: 0x000356A2 File Offset: 0x000338A2
		public SkillGridItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000356B4 File Offset: 0x000338B4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			Widget focusLevelWidget = this.FocusLevelWidget;
			if (focusLevelWidget != null)
			{
				focusLevelWidget.SetState(this.CurrentFocusLevel.ToString());
			}
			if (this._isVisualsDirty)
			{
				base.Brush = (this.CanLearnSkill ? this.CanLearnBrush : this.CannotLearnBrush);
				this._isVisualsDirty = false;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00035712 File Offset: 0x00033912
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0003571A File Offset: 0x0003391A
		public Widget FocusLevelWidget
		{
			get
			{
				return this._focusLevelWidget;
			}
			set
			{
				if (this._focusLevelWidget != value)
				{
					this._focusLevelWidget = value;
					base.OnPropertyChanged<Widget>(value, "FocusLevelWidget");
				}
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00035738 File Offset: 0x00033938
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00035740 File Offset: 0x00033940
		public bool CanLearnSkill
		{
			get
			{
				return this._canLearnSkill;
			}
			set
			{
				if (this._canLearnSkill != value)
				{
					this._canLearnSkill = value;
					base.OnPropertyChanged(value, "CanLearnSkill");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00035765 File Offset: 0x00033965
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0003576D File Offset: 0x0003396D
		public int CurrentFocusLevel
		{
			get
			{
				return this._currentFocusLevel;
			}
			set
			{
				if (this._currentFocusLevel != value)
				{
					this._currentFocusLevel = value;
					base.OnPropertyChanged(value, "CurrentFocusLevel");
				}
			}
		}

		// Token: 0x040008E2 RID: 2274
		private bool _isVisualsDirty = true;

		// Token: 0x040008E3 RID: 2275
		private Widget _focusLevelWidget;

		// Token: 0x040008E4 RID: 2276
		private int _currentFocusLevel;

		// Token: 0x040008E5 RID: 2277
		private bool _canLearnSkill;
	}
}
