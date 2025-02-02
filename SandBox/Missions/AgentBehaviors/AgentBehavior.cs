using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000077 RID: 119
	public abstract class AgentBehavior
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001F35D File Offset: 0x0001D55D
		public AgentNavigator Navigator
		{
			get
			{
				return this.BehaviorGroup.Navigator;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001F36A File Offset: 0x0001D56A
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0001F372 File Offset: 0x0001D572
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this._isActive != value)
				{
					this._isActive = value;
					if (this._isActive)
					{
						this.OnActivate();
						return;
					}
					this.OnDeactivate();
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001F399 File Offset: 0x0001D599
		public Agent OwnerAgent
		{
			get
			{
				return this.Navigator.OwnerAgent;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001F3A6 File Offset: 0x0001D5A6
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0001F3AE File Offset: 0x0001D5AE
		public Mission Mission { get; private set; }

		// Token: 0x06000490 RID: 1168 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		protected AgentBehavior(AgentBehaviorGroup behaviorGroup)
		{
			this.Mission = behaviorGroup.Mission;
			this.CheckTime = 40f + MBRandom.RandomFloat * 20f;
			this.BehaviorGroup = behaviorGroup;
			this._isActive = false;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001F407 File Offset: 0x0001D607
		public virtual float GetAvailability(bool isSimulation)
		{
			return 0f;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001F40E File Offset: 0x0001D60E
		public virtual void Tick(float dt, bool isSimulation)
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001F410 File Offset: 0x0001D610
		public virtual void ConversationTick()
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001F412 File Offset: 0x0001D612
		protected virtual void OnActivate()
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001F414 File Offset: 0x0001D614
		protected virtual void OnDeactivate()
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001F416 File Offset: 0x0001D616
		public virtual bool CheckStartWithBehavior()
		{
			return false;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001F419 File Offset: 0x0001D619
		public virtual void OnSpecialTargetChanged()
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001F41B File Offset: 0x0001D61B
		public virtual void SetCustomWanderTarget(UsableMachine customUsableMachine)
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001F41D File Offset: 0x0001D61D
		public virtual void OnAgentRemoved(Agent agent)
		{
		}

		// Token: 0x0600049A RID: 1178
		public abstract string GetDebugInfo();

		// Token: 0x0400021F RID: 543
		public float CheckTime = 15f;

		// Token: 0x04000220 RID: 544
		protected readonly AgentBehaviorGroup BehaviorGroup;

		// Token: 0x04000221 RID: 545
		private bool _isActive;
	}
}
