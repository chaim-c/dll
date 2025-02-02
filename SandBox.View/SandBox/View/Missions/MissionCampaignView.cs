using System;
using System.Collections.Generic;
using SandBox.BoardGames.MissionLogics;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Missions
{
	// Token: 0x02000014 RID: 20
	public class MissionCampaignView : MissionView
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00005A0C File Offset: 0x00003C0C
		public override void OnMissionScreenPreLoad()
		{
			this._mapScreen = MapScreen.Instance;
			if (this._mapScreen != null && base.Mission.NeedsMemoryCleanup && ScreenManager.ScreenTypeExistsAtList(this._mapScreen))
			{
				this._mapScreen.ClearGPUMemory();
				Utilities.ClearShaderMemory();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005A4B File Offset: 0x00003C4B
		public override void OnMissionScreenFinalize()
		{
			MapScreen mapScreen = this._mapScreen;
			if (((mapScreen != null) ? mapScreen.BannerTexturedMaterialCache : null) != null)
			{
				this._mapScreen.BannerTexturedMaterialCache.Clear();
			}
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005A88 File Offset: 0x00003C88
		[CommandLineFunctionality.CommandLineArgumentFunction("get_face_and_helmet_info_of_followed_agent", "mission")]
		public static string GetFaceAndHelmetInfoOfFollowedAgent(List<string> strings)
		{
			MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
			if (missionScreen == null)
			{
				return "Only works at missions";
			}
			Agent lastFollowedAgent = missionScreen.LastFollowedAgent;
			if (lastFollowedAgent == null)
			{
				return "An agent needs to be focussed.";
			}
			string text = "";
			text += lastFollowedAgent.BodyPropertiesValue.ToString();
			EquipmentElement equipmentFromSlot = lastFollowedAgent.SpawnEquipment.GetEquipmentFromSlot(EquipmentIndex.NumAllWeaponSlots);
			if (!equipmentFromSlot.IsEmpty)
			{
				text = text + "\n Armor Name: " + equipmentFromSlot.Item.Name.ToString();
				text = text + "\n Mesh Name: " + equipmentFromSlot.Item.MultiMeshName;
			}
			if (lastFollowedAgent.Character != null)
			{
				CharacterObject characterObject = lastFollowedAgent.Character as CharacterObject;
				if (characterObject != null)
				{
					text = text + "\n Troop Id: " + characterObject.StringId;
				}
			}
			TaleWorlds.InputSystem.Input.SetClipboardText(text);
			return "Copied to clipboard:\n" + text;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005B64 File Offset: 0x00003D64
		public override void EarlyStart()
		{
			base.EarlyStart();
			this._missionMainAgentController = Mission.Current.GetMissionBehavior<MissionMainAgentController>();
			MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			if (missionBehavior != null)
			{
				missionBehavior.GameStarted += this._missionMainAgentController.Disable;
				missionBehavior.GameEnded += this._missionMainAgentController.Enable;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005BC3 File Offset: 0x00003DC3
		public override void OnRenderingStarted()
		{
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.Mission));
		}

		// Token: 0x04000039 RID: 57
		private MapScreen _mapScreen;

		// Token: 0x0400003A RID: 58
		private MissionMainAgentController _missionMainAgentController;
	}
}
