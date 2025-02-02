using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	public class SettingsPropertyGroupDefinition
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008672 File Offset: 0x00006872
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000867A File Offset: 0x0000687A
		protected char SubGroupDelimiter { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008683 File Offset: 0x00006883
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000868B File Offset: 0x0000688B
		[Nullable(2)]
		public SettingsPropertyGroupDefinition Parent { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008694 File Offset: 0x00006894
		public string GroupNameRaw
		{
			get
			{
				return this._groupNameRaw;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000869C File Offset: 0x0000689C
		public string GroupName
		{
			get
			{
				return this.DisplayGroupNameRaw;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000086A4 File Offset: 0x000068A4
		public string DisplayGroupNameRaw
		{
			get
			{
				bool flag = this.Parent == null;
				string result;
				if (flag)
				{
					result = LocalizationUtils.Localize(this._groupNameRaw, null);
				}
				else
				{
					string localizedParentGroup = LocalizationUtils.Localize(this.Parent._groupNameRaw, null);
					string localizedGroupName = LocalizationUtils.Localize(this._groupNameRaw, null);
					result = localizedGroupName.Replace(localizedParentGroup, string.Empty).TrimStart(new char[]
					{
						this.SubGroupDelimiter
					});
				}
				return result;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008711 File Offset: 0x00006911
		public int Order { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008719 File Offset: 0x00006919
		public IEnumerable<SettingsPropertyGroupDefinition> SubGroups
		{
			get
			{
				return this.subGroups.SortDefault();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008726 File Offset: 0x00006926
		public IEnumerable<ISettingsPropertyDefinition> SettingProperties
		{
			get
			{
				return this.settingProperties.SortDefault();
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008733 File Offset: 0x00006933
		public SettingsPropertyGroupDefinition(string groupName, int order = -1)
		{
			this._groupNameRaw = groupName;
			this.Order = order;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000876C File Offset: 0x0000696C
		[Obsolete("Override not needed", true)]
		public SettingsPropertyGroupDefinition(string groupName, [Nullable(2)] string _, int order = -1)
		{
			this._groupNameRaw = groupName;
			this.Order = order;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000087A8 File Offset: 0x000069A8
		public SettingsPropertyGroupDefinition SetParent(SettingsPropertyGroupDefinition parent)
		{
			this.Parent = parent;
			return this;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000087C4 File Offset: 0x000069C4
		public SettingsPropertyGroupDefinition SetSubGroupDelimiter(char subGroupDelimiter)
		{
			this.SubGroupDelimiter = subGroupDelimiter;
			return this;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000087DF File Offset: 0x000069DF
		public void Add(ISettingsPropertyDefinition settingProp)
		{
			this.settingProperties.Add(settingProp);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000087EF File Offset: 0x000069EF
		public void Add(SettingsPropertyGroupDefinition settingProp)
		{
			this.subGroups.Add(settingProp.SetParent(this));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008805 File Offset: 0x00006A05
		[return: Nullable(2)]
		public SettingsPropertyGroupDefinition GetGroup(string groupName)
		{
			return this.subGroups.GetGroupFromName(groupName);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008813 File Offset: 0x00006A13
		[Obsolete("Use GetGroup", true)]
		[return: Nullable(2)]
		public SettingsPropertyGroupDefinition GetGroupFor(string groupName)
		{
			return this.subGroups.GetGroupFromName(groupName);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008821 File Offset: 0x00006A21
		public override string ToString()
		{
			return this.GroupName;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000882C File Offset: 0x00006A2C
		public SettingsPropertyGroupDefinition Clone(bool keepRefs = true)
		{
			SettingsPropertyGroupDefinition settings = new SettingsPropertyGroupDefinition(this.GroupName, this.Order).SetSubGroupDelimiter(this.SubGroupDelimiter);
			foreach (ISettingsPropertyDefinition prop in this.SettingProperties)
			{
				settings.Add(prop.Clone(keepRefs));
			}
			foreach (SettingsPropertyGroupDefinition subGroup in this.SubGroups)
			{
				settings.Add(subGroup.Clone(keepRefs));
			}
			return settings;
		}

		// Token: 0x04000098 RID: 152
		public static readonly string DefaultGroupName = "{=SettingsPropertyGroupDefinition_Misc}Misc";

		// Token: 0x04000099 RID: 153
		public static readonly IPropertyGroupDefinition DefaultGroup = new SettingsPropertyGroupDefinition.DefaultPropertyGroupDefinition();

		// Token: 0x0400009A RID: 154
		protected readonly string _groupNameRaw;

		// Token: 0x0400009B RID: 155
		protected readonly string _groupNameOverrideRaw = string.Empty;

		// Token: 0x0400009C RID: 156
		protected readonly List<SettingsPropertyGroupDefinition> subGroups = new List<SettingsPropertyGroupDefinition>();

		// Token: 0x0400009D RID: 157
		protected readonly List<ISettingsPropertyDefinition> settingProperties = new List<ISettingsPropertyDefinition>();

		// Token: 0x020001A5 RID: 421
		[NullableContext(0)]
		private class DefaultPropertyGroupDefinition : IPropertyGroupDefinition
		{
			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00024E91 File Offset: 0x00023091
			[Nullable(1)]
			public string GroupName
			{
				[NullableContext(1)]
				get
				{
					return SettingsPropertyGroupDefinition.DefaultGroupName;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00024E98 File Offset: 0x00023098
			public int GroupOrder
			{
				get
				{
					return 0;
				}
			}
		}
	}
}
