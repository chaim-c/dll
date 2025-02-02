using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027D RID: 637
	public class MissionHardBorderPlacer : MissionLogic
	{
		// Token: 0x06002197 RID: 8599 RVA: 0x0007ADE8 File Offset: 0x00078FE8
		public override void EarlyStart()
		{
			base.EarlyStart();
			Scene scene = base.Mission.Scene;
			GameEntity entity = GameEntity.CreateEmpty(scene, true);
			scene.FillEntityWithHardBorderPhysicsBarrier(entity);
		}
	}
}
