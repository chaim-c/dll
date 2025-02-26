﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030B RID: 779
	public static class MultiplayerGameTypes
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x000A4A72 File Offset: 0x000A2C72
		public static void Initialize()
		{
			MultiplayerGameTypes.CreateGameTypeInformations();
			MultiplayerGameTypes.LoadMultiplayerSceneInformations();
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000A4A7E File Offset: 0x000A2C7E
		public static bool CheckGameTypeInfoExists(string gameType)
		{
			return MultiplayerGameTypes._multiplayerGameTypeInfos.ContainsKey(gameType);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000A4A8B File Offset: 0x000A2C8B
		public static MultiplayerGameTypeInfo GetGameTypeInfo(string gameType)
		{
			if (MultiplayerGameTypes._multiplayerGameTypeInfos.ContainsKey(gameType))
			{
				return MultiplayerGameTypes._multiplayerGameTypeInfos[gameType];
			}
			Debug.Print("Cannot find game type:" + gameType, 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000A4AC4 File Offset: 0x000A2CC4
		private static void LoadMultiplayerSceneInformations()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/Multiplayer/MultiplayerScenes.xml");
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == "MultiplayerScenes")
				{
					using (IEnumerator enumerator2 = xmlNode.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.NodeType != XmlNodeType.Comment)
							{
								string innerText = xmlNode2.Attributes["name"].InnerText;
								foreach (object obj3 in xmlNode2.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj3;
									if (xmlNode3.NodeType != XmlNodeType.Comment)
									{
										string innerText2 = xmlNode3.Attributes["name"].InnerText;
										if (MultiplayerGameTypes._multiplayerGameTypeInfos.ContainsKey(innerText2))
										{
											MultiplayerGameTypes._multiplayerGameTypeInfos[innerText2].Scenes.Add(innerText);
										}
									}
								}
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000A4C7C File Offset: 0x000A2E7C
		private static void CreateGameTypeInformations()
		{
			MultiplayerGameTypes._multiplayerGameTypeInfos = new Dictionary<string, MultiplayerGameTypeInfo>();
			foreach (MultiplayerGameTypeInfo multiplayerGameTypeInfo in Module.CurrentModule.GetMultiplayerGameTypes())
			{
				MultiplayerGameTypes._multiplayerGameTypeInfos.Add(multiplayerGameTypeInfo.GameType, multiplayerGameTypeInfo);
			}
		}

		// Token: 0x04001047 RID: 4167
		private static Dictionary<string, MultiplayerGameTypeInfo> _multiplayerGameTypeInfos;
	}
}
