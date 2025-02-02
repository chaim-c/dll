using System;
using System.Collections.Generic;
using SandBox.Missions.MissionLogics.Arena;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions.Tournaments
{
	// Token: 0x02000021 RID: 33
	internal class ArenaPreloadView : MissionView
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000C560 File Offset: 0x0000A760
		public override void OnPreMissionTick(float dt)
		{
			if (!this._preloadDone)
			{
				List<BasicCharacterObject> list = new List<BasicCharacterObject>();
				if (Mission.Current.GetMissionBehavior<ArenaPracticeFightMissionController>() != null)
				{
					foreach (CharacterObject item in ArenaPracticeFightMissionController.GetParticipantCharacters(Settlement.CurrentSettlement))
					{
						list.Add(item);
					}
					list.Add(CharacterObject.PlayerCharacter);
				}
				TournamentBehavior missionBehavior = Mission.Current.GetMissionBehavior<TournamentBehavior>();
				if (missionBehavior != null)
				{
					foreach (CharacterObject item2 in missionBehavior.GetAllPossibleParticipants())
					{
						list.Add(item2);
					}
				}
				this._helperInstance.PreloadCharacters(list);
				this._preloadDone = true;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000C644 File Offset: 0x0000A844
		public override void OnSceneRenderingStarted()
		{
			this._helperInstance.WaitForMeshesToBeLoaded();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000C651 File Offset: 0x0000A851
		public override void OnMissionStateDeactivated()
		{
			base.OnMissionStateDeactivated();
			this._helperInstance.Clear();
		}

		// Token: 0x04000079 RID: 121
		private readonly PreloadHelper _helperInstance = new PreloadHelper();

		// Token: 0x0400007A RID: 122
		private bool _preloadDone;
	}
}
