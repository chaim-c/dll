using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027B RID: 635
	public class MissionBoundaryPlacer : MissionLogic
	{
		// Token: 0x0600218A RID: 8586 RVA: 0x0007A53C File Offset: 0x0007873C
		public override void EarlyStart()
		{
			this.AddMissionBoundaries();
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0007A544 File Offset: 0x00078744
		public void AddMissionBoundaries()
		{
			string name;
			List<Vec2> sceneBoundaryPoints = MBSceneUtilities.GetSceneBoundaryPoints(base.Mission.Scene, out name);
			base.Mission.Boundaries.Add(name, sceneBoundaryPoints);
		}

		// Token: 0x04000C79 RID: 3193
		public const string DefaultWalkAreaBoundaryName = "walk_area";
	}
}
