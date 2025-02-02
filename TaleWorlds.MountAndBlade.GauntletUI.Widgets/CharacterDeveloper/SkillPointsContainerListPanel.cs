using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000179 RID: 377
	public class SkillPointsContainerListPanel : ListPanel
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x0003578B File Offset: 0x0003398B
		public SkillPointsContainerListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00035794 File Offset: 0x00033994
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			for (int i = 0; i < base.ChildCount; i++)
			{
				if (!this._initialized)
				{
					base.GetChild(i).RegisterBrushStatesOfWidget();
				}
				bool flag = this.CurrentFocusLevel >= i + 1;
				base.GetChild(i).SetState(flag ? "Full" : "Empty");
			}
			this._initialized = true;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x000357FE File Offset: 0x000339FE
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x00035806 File Offset: 0x00033A06
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

		// Token: 0x040008E6 RID: 2278
		private bool _initialized;

		// Token: 0x040008E7 RID: 2279
		private int _currentFocusLevel;
	}
}
