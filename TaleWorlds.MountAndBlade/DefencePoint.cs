using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031D RID: 797
	public class DefencePoint : ScriptComponentBehavior
	{
		// Token: 0x06002B02 RID: 11010 RVA: 0x000A66A2 File Offset: 0x000A48A2
		public void AddDefender(Agent defender)
		{
			this.defenders.Add(defender);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000A66B0 File Offset: 0x000A48B0
		public bool RemoveDefender(Agent defender)
		{
			return this.defenders.Remove(defender);
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000A66BE File Offset: 0x000A48BE
		public IEnumerable<Agent> Defenders
		{
			get
			{
				return this.defenders;
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000A66C8 File Offset: 0x000A48C8
		public void PurgeInactiveDefenders()
		{
			foreach (Agent defender in (from d in this.defenders
			where !d.IsActive()
			select d).ToList<Agent>())
			{
				this.RemoveDefender(defender);
			}
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000A6748 File Offset: 0x000A4948
		private MatrixFrame GetPosition(int index)
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			Vec3 f = globalFrame.rotation.f;
			f.Normalize();
			globalFrame.origin -= f * (float)index * ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRadius) * 2f * 1.5f;
			return globalFrame;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000A67B4 File Offset: 0x000A49B4
		public MatrixFrame GetVacantPosition(Agent a)
		{
			Mission mission = Mission.Current;
			Team team = mission.Teams.First((Team t) => t.Side == this.Side);
			for (int i = 0; i < 100; i++)
			{
				MatrixFrame position = this.GetPosition(i);
				Agent closestAllyAgent = mission.GetClosestAllyAgent(team, position.origin, ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRadius));
				if (closestAllyAgent == null || closestAllyAgent == a)
				{
					return position;
				}
			}
			Debug.FailedAssert("Couldn't find a vacant position", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\DefencePoint.cs", "GetVacantPosition", 73);
			return MatrixFrame.Identity;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000A6834 File Offset: 0x000A4A34
		public int CountOccupiedDefenderPositions()
		{
			Mission mission = Mission.Current;
			Team team = mission.Teams.First((Team t) => t.Side == this.Side);
			for (int i = 0; i < 100; i++)
			{
				MatrixFrame position = this.GetPosition(i);
				if (mission.GetClosestAllyAgent(team, position.origin, ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRadius)) == null)
				{
					return i;
				}
			}
			return 100;
		}

		// Token: 0x0400109E RID: 4254
		private List<Agent> defenders = new List<Agent>();

		// Token: 0x0400109F RID: 4255
		public BattleSideEnum Side;
	}
}
