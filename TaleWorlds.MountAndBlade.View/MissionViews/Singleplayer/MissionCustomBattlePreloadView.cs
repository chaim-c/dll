using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000067 RID: 103
	public class MissionCustomBattlePreloadView : MissionView
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x0002295C File Offset: 0x00020B5C
		public override void OnPreMissionTick(float dt)
		{
			if (!this._preloadDone)
			{
				MissionCombatantsLogic missionBehavior = base.Mission.GetMissionBehavior<MissionCombatantsLogic>();
				List<BasicCharacterObject> list = new List<BasicCharacterObject>();
				foreach (IBattleCombatant battleCombatant in missionBehavior.GetAllCombatants())
				{
					list.AddRange(((CustomBattleCombatant)battleCombatant).Characters);
				}
				this._helperInstance.PreloadCharacters(list);
				SiegeDeploymentMissionController missionBehavior2 = Mission.Current.GetMissionBehavior<SiegeDeploymentMissionController>();
				if (missionBehavior2 != null)
				{
					this._helperInstance.PreloadItems(missionBehavior2.GetSiegeMissiles());
				}
				this._preloadDone = true;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00022A00 File Offset: 0x00020C00
		public override void OnSceneRenderingStarted()
		{
			this._helperInstance.WaitForMeshesToBeLoaded();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00022A0D File Offset: 0x00020C0D
		public override void OnMissionStateDeactivated()
		{
			base.OnMissionStateDeactivated();
			this._helperInstance.Clear();
		}

		// Token: 0x040002AA RID: 682
		private PreloadHelper _helperInstance = new PreloadHelper();

		// Token: 0x040002AB RID: 683
		private bool _preloadDone;
	}
}
