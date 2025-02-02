using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000156 RID: 342
	public class CastleGateAI : UsableMachineAIBase
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x0003768A File Offset: 0x0003588A
		public void ResetInitialGateState(CastleGate.GateState newInitialState)
		{
			this._initialState = newInitialState;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00037693 File Offset: 0x00035893
		public CastleGateAI(CastleGate gate) : base(gate)
		{
			this._initialState = gate.State;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000376A8 File Offset: 0x000358A8
		public override bool HasActionCompleted
		{
			get
			{
				return ((CastleGate)this.UsableMachine).State != this._initialState;
			}
		}

		// Token: 0x04000461 RID: 1121
		private CastleGate.GateState _initialState;
	}
}
