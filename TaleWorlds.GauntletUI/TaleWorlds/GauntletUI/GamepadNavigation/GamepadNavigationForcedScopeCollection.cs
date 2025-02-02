using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.GamepadNavigation
{
	// Token: 0x02000046 RID: 70
	public class GamepadNavigationForcedScopeCollection
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00011467 File Offset: 0x0000F667
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0001146F File Offset: 0x0000F66F
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					Action<GamepadNavigationForcedScopeCollection> onAvailabilityChanged = this.OnAvailabilityChanged;
					if (onAvailabilityChanged == null)
					{
						return;
					}
					onAvailabilityChanged(this);
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00011492 File Offset: 0x0000F692
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001149D File Offset: 0x0000F69D
		public bool IsDisabled
		{
			get
			{
				return !this.IsEnabled;
			}
			set
			{
				if (value == this.IsEnabled)
				{
					this.IsEnabled = !value;
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000114B2 File Offset: 0x0000F6B2
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x000114BA File Offset: 0x0000F6BA
		public string CollectionID { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000114C3 File Offset: 0x0000F6C3
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x000114CB File Offset: 0x0000F6CB
		public int CollectionOrder { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000114D4 File Offset: 0x0000F6D4
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x000114DC File Offset: 0x0000F6DC
		public Widget ParentWidget
		{
			get
			{
				return this._parentWidget;
			}
			set
			{
				if (value != this._parentWidget)
				{
					if (this._parentWidget != null)
					{
						this._invisibleParents.Clear();
						for (Widget parentWidget = this._parentWidget; parentWidget != null; parentWidget = parentWidget.ParentWidget)
						{
							parentWidget.OnVisibilityChanged -= this.OnParentVisibilityChanged;
						}
					}
					this._parentWidget = value;
					for (Widget parentWidget2 = this._parentWidget; parentWidget2 != null; parentWidget2 = parentWidget2.ParentWidget)
					{
						if (!parentWidget2.IsVisible)
						{
							this._invisibleParents.Add(parentWidget2);
						}
						parentWidget2.OnVisibilityChanged += this.OnParentVisibilityChanged;
					}
				}
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001156A File Offset: 0x0000F76A
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00011572 File Offset: 0x0000F772
		public List<GamepadNavigationScope> Scopes { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001157B File Offset: 0x0000F77B
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00011583 File Offset: 0x0000F783
		public GamepadNavigationScope ActiveScope { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001158C File Offset: 0x0000F78C
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00011594 File Offset: 0x0000F794
		public GamepadNavigationScope PreviousScope { get; set; }

		// Token: 0x06000429 RID: 1065 RVA: 0x0001159D File Offset: 0x0000F79D
		public GamepadNavigationForcedScopeCollection()
		{
			this.Scopes = new List<GamepadNavigationScope>();
			this._invisibleParents = new List<Widget>();
			this.IsEnabled = true;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000115C4 File Offset: 0x0000F7C4
		private void OnParentVisibilityChanged(Widget parent)
		{
			bool flag = this._invisibleParents.Count == 0;
			if (!parent.IsVisible)
			{
				this._invisibleParents.Add(parent);
			}
			else
			{
				this._invisibleParents.Remove(parent);
			}
			bool flag2 = this._invisibleParents.Count == 0;
			if (flag != flag2)
			{
				Action<GamepadNavigationForcedScopeCollection> onAvailabilityChanged = this.OnAvailabilityChanged;
				if (onAvailabilityChanged == null)
				{
					return;
				}
				onAvailabilityChanged(this);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011628 File Offset: 0x0000F828
		public bool IsAvailable()
		{
			if (this.IsEnabled && this._invisibleParents.Count == 0)
			{
				if (this.Scopes.Any((GamepadNavigationScope x) => x.IsAvailable()))
				{
					return this.ParentWidget.Context.GamepadNavigation.IsAvailableForNavigation();
				}
			}
			return false;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001168D File Offset: 0x0000F88D
		public void AddScope(GamepadNavigationScope scope)
		{
			if (!this.Scopes.Contains(scope))
			{
				this.Scopes.Add(scope);
			}
			Action<GamepadNavigationForcedScopeCollection> onAvailabilityChanged = this.OnAvailabilityChanged;
			if (onAvailabilityChanged == null)
			{
				return;
			}
			onAvailabilityChanged(this);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000116BA File Offset: 0x0000F8BA
		public void RemoveScope(GamepadNavigationScope scope)
		{
			if (this.Scopes.Contains(scope))
			{
				this.Scopes.Remove(scope);
			}
			Action<GamepadNavigationForcedScopeCollection> onAvailabilityChanged = this.OnAvailabilityChanged;
			if (onAvailabilityChanged == null)
			{
				return;
			}
			onAvailabilityChanged(this);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000116E8 File Offset: 0x0000F8E8
		public void ClearScopes()
		{
			this.Scopes.Clear();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000116F5 File Offset: 0x0000F8F5
		public override string ToString()
		{
			return string.Format("ID:{0} C.C.:{1}", this.CollectionID, this.Scopes.Count);
		}

		// Token: 0x0400020A RID: 522
		public Action<GamepadNavigationForcedScopeCollection> OnAvailabilityChanged;

		// Token: 0x0400020B RID: 523
		private List<Widget> _invisibleParents;

		// Token: 0x0400020C RID: 524
		private bool _isEnabled;

		// Token: 0x0400020F RID: 527
		private Widget _parentWidget;
	}
}
