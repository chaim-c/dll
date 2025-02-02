using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000172 RID: 370
	public class CharacterDeveloperPerksContainerWidget : Widget
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00034659 File Offset: 0x00032859
		public CharacterDeveloperPerksContainerWidget(UIContext context) : base(context)
		{
			this._perkWidgets = new List<PerkItemButtonWidget>();
			this._navigationScopes = new List<GamepadNavigationScope>();
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00034680 File Offset: 0x00032880
		private void RefreshScopes()
		{
			foreach (GamepadNavigationScope scope in this._navigationScopes)
			{
				base.GamepadNavigationContext.RemoveNavigationScope(scope);
			}
			this._navigationScopes.Clear();
			GamepadNavigationScope gamepadNavigationScope = this.BuildNewScope(this.FirstScopeID);
			this._navigationScopes.Add(gamepadNavigationScope);
			base.GamepadNavigationContext.AddNavigationScope(gamepadNavigationScope, true);
			int num = -1;
			if (this._perkWidgets.Count > 0)
			{
				num = this._perkWidgets[0].AlternativeType;
			}
			for (int i = 0; i < this._perkWidgets.Count; i++)
			{
				if (this._perkWidgets[i].AlternativeType == 0 || num == 0)
				{
					GamepadNavigationScope gamepadNavigationScope2 = this.BuildNewScope("Scope-" + i);
					this._navigationScopes.Add(gamepadNavigationScope2);
					base.GamepadNavigationContext.AddNavigationScope(gamepadNavigationScope2, true);
				}
				this._perkWidgets[i].GamepadNavigationIndex = 0;
				this._navigationScopes[this._navigationScopes.Count - 1].AddWidget(this._perkWidgets[i]);
				num = this._perkWidgets[i].AlternativeType;
			}
			for (int j = 0; j < this._navigationScopes.Count; j++)
			{
				List<Widget> list = this._navigationScopes[j].NavigatableWidgets.ToList<Widget>();
				list = (from w in list
				orderby ((PerkItemButtonWidget)w).AlternativeType
				select w).ToList<Widget>();
				this._navigationScopes[j].ClearNavigatableWidgets();
				for (int k = 0; k < list.Count; k++)
				{
					list[k].GamepadNavigationIndex = k;
					this._navigationScopes[j].AddWidget(list[k]);
				}
				if (this._navigationScopes[j].NavigatableWidgets.Count > 1)
				{
					this._navigationScopes[j].AlternateMovementStepSize = MathF.Round((float)this._navigationScopes[j].NavigatableWidgets.Count / 2f);
					this._navigationScopes[j].AlternateScopeMovements = GamepadNavigationTypes.Vertical;
				}
				this._navigationScopes[j].DownNavigationScopeID = this.DownScopeID;
				this._navigationScopes[j].UpNavigationScopeID = this.UpScopeID;
				if (j == 0)
				{
					this._navigationScopes[j].LeftNavigationScopeID = this.LeftScopeID;
					if (this._navigationScopes.Count > 1)
					{
						this._navigationScopes[j].RightNavigationScopeID = this._navigationScopes[j + 1].ScopeID;
					}
				}
				else if (j == this._navigationScopes.Count - 1)
				{
					if (this._navigationScopes.Count > 1)
					{
						this._navigationScopes[j].LeftNavigationScopeID = this._navigationScopes[j - 1].ScopeID;
					}
					this._navigationScopes[j].RightNavigationScopeID = this.RightScopeID;
				}
				else if (j > 0 && j < this._navigationScopes.Count - 1)
				{
					this._navigationScopes[j].LeftNavigationScopeID = this._navigationScopes[j - 1].ScopeID;
					this._navigationScopes[j].RightNavigationScopeID = this._navigationScopes[j + 1].ScopeID;
				}
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00034A4C File Offset: 0x00032C4C
		protected override void OnLateUpdate(float dt)
		{
			if (!this._initialized || this._lastPerkCount != this._perkWidgets.Count)
			{
				this.RefreshScopes();
				this._initialized = true;
				this._lastPerkCount = this._perkWidgets.Count;
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00034A87 File Offset: 0x00032C87
		private GamepadNavigationScope BuildNewScope(string scopeID)
		{
			return new GamepadNavigationScope
			{
				ScopeID = scopeID,
				ParentWidget = this,
				ScopeMovements = GamepadNavigationTypes.Horizontal,
				DoNotAutomaticallyFindChildren = true
			};
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00034AAC File Offset: 0x00032CAC
		protected override void OnChildAdded(Widget child)
		{
			PerkItemButtonWidget item;
			if ((item = (child as PerkItemButtonWidget)) != null)
			{
				this._perkWidgets.Add(item);
				this._initialized = false;
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00034AD8 File Offset: 0x00032CD8
		protected override void OnChildRemoved(Widget child)
		{
			PerkItemButtonWidget item;
			if ((item = (child as PerkItemButtonWidget)) != null)
			{
				this._perkWidgets.Remove(item);
				this._initialized = false;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00034B03 File Offset: 0x00032D03
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x00034B0B File Offset: 0x00032D0B
		public string LeftScopeID { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00034B14 File Offset: 0x00032D14
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x00034B1C File Offset: 0x00032D1C
		public string RightScopeID { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00034B25 File Offset: 0x00032D25
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x00034B2D File Offset: 0x00032D2D
		public string DownScopeID { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00034B36 File Offset: 0x00032D36
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x00034B3E File Offset: 0x00032D3E
		public string UpScopeID { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00034B47 File Offset: 0x00032D47
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00034B4F File Offset: 0x00032D4F
		public string FirstScopeID { get; set; }

		// Token: 0x040008B9 RID: 2233
		private List<GamepadNavigationScope> _navigationScopes;

		// Token: 0x040008BA RID: 2234
		private List<PerkItemButtonWidget> _perkWidgets;

		// Token: 0x040008BB RID: 2235
		private bool _initialized;

		// Token: 0x040008BC RID: 2236
		private int _lastPerkCount = -1;
	}
}
