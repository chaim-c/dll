using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CF RID: 463
	public static class MusicParameters
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0005C8B6 File Offset: 0x0005AAB6
		public static int SmallBattleTreshold
		{
			get
			{
				return (int)MusicParameters._parameters[0];
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0005C8C0 File Offset: 0x0005AAC0
		public static int MediumBattleTreshold
		{
			get
			{
				return (int)MusicParameters._parameters[1];
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0005C8CA File Offset: 0x0005AACA
		public static int LargeBattleTreshold
		{
			get
			{
				return (int)MusicParameters._parameters[2];
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0005C8D4 File Offset: 0x0005AAD4
		public static float SmallBattleDistanceTreshold
		{
			get
			{
				return MusicParameters._parameters[3];
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0005C8DD File Offset: 0x0005AADD
		public static float MediumBattleDistanceTreshold
		{
			get
			{
				return MusicParameters._parameters[4];
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0005C8E6 File Offset: 0x0005AAE6
		public static float LargeBattleDistanceTreshold
		{
			get
			{
				return MusicParameters._parameters[5];
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0005C8EF File Offset: 0x0005AAEF
		public static float MaxBattleDistanceTreshold
		{
			get
			{
				return MusicParameters._parameters[6];
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0005C8F8 File Offset: 0x0005AAF8
		public static float MinIntensity
		{
			get
			{
				return MusicParameters._parameters[7];
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0005C901 File Offset: 0x0005AB01
		public static float DefaultStartIntensity
		{
			get
			{
				return MusicParameters._parameters[8];
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0005C90A File Offset: 0x0005AB0A
		public static float PlayerChargeEffectMultiplierOnIntensity
		{
			get
			{
				return MusicParameters._parameters[9];
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0005C914 File Offset: 0x0005AB14
		public static float BattleSizeEffectOnStartIntensity
		{
			get
			{
				return MusicParameters._parameters[10];
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0005C91E File Offset: 0x0005AB1E
		public static float RandomEffectMultiplierOnStartIntensity
		{
			get
			{
				return MusicParameters._parameters[11];
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0005C928 File Offset: 0x0005AB28
		public static float FriendlyTroopDeadEffectOnIntensity
		{
			get
			{
				return MusicParameters._parameters[12];
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0005C932 File Offset: 0x0005AB32
		public static float EnemyTroopDeadEffectOnIntensity
		{
			get
			{
				return MusicParameters._parameters[13];
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0005C93C File Offset: 0x0005AB3C
		public static float PlayerTroopDeadEffectMultiplierOnIntensity
		{
			get
			{
				return MusicParameters._parameters[14];
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0005C946 File Offset: 0x0005AB46
		public static float BattleRatioTresholdOnIntensity
		{
			get
			{
				return MusicParameters._parameters[15];
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0005C950 File Offset: 0x0005AB50
		public static float BattleTurnsOneSideCooldown
		{
			get
			{
				return MusicParameters._parameters[16];
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0005C95A File Offset: 0x0005AB5A
		public static float CampaignDarkModeThreshold
		{
			get
			{
				return MusicParameters._parameters[17];
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0005C964 File Offset: 0x0005AB64
		public static void LoadFromXml()
		{
			MusicParameters._parameters = new float[18];
			string path = ModuleHelper.GetModuleFullPath("Native") + "ModuleData/music_parameters.xml";
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(path);
			string xml = streamReader.ReadToEnd();
			xmlDocument.LoadXml(xml);
			streamReader.Close();
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == "music_parameters")
				{
					using (IEnumerator enumerator2 = xmlNode.ChildNodes.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.NodeType == XmlNodeType.Element)
							{
								MusicParameters.MusicParametersEnum musicParametersEnum = (MusicParameters.MusicParametersEnum)Enum.Parse(typeof(MusicParameters.MusicParametersEnum), xmlNode2.Attributes["id"].Value);
								float num = float.Parse(xmlNode2.Attributes["value"].Value, CultureInfo.InvariantCulture);
								MusicParameters._parameters[(int)musicParametersEnum] = num;
							}
						}
						break;
					}
				}
			}
			Debug.Print("MusicParameters have been resetted.", 0, Debug.DebugColor.Green, 281474976710656UL);
		}

		// Token: 0x04000834 RID: 2100
		private static float[] _parameters;

		// Token: 0x04000835 RID: 2101
		public const float ZeroIntensity = 0f;

		// Token: 0x020004DB RID: 1243
		private enum MusicParametersEnum
		{
			// Token: 0x04001B34 RID: 6964
			SmallBattleTreshold,
			// Token: 0x04001B35 RID: 6965
			MediumBattleTreshold,
			// Token: 0x04001B36 RID: 6966
			LargeBattleTreshold,
			// Token: 0x04001B37 RID: 6967
			SmallBattleDistanceTreshold,
			// Token: 0x04001B38 RID: 6968
			MediumBattleDistanceTreshold,
			// Token: 0x04001B39 RID: 6969
			LargeBattleDistanceTreshold,
			// Token: 0x04001B3A RID: 6970
			MaxBattleDistanceTreshold,
			// Token: 0x04001B3B RID: 6971
			MinIntensity,
			// Token: 0x04001B3C RID: 6972
			DefaultStartIntensity,
			// Token: 0x04001B3D RID: 6973
			PlayerChargeEffectMultiplierOnIntensity,
			// Token: 0x04001B3E RID: 6974
			BattleSizeEffectOnStartIntensity,
			// Token: 0x04001B3F RID: 6975
			RandomEffectMultiplierOnStartIntensity,
			// Token: 0x04001B40 RID: 6976
			FriendlyTroopDeadEffectOnIntensity,
			// Token: 0x04001B41 RID: 6977
			EnemyTroopDeadEffectOnIntensity,
			// Token: 0x04001B42 RID: 6978
			PlayerTroopDeadEffectMultiplierOnIntensity,
			// Token: 0x04001B43 RID: 6979
			BattleRatioTresholdOnIntensity,
			// Token: 0x04001B44 RID: 6980
			BattleTurnsOneSideCooldown,
			// Token: 0x04001B45 RID: 6981
			CampaignDarkModeThreshold,
			// Token: 0x04001B46 RID: 6982
			Count
		}
	}
}
