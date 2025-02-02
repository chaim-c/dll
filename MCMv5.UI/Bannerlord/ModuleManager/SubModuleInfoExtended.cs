using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200004F RID: 79
	[NullableContext(1)]
	[Nullable(0)]
	internal class SubModuleInfoExtended : IEquatable<SubModuleInfoExtended>
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000DDA5 File Offset: 0x0000BFA5
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SubModuleInfoExtended);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
		public string Name { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000DDC2 File Offset: 0x0000BFC2
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000DDCA File Offset: 0x0000BFCA
		public string DLLName { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000DDD3 File Offset: 0x0000BFD3
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		public IReadOnlyList<string> Assemblies { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public string SubModuleClassType { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000DDFD File Offset: 0x0000BFFD
		public IReadOnlyDictionary<string, IReadOnlyList<string>> Tags { get; set; }

		// Token: 0x0600035A RID: 858 RVA: 0x0000DE08 File Offset: 0x0000C008
		public SubModuleInfoExtended()
		{
			this.Name = string.Empty;
			this.DLLName = string.Empty;
			this.Assemblies = Array.Empty<string>();
			this.SubModuleClassType = string.Empty;
			this.Tags = new Dictionary<string, IReadOnlyList<string>>();
			base..ctor();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000DE54 File Offset: 0x0000C054
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

		// Token: 0x0600035C RID: 860 RVA: 0x0000DECC File Offset: 0x0000C0CC
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

		// Token: 0x0600035D RID: 861 RVA: 0x0000E231 File Offset: 0x0000C431
		public override string ToString()
		{
			return this.Name + " - " + this.DLLName;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000E24C File Offset: 0x0000C44C
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

		// Token: 0x0600035F RID: 863 RVA: 0x0000E287 File Offset: 0x0000C487
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000E294 File Offset: 0x0000C494
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

		// Token: 0x06000361 RID: 865 RVA: 0x0000E324 File Offset: 0x0000C524
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SubModuleInfoExtended left, SubModuleInfoExtended right)
		{
			return !(left == right);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000E330 File Offset: 0x0000C530
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SubModuleInfoExtended left, SubModuleInfoExtended right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E346 File Offset: 0x0000C546
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SubModuleInfoExtended);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000E35C File Offset: 0x0000C55C
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
