using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.FluentBuilder.Models;
using MCM.Abstractions.Wrapper;
using MCM.Common;
using MCM.Implementation.FluentBuilder.Models;

namespace MCM.Implementation.FluentBuilder
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DefaultSettingsPropertyGroupBuilder : ISettingsPropertyGroupBuilder, IPropertyGroupDefinition
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005EC1 File Offset: 0x000040C1
		public Dictionary<string, ISettingsPropertyBuilder> Properties { get; } = new Dictionary<string, ISettingsPropertyBuilder>();

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005EC9 File Offset: 0x000040C9
		public string GroupName { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005ED1 File Offset: 0x000040D1
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005ED9 File Offset: 0x000040D9
		public int GroupOrder { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005EE2 File Offset: 0x000040E2
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005EEA File Offset: 0x000040EA
		private bool HasGroupToggle { get; set; }

		// Token: 0x06000119 RID: 281 RVA: 0x00005EF3 File Offset: 0x000040F3
		public DefaultSettingsPropertyGroupBuilder(string name)
		{
			this.GroupName = name;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005F10 File Offset: 0x00004110
		public ISettingsPropertyGroupBuilder SetGroupOrder(int value)
		{
			this.GroupOrder = value;
			return this;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005F2C File Offset: 0x0000412C
		public ISettingsPropertyGroupBuilder AddToggle(string id, string name, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyGroupToggleBuilder> builder)
		{
			bool hasGroupToggle = this.HasGroupToggle;
			if (hasGroupToggle)
			{
				throw new InvalidOperationException("There already exists a group toggle property!");
			}
			this.HasGroupToggle = true;
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyGroupToggleBuilder(id, name, @ref);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyGroupToggleBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005FA4 File Offset: 0x000041A4
		public ISettingsPropertyGroupBuilder AddBool(string id, string name, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyBoolBuilder> builder)
		{
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyBoolBuilder(id, name, @ref);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyBoolBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006000 File Offset: 0x00004200
		public ISettingsPropertyGroupBuilder AddDropdown(string id, string name, int selectedIndex, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyDropdownBuilder> builder)
		{
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyDropdownBuilder(id, name, selectedIndex, @ref);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyDropdownBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000605C File Offset: 0x0000425C
		public ISettingsPropertyGroupBuilder AddInteger(string id, string name, int minValue, int maxValue, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyIntegerBuilder> builder)
		{
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyIntegerBuilder(id, name, minValue, maxValue, @ref);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyIntegerBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000060BC File Offset: 0x000042BC
		public ISettingsPropertyGroupBuilder AddFloatingInteger(string id, string name, float minValue, float maxValue, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyFloatingIntegerBuilder> builder)
		{
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyFloatingIntegerBuilder(id, name, minValue, maxValue, @ref);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyFloatingIntegerBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000611C File Offset: 0x0000431C
		public ISettingsPropertyGroupBuilder AddText(string id, string name, IRef @ref, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyTextBuilder> builder)
		{
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyTextBuilder(id, name, @ref);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyTextBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006178 File Offset: 0x00004378
		public ISettingsPropertyGroupBuilder AddButton(string id, string name, IRef @ref, string content, [Nullable(new byte[]
		{
			2,
			1
		})] Action<ISettingsPropertyButtonBuilder> builder)
		{
			bool flag = !this.Properties.ContainsKey(name);
			if (flag)
			{
				this.Properties[name] = new DefaultSettingsPropertyButtonBuilder(id, name, @ref, content);
			}
			if (builder != null)
			{
				builder((ISettingsPropertyButtonBuilder)this.Properties[name]);
			}
			return this;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000061D4 File Offset: 0x000043D4
		public ISettingsPropertyGroupBuilder AddCustom<[Nullable(0)] TSettingsPropertyBuilder>(ISettingsPropertyBuilder<TSettingsPropertyBuilder> builder) where TSettingsPropertyBuilder : ISettingsPropertyBuilder
		{
			bool flag = !this.Properties.ContainsKey(builder.Name);
			if (flag)
			{
				this.Properties[builder.Name] = builder;
			}
			return this;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006212 File Offset: 0x00004412
		public IPropertyGroupDefinition GetPropertyGroupDefinition()
		{
			return new PropertyGroupDefinitionWrapper(this);
		}
	}
}
