using System;
using System.Collections.Generic;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000148 RID: 328
	public class DetachmentData
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0002F8D7 File Offset: 0x0002DAD7
		public int AgentCount
		{
			get
			{
				return this.joinedFormations.SumQ((Formation f) => f.CountOfDetachableNonplayerUnits) + this.MovingAgentCount + this.DefendingAgentCount;
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002F914 File Offset: 0x0002DB14
		public bool IsPrecalculated()
		{
			int count = this.agentScores.Count;
			return count > 0 && count >= this.AgentCount;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002F93F File Offset: 0x0002DB3F
		public DetachmentData()
		{
			this.firstTime = MBCommon.GetTotalMissionTime();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0002F968 File Offset: 0x0002DB68
		public void RemoveScoreOfAgent(Agent agent)
		{
			for (int i = this.agentScores.Count - 1; i >= 0; i--)
			{
				if (this.agentScores[i].Item1 == agent)
				{
					this.agentScores.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x040003CE RID: 974
		public List<Formation> joinedFormations = new List<Formation>();

		// Token: 0x040003CF RID: 975
		public List<ValueTuple<Agent, List<float>>> agentScores = new List<ValueTuple<Agent, List<float>>>();

		// Token: 0x040003D0 RID: 976
		public int MovingAgentCount;

		// Token: 0x040003D1 RID: 977
		public int DefendingAgentCount;

		// Token: 0x040003D2 RID: 978
		public float firstTime;
	}
}
