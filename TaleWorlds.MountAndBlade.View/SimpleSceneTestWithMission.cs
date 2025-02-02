using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x02000018 RID: 24
	public class SimpleSceneTestWithMission
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006EA0 File Offset: 0x000050A0
		public SimpleSceneTestWithMission(string sceneName, DecalAtlasGroup atlasGroup = DecalAtlasGroup.All)
		{
			this._sceneName = sceneName;
			this._customDecalGroup = atlasGroup;
			this._mission = this.OpenSceneWithMission(this._sceneName, "");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006ECD File Offset: 0x000050CD
		public bool LoadingFinished()
		{
			return this._mission.IsLoadingFinished && Utilities.GetNumberOfShaderCompilationsInProgress() == 0;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006EE8 File Offset: 0x000050E8
		private Mission OpenSceneWithMission(string scene, string sceneLevels = "")
		{
			LoadingWindow.DisableGlobalLoadingWindow();
			return MissionState.OpenNew("SimpleSceneTestWithMission", new MissionInitializerRecord(scene)
			{
				PlayingInCampaignMode = false,
				AtmosphereOnCampaign = AtmosphereInfo.GetInvalidAtmosphereInfo(),
				DecalAtlasGroup = (int)this._customDecalGroup,
				SceneLevels = sceneLevels
			}, (Mission missionController) => new MissionBehavior[]
			{
				new MissionOptionsComponent(),
				new BasicLeaveMissionLogic(false, 0),
				new MissionHardBorderPlacer(),
				new MissionBoundaryPlacer(),
				new MissionBoundaryCrossingHandler(),
				new EquipmentControllerLeaveLogic()
			}, true, true);
		}

		// Token: 0x0400003F RID: 63
		private Mission _mission;

		// Token: 0x04000040 RID: 64
		private string _sceneName;

		// Token: 0x04000041 RID: 65
		private DecalAtlasGroup _customDecalGroup;
	}
}
