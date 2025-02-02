using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x02000004 RID: 4
	public class SettlementPositionScript : ScriptComponentBehavior
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		private string SettlementsXmlPath
		{
			get
			{
				string text = base.Scene.GetModulePath();
				text = text.Remove(0, 6);
				return BasePath.Name + text + "ModuleData/settlements.xml";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		private string SettlementsDistanceCacheFilePath
		{
			get
			{
				string text = base.Scene.GetModulePath();
				text = text.Remove(0, 6);
				return BasePath.Name + text + "ModuleData/settlements_distance_cache.bin";
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "SavePositions")
			{
				this.SaveSettlementPositions();
			}
			if (variableName == "ComputeAndSaveSettlementDistanceCache")
			{
				this.SaveSettlementDistanceCache();
			}
			if (variableName == "CheckPositions")
			{
				this.CheckSettlementPositions();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210D File Offset: 0x0000030D
		protected override void OnSceneSave(string saveFolder)
		{
			base.OnSceneSave(saveFolder);
			this.SaveSettlementPositions();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		private void CheckSettlementPositions()
		{
			XmlDocument xmlDocument = this.LoadXmlFile(this.SettlementsXmlPath);
			base.GameEntity.RemoveAllChildren();
			foreach (object obj in xmlDocument.DocumentElement.SelectNodes("Settlement"))
			{
				string value = ((XmlNode)obj).Attributes["id"].Value;
				GameEntity campaignEntityWithName = base.Scene.GetCampaignEntityWithName(value);
				Vec3 origin = campaignEntityWithName.GetGlobalFrame().origin;
				Vec3 vec = default(Vec3);
				List<GameEntity> list = new List<GameEntity>();
				campaignEntityWithName.GetChildrenRecursive(ref list);
				bool flag = false;
				foreach (GameEntity gameEntity in list)
				{
					if (gameEntity.HasTag("main_map_city_gate"))
					{
						vec = gameEntity.GetGlobalFrame().origin;
						flag = true;
						break;
					}
				}
				Vec3 pos = origin;
				if (flag)
				{
					pos = vec;
				}
				PathFaceRecord pathFaceRecord = new PathFaceRecord(-1, -1, -1);
				base.GameEntity.Scene.GetNavMeshFaceIndex(ref pathFaceRecord, pos.AsVec2, true, false);
				int num = 0;
				if (pathFaceRecord.IsValid())
				{
					num = pathFaceRecord.FaceGroupIndex;
				}
				if (num == 0 || num == 7 || num == 8 || num == 10 || num == 11 || num == 13 || num == 14)
				{
					MBEditor.ZoomToPosition(pos);
					break;
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022C0 File Offset: 0x000004C0
		protected override void OnInit()
		{
			try
			{
				Debug.Print("SettlementsDistanceCacheFilePath: " + this.SettlementsDistanceCacheFilePath, 0, Debug.DebugColor.White, 17592186044416UL);
				BinaryReader binaryReader = new BinaryReader(File.Open(this.SettlementsDistanceCacheFilePath, FileMode.Open, FileAccess.Read));
				if (Campaign.Current.Models.MapDistanceModel is DefaultMapDistanceModel)
				{
					((DefaultMapDistanceModel)Campaign.Current.Models.MapDistanceModel).LoadCacheFromFile(binaryReader);
				}
				binaryReader.Close();
			}
			catch
			{
				Debug.FailedAssert("SettlementsDistanceCacheFilePath could not be read!. Cache will be created right now, This may take few minutes!", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\SettlementPositionScript.cs", "OnInit", 165);
				Debug.Print("SettlementsDistanceCacheFilePath could not be read!. Cache will be created right now, This may take few minutes!", 0, Debug.DebugColor.White, 17592186044416UL);
				this.SaveSettlementDistanceCache();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002384 File Offset: 0x00000584
		private List<SettlementPositionScript.SettlementRecord> LoadSettlementData(XmlDocument settlementDocument)
		{
			List<SettlementPositionScript.SettlementRecord> list = new List<SettlementPositionScript.SettlementRecord>();
			base.GameEntity.RemoveAllChildren();
			foreach (object obj in settlementDocument.DocumentElement.SelectNodes("Settlement"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				string value = xmlNode.Attributes["name"].Value;
				string value2 = xmlNode.Attributes["id"].Value;
				GameEntity campaignEntityWithName = base.Scene.GetCampaignEntityWithName(value2);
				if (!(campaignEntityWithName == null))
				{
					Vec2 asVec = campaignEntityWithName.GetGlobalFrame().origin.AsVec2;
					Vec2 vec = default(Vec2);
					List<GameEntity> list2 = new List<GameEntity>();
					campaignEntityWithName.GetChildrenRecursive(ref list2);
					bool flag = false;
					foreach (GameEntity gameEntity in list2)
					{
						if (gameEntity.HasTag("main_map_city_gate"))
						{
							vec = gameEntity.GetGlobalFrame().origin.AsVec2;
							flag = true;
						}
					}
					list.Add(new SettlementPositionScript.SettlementRecord(value, value2, asVec, flag ? vec : asVec, xmlNode, flag));
				}
			}
			return list;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000250C File Offset: 0x0000070C
		private void SaveSettlementPositions()
		{
			XmlDocument xmlDocument = this.LoadXmlFile(this.SettlementsXmlPath);
			foreach (SettlementPositionScript.SettlementRecord settlementRecord in this.LoadSettlementData(xmlDocument))
			{
				if (settlementRecord.Node.Attributes["posX"] == null)
				{
					XmlAttribute node = xmlDocument.CreateAttribute("posX");
					settlementRecord.Node.Attributes.Append(node);
				}
				settlementRecord.Node.Attributes["posX"].Value = settlementRecord.Position.X.ToString();
				if (settlementRecord.Node.Attributes["posY"] == null)
				{
					XmlAttribute node2 = xmlDocument.CreateAttribute("posY");
					settlementRecord.Node.Attributes.Append(node2);
				}
				settlementRecord.Node.Attributes["posY"].Value = settlementRecord.Position.Y.ToString();
				if (settlementRecord.HasGate)
				{
					if (settlementRecord.Node.Attributes["gate_posX"] == null)
					{
						XmlAttribute node3 = xmlDocument.CreateAttribute("gate_posX");
						settlementRecord.Node.Attributes.Append(node3);
					}
					settlementRecord.Node.Attributes["gate_posX"].Value = settlementRecord.GatePosition.X.ToString();
					if (settlementRecord.Node.Attributes["gate_posY"] == null)
					{
						XmlAttribute node4 = xmlDocument.CreateAttribute("gate_posY");
						settlementRecord.Node.Attributes.Append(node4);
					}
					settlementRecord.Node.Attributes["gate_posY"].Value = settlementRecord.GatePosition.Y.ToString();
				}
			}
			xmlDocument.Save(this.SettlementsXmlPath);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002734 File Offset: 0x00000934
		private void SaveSettlementDistanceCache()
		{
			BinaryWriter binaryWriter = null;
			try
			{
				XmlDocument settlementDocument = this.LoadXmlFile(this.SettlementsXmlPath);
				List<SettlementPositionScript.SettlementRecord> list = this.LoadSettlementData(settlementDocument);
				int navigationMeshIndexOfTerrainType = MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Mountain);
				int navigationMeshIndexOfTerrainType2 = MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Lake);
				int navigationMeshIndexOfTerrainType3 = MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Water);
				int navigationMeshIndexOfTerrainType4 = MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.River);
				int navigationMeshIndexOfTerrainType5 = MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Canyon);
				int navigationMeshIndexOfTerrainType6 = MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.RuralArea);
				base.Scene.SetAbilityOfFacesWithId(navigationMeshIndexOfTerrainType, false);
				base.Scene.SetAbilityOfFacesWithId(navigationMeshIndexOfTerrainType2, false);
				base.Scene.SetAbilityOfFacesWithId(navigationMeshIndexOfTerrainType3, false);
				base.Scene.SetAbilityOfFacesWithId(navigationMeshIndexOfTerrainType4, false);
				base.Scene.SetAbilityOfFacesWithId(navigationMeshIndexOfTerrainType5, false);
				base.Scene.SetAbilityOfFacesWithId(navigationMeshIndexOfTerrainType6, false);
				binaryWriter = new BinaryWriter(File.Open(this.SettlementsDistanceCacheFilePath, FileMode.Create));
				binaryWriter.Write(list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					binaryWriter.Write(list[i].SettlementId);
					Vec2 gatePosition = list[i].GatePosition;
					PathFaceRecord pathFaceRecord = new PathFaceRecord(-1, -1, -1);
					base.Scene.GetNavMeshFaceIndex(ref pathFaceRecord, gatePosition, false, false);
					for (int j = i + 1; j < list.Count; j++)
					{
						binaryWriter.Write(list[j].SettlementId);
						Vec2 gatePosition2 = list[j].GatePosition;
						PathFaceRecord pathFaceRecord2 = new PathFaceRecord(-1, -1, -1);
						base.Scene.GetNavMeshFaceIndex(ref pathFaceRecord2, gatePosition2, false, false);
						float value;
						base.Scene.GetPathDistanceBetweenAIFaces(pathFaceRecord.FaceIndex, pathFaceRecord2.FaceIndex, gatePosition, gatePosition2, 0.1f, float.MaxValue, out value);
						binaryWriter.Write(value);
					}
				}
				int navMeshFaceCount = base.Scene.GetNavMeshFaceCount();
				for (int k = 0; k < navMeshFaceCount; k++)
				{
					int idOfNavMeshFace = base.Scene.GetIdOfNavMeshFace(k);
					if (idOfNavMeshFace != navigationMeshIndexOfTerrainType && idOfNavMeshFace != navigationMeshIndexOfTerrainType2 && idOfNavMeshFace != navigationMeshIndexOfTerrainType3 && idOfNavMeshFace != navigationMeshIndexOfTerrainType4 && idOfNavMeshFace != navigationMeshIndexOfTerrainType5 && idOfNavMeshFace != navigationMeshIndexOfTerrainType6)
					{
						Vec3 zero = Vec3.Zero;
						base.Scene.GetNavMeshCenterPosition(k, ref zero);
						Vec2 asVec = zero.AsVec2;
						float num = float.MaxValue;
						string value2 = "";
						for (int l = 0; l < list.Count; l++)
						{
							Vec2 gatePosition3 = list[l].GatePosition;
							PathFaceRecord pathFaceRecord3 = new PathFaceRecord(-1, -1, -1);
							base.Scene.GetNavMeshFaceIndex(ref pathFaceRecord3, gatePosition3, false, false);
							float num2;
							if ((num == 3.4028235E+38f || asVec.DistanceSquared(gatePosition3) < num * num) && base.Scene.GetPathDistanceBetweenAIFaces(k, pathFaceRecord3.FaceIndex, asVec, gatePosition3, 0.1f, num, out num2) && num2 < num)
							{
								num = num2;
								value2 = list[l].SettlementId;
							}
						}
						if (!string.IsNullOrEmpty(value2))
						{
							binaryWriter.Write(k);
							binaryWriter.Write(value2);
						}
					}
				}
				binaryWriter.Write(-1);
			}
			catch
			{
			}
			finally
			{
				base.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Mountain), true);
				base.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Lake), true);
				base.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Water), true);
				base.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.River), true);
				base.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Canyon), true);
				base.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.RuralArea), true);
				if (binaryWriter != null)
				{
					binaryWriter.Close();
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002AE0 File Offset: 0x00000CE0
		private XmlDocument LoadXmlFile(string path)
		{
			Debug.Print("opening " + path, 0, Debug.DebugColor.White, 17592186044416UL);
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(path);
			string xml = streamReader.ReadToEnd();
			xmlDocument.LoadXml(xml);
			streamReader.Close();
			return xmlDocument;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002B29 File Offset: 0x00000D29
		protected override bool IsOnlyVisual()
		{
			return true;
		}

		// Token: 0x04000001 RID: 1
		public SimpleButton CheckPositions;

		// Token: 0x04000002 RID: 2
		public SimpleButton SavePositions;

		// Token: 0x04000003 RID: 3
		public SimpleButton ComputeAndSaveSettlementDistanceCache;

		// Token: 0x0200005E RID: 94
		private struct SettlementRecord
		{
			// Token: 0x06000433 RID: 1075 RVA: 0x00022FA7 File Offset: 0x000211A7
			public SettlementRecord(string settlementName, string settlementId, Vec2 position, Vec2 gatePosition, XmlNode node, bool hasGate)
			{
				this.SettlementName = settlementName;
				this.SettlementId = settlementId;
				this.Position = position;
				this.GatePosition = gatePosition;
				this.Node = node;
				this.HasGate = hasGate;
			}

			// Token: 0x04000250 RID: 592
			public readonly string SettlementName;

			// Token: 0x04000251 RID: 593
			public readonly string SettlementId;

			// Token: 0x04000252 RID: 594
			public readonly XmlNode Node;

			// Token: 0x04000253 RID: 595
			public readonly Vec2 Position;

			// Token: 0x04000254 RID: 596
			public readonly Vec2 GatePosition;

			// Token: 0x04000255 RID: 597
			public readonly bool HasGate;
		}
	}
}
