using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000181 RID: 385
	public class TauntSlotDataContainer : MultiplayerLocalDataContainer<TauntSlotData>
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x00011FBA File Offset: 0x000101BA
		protected override string GetSaveDirectoryName()
		{
			return "Data";
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00011FC1 File Offset: 0x000101C1
		protected override string GetSaveFileName()
		{
			return "TauntSlots.json";
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00011FC8 File Offset: 0x000101C8
		protected override PlatformFilePath GetCompatibilityFilePath()
		{
			return new PlatformFilePath(new PlatformDirectoryPath(PlatformFileType.User, "Data"), "Taunts.json");
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00011FE0 File Offset: 0x000101E0
		protected override List<TauntSlotData> DeserializeInCompatibilityMode(string serializedJson)
		{
			List<TauntSlotData> list = new List<TauntSlotData>();
			try
			{
				Dictionary<string, List<ValueTuple<string, int>>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ValueTuple<string, int>>>>(serializedJson);
				if (dictionary != null)
				{
					foreach (KeyValuePair<string, List<ValueTuple<string, int>>> keyValuePair in dictionary)
					{
						string key = keyValuePair.Key;
						List<TauntIndexData> list2 = new List<TauntIndexData>();
						if (keyValuePair.Value != null)
						{
							foreach (ValueTuple<string, int> valueTuple in keyValuePair.Value)
							{
								if (string.IsNullOrEmpty(valueTuple.Item1))
								{
									Debug.FailedAssert("Taunt id is null when trying to load in compatibility mode", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\TauntSlotDataContainer.cs", "DeserializeInCompatibilityMode", 120);
								}
								else
								{
									for (int i = 0; i < list2.Count; i++)
									{
										if (list2[i].TauntIndex == valueTuple.Item2)
										{
											Debug.FailedAssert("Taunt index used for multiple taunts", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\TauntSlotDataContainer.cs", "DeserializeInCompatibilityMode", 128);
										}
									}
									list2.Add(new TauntIndexData(valueTuple.Item1, valueTuple.Item2));
								}
							}
						}
						list.Add(new TauntSlotData(key)
						{
							TauntIndices = list2
						});
					}
				}
			}
			catch
			{
				Debug.FailedAssert("Failed to resolve taunt slot data in compatibility mode. Resetting local data.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\TauntSlotDataContainer.cs", "DeserializeInCompatibilityMode", 145);
			}
			return list;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00012194 File Offset: 0x00010394
		public MBReadOnlyList<TauntIndexData> GetTauntIndicesForPlayer(string playerId)
		{
			MBReadOnlyList<TauntSlotData> entries = base.GetEntries();
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].PlayerId == playerId)
				{
					return new MBReadOnlyList<TauntIndexData>(entries[i].TauntIndices);
				}
			}
			return null;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000121E0 File Offset: 0x000103E0
		public void SetTauntIndicesForPlayer(string playerId, List<TauntIndexData> tauntIndices)
		{
			MBReadOnlyList<TauntSlotData> entries = base.GetEntries();
			TauntSlotData tauntSlotData = null;
			int index = -1;
			for (int i = 0; i < entries.Count; i++)
			{
				TauntSlotData tauntSlotData2 = entries[i];
				if (tauntSlotData2.PlayerId == playerId)
				{
					tauntSlotData = tauntSlotData2;
					index = i;
					break;
				}
			}
			TauntSlotData tauntSlotData3 = new TauntSlotData(playerId);
			tauntSlotData3.TauntIndices = tauntIndices.ToList<TauntIndexData>();
			if (tauntSlotData != null)
			{
				base.RemoveEntry(tauntSlotData);
				base.InsertEntry(tauntSlotData3, index);
				return;
			}
			base.AddEntry(tauntSlotData3);
		}
	}
}
