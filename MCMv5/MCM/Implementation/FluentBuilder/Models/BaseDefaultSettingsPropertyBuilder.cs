using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;

namespace MCM.Implementation.FluentBuilder.Models
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class BaseDefaultSettingsPropertyBuilder<[Nullable(0)] TSettingsPropertyBuilder> : ISettingsPropertyBuilder<TSettingsPropertyBuilder>, ISettingsPropertyBuilder, IPropertyDefinitionBase, IPropertyDefinitionWithId where TSettingsPropertyBuilder : ISettingsPropertyBuilder
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000621A File Offset: 0x0000441A
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00006222 File Offset: 0x00004422
		protected TSettingsPropertyBuilder SettingsPropertyBuilder { get; set; } = default(TSettingsPropertyBuilder);

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000622B File Offset: 0x0000442B
		public string Name { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006233 File Offset: 0x00004433
		public string Id { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000623B File Offset: 0x0000443B
		public IRef PropertyReference { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006243 File Offset: 0x00004443
		public string DisplayName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000624B File Offset: 0x0000444B
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006253 File Offset: 0x00004453
		public int Order { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000625C File Offset: 0x0000445C
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006264 File Offset: 0x00004464
		public bool RequireRestart { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000626D File Offset: 0x0000446D
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006275 File Offset: 0x00004475
		public string HintText { get; private set; } = string.Empty;

		// Token: 0x06000130 RID: 304 RVA: 0x0000627E File Offset: 0x0000447E
		protected BaseDefaultSettingsPropertyBuilder(string id, string name, IRef @ref)
		{
			this.Id = id;
			this.Name = name;
			this.PropertyReference = @ref;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000062B4 File Offset: 0x000044B4
		public TSettingsPropertyBuilder SetOrder(int value)
		{
			this.Order = value;
			return this.SettingsPropertyBuilder;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000062D4 File Offset: 0x000044D4
		public TSettingsPropertyBuilder SetRequireRestart(bool value)
		{
			this.RequireRestart = value;
			return this.SettingsPropertyBuilder;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000062F4 File Offset: 0x000044F4
		public TSettingsPropertyBuilder SetHintText(string value)
		{
			this.HintText = value;
			return this.SettingsPropertyBuilder;
		}

		// Token: 0x06000134 RID: 308
		public abstract IEnumerable<IPropertyDefinitionBase> GetDefinitions();
	}
}
