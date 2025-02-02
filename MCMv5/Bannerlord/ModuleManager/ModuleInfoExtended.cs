using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000139 RID: 313
	[NullableContext(1)]
	[Nullable(0)]
	internal class ModuleInfoExtended : IEquatable<ModuleInfoExtended>
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001A195 File Offset: 0x00018395
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleInfoExtended);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001A1A1 File Offset: 0x000183A1
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0001A1A9 File Offset: 0x000183A9
		public string Id { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001A1B2 File Offset: 0x000183B2
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0001A1BA File Offset: 0x000183BA
		public string Name { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001A1C3 File Offset: 0x000183C3
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001A1CB File Offset: 0x000183CB
		public bool IsOfficial { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001A1D4 File Offset: 0x000183D4
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001A1DC File Offset: 0x000183DC
		public ApplicationVersion Version { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001A1ED File Offset: 0x000183ED
		public bool IsSingleplayerModule { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001A1F6 File Offset: 0x000183F6
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0001A1FE File Offset: 0x000183FE
		public bool IsMultiplayerModule { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001A207 File Offset: 0x00018407
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001A20F File Offset: 0x0001840F
		public bool IsServerModule { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001A218 File Offset: 0x00018418
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001A220 File Offset: 0x00018420
		public IReadOnlyList<SubModuleInfoExtended> SubModules { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001A229 File Offset: 0x00018429
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0001A231 File Offset: 0x00018431
		public IReadOnlyList<DependentModule> DependentModules { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001A23A File Offset: 0x0001843A
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0001A242 File Offset: 0x00018442
		public IReadOnlyList<DependentModule> ModulesToLoadAfterThis { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001A24B File Offset: 0x0001844B
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0001A253 File Offset: 0x00018453
		public IReadOnlyList<DependentModule> IncompatibleModules { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001A25C File Offset: 0x0001845C
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0001A264 File Offset: 0x00018464
		public string Url { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001A26D File Offset: 0x0001846D
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0001A275 File Offset: 0x00018475
		public string UpdateInfo { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001A27E File Offset: 0x0001847E
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0001A286 File Offset: 0x00018486
		public IReadOnlyList<DependentModuleMetadata> DependentModuleMetadatas { get; set; }

		// Token: 0x06000838 RID: 2104 RVA: 0x0001A290 File Offset: 0x00018490
		[NullableContext(2)]
		public static ModuleInfoExtended FromXml(XmlDocument xmlDocument)
		{
			bool flag = xmlDocument == null;
			ModuleInfoExtended result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XmlNode moduleNode = xmlDocument.SelectSingleNode("Module");
				string text;
				if (moduleNode == null)
				{
					text = null;
				}
				else
				{
					XmlNode xmlNode = moduleNode.SelectSingleNode("Id");
					if (xmlNode == null)
					{
						text = null;
					}
					else
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						if (attributes == null)
						{
							text = null;
						}
						else
						{
							XmlAttribute xmlAttribute = attributes["value"];
							text = ((xmlAttribute != null) ? xmlAttribute.InnerText : null);
						}
					}
				}
				string id = text ?? string.Empty;
				string text2;
				if (moduleNode == null)
				{
					text2 = null;
				}
				else
				{
					XmlNode xmlNode2 = moduleNode.SelectSingleNode("Name");
					if (xmlNode2 == null)
					{
						text2 = null;
					}
					else
					{
						XmlAttributeCollection attributes2 = xmlNode2.Attributes;
						if (attributes2 == null)
						{
							text2 = null;
						}
						else
						{
							XmlAttribute xmlAttribute2 = attributes2["value"];
							text2 = ((xmlAttribute2 != null) ? xmlAttribute2.InnerText : null);
						}
					}
				}
				string name = text2 ?? string.Empty;
				string versionAsString;
				if (moduleNode == null)
				{
					versionAsString = null;
				}
				else
				{
					XmlNode xmlNode3 = moduleNode.SelectSingleNode("Version");
					if (xmlNode3 == null)
					{
						versionAsString = null;
					}
					else
					{
						XmlAttributeCollection attributes3 = xmlNode3.Attributes;
						if (attributes3 == null)
						{
							versionAsString = null;
						}
						else
						{
							XmlAttribute xmlAttribute3 = attributes3["value"];
							versionAsString = ((xmlAttribute3 != null) ? xmlAttribute3.InnerText : null);
						}
					}
				}
				ApplicationVersion version;
				ApplicationVersion.TryParse(versionAsString, out version);
				string text3;
				if (moduleNode == null)
				{
					text3 = null;
				}
				else
				{
					XmlNode xmlNode4 = moduleNode.SelectSingleNode("ModuleType");
					if (xmlNode4 == null)
					{
						text3 = null;
					}
					else
					{
						XmlAttributeCollection attributes4 = xmlNode4.Attributes;
						text3 = ((attributes4 != null) ? attributes4["value"].InnerText : null);
					}
				}
				string moduleType = text3;
				if (moduleNode != null)
				{
					XmlNode xmlNode5 = moduleNode.SelectSingleNode("Official");
					bool? flag2;
					if (xmlNode5 == null)
					{
						flag2 = null;
					}
					else
					{
						XmlAttributeCollection attributes5 = xmlNode5.Attributes;
						if (attributes5 == null)
						{
							flag2 = null;
						}
						else
						{
							XmlAttribute xmlAttribute4 = attributes5["value"];
							flag2 = ((xmlAttribute4 != null) ? new bool?(xmlAttribute4.InnerText.Equals("true")) : null);
						}
					}
					bool? flag3 = flag2;
					if (flag3.GetValueOrDefault())
					{
						goto IL_1A7;
					}
				}
				bool flag4;
				if (!(moduleType == "Official"))
				{
					flag4 = (moduleType == "OfficialOptional");
					goto IL_1A8;
				}
				IL_1A7:
				flag4 = true;
				IL_1A8:
				bool isOfficial = flag4;
				string text4;
				if (moduleNode == null)
				{
					text4 = null;
				}
				else
				{
					XmlNode xmlNode6 = moduleNode.SelectSingleNode("ModuleCategory");
					if (xmlNode6 == null)
					{
						text4 = null;
					}
					else
					{
						XmlAttributeCollection attributes6 = xmlNode6.Attributes;
						if (attributes6 == null)
						{
							text4 = null;
						}
						else
						{
							XmlAttribute xmlAttribute5 = attributes6["value"];
							text4 = ((xmlAttribute5 != null) ? xmlAttribute5.InnerText : null);
						}
					}
				}
				string moduleCategory = text4;
				if (moduleNode != null)
				{
					XmlNode xmlNode7 = moduleNode.SelectSingleNode("SingleplayerModule");
					bool? flag5;
					if (xmlNode7 == null)
					{
						flag5 = null;
					}
					else
					{
						XmlAttributeCollection attributes7 = xmlNode7.Attributes;
						if (attributes7 == null)
						{
							flag5 = null;
						}
						else
						{
							XmlAttribute xmlAttribute6 = attributes7["value"];
							flag5 = ((xmlAttribute6 != null) ? new bool?(xmlAttribute6.InnerText.Equals("true")) : null);
						}
					}
					bool? flag3 = flag5;
					if (flag3.GetValueOrDefault())
					{
						goto IL_26E;
					}
				}
				bool flag6;
				if (!(moduleCategory == "Singleplayer"))
				{
					flag6 = (moduleCategory == "SingleplayerOptional");
					goto IL_26F;
				}
				IL_26E:
				flag6 = true;
				IL_26F:
				bool isSingleplayerModule = flag6;
				if (moduleNode != null)
				{
					XmlNode xmlNode8 = moduleNode.SelectSingleNode("MultiplayerModule");
					bool? flag7;
					if (xmlNode8 == null)
					{
						flag7 = null;
					}
					else
					{
						XmlAttributeCollection attributes8 = xmlNode8.Attributes;
						if (attributes8 == null)
						{
							flag7 = null;
						}
						else
						{
							XmlAttribute xmlAttribute7 = attributes8["value"];
							flag7 = ((xmlAttribute7 != null) ? new bool?(xmlAttribute7.InnerText.Equals("true")) : null);
						}
					}
					bool? flag3 = flag7;
					if (flag3.GetValueOrDefault())
					{
						goto IL_2F9;
					}
				}
				bool flag8;
				if (!(moduleCategory == "Multiplayer"))
				{
					flag8 = (moduleCategory == "MultiplayerOptional");
					goto IL_2FA;
				}
				IL_2F9:
				flag8 = true;
				IL_2FA:
				bool isMultiplayerModule = flag8;
				bool isServerModule = moduleCategory == "Server" || moduleCategory == "ServerOptional";
				XmlNode dependentModulesNode = (moduleNode != null) ? moduleNode.SelectSingleNode("DependedModules") : null;
				XmlNodeList dependentModulesList = (dependentModulesNode != null) ? dependentModulesNode.SelectNodes("DependedModule") : null;
				List<DependentModule> dependentModules = new List<DependentModule>((dependentModulesList != null) ? dependentModulesList.Count : 0);
				int i = 0;
				for (;;)
				{
					int num = i;
					int? num2 = (dependentModulesList != null) ? new int?(dependentModulesList.Count) : null;
					if (!(num < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					XmlAttribute xmlAttribute8;
					if (dependentModulesList == null)
					{
						xmlAttribute8 = null;
					}
					else
					{
						XmlNode xmlNode9 = dependentModulesList[i];
						if (xmlNode9 == null)
						{
							xmlAttribute8 = null;
						}
						else
						{
							XmlAttributeCollection attributes9 = xmlNode9.Attributes;
							xmlAttribute8 = ((attributes9 != null) ? attributes9["Id"] : null);
						}
					}
					XmlAttribute idAttr = xmlAttribute8;
					bool flag9 = idAttr != null;
					if (flag9)
					{
						XmlNode xmlNode10 = dependentModulesList[i];
						string versionAsString2;
						if (xmlNode10 == null)
						{
							versionAsString2 = null;
						}
						else
						{
							XmlAttributeCollection attributes10 = xmlNode10.Attributes;
							if (attributes10 == null)
							{
								versionAsString2 = null;
							}
							else
							{
								XmlAttribute xmlAttribute9 = attributes10["DependentVersion"];
								versionAsString2 = ((xmlAttribute9 != null) ? xmlAttribute9.InnerText : null);
							}
						}
						ApplicationVersion dVersion;
						ApplicationVersion.TryParse(versionAsString2, out dVersion);
						XmlNode xmlNode11 = dependentModulesList[i];
						XmlAttribute xmlAttribute10;
						if (xmlNode11 == null)
						{
							xmlAttribute10 = null;
						}
						else
						{
							XmlAttributeCollection attributes11 = xmlNode11.Attributes;
							xmlAttribute10 = ((attributes11 != null) ? attributes11["Optional"] : null);
						}
						XmlAttribute optional = xmlAttribute10;
						bool isOptional = optional != null && optional.InnerText.Equals("true");
						dependentModules.Add(new DependentModule(idAttr.InnerText, dVersion, isOptional));
					}
					i++;
				}
				XmlNode modulesToLoadAfterThisNode = (moduleNode != null) ? moduleNode.SelectSingleNode("ModulesToLoadAfterThis") : null;
				XmlNodeList modulesToLoadAfterThisList = (modulesToLoadAfterThisNode != null) ? modulesToLoadAfterThisNode.SelectNodes("Module") : null;
				List<DependentModule> modulesToLoadAfterThis = new List<DependentModule>((modulesToLoadAfterThisList != null) ? modulesToLoadAfterThisList.Count : 0);
				int j = 0;
				for (;;)
				{
					int num3 = j;
					int? num2 = (modulesToLoadAfterThisList != null) ? new int?(modulesToLoadAfterThisList.Count) : null;
					if (!(num3 < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					XmlAttribute xmlAttribute11;
					if (modulesToLoadAfterThisList == null)
					{
						xmlAttribute11 = null;
					}
					else
					{
						XmlNode xmlNode12 = modulesToLoadAfterThisList[j];
						if (xmlNode12 == null)
						{
							xmlAttribute11 = null;
						}
						else
						{
							XmlAttributeCollection attributes12 = xmlNode12.Attributes;
							xmlAttribute11 = ((attributes12 != null) ? attributes12["Id"] : null);
						}
					}
					XmlAttribute idAttr2 = xmlAttribute11;
					bool flag10 = idAttr2 != null;
					if (flag10)
					{
						modulesToLoadAfterThis.Add(new DependentModule
						{
							Id = idAttr2.InnerText,
							IsOptional = true
						});
					}
					j++;
				}
				XmlNode incompatibleModulesNode = (moduleNode != null) ? moduleNode.SelectSingleNode("IncompatibleModules") : null;
				XmlNodeList incompatibleModulesList = (incompatibleModulesNode != null) ? incompatibleModulesNode.SelectNodes("Module") : null;
				List<DependentModule> incompatibleModules = new List<DependentModule>((incompatibleModulesList != null) ? incompatibleModulesList.Count : 0);
				int k = 0;
				for (;;)
				{
					int num4 = k;
					int? num2 = (incompatibleModulesList != null) ? new int?(incompatibleModulesList.Count) : null;
					if (!(num4 < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					XmlAttribute xmlAttribute12;
					if (incompatibleModulesList == null)
					{
						xmlAttribute12 = null;
					}
					else
					{
						XmlNode xmlNode13 = incompatibleModulesList[k];
						if (xmlNode13 == null)
						{
							xmlAttribute12 = null;
						}
						else
						{
							XmlAttributeCollection attributes13 = xmlNode13.Attributes;
							xmlAttribute12 = ((attributes13 != null) ? attributes13["Id"] : null);
						}
					}
					XmlAttribute idAttr3 = xmlAttribute12;
					bool flag11 = idAttr3 != null;
					if (flag11)
					{
						incompatibleModules.Add(new DependentModule
						{
							Id = idAttr3.InnerText,
							IsOptional = true
						});
					}
					k++;
				}
				XmlNode subModulesNode = (moduleNode != null) ? moduleNode.SelectSingleNode("SubModules") : null;
				XmlNodeList subModuleList = (subModulesNode != null) ? subModulesNode.SelectNodes("SubModule") : null;
				List<SubModuleInfoExtended> subModules = new List<SubModuleInfoExtended>((subModuleList != null) ? subModuleList.Count : 0);
				int l = 0;
				for (;;)
				{
					int num5 = l;
					int? num2 = (subModuleList != null) ? new int?(subModuleList.Count) : null;
					if (!(num5 < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					SubModuleInfoExtended subModule = SubModuleInfoExtended.FromXml((subModuleList != null) ? subModuleList[l] : null);
					bool flag12 = subModule != null;
					if (flag12)
					{
						subModules.Add(subModule);
					}
					l++;
				}
				string text5;
				if (moduleNode == null)
				{
					text5 = null;
				}
				else
				{
					XmlNode xmlNode14 = moduleNode.SelectSingleNode("Url");
					if (xmlNode14 == null)
					{
						text5 = null;
					}
					else
					{
						XmlAttributeCollection attributes14 = xmlNode14.Attributes;
						if (attributes14 == null)
						{
							text5 = null;
						}
						else
						{
							XmlAttribute xmlAttribute13 = attributes14["value"];
							text5 = ((xmlAttribute13 != null) ? xmlAttribute13.InnerText : null);
						}
					}
				}
				string url = text5 ?? string.Empty;
				string text6;
				if (moduleNode == null)
				{
					text6 = null;
				}
				else
				{
					XmlNode xmlNode15 = moduleNode.SelectSingleNode("UpdateInfo");
					if (xmlNode15 == null)
					{
						text6 = null;
					}
					else
					{
						XmlAttributeCollection attributes15 = xmlNode15.Attributes;
						if (attributes15 == null)
						{
							text6 = null;
						}
						else
						{
							XmlAttribute xmlAttribute14 = attributes15["provider"];
							text6 = ((xmlAttribute14 != null) ? xmlAttribute14.InnerText : null);
						}
					}
				}
				string updateInfoProvider = text6 ?? string.Empty;
				string text7;
				if (moduleNode == null)
				{
					text7 = null;
				}
				else
				{
					XmlNode xmlNode16 = moduleNode.SelectSingleNode("UpdateInfo");
					if (xmlNode16 == null)
					{
						text7 = null;
					}
					else
					{
						XmlAttributeCollection attributes16 = xmlNode16.Attributes;
						if (attributes16 == null)
						{
							text7 = null;
						}
						else
						{
							XmlAttribute xmlAttribute15 = attributes16["value"];
							text7 = ((xmlAttribute15 != null) ? xmlAttribute15.InnerText : null);
						}
					}
				}
				string updateInfoValue = text7 ?? string.Empty;
				XmlNode dependentModuleMetadatasNode = (moduleNode != null) ? moduleNode.SelectSingleNode("DependedModuleMetadatas") : null;
				XmlNodeList dependentModuleMetadatasList = (dependentModuleMetadatasNode != null) ? dependentModuleMetadatasNode.SelectNodes("DependedModuleMetadata") : null;
				XmlNode loadAfterModules = (moduleNode != null) ? moduleNode.SelectSingleNode("LoadAfterModules") : null;
				XmlNodeList loadAfterModuleList = (loadAfterModules != null) ? loadAfterModules.SelectNodes("LoadAfterModule") : null;
				XmlNode optionalDependentModules = (moduleNode != null) ? moduleNode.SelectSingleNode("OptionalDependModules") : null;
				IEnumerable<XmlNode> enumerable;
				if (dependentModulesNode == null)
				{
					enumerable = null;
				}
				else
				{
					XmlNodeList xmlNodeList = dependentModulesNode.SelectNodes("OptionalDependModule");
					enumerable = ((xmlNodeList != null) ? xmlNodeList.Cast<XmlNode>() : null);
				}
				IEnumerable<XmlNode> first = enumerable ?? Enumerable.Empty<XmlNode>();
				IEnumerable<XmlNode> enumerable2;
				if (optionalDependentModules == null)
				{
					enumerable2 = null;
				}
				else
				{
					XmlNodeList xmlNodeList2 = optionalDependentModules.SelectNodes("OptionalDependModule");
					enumerable2 = ((xmlNodeList2 != null) ? xmlNodeList2.Cast<XmlNode>() : null);
				}
				IEnumerable<XmlNode> first2 = first.Concat(enumerable2 ?? Enumerable.Empty<XmlNode>());
				IEnumerable<XmlNode> enumerable3;
				if (optionalDependentModules == null)
				{
					enumerable3 = null;
				}
				else
				{
					XmlNodeList xmlNodeList3 = optionalDependentModules.SelectNodes("DependModule");
					enumerable3 = ((xmlNodeList3 != null) ? xmlNodeList3.Cast<XmlNode>() : null);
				}
				List<XmlNode> optionalDependentModuleList = first2.Concat(enumerable3 ?? Enumerable.Empty<XmlNode>()).ToList<XmlNode>();
				int capacity;
				if (dependentModuleMetadatasList == null)
				{
					int? num6 = (loadAfterModuleList != null) ? new int?(loadAfterModuleList.Count) : null;
					capacity = (((num6 != null) ? new int?(num6.GetValueOrDefault()) : null) ?? optionalDependentModuleList.Count);
				}
				else
				{
					capacity = dependentModuleMetadatasList.Count;
				}
				List<DependentModuleMetadata> dependentModuleMetadatas = new List<DependentModuleMetadata>(capacity);
				int m = 0;
				for (;;)
				{
					int num7 = m;
					int? num2 = (dependentModuleMetadatasList != null) ? new int?(dependentModuleMetadatasList.Count) : null;
					if (!(num7 < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					XmlAttribute xmlAttribute16;
					if (dependentModuleMetadatasList == null)
					{
						xmlAttribute16 = null;
					}
					else
					{
						XmlNode xmlNode17 = dependentModuleMetadatasList[m];
						if (xmlNode17 == null)
						{
							xmlAttribute16 = null;
						}
						else
						{
							XmlAttributeCollection attributes17 = xmlNode17.Attributes;
							xmlAttribute16 = ((attributes17 != null) ? attributes17["id"] : null);
						}
					}
					XmlAttribute idAttr4 = xmlAttribute16;
					bool flag13 = idAttr4 != null;
					if (flag13)
					{
						XmlNode xmlNode18 = dependentModuleMetadatasList[m];
						XmlAttribute xmlAttribute17;
						if (xmlNode18 == null)
						{
							xmlAttribute17 = null;
						}
						else
						{
							XmlAttributeCollection attributes18 = xmlNode18.Attributes;
							xmlAttribute17 = ((attributes18 != null) ? attributes18["order"] : null);
						}
						XmlAttribute orderAttr = xmlAttribute17;
						LoadTypeParse loadType;
						LoadType order = (LoadType)((orderAttr != null && Enum.TryParse<LoadTypeParse>(orderAttr.InnerText, out loadType)) ? loadType : ((LoadTypeParse)0));
						XmlNode xmlNode19 = dependentModuleMetadatasList[m];
						bool? flag14;
						if (xmlNode19 == null)
						{
							flag14 = null;
						}
						else
						{
							XmlAttributeCollection attributes19 = xmlNode19.Attributes;
							if (attributes19 == null)
							{
								flag14 = null;
							}
							else
							{
								XmlAttribute xmlAttribute18 = attributes19["optional"];
								flag14 = ((xmlAttribute18 != null) ? new bool?(xmlAttribute18.InnerText.Equals("true")) : null);
							}
						}
						bool? flag3 = flag14;
						bool optional2 = flag3.GetValueOrDefault();
						XmlNode xmlNode20 = dependentModuleMetadatasList[m];
						bool? flag15;
						if (xmlNode20 == null)
						{
							flag15 = null;
						}
						else
						{
							XmlAttributeCollection attributes20 = xmlNode20.Attributes;
							if (attributes20 == null)
							{
								flag15 = null;
							}
							else
							{
								XmlAttribute xmlAttribute19 = attributes20["incompatible"];
								flag15 = ((xmlAttribute19 != null) ? new bool?(xmlAttribute19.InnerText.Equals("true")) : null);
							}
						}
						flag3 = flag15;
						bool incompatible = flag3.GetValueOrDefault();
						XmlNode xmlNode21 = dependentModuleMetadatasList[m];
						string versionAsString3;
						if (xmlNode21 == null)
						{
							versionAsString3 = null;
						}
						else
						{
							XmlAttributeCollection attributes21 = xmlNode21.Attributes;
							if (attributes21 == null)
							{
								versionAsString3 = null;
							}
							else
							{
								XmlAttribute xmlAttribute20 = attributes21["version"];
								versionAsString3 = ((xmlAttribute20 != null) ? xmlAttribute20.InnerText : null);
							}
						}
						ApplicationVersion v;
						ApplicationVersion dVersion2 = ApplicationVersion.TryParse(versionAsString3, out v) ? v : ApplicationVersion.Empty;
						XmlNode xmlNode22 = dependentModuleMetadatasList[m];
						string text8;
						if (xmlNode22 == null)
						{
							text8 = null;
						}
						else
						{
							XmlAttributeCollection attributes22 = xmlNode22.Attributes;
							if (attributes22 == null)
							{
								text8 = null;
							}
							else
							{
								XmlAttribute xmlAttribute21 = attributes22["version"];
								text8 = ((xmlAttribute21 != null) ? xmlAttribute21.InnerText : null);
							}
						}
						ApplicationVersionRange vr;
						ApplicationVersionRange dVersionRange = ApplicationVersionRange.TryParse(text8 ?? string.Empty, out vr) ? vr : ApplicationVersionRange.Empty;
						dependentModuleMetadatas.Add(new DependentModuleMetadata
						{
							Id = idAttr4.InnerText,
							LoadType = order,
							IsOptional = optional2,
							IsIncompatible = incompatible,
							Version = dVersion2,
							VersionRange = dVersionRange
						});
					}
					m++;
				}
				int n = 0;
				for (;;)
				{
					int num8 = n;
					int? num2 = (loadAfterModuleList != null) ? new int?(loadAfterModuleList.Count) : null;
					if (!(num8 < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					XmlAttribute xmlAttribute22;
					if (loadAfterModuleList == null)
					{
						xmlAttribute22 = null;
					}
					else
					{
						XmlNode xmlNode23 = loadAfterModuleList[n];
						if (xmlNode23 == null)
						{
							xmlAttribute22 = null;
						}
						else
						{
							XmlAttributeCollection attributes23 = xmlNode23.Attributes;
							xmlAttribute22 = ((attributes23 != null) ? attributes23["Id"] : null);
						}
					}
					XmlAttribute idAttr5 = xmlAttribute22;
					bool flag16 = idAttr5 != null;
					if (flag16)
					{
						dependentModuleMetadatas.Add(new DependentModuleMetadata
						{
							Id = idAttr5.InnerText,
							LoadType = LoadType.LoadAfterThis,
							IsOptional = false,
							IsIncompatible = false,
							Version = ApplicationVersion.Empty,
							VersionRange = ApplicationVersionRange.Empty
						});
					}
					n++;
				}
				for (int i2 = 0; i2 < optionalDependentModuleList.Count; i2++)
				{
					XmlAttributeCollection attributes24 = optionalDependentModuleList[i2].Attributes;
					XmlAttribute idAttr6 = (attributes24 != null) ? attributes24["Id"] : null;
					bool flag17 = idAttr6 != null;
					if (flag17)
					{
						dependentModuleMetadatas.Add(new DependentModuleMetadata
						{
							Id = idAttr6.InnerText,
							LoadType = LoadType.None,
							IsOptional = true,
							IsIncompatible = false,
							Version = ApplicationVersion.Empty,
							VersionRange = ApplicationVersionRange.Empty
						});
					}
				}
				XmlNode requiredGameVersion = (moduleNode != null) ? moduleNode.SelectSingleNode("RequiredGameVersion") : null;
				string text9;
				if (requiredGameVersion == null)
				{
					text9 = null;
				}
				else
				{
					XmlAttributeCollection attributes25 = requiredGameVersion.Attributes;
					if (attributes25 == null)
					{
						text9 = null;
					}
					else
					{
						XmlAttribute xmlAttribute23 = attributes25["value"];
						text9 = ((xmlAttribute23 != null) ? xmlAttribute23.InnerText : null);
					}
				}
				string requiredGameVersionVal = text9 ?? string.Empty;
				bool flag18;
				if (requiredGameVersion == null)
				{
					flag18 = false;
				}
				else
				{
					XmlAttributeCollection attributes26 = requiredGameVersion.Attributes;
					bool? flag19;
					if (attributes26 == null)
					{
						flag19 = null;
					}
					else
					{
						XmlAttribute xmlAttribute24 = attributes26["optional"];
						flag19 = ((xmlAttribute24 != null) ? new bool?(xmlAttribute24.InnerText.Equals("true")) : null);
					}
					bool? flag3 = flag19;
					flag18 = flag3.GetValueOrDefault();
				}
				bool requiredGameVersionOptional = flag18;
				ApplicationVersion gameVersion;
				bool flag20 = !string.IsNullOrWhiteSpace(requiredGameVersionVal) && ApplicationVersion.TryParse(requiredGameVersionVal, out gameVersion);
				if (flag20)
				{
					string[] officialModuleIds = ModuleInfoExtended.OfficialModuleIds;
					for (int num9 = 0; num9 < officialModuleIds.Length; num9++)
					{
						string moduleId = officialModuleIds[num9];
						bool isNative = moduleId.Equals(ModuleInfoExtended.NativeModuleId);
						DependentModuleMetadata module = dependentModuleMetadatas.Find((DependentModuleMetadata dmm) => dmm.Id.Equals(moduleId, StringComparison.Ordinal));
						bool flag21 = module != null;
						if (flag21)
						{
							dependentModuleMetadatas.Remove(module);
						}
						dependentModuleMetadatas.Add(new DependentModuleMetadata
						{
							Id = moduleId,
							LoadType = LoadType.LoadBeforeThis,
							IsOptional = (requiredGameVersionOptional && !isNative),
							IsIncompatible = false,
							Version = gameVersion,
							VersionRange = ApplicationVersionRange.Empty
						});
					}
				}
				result = new ModuleInfoExtended
				{
					Id = id,
					Name = name,
					IsOfficial = isOfficial,
					Version = version,
					IsSingleplayerModule = isSingleplayerModule,
					IsMultiplayerModule = isMultiplayerModule,
					IsServerModule = isServerModule,
					SubModules = subModules,
					DependentModules = dependentModules,
					ModulesToLoadAfterThis = modulesToLoadAfterThis,
					IncompatibleModules = incompatibleModules,
					Url = url,
					UpdateInfo = ((!string.IsNullOrEmpty(updateInfoProvider) && !string.IsNullOrEmpty(updateInfoValue)) ? (updateInfoProvider + ":" + updateInfoValue) : string.Empty),
					DependentModuleMetadatas = dependentModuleMetadatas
				};
			}
			return result;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B1CC File Offset: 0x000193CC
		public ModuleInfoExtended()
		{
			this.Id = string.Empty;
			this.Name = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.SubModules = Array.Empty<SubModuleInfoExtended>();
			this.DependentModules = Array.Empty<DependentModule>();
			this.ModulesToLoadAfterThis = Array.Empty<DependentModule>();
			this.IncompatibleModules = Array.Empty<DependentModule>();
			this.Url = string.Empty;
			this.UpdateInfo = string.Empty;
			this.DependentModuleMetadatas = Array.Empty<DependentModuleMetadata>();
			base..ctor();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B250 File Offset: 0x00019450
		public ModuleInfoExtended(string id, string name, bool isOfficial, ApplicationVersion version, bool isSingleplayerModule, bool isMultiplayerModule, IReadOnlyList<SubModuleInfoExtended> subModules, IReadOnlyList<DependentModule> dependentModules, IReadOnlyList<DependentModule> modulesToLoadAfterThis, IReadOnlyList<DependentModule> incompatibleModules, IReadOnlyList<DependentModuleMetadata> dependentModuleMetadatas, string url)
		{
			this.Id = string.Empty;
			this.Name = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.SubModules = Array.Empty<SubModuleInfoExtended>();
			this.DependentModules = Array.Empty<DependentModule>();
			this.ModulesToLoadAfterThis = Array.Empty<DependentModule>();
			this.IncompatibleModules = Array.Empty<DependentModule>();
			this.Url = string.Empty;
			this.UpdateInfo = string.Empty;
			this.DependentModuleMetadatas = Array.Empty<DependentModuleMetadata>();
			base..ctor();
			this.Id = id;
			this.Name = name;
			this.IsOfficial = isOfficial;
			this.Version = version;
			this.IsSingleplayerModule = isSingleplayerModule;
			this.IsMultiplayerModule = isMultiplayerModule;
			this.SubModules = subModules;
			this.DependentModules = dependentModules;
			this.ModulesToLoadAfterThis = modulesToLoadAfterThis;
			this.IncompatibleModules = incompatibleModules;
			this.DependentModuleMetadatas = dependentModuleMetadatas;
			this.Url = url;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B33C File Offset: 0x0001953C
		public bool IsNative()
		{
			return this.Id.Equals(ModuleInfoExtended.NativeModuleId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001B34F File Offset: 0x0001954F
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.Id, this.Version);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001B368 File Offset: 0x00019568
		[NullableContext(2)]
		public virtual bool Equals(ModuleInfoExtended other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				result = (flag2 || this.Id == other.Id);
			}
			return result;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001B3A3 File Offset: 0x000195A3
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001B3B0 File Offset: 0x000195B0
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Name = ");
			builder.Append(this.Name);
			builder.Append(", IsOfficial = ");
			builder.Append(this.IsOfficial.ToString());
			builder.Append(", Version = ");
			builder.Append(this.Version);
			builder.Append(", IsSingleplayerModule = ");
			builder.Append(this.IsSingleplayerModule.ToString());
			builder.Append(", IsMultiplayerModule = ");
			builder.Append(this.IsMultiplayerModule.ToString());
			builder.Append(", IsServerModule = ");
			builder.Append(this.IsServerModule.ToString());
			builder.Append(", SubModules = ");
			builder.Append(this.SubModules);
			builder.Append(", DependentModules = ");
			builder.Append(this.DependentModules);
			builder.Append(", ModulesToLoadAfterThis = ");
			builder.Append(this.ModulesToLoadAfterThis);
			builder.Append(", IncompatibleModules = ");
			builder.Append(this.IncompatibleModules);
			builder.Append(", Url = ");
			builder.Append(this.Url);
			builder.Append(", UpdateInfo = ");
			builder.Append(this.UpdateInfo);
			builder.Append(", DependentModuleMetadatas = ");
			builder.Append(this.DependentModuleMetadatas);
			return true;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B559 File Offset: 0x00019759
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleInfoExtended left, ModuleInfoExtended right)
		{
			return !(left == right);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B565 File Offset: 0x00019765
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleInfoExtended left, ModuleInfoExtended right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001B57B File Offset: 0x0001977B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleInfoExtended);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001B594 File Offset: 0x00019794
		[CompilerGenerated]
		protected ModuleInfoExtended(ModuleInfoExtended original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Name = original.<Name>k__BackingField;
			this.IsOfficial = original.<IsOfficial>k__BackingField;
			this.Version = original.<Version>k__BackingField;
			this.IsSingleplayerModule = original.<IsSingleplayerModule>k__BackingField;
			this.IsMultiplayerModule = original.<IsMultiplayerModule>k__BackingField;
			this.IsServerModule = original.<IsServerModule>k__BackingField;
			this.SubModules = original.<SubModules>k__BackingField;
			this.DependentModules = original.<DependentModules>k__BackingField;
			this.ModulesToLoadAfterThis = original.<ModulesToLoadAfterThis>k__BackingField;
			this.IncompatibleModules = original.<IncompatibleModules>k__BackingField;
			this.Url = original.<Url>k__BackingField;
			this.UpdateInfo = original.<UpdateInfo>k__BackingField;
			this.DependentModuleMetadatas = original.<DependentModuleMetadatas>k__BackingField;
		}

		// Token: 0x04000257 RID: 599
		private static readonly string NativeModuleId = "Native";

		// Token: 0x04000258 RID: 600
		private static readonly string[] OfficialModuleIds = new string[]
		{
			ModuleInfoExtended.NativeModuleId,
			"SandBox",
			"SandBoxCore",
			"StoryMode",
			"CustomBattle",
			"BirthAndDeath",
			"Multiplayer"
		};
	}
}
