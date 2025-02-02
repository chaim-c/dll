using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200019B RID: 411
	public class ActionIndexCache : IEquatable<ActionIndexCache>, IEquatable<ActionIndexValueCache>
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0004F9F9 File Offset: 0x0004DBF9
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x0004FA00 File Offset: 0x0004DC00
		public static ActionIndexCache act_none { get; private set; } = new ActionIndexCache(-1, "act_none");

		// Token: 0x0600153E RID: 5438 RVA: 0x0004FA08 File Offset: 0x0004DC08
		public static ActionIndexCache Create(string actName)
		{
			if (string.IsNullOrWhiteSpace(actName))
			{
				return ActionIndexCache.act_none;
			}
			return new ActionIndexCache(actName);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0004FA1E File Offset: 0x0004DC1E
		private ActionIndexCache(string actName)
		{
			this._name = actName;
			this._actionIndex = -2;
			this._isValidAction = true;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0004FA3C File Offset: 0x0004DC3C
		private ActionIndexCache(int actionIndex, string actName)
		{
			this._name = actName;
			this._actionIndex = actionIndex;
			this._isValidAction = false;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0004FA59 File Offset: 0x0004DC59
		internal ActionIndexCache(int actionIndex)
		{
			this._name = null;
			this._actionIndex = actionIndex;
			this._isValidAction = (actionIndex >= 0);
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0004FA7C File Offset: 0x0004DC7C
		public int Index
		{
			get
			{
				if (!this._isValidAction)
				{
					return ActionIndexCache.act_none._actionIndex;
				}
				if (this._actionIndex == -2)
				{
					this.ResolveIndex();
				}
				return this._actionIndex;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0004FAA7 File Offset: 0x0004DCA7
		public string Name
		{
			get
			{
				if (!this._isValidAction)
				{
					return ActionIndexCache.act_none._name;
				}
				if (this._name == null)
				{
					this.ResolveName();
				}
				return this._name;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0004FAD0 File Offset: 0x0004DCD0
		private void ResolveIndex()
		{
			this._actionIndex = MBAnimation.GetActionCodeWithName(this._name);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0004FAE3 File Offset: 0x0004DCE3
		private void ResolveName()
		{
			this._name = MBAnimation.GetActionNameWithCode(this._actionIndex);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004FAF6 File Offset: 0x0004DCF6
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

		// Token: 0x06001547 RID: 5447 RVA: 0x0004FB1E File Offset: 0x0004DD1E
		public bool Equals(ActionIndexCache other)
		{
			return other != null && this.Index == other.Index;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0004FB33 File Offset: 0x0004DD33
		public bool Equals(ActionIndexValueCache other)
		{
			return this.Index == other.Index;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0004FB44 File Offset: 0x0004DD44
		public static bool operator ==(ActionIndexValueCache action0, ActionIndexCache action1)
		{
			return action0.Equals(action1);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0004FB4E File Offset: 0x0004DD4E
		public static bool operator !=(ActionIndexValueCache action0, ActionIndexCache action1)
		{
			return !(action0 == action1);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0004FB5A File Offset: 0x0004DD5A
		public static bool operator ==(ActionIndexCache action0, ActionIndexCache action1)
		{
			return (action0 != null || action1 == null) && (action1 != null || action0 == null) && (action0 == action1 || action0.Equals(action1));
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004FB79 File Offset: 0x0004DD79
		public static bool operator !=(ActionIndexCache action0, ActionIndexCache action1)
		{
			return !(action0 == action1);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004FB88 File Offset: 0x0004DD88
		public override int GetHashCode()
		{
			return this.Index.GetHashCode();
		}

		// Token: 0x04000750 RID: 1872
		private const int UnresolvedActionIndex = -2;

		// Token: 0x04000751 RID: 1873
		private string _name;

		// Token: 0x04000752 RID: 1874
		private int _actionIndex;

		// Token: 0x04000753 RID: 1875
		private bool _isValidAction;
	}
}
