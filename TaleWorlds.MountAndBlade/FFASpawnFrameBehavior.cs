using System;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BD RID: 701
	public class FFASpawnFrameBehavior : SpawnFrameBehaviorBase
	{
		// Token: 0x060026A2 RID: 9890 RVA: 0x00091A81 File Offset: 0x0008FC81
		public override MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn)
		{
			return base.GetSpawnFrameFromSpawnPoints(this.SpawnPoints.ToList<GameEntity>(), null, hasMount);
		}
	}
}
