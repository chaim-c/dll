using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x0200003A RID: 58
	public class PopupSceneSequence : ScriptComponentBehavior
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0001873D File Offset: 0x0001693D
		public void InitializeWithAgentVisuals(AgentVisuals visuals)
		{
			this._agentVisuals = visuals;
			this._time = 0f;
			this._triggered = false;
			this._state = 0;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00018767 File Offset: 0x00016967
		protected override void OnInit()
		{
			base.OnInit();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001877B File Offset: 0x0001697B
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00018788 File Offset: 0x00016988
		protected override void OnTick(float dt)
		{
			this._time += dt;
			if (!this._triggered)
			{
				if (this._state == 0 && this._time >= this.InitialActivationTime)
				{
					this._triggered = true;
					this.OnInitialState();
				}
				if (this._state == 1 && this._time >= this.PositiveActivationTime)
				{
					this._triggered = true;
					this.OnPositiveState();
				}
				if (this._state == 2 && this._time >= this.NegativeActivationTime)
				{
					this._triggered = true;
					this.OnNegativeState();
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00018816 File Offset: 0x00016A16
		public virtual void OnInitialState()
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00018818 File Offset: 0x00016A18
		public virtual void OnPositiveState()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001881A File Offset: 0x00016A1A
		public virtual void OnNegativeState()
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0001881C File Offset: 0x00016A1C
		public void SetInitialState()
		{
			this._triggered = false;
			this._state = 0;
			this._time = 0f;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00018837 File Offset: 0x00016A37
		public void SetPositiveState()
		{
			this._triggered = false;
			this._state = 1;
			this._time = 0f;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00018852 File Offset: 0x00016A52
		public void SetNegativeState()
		{
			this._triggered = false;
			this._state = 2;
			this._time = 0f;
		}

		// Token: 0x040001D9 RID: 473
		public float InitialActivationTime;

		// Token: 0x040001DA RID: 474
		public float PositiveActivationTime;

		// Token: 0x040001DB RID: 475
		public float NegativeActivationTime;

		// Token: 0x040001DC RID: 476
		protected AgentVisuals _agentVisuals;

		// Token: 0x040001DD RID: 477
		protected float _time;

		// Token: 0x040001DE RID: 478
		protected bool _triggered;

		// Token: 0x040001DF RID: 479
		protected int _state;
	}
}
