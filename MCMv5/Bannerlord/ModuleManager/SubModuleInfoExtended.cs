using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200013C RID: 316
	[NullableContext(1)]
	[Nullable(0)]
	internal class SubModuleInfoExtended : IEquatable<SubModuleInfoExtended>
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001BA69 File Offset: 0x00019C69
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SubModuleInfoExtended);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001BA75 File Offset: 0x00019C75
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0001BA7D File Offset: 0x00019C7D
		public string Name { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001BA86 File Offset: 0x00019C86
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001BA8E File Offset: 0x00019C8E
		public string DLLName { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001BA97 File Offset: 0x00019C97
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0001BA9F File Offset: 0x00019C9F
		public IReadOnlyList<string> Assemblies { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		public string SubModuleClassType { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001BAB9 File Offset: 0x00019CB9
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0001BAC1 File Offset: 0x00019CC1
		public IReadOnlyDictionary<string, IReadOnlyList<string>> Tags { get; set; }

		// Token: 0x06000868 RID: 2152 RVA: 0x0001BACC File Offset: 0x00019CCC
		public SubModuleInfoExtended()
		{
			this.Name = string.Empty;
			this.DLLName = string.Empty;
			this.Assemblies = Array.Empty<string>();
			this.SubModuleClassType = string.Empty;
			this.Tags = new Dictionary<string, IReadOnlyList<string>>();
			base..ctor();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001BB18 File Offset: 0x00019D18
		public SubModuleInfoExtended(string name, string dllName, IReadOnlyList<string> assemblies, string subModuleClassType, IReadOnlyDictionary<string, IReadOnlyList<string>> tags)
		{
			this.Name = string.Empty;
			this.DLLName = string.Empty;
			this.Assemblies = Array.Empty<string>();
			this.SubModuleClassType = string.Empty;
			this.Tags = new Dictionary<string, IReadOnlyList<string>>();
			base..ctor();
			this.Name = name;
			this.DLLName = dllName;
			this.Assemblies = assemblies;
			this.SubModuleClassType = subModuleClassType;
			this.Tags = tags;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001BB90 File Offset: 0x00019D90
		[NullableContext(2)]
		public static SubModuleInfoExtended FromXml(XmlNode subModuleNode)
		{
			bool flag = subModuleNode == null;
			SubModuleInfoExtended result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XmlNode xmlNode = subModuleNode.SelectSingleNode("Name");
				string text;
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
				string name = text ?? string.Empty;
				XmlNode xmlNode2 = subModuleNode.SelectSingleNode("DLLName");
				string text2;
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
				string dllName = text2 ?? string.Empty;
				XmlNode xmlNode3 = subModuleNode.SelectSingleNode("SubModuleClassType");
				string text3;
				if (xmlNode3 == null)
				{
					text3 = null;
				}
				else
				{
					XmlAttributeCollection attributes3 = xmlNode3.Attributes;
					if (attributes3 == null)
					{
						text3 = null;
					}
					else
					{
						XmlAttribute xmlAttribute3 = attributes3["value"];
						text3 = ((xmlAttribute3 != null) ? xmlAttribute3.InnerText : null);
					}
				}
				string subModuleClassType = text3 ?? string.Empty;
				string[] assemblies = Array.Empty<string>();
				bool flag2 = subModuleNode.SelectSingleNode("Assemblies") != null;
				if (flag2)
				{
					XmlNode xmlNode4 = subModuleNode.SelectSingleNode("Assemblies");
					XmlNodeList assembliesList = (xmlNode4 != null) ? xmlNode4.SelectNodes("Assembly") : null;
					assemblies = new string[(assembliesList != null) ? assembliesList.Count : 0];
					int i = 0;
					for (;;)
					{
						int num = i;
						int? num2 = (assembliesList != null) ? new int?(assembliesList.Count) : null;
						if (!(num < num2.GetValueOrDefault() & num2 != null))
						{
							break;
						}
						string[] array = assemblies;
						int num3 = i;
						string text4;
						if (assembliesList == null)
						{
							text4 = null;
						}
						else
						{
							XmlNode xmlNode5 = assembliesList[i];
							if (xmlNode5 == null)
							{
								text4 = null;
							}
							else
							{
								XmlAttributeCollection attributes4 = xmlNode5.Attributes;
								if (attributes4 == null)
								{
									text4 = null;
								}
								else
								{
									XmlAttribute xmlAttribute4 = attributes4["value"];
									text4 = ((xmlAttribute4 != null) ? xmlAttribute4.InnerText : null);
								}
							}
						}
						string value = text4;
						array[num3] = ((value != null) ? value : string.Empty);
						i++;
					}
				}
				XmlNode xmlNode6 = subModuleNode.SelectSingleNode("Tags");
				XmlNodeList tagsList = (xmlNode6 != null) ? xmlNode6.SelectNodes("Tag") : null;
				Dictionary<string, List<string>> tags = new Dictionary<string, List<string>>();
				int j = 0;
				for (;;)
				{
					int num4 = j;
					int? num2 = (tagsList != null) ? new int?(tagsList.Count) : null;
					if (!(num4 < num2.GetValueOrDefault() & num2 != null))
					{
						break;
					}
					string text5;
					if (tagsList == null)
					{
						text5 = null;
					}
					else
					{
						XmlNode xmlNode7 = tagsList[j];
						if (xmlNode7 == null)
						{
							text5 = null;
						}
						else
						{
							XmlAttributeCollection attributes5 = xmlNode7.Attributes;
							if (attributes5 == null)
							{
								text5 = null;
							}
							else
							{
								XmlAttribute xmlAttribute5 = attributes5["key"];
								text5 = ((xmlAttribute5 != null) ? xmlAttribute5.InnerText : null);
							}
						}
					}
					string key = text5;
					string value2;
					bool flag3;
					if (key != null)
					{
						XmlNode xmlNode8 = tagsList[j];
						string text6;
						if (xmlNode8 == null)
						{
							text6 = null;
						}
						else
						{
							XmlAttributeCollection attributes6 = xmlNode8.Attributes;
							if (attributes6 == null)
							{
								text6 = null;
							}
							else
							{
								XmlAttribute xmlAttribute6 = attributes6["value"];
								text6 = ((xmlAttribute6 != null) ? xmlAttribute6.InnerText : null);
							}
						}
						value2 = text6;
						flag3 = (value2 != null);
					}
					else
					{
						flag3 = false;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						List<string> list;
						bool flag5 = tags.TryGetValue(key, out list);
						if (flag5)
						{
							list.Add(value2);
						}
						else
						{
							tags[key] = new List<string>
							{
								value2
							};
						}
					}
					j++;
				}
				SubModuleInfoExtended subModuleInfoExtended = new SubModuleInfoExtended();
				subModuleInfoExtended.Name = name;
				subModuleInfoExtended.DLLName = dllName;
				subModuleInfoExtended.Assemblies = assemblies;
				subModuleInfoExtended.SubModuleClassType = subModuleClassType;
				subModuleInfoExtended.Tags = new ReadOnlyDictionary<string, IReadOnlyList<string>>(tags.ToDictionary((KeyValuePair<string, List<string>> x) => x.Key, (KeyValuePair<string, List<string>> x) => new ReadOnlyCollection<string>(x.Value)));
				result = subModuleInfoExtended;
			}
			return result;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001BEF5 File Offset: 0x0001A0F5
		public override string ToString()
		{
			return this.Name + " - " + this.DLLName;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001BF10 File Offset: 0x0001A110
		[NullableContext(2)]
		public virtual bool Equals(SubModuleInfoExtended other)
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
				result = (flag2 || this.Name == other.Name);
			}
			return result;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001BF4B File Offset: 0x0001A14B
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001BF58 File Offset: 0x0001A158
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Name = ");
			builder.Append(this.Name);
			builder.Append(", DLLName = ");
			builder.Append(this.DLLName);
			builder.Append(", Assemblies = ");
			builder.Append(this.Assemblies);
			builder.Append(", SubModuleClassType = ");
			builder.Append(this.SubModuleClassType);
			builder.Append(", Tags = ");
			builder.Append(this.Tags);
			return true;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SubModuleInfoExtended left, SubModuleInfoExtended right)
		{
			return !(left == right);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SubModuleInfoExtended left, SubModuleInfoExtended right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001C00A File Offset: 0x0001A20A
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SubModuleInfoExtended);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001C020 File Offset: 0x0001A220
		[CompilerGenerated]
		protected SubModuleInfoExtended(SubModuleInfoExtended original)
		{
			this.Name = original.<Name>k__BackingField;
			this.DLLName = original.<DLLName>k__BackingField;
			this.Assemblies = original.<Assemblies>k__BackingField;
			this.SubModuleClassType = original.<SubModuleClassType>k__BackingField;
			this.Tags = original.<Tags>k__BackingField;
		}
	}
}
