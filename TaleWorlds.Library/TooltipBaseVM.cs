using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200003C RID: 60
	public abstract class TooltipBaseVM : ViewModel
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000763D File Offset: 0x0000583D
		public TooltipBaseVM(Type invokedType, object[] invokedArgs)
		{
			this._invokedType = invokedType;
			this._invokedArgs = invokedArgs;
			this.RegisterCallbacks();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007659 File Offset: 0x00005859
		public override void OnFinalize()
		{
			this.UnregisterCallbacks();
			this.OnFinalizeInternal();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007667 File Offset: 0x00005867
		protected virtual void OnFinalizeInternal()
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000766C File Offset: 0x0000586C
		public virtual void Tick(float dt)
		{
			if (this.IsActive && this._isPeriodicRefreshEnabled)
			{
				this._periodicRefreshTimer -= dt;
				if (this._periodicRefreshTimer < 0f)
				{
					this.OnPeriodicRefresh();
					this._periodicRefreshTimer = this._periodicRefreshDelay;
					return;
				}
			}
			else
			{
				this._periodicRefreshTimer = this._periodicRefreshDelay;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000076C4 File Offset: 0x000058C4
		protected void InvokeRefreshData<T>(T tooltip) where T : TooltipBaseVM
		{
			ValueTuple<Type, object, string> valueTuple;
			Action<T, object[]> action;
			if (InformationManager.RegisteredTypes.TryGetValue(this._invokedType, out valueTuple) && (action = (valueTuple.Item2 as Action<T, object[]>)) != null)
			{
				action(tooltip, this._invokedArgs);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007701 File Offset: 0x00005901
		protected virtual void OnPeriodicRefresh()
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007703 File Offset: 0x00005903
		protected virtual void OnIsExtendedChanged()
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007705 File Offset: 0x00005905
		private void RegisterCallbacks()
		{
			InformationManager.RegisterIsAnyTooltipActiveCallback(new Func<bool>(this.IsAnyTooltipActive));
			InformationManager.RegisterIsAnyTooltipExtendedCallback(new Func<bool>(this.IsAnyTooltipExtended));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007729 File Offset: 0x00005929
		private void UnregisterCallbacks()
		{
			InformationManager.UnregisterIsAnyTooltipActiveCallback(new Func<bool>(this.IsAnyTooltipActive));
			InformationManager.UnregisterIsAnyTooltipExtendedCallback(new Func<bool>(this.IsAnyTooltipExtended));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000774D File Offset: 0x0000594D
		private bool IsAnyTooltipActive()
		{
			return this.IsActive;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007755 File Offset: 0x00005955
		private bool IsAnyTooltipExtended()
		{
			return this.IsExtended;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000775D File Offset: 0x0000595D
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00007765 File Offset: 0x00005965
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007783 File Offset: 0x00005983
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000778B File Offset: 0x0000598B
		[DataSourceProperty]
		public bool IsExtended
		{
			get
			{
				return this._isExtended;
			}
			set
			{
				if (this._isExtended != value)
				{
					this._isExtended = value;
					base.OnPropertyChangedWithValue(value, "IsExtended");
					this.OnIsExtendedChanged();
				}
			}
		}

		// Token: 0x040000B0 RID: 176
		protected readonly Type _invokedType;

		// Token: 0x040000B1 RID: 177
		protected readonly object[] _invokedArgs;

		// Token: 0x040000B2 RID: 178
		protected bool _isPeriodicRefreshEnabled;

		// Token: 0x040000B3 RID: 179
		protected float _periodicRefreshDelay;

		// Token: 0x040000B4 RID: 180
		private float _periodicRefreshTimer;

		// Token: 0x040000B5 RID: 181
		private bool _isActive;

		// Token: 0x040000B6 RID: 182
		private bool _isExtended;
	}
}
