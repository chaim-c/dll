using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200019A RID: 410
	public struct ActionIndexValueCache : IEquatable<ActionIndexCache>, IEquatable<ActionIndexValueCache>
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004F85A File Offset: 0x0004DA5A
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x0004F861 File Offset: 0x0004DA61
		public static ActionIndexValueCache act_none { get; private set; } = new ActionIndexValueCache(-1, "act_none");

		// Token: 0x0600152A RID: 5418 RVA: 0x0004F869 File Offset: 0x0004DA69
		public static ActionIndexValueCache Create(string actName)
		{
			if (string.IsNullOrWhiteSpace(actName))
			{
				return ActionIndexValueCache.act_none;
			}
			return new ActionIndexValueCache(actName);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004F87F File Offset: 0x0004DA7F
		public static ActionIndexValueCache Create(ActionIndexCache actCache)
		{
			return new ActionIndexValueCache(actCache.Index);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004F88C File Offset: 0x0004DA8C
		private ActionIndexValueCache(string actName)
		{
			this._name = actName;
			this._actionIndex = -2;
			this._isValidAction = true;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004F8A4 File Offset: 0x0004DAA4
		private ActionIndexValueCache(int actionIndex, string actName)
		{
			this._name = actName;
			this._actionIndex = actionIndex;
			this._isValidAction = false;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0004F8BB File Offset: 0x0004DABB
		internal ActionIndexValueCache(int actionIndex)
		{
			this._name = null;
			this._actionIndex = actionIndex;
			this._isValidAction = (actionIndex >= 0);
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0004F8D8 File Offset: 0x0004DAD8
		public int Index
		{
			get
			{
				if (!this._isValidAction)
				{
					return ActionIndexValueCache.act_none._actionIndex;
				}
				if (this._actionIndex == -2)
				{
					this.ResolveIndex();
				}
				return this._actionIndex;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0004F903 File Offset: 0x0004DB03
		public string Name
		{
			get
			{
				if (!this._isValidAction)
				{
					return ActionIndexValueCache.act_none._name;
				}
				if (this._name == null)
				{
					this.ResolveName();
				}
				return this._name;
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0004F92C File Offset: 0x0004DB2C
		private void ResolveIndex()
		{
			this._actionIndex = MBAnimation.GetActionCodeWithName(this._name);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0004F93F File Offset: 0x0004DB3F
		private void ResolveName()
		{
			this._name = MBAnimation.GetActionNameWithCode(this._actionIndex);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0004F952 File Offset: 0x0004DB52
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is ActionIndexCache)
			{
				return this.Equals((ActionIndexCache)obj);
			}
			return this.Equals((ActionIndexValueCache)obj);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0004F97A File Offset: 0x0004DB7A
		public bool Equals(ActionIndexCache other)
		{
			return other != null && this.Index == other.Index;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0004F98F File Offset: 0x0004DB8F
		public bool Equals(ActionIndexValueCache other)
		{
			return this.Index == other.Index;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0004F9A0 File Offset: 0x0004DBA0
		public static bool operator ==(ActionIndexCache action0, ActionIndexValueCache action1)
		{
			return action0.Equals(action1);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0004F9A9 File Offset: 0x0004DBA9
		public static bool operator !=(ActionIndexCache action0, ActionIndexValueCache action1)
		{
			return !(action0 == action1);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0004F9B5 File Offset: 0x0004DBB5
		public static bool operator ==(ActionIndexValueCache action0, ActionIndexValueCache action1)
		{
			return action0.Equals(action1);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0004F9BF File Offset: 0x0004DBBF
		public static bool operator !=(ActionIndexValueCache action0, ActionIndexValueCache action1)
		{
			return !(action0 == action1);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0004F9CC File Offset: 0x0004DBCC
		public override int GetHashCode()
		{
			return this.Index.GetHashCode();
		}

		// Token: 0x0400074B RID: 1867
		private const int UnresolvedActionIndex = -2;

		// Token: 0x0400074C RID: 1868
		private string _name;

		// Token: 0x0400074D RID: 1869
		private int _actionIndex;

		// Token: 0x0400074E RID: 1870
		private bool _isValidAction;
	}
}
