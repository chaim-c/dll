using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000078 RID: 120
	public abstract class AgentBehaviorGroup
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001F41F File Offset: 0x0001D61F
		public Agent OwnerAgent
		{
			get
			{
				return this.Navigator.OwnerAgent;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001F42C File Offset: 0x0001D62C
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001F434 File Offset: 0x0001D634
		public AgentBehavior ScriptedBehavior { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001F43D File Offset: 0x0001D63D
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001F445 File Offset: 0x0001D645
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001F46C File Offset: 0x0001D66C
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0001F474 File Offset: 0x0001D674
		public Mission Mission { get; private set; }

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001F47D File Offset: 0x0001D67D
		protected AgentBehaviorGroup(AgentNavigator navigator, Mission mission)
		{
			this.Mission = mission;
			this.Behaviors = new List<AgentBehavior>();
			this.Navigator = navigator;
			this._isActive = false;
			this.ScriptedBehavior = null;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		public T AddBehavior<T>() where T : AgentBehavior
		{
			T t = Activator.CreateInstance(typeof(T), new object[]
			{
				this
			}) as T;
			if (t != null)
			{
				foreach (AgentBehavior agentBehavior in this.Behaviors)
				{
					if (agentBehavior.GetType() == t.GetType())
					{
						return agentBehavior as T;
					}
				}
				this.Behaviors.Add(t);
				return t;
			}
			return t;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001F56C File Offset: 0x0001D76C
		public T GetBehavior<T>() where T : AgentBehavior
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior is T)
				{
					return (T)((object)agentBehavior);
				}
			}
			return default(T);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		public bool HasBehavior<T>() where T : AgentBehavior
		{
			using (List<AgentBehavior>.Enumerator enumerator = this.Behaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is T)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001F630 File Offset: 0x0001D830
		public void RemoveBehavior<T>() where T : AgentBehavior
		{
			for (int i = 0; i < this.Behaviors.Count; i++)
			{
				if (this.Behaviors[i] is T)
				{
					bool isActive = this.Behaviors[i].IsActive;
					this.Behaviors[i].IsActive = false;
					if (this.ScriptedBehavior == this.Behaviors[i])
					{
						this.ScriptedBehavior = null;
					}
					this.Behaviors.RemoveAt(i);
					if (isActive)
					{
						this.ForceThink(0f);
					}
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		public void SetScriptedBehavior<T>() where T : AgentBehavior
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior is T)
				{
					this.ScriptedBehavior = agentBehavior;
					this.ForceThink(0f);
					break;
				}
			}
			foreach (AgentBehavior agentBehavior2 in this.Behaviors)
			{
				if (agentBehavior2 != this.ScriptedBehavior)
				{
					agentBehavior2.IsActive = false;
				}
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001F774 File Offset: 0x0001D974
		public void DisableScriptedBehavior()
		{
			if (this.ScriptedBehavior != null)
			{
				this.ScriptedBehavior.IsActive = false;
				this.ScriptedBehavior = null;
				this.ForceThink(0f);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001F79C File Offset: 0x0001D99C
		public void DisableAllBehaviors()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				agentBehavior.IsActive = false;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		public AgentBehavior GetActiveBehavior()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior.IsActive)
				{
					return agentBehavior;
				}
			}
			return null;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001F84C File Offset: 0x0001DA4C
		public virtual void Tick(float dt, bool isSimulation)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001F84E File Offset: 0x0001DA4E
		public virtual void ConversationTick()
		{
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001F850 File Offset: 0x0001DA50
		public virtual void OnAgentRemoved(Agent agent)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001F852 File Offset: 0x0001DA52
		protected virtual void OnActivate()
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001F854 File Offset: 0x0001DA54
		protected virtual void OnDeactivate()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				agentBehavior.IsActive = false;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
		public virtual float GetScore(bool isSimulation)
		{
			return 0f;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001F8AF File Offset: 0x0001DAAF
		public virtual void ForceThink(float inSeconds)
		{
		}

		// Token: 0x04000223 RID: 547
		public AgentNavigator Navigator;

		// Token: 0x04000224 RID: 548
		public List<AgentBehavior> Behaviors;

		// Token: 0x04000225 RID: 549
		protected float CheckBehaviorTime = 5f;

		// Token: 0x04000226 RID: 550
		protected Timer CheckBehaviorTimer;

		// Token: 0x04000228 RID: 552
		private bool _isActive;
	}
}
