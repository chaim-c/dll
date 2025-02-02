using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000165 RID: 357
	public static class TauntUsageManager
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x0000F5BA File Offset: 0x0000D7BA
		static TauntUsageManager()
		{
			TauntUsageManager.Read();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		public static void Read()
		{
			foreach (object obj in TauntUsageManager.LoadXmlFile(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/Multiplayer/taunt_usage_sets.xml").DocumentElement.SelectNodes("taunt_usage_set"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerText = xmlNode.Attributes["id"].InnerText;
				TauntUsageManager._tauntUsageSets.Add(new TauntUsageManager.TauntUsageSet());
				TauntUsageManager._tauntUsageSetIndexMap[innerText] = TauntUsageManager._tauntUsageSets.Count - 1;
				foreach (object obj2 in xmlNode.SelectNodes("taunt_usage"))
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					TauntUsageManager.TauntUsage.TauntUsageFlag tauntUsageFlag = TauntUsageManager.TauntUsage.TauntUsageFlag.None;
					XmlAttribute xmlAttribute = xmlNode2.Attributes["requires_bow"];
					if (bool.Parse(((xmlAttribute != null) ? xmlAttribute.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresBow;
					}
					XmlAttribute xmlAttribute2 = xmlNode2.Attributes["requires_on_foot"];
					if (bool.Parse(((xmlAttribute2 != null) ? xmlAttribute2.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresOnFoot;
					}
					XmlAttribute xmlAttribute3 = xmlNode2.Attributes["requires_shield"];
					if (bool.Parse(((xmlAttribute3 != null) ? xmlAttribute3.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresShield;
					}
					XmlAttribute xmlAttribute4 = xmlNode2.Attributes["is_left_stance"];
					if (bool.Parse(((xmlAttribute4 != null) ? xmlAttribute4.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.IsLeftStance;
					}
					XmlAttribute xmlAttribute5 = xmlNode2.Attributes["unsuitable_for_two_handed"];
					if (bool.Parse(((xmlAttribute5 != null) ? xmlAttribute5.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForTwoHanded;
					}
					XmlAttribute xmlAttribute6 = xmlNode2.Attributes["unsuitable_for_one_handed"];
					if (bool.Parse(((xmlAttribute6 != null) ? xmlAttribute6.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForOneHanded;
					}
					XmlAttribute xmlAttribute7 = xmlNode2.Attributes["unsuitable_for_shield"];
					if (bool.Parse(((xmlAttribute7 != null) ? xmlAttribute7.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForShield;
					}
					XmlAttribute xmlAttribute8 = xmlNode2.Attributes["unsuitable_for_bow"];
					if (bool.Parse(((xmlAttribute8 != null) ? xmlAttribute8.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForBow;
					}
					XmlAttribute xmlAttribute9 = xmlNode2.Attributes["unsuitable_for_crossbow"];
					if (bool.Parse(((xmlAttribute9 != null) ? xmlAttribute9.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForCrossbow;
					}
					XmlAttribute xmlAttribute10 = xmlNode2.Attributes["unsuitable_for_empty"];
					if (bool.Parse(((xmlAttribute10 != null) ? xmlAttribute10.Value : null) ?? "False"))
					{
						tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForEmpty;
					}
					string value = xmlNode2.Attributes["action"].Value;
					TauntUsageManager._tauntUsageSets.Last<TauntUsageManager.TauntUsageSet>().AddUsage(new TauntUsageManager.TauntUsage(tauntUsageFlag, value));
				}
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000F914 File Offset: 0x0000DB14
		public static TauntUsageManager.TauntUsageSet GetUsageSet(string id)
		{
			int num;
			if (TauntUsageManager._tauntUsageSetIndexMap.TryGetValue(id, out num) && num >= 0 && num < TauntUsageManager._tauntUsageSets.Count)
			{
				return TauntUsageManager._tauntUsageSets[num];
			}
			return null;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000F950 File Offset: 0x0000DB50
		public static string GetAction(int index, bool isLeftStance, bool onFoot, WeaponComponentData mainHandWeapon, WeaponComponentData offhandWeapon)
		{
			string result = null;
			foreach (TauntUsageManager.TauntUsage tauntUsage in TauntUsageManager._tauntUsageSets[index].GetUsages())
			{
				if (tauntUsage.IsSuitable(isLeftStance, onFoot, mainHandWeapon, offhandWeapon))
				{
					result = tauntUsage.GetAction();
				}
			}
			return result;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		private static TextObject GetHintTextFromReasons(List<TextObject> reasons)
		{
			TextObject textObject = TextObject.Empty;
			for (int i = 0; i < reasons.Count; i++)
			{
				if (i >= 1)
				{
					GameTexts.SetVariable("STR1", textObject.ToString());
					GameTexts.SetVariable("STR2", reasons[i]);
					textObject = GameTexts.FindText("str_string_newline_string", null);
				}
				else
				{
					textObject = reasons[i];
				}
			}
			return textObject;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public static string GetActionDisabledReasonText(TauntUsageManager.TauntUsage.TauntUsageFlag disabledReasonFlag)
		{
			List<TextObject> list = new List<TextObject>();
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresBow))
			{
				list.Add(new TextObject("{=2GE0in0u}Requires Bow.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresShield))
			{
				list.Add(new TextObject("{=6Tw6BLXI}Requires Shield.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresOnFoot))
			{
				list.Add(new TextObject("{=GHQMM8Df}Can't be used while mounted.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForTwoHanded))
			{
				list.Add(new TextObject("{=EhK4Q6S4}Can't be used with Two Handed weapons.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForOneHanded))
			{
				list.Add(new TextObject("{=wJbkXP98}Can't be used with One Handed weapons.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForShield))
			{
				list.Add(new TextObject("{=bJMUTZ00}Can't be used with Shields.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForBow))
			{
				list.Add(new TextObject("{=B9Gp7pIf}Can't be used with Bows.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForCrossbow))
			{
				list.Add(new TextObject("{=kkzKtP78}Can't be used with Crossbows.", null));
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForEmpty))
			{
				list.Add(new TextObject("{=F59nAr9s}Can't be used without a weapon.", null));
			}
			if (list.Count > 0)
			{
				return TauntUsageManager.GetHintTextFromReasons(list).ToString();
			}
			if (disabledReasonFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.IsLeftStance))
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public static TauntUsageManager.TauntUsage.TauntUsageFlag GetIsActionNotSuitableReason(int index, bool isLeftStance, bool onFoot, WeaponComponentData mainHandWeapon, WeaponComponentData offhandWeapon)
		{
			MBReadOnlyList<TauntUsageManager.TauntUsage> usages = TauntUsageManager._tauntUsageSets[index].GetUsages();
			if (usages.Count == 0)
			{
				Debug.FailedAssert("Taunt usages are empty", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\TauntUsageManager.cs", "GetIsActionNotSuitableReason", 214);
				return TauntUsageManager.TauntUsage.TauntUsageFlag.None;
			}
			TauntUsageManager.TauntUsage.TauntUsageFlag[] array = new TauntUsageManager.TauntUsage.TauntUsageFlag[usages.Count];
			for (int i = 0; i < usages.Count; i++)
			{
				TauntUsageManager.TauntUsage.TauntUsageFlag isNotSuitableReason = usages[i].GetIsNotSuitableReason(isLeftStance, onFoot, mainHandWeapon, offhandWeapon);
				if (isNotSuitableReason == TauntUsageManager.TauntUsage.TauntUsageFlag.None)
				{
					return TauntUsageManager.TauntUsage.TauntUsageFlag.None;
				}
				array[i] = isNotSuitableReason;
			}
			Array.Sort<TauntUsageManager.TauntUsage.TauntUsageFlag>(array, new TauntUsageManager.TauntUsageFlagComparer());
			return array[0];
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000FBDB File Offset: 0x0000DDDB
		public static int GetTauntItemCount()
		{
			return TauntUsageManager._tauntUsageSets.Count;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		public static int GetIndexOfAction(string id)
		{
			int result;
			if (TauntUsageManager._tauntUsageSetIndexMap.TryGetValue(id, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000FC07 File Offset: 0x0000DE07
		public static string GetDefaultAction(int index)
		{
			TauntUsageManager.TauntUsage tauntUsage = TauntUsageManager._tauntUsageSets[index].GetUsages().Last<TauntUsageManager.TauntUsage>();
			if (tauntUsage == null)
			{
				return null;
			}
			return tauntUsage.GetAction();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		private static XmlDocument LoadXmlFile(string path)
		{
			string xml = new StreamReader(path).ReadToEnd();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			return xmlDocument;
		}

		// Token: 0x040004AA RID: 1194
		private static List<TauntUsageManager.TauntUsageSet> _tauntUsageSets = new List<TauntUsageManager.TauntUsageSet>();

		// Token: 0x040004AB RID: 1195
		private static Dictionary<string, int> _tauntUsageSetIndexMap = new Dictionary<string, int>();

		// Token: 0x020001E1 RID: 481
		private class TauntUsageFlagComparer : IComparer<TauntUsageManager.TauntUsage.TauntUsageFlag>
		{
			// Token: 0x06000BA8 RID: 2984 RVA: 0x00017834 File Offset: 0x00015A34
			public int Compare(TauntUsageManager.TauntUsage.TauntUsageFlag x, TauntUsageManager.TauntUsage.TauntUsageFlag y)
			{
				int num = (int)x;
				return num.CompareTo((int)y);
			}
		}

		// Token: 0x020001E2 RID: 482
		public class TauntUsageSet
		{
			// Token: 0x06000BAA RID: 2986 RVA: 0x00017853 File Offset: 0x00015A53
			public TauntUsageSet()
			{
				this._tauntUsages = new MBList<TauntUsageManager.TauntUsage>();
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x00017866 File Offset: 0x00015A66
			public void AddUsage(TauntUsageManager.TauntUsage usage)
			{
				this._tauntUsages.Add(usage);
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x00017874 File Offset: 0x00015A74
			public MBReadOnlyList<TauntUsageManager.TauntUsage> GetUsages()
			{
				return this._tauntUsages;
			}

			// Token: 0x040006D6 RID: 1750
			private MBList<TauntUsageManager.TauntUsage> _tauntUsages;
		}

		// Token: 0x020001E3 RID: 483
		public class TauntUsage
		{
			// Token: 0x1700036C RID: 876
			// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0001787C File Offset: 0x00015A7C
			public TauntUsageManager.TauntUsage.TauntUsageFlag UsageFlag { get; }

			// Token: 0x06000BAE RID: 2990 RVA: 0x00017884 File Offset: 0x00015A84
			public TauntUsage(TauntUsageManager.TauntUsage.TauntUsageFlag usageFlag, string actionName)
			{
				this.UsageFlag = usageFlag;
				this._actionName = actionName;
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x0001789A File Offset: 0x00015A9A
			public bool IsSuitable(bool isLeftStance, bool isOnFoot, WeaponComponentData mainHandWeapon, WeaponComponentData offhandWeapon)
			{
				return this.GetIsNotSuitableReason(isLeftStance, isOnFoot, mainHandWeapon, offhandWeapon) == TauntUsageManager.TauntUsage.TauntUsageFlag.None;
			}

			// Token: 0x06000BB0 RID: 2992 RVA: 0x000178AC File Offset: 0x00015AAC
			public TauntUsageManager.TauntUsage.TauntUsageFlag GetIsNotSuitableReason(bool isLeftStance, bool isOnFoot, WeaponComponentData mainHandWeapon, WeaponComponentData offhandWeapon)
			{
				TauntUsageManager.TauntUsage.TauntUsageFlag tauntUsageFlag = TauntUsageManager.TauntUsage.TauntUsageFlag.None;
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresBow) && (mainHandWeapon == null || !mainHandWeapon.IsBow))
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresBow;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresShield) && (offhandWeapon == null || !offhandWeapon.IsShield))
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresShield;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresOnFoot) && !isOnFoot)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.RequiresOnFoot;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForTwoHanded) && mainHandWeapon != null && mainHandWeapon.IsTwoHanded)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForTwoHanded;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForOneHanded) && mainHandWeapon != null && mainHandWeapon.IsOneHanded)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForOneHanded;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForShield) && offhandWeapon != null && offhandWeapon.IsShield)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForShield;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForBow) && mainHandWeapon != null && mainHandWeapon.IsBow)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForBow;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForCrossbow) && mainHandWeapon != null && mainHandWeapon.IsCrossBow)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForCrossbow;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForEmpty) && mainHandWeapon == null && offhandWeapon == null)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.UnsuitableForEmpty;
				}
				if (this.UsageFlag.HasAllFlags(TauntUsageManager.TauntUsage.TauntUsageFlag.IsLeftStance) != isLeftStance)
				{
					tauntUsageFlag |= TauntUsageManager.TauntUsage.TauntUsageFlag.IsLeftStance;
				}
				return tauntUsageFlag;
			}

			// Token: 0x06000BB1 RID: 2993 RVA: 0x000179EA File Offset: 0x00015BEA
			public string GetAction()
			{
				return this._actionName;
			}

			// Token: 0x040006D8 RID: 1752
			private string _actionName;

			// Token: 0x020001EF RID: 495
			[Flags]
			public enum TauntUsageFlag
			{
				// Token: 0x04000702 RID: 1794
				None = 0,
				// Token: 0x04000703 RID: 1795
				RequiresBow = 1,
				// Token: 0x04000704 RID: 1796
				RequiresShield = 2,
				// Token: 0x04000705 RID: 1797
				IsLeftStance = 4,
				// Token: 0x04000706 RID: 1798
				RequiresOnFoot = 8,
				// Token: 0x04000707 RID: 1799
				UnsuitableForTwoHanded = 16,
				// Token: 0x04000708 RID: 1800
				UnsuitableForOneHanded = 32,
				// Token: 0x04000709 RID: 1801
				UnsuitableForShield = 64,
				// Token: 0x0400070A RID: 1802
				UnsuitableForBow = 128,
				// Token: 0x0400070B RID: 1803
				UnsuitableForCrossbow = 256,
				// Token: 0x0400070C RID: 1804
				UnsuitableForEmpty = 512
			}
		}
	}
}
