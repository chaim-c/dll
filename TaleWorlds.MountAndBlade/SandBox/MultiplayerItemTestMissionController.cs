using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x020000D9 RID: 217
	public class MultiplayerItemTestMissionController : MissionLogic
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public MultiplayerItemTestMissionController(BasicCultureObject culture)
		{
			this._culture = culture;
			if (!MultiplayerItemTestMissionController._initializeFlag)
			{
				Game.Current.ObjectManager.LoadXML("MPCharacters", false);
				MultiplayerItemTestMissionController._initializeFlag = true;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0000ED35 File Offset: 0x0000CF35
		public override void AfterStart()
		{
			this.GetAllTroops();
			this.SpawnMainAgent();
			this.SpawnMultiplayerTroops();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		private void SpawnMultiplayerTroops()
		{
			foreach (BasicCharacterObject basicCharacterObject in this._troops)
			{
				Vec3 v;
				Vec2 vec;
				this.GetNextSpawnFrame(out v, out vec);
				foreach (Equipment equipment in basicCharacterObject.AllEquipments)
				{
					base.Mission.SpawnAgent(new AgentBuildData(new BasicBattleAgentOrigin(basicCharacterObject)).Equipment(equipment).InitialPosition(v).InitialDirection(vec), false);
					v += new Vec3(0f, 2f, 0f, -1f);
				}
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000EE34 File Offset: 0x0000D034
		private void GetNextSpawnFrame(out Vec3 position, out Vec2 direction)
		{
			this._coordinate += new Vec3(3f, 0f, 0f, -1f);
			if (this._coordinate.x > (float)this._mapHorizontalEndCoordinate)
			{
				this._coordinate.x = 3f;
				this._coordinate.y = this._coordinate.y + 3f;
			}
			position = this._coordinate;
			direction = new Vec2(0f, -1f);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		private XmlDocument LoadXmlFile(string path)
		{
			Debug.Print("opening " + path, 0, Debug.DebugColor.White, 17592186044416UL);
			XmlDocument xmlDocument = new XmlDocument();
			string xml = new StreamReader(path).ReadToEnd();
			xmlDocument.LoadXml(xml);
			return xmlDocument;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000EF08 File Offset: 0x0000D108
		private void SpawnMainAgent()
		{
			if (this.mainAgent == null || this.mainAgent.State != AgentState.Active)
			{
				BasicCharacterObject @object = Game.Current.ObjectManager.GetObject<BasicCharacterObject>("main_hero");
				Mission mission = base.Mission;
				AgentBuildData agentBuildData = new AgentBuildData(new BasicBattleAgentOrigin(@object)).Team(base.Mission.DefenderTeam);
				Vec3 vec = new Vec3(200f + (float)MBRandom.RandomInt(15), 200f + (float)MBRandom.RandomInt(15), 1f, -1f);
				this.mainAgent = mission.SpawnAgent(agentBuildData.InitialPosition(vec).InitialDirection(Vec2.Forward).Controller(Agent.ControllerType.Player), false);
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		private void GetAllTroops()
		{
			foreach (object obj in this.LoadXmlFile(BasePath.Name + "/Modules/Native/ModuleData/mpcharacters.xml").DocumentElement.SelectNodes("NPCCharacter"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlAttributeCollection attributes = xmlNode.Attributes;
				if (((attributes != null) ? attributes["occupation"] : null) != null && xmlNode.Attributes["occupation"].InnerText == "Soldier")
				{
					string innerText = xmlNode.Attributes["id"].InnerText;
					BasicCharacterObject @object = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(innerText);
					if (@object != null && @object.Culture == this._culture)
					{
						this._troops.Add(@object);
					}
				}
			}
		}

		// Token: 0x04000206 RID: 518
		private Agent mainAgent;

		// Token: 0x04000207 RID: 519
		private BasicCultureObject _culture;

		// Token: 0x04000208 RID: 520
		private List<BasicCharacterObject> _troops = new List<BasicCharacterObject>();

		// Token: 0x04000209 RID: 521
		private const float HorizontalGap = 3f;

		// Token: 0x0400020A RID: 522
		private const float VerticalGap = 3f;

		// Token: 0x0400020B RID: 523
		private Vec3 _coordinate = new Vec3(200f, 200f, 0f, -1f);

		// Token: 0x0400020C RID: 524
		private int _mapHorizontalEndCoordinate = 800;

		// Token: 0x0400020D RID: 525
		private static bool _initializeFlag;
	}
}
