using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000307 RID: 775
	public class MPPerkSelectionManager
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000A4286 File Offset: 0x000A2486
		public static MPPerkSelectionManager Instance
		{
			get
			{
				MPPerkSelectionManager result;
				if ((result = MPPerkSelectionManager._instance) == null)
				{
					result = (MPPerkSelectionManager._instance = new MPPerkSelectionManager());
				}
				return result;
			}
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000A429C File Offset: 0x000A249C
		public static void FreeInstance()
		{
			if (MPPerkSelectionManager._instance != null)
			{
				Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> selections = MPPerkSelectionManager._instance._selections;
				if (selections != null)
				{
					selections.Clear();
				}
				Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> pendingChanges = MPPerkSelectionManager._instance._pendingChanges;
				if (pendingChanges != null)
				{
					pendingChanges.Clear();
				}
				MPPerkSelectionManager._instance = null;
			}
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000A42D8 File Offset: 0x000A24D8
		public void InitializeForUser(string username, PlayerId playerId)
		{
			if (this._playerIdOfSelectionsOwner != playerId)
			{
				Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> selections = this._selections;
				if (selections != null)
				{
					selections.Clear();
				}
				this._playerIdOfSelectionsOwner = playerId;
				this._xmlPath = new PlatformFilePath(EngineFilePaths.ConfigsPath, "MPDefaultPerks_" + playerId + ".xml");
				try
				{
					PlatformFilePath platformFilePath = new PlatformFilePath(EngineFilePaths.ConfigsPath, "MPDefaultPerks_" + username + ".xml");
					if (FileHelper.FileExists(platformFilePath))
					{
						FileHelper.CopyFile(platformFilePath, this._xmlPath);
						FileHelper.DeleteFile(platformFilePath);
					}
				}
				catch (Exception)
				{
				}
				Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> dictionary = this.LoadSelectionsForUserFromXML();
				this._selections = (dictionary ?? new Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>());
			}
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000A4398 File Offset: 0x000A2598
		public void ResetPendingChanges()
		{
			Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> pendingChanges = this._pendingChanges;
			if (pendingChanges != null)
			{
				pendingChanges.Clear();
			}
			Action onAfterResetPendingChanges = this.OnAfterResetPendingChanges;
			if (onAfterResetPendingChanges == null)
			{
				return;
			}
			onAfterResetPendingChanges();
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000A43BC File Offset: 0x000A25BC
		public void TryToApplyAndSavePendingChanges()
		{
			if (this._pendingChanges != null)
			{
				foreach (KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> keyValuePair in this._pendingChanges)
				{
					if (this._selections.ContainsKey(keyValuePair.Key))
					{
						this._selections.Remove(keyValuePair.Key);
					}
					this._selections.Add(keyValuePair.Key, keyValuePair.Value);
				}
				this._pendingChanges.Clear();
				List<KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>> selections = new List<KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>>();
				foreach (KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> keyValuePair2 in this._selections)
				{
					selections.Add(new KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>(keyValuePair2.Key, keyValuePair2.Value));
				}
				((ITask)AsyncTask.CreateWithDelegate(new ManagedDelegate
				{
					Instance = delegate
					{
						MPPerkSelectionManager instance = MPPerkSelectionManager.Instance;
						lock (instance)
						{
							this.SaveAsXML(selections);
						}
					}
				}, true)).Invoke();
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000A44F4 File Offset: 0x000A26F4
		public List<MPPerkSelectionManager.MPPerkSelection> GetSelectionsForHeroClass(MultiplayerClassDivisions.MPHeroClass currentHeroClass)
		{
			List<MPPerkSelectionManager.MPPerkSelection> result = new List<MPPerkSelectionManager.MPPerkSelection>();
			if ((this._pendingChanges == null || !this._pendingChanges.TryGetValue(currentHeroClass, out result)) && this._selections != null)
			{
				this._selections.TryGetValue(currentHeroClass, out result);
			}
			return result;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000A453C File Offset: 0x000A273C
		public void SetSelectionsForHeroClassTemporarily(MultiplayerClassDivisions.MPHeroClass currentHeroClass, List<MPPerkSelectionManager.MPPerkSelection> perkChoices)
		{
			if (this._pendingChanges == null)
			{
				this._pendingChanges = new Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>();
			}
			List<MPPerkSelectionManager.MPPerkSelection> list;
			if (!this._pendingChanges.TryGetValue(currentHeroClass, out list))
			{
				list = new List<MPPerkSelectionManager.MPPerkSelection>();
				this._pendingChanges.Add(currentHeroClass, list);
			}
			else
			{
				list.Clear();
			}
			int count = perkChoices.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(perkChoices[i]);
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000A45A8 File Offset: 0x000A27A8
		private Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> LoadSelectionsForUserFromXML()
		{
			Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> dictionary = null;
			MPPerkSelectionManager instance = MPPerkSelectionManager.Instance;
			lock (instance)
			{
				bool flag2 = FileHelper.FileExists(this._xmlPath);
				if (flag2)
				{
					dictionary = new Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>();
					try
					{
						MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> mpheroClasses = MultiplayerClassDivisions.GetMPHeroClasses();
						int count = mpheroClasses.Count;
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(this._xmlPath);
						foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							XmlNode xmlNode2 = xmlNode.Attributes["id"];
							MultiplayerClassDivisions.MPHeroClass mpheroClass = null;
							string value = xmlNode2.Value;
							for (int i = 0; i < count; i++)
							{
								if (mpheroClasses[i].StringId == value)
								{
									mpheroClass = mpheroClasses[i];
									break;
								}
							}
							if (mpheroClass != null)
							{
								List<MPPerkSelectionManager.MPPerkSelection> list = new List<MPPerkSelectionManager.MPPerkSelection>(2);
								foreach (object obj2 in xmlNode.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj2;
									XmlAttribute xmlAttribute = xmlNode3.Attributes["index"];
									XmlAttribute xmlAttribute2 = xmlNode3.Attributes["listIndex"];
									if (xmlAttribute != null && xmlAttribute2 != null)
									{
										int index = Convert.ToInt32(xmlAttribute.Value);
										int listIndex = Convert.ToInt32(xmlAttribute2.Value);
										list.Add(new MPPerkSelectionManager.MPPerkSelection(index, listIndex));
									}
									else
									{
										flag2 = false;
									}
								}
								dictionary.Add(mpheroClass, list);
							}
							else
							{
								flag2 = false;
							}
						}
					}
					catch
					{
						flag2 = false;
					}
				}
				if (!flag2)
				{
					dictionary = null;
				}
			}
			return dictionary;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000A47C8 File Offset: 0x000A29C8
		private bool SaveAsXML(List<KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>>> selections)
		{
			bool result = true;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.InsertBefore(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null), xmlDocument.DocumentElement);
				XmlElement xmlElement = xmlDocument.CreateElement("HeroClasses");
				xmlDocument.AppendChild(xmlElement);
				foreach (KeyValuePair<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> keyValuePair in selections)
				{
					MultiplayerClassDivisions.MPHeroClass key = keyValuePair.Key;
					List<MPPerkSelectionManager.MPPerkSelection> value = keyValuePair.Value;
					XmlElement xmlElement2 = xmlDocument.CreateElement("HeroClass");
					xmlElement2.SetAttribute("id", key.StringId);
					xmlElement.AppendChild(xmlElement2);
					foreach (MPPerkSelectionManager.MPPerkSelection mpperkSelection in value)
					{
						XmlElement xmlElement3 = xmlDocument.CreateElement("PerkSelection");
						xmlElement3.SetAttribute("index", mpperkSelection.Index.ToString());
						xmlElement3.SetAttribute("listIndex", mpperkSelection.ListIndex.ToString());
						xmlElement2.AppendChild(xmlElement3);
					}
				}
				xmlDocument.Save(this._xmlPath);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0400103F RID: 4159
		private static MPPerkSelectionManager _instance;

		// Token: 0x04001040 RID: 4160
		public Action OnAfterResetPendingChanges;

		// Token: 0x04001041 RID: 4161
		private Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> _selections;

		// Token: 0x04001042 RID: 4162
		private Dictionary<MultiplayerClassDivisions.MPHeroClass, List<MPPerkSelectionManager.MPPerkSelection>> _pendingChanges;

		// Token: 0x04001043 RID: 4163
		private PlatformFilePath _xmlPath;

		// Token: 0x04001044 RID: 4164
		private PlayerId _playerIdOfSelectionsOwner;

		// Token: 0x020005CF RID: 1487
		public struct MPPerkSelection
		{
			// Token: 0x06003B57 RID: 15191 RVA: 0x000E8EF0 File Offset: 0x000E70F0
			public MPPerkSelection(int index, int listIndex)
			{
				this.Index = index;
				this.ListIndex = listIndex;
			}

			// Token: 0x04001E94 RID: 7828
			public readonly int Index;

			// Token: 0x04001E95 RID: 7829
			public readonly int ListIndex;
		}
	}
}
