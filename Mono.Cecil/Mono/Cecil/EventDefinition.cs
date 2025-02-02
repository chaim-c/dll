using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000C6 RID: 198
	public sealed class EventDefinition : EventReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000199FB File Offset: 0x00017BFB
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00019A03 File Offset: 0x00017C03
		public EventAttributes Attributes
		{
			get
			{
				return (EventAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00019A0C File Offset: 0x00017C0C
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00019A29 File Offset: 0x00017C29
		public MethodDefinition AddMethod
		{
			get
			{
				if (this.add_method != null)
				{
					return this.add_method;
				}
				this.InitializeMethods();
				return this.add_method;
			}
			set
			{
				this.add_method = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00019A32 File Offset: 0x00017C32
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00019A4F File Offset: 0x00017C4F
		public MethodDefinition InvokeMethod
		{
			get
			{
				if (this.invoke_method != null)
				{
					return this.invoke_method;
				}
				this.InitializeMethods();
				return this.invoke_method;
			}
			set
			{
				this.invoke_method = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00019A58 File Offset: 0x00017C58
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00019A75 File Offset: 0x00017C75
		public MethodDefinition RemoveMethod
		{
			get
			{
				if (this.remove_method != null)
				{
					return this.remove_method;
				}
				this.InitializeMethods();
				return this.remove_method;
			}
			set
			{
				this.remove_method = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00019A7E File Offset: 0x00017C7E
		public bool HasOtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods.Count > 0;
				}
				this.InitializeMethods();
				return !this.other_methods.IsNullOrEmpty<MethodDefinition>();
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00019AAC File Offset: 0x00017CAC
		public Collection<MethodDefinition> OtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				this.InitializeMethods();
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				return this.other_methods = new Collection<MethodDefinition>();
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00019AEB File Offset: 0x00017CEB
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00019B10 File Offset: 0x00017D10
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00019B2E File Offset: 0x00017D2E
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00019B40 File Offset: 0x00017D40
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00019B59 File Offset: 0x00017D59
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00019B6B File Offset: 0x00017D6B
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00019B84 File Offset: 0x00017D84
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x00019B91 File Offset: 0x00017D91
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00019B9A File Offset: 0x00017D9A
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00019B9D File Offset: 0x00017D9D
		public EventDefinition(string name, EventAttributes attributes, TypeReference eventType) : base(name, eventType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Event);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00019BC8 File Offset: 0x00017DC8
		private void InitializeMethods()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				return;
			}
			lock (module.SyncRoot)
			{
				if (this.add_method == null && this.invoke_method == null && this.remove_method == null)
				{
					if (module.HasImage())
					{
						module.Read<EventDefinition, EventDefinition>(this, (EventDefinition @event, MetadataReader reader) => reader.ReadMethods(@event));
					}
				}
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00019C50 File Offset: 0x00017E50
		public override EventDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040004A3 RID: 1187
		private ushort attributes;

		// Token: 0x040004A4 RID: 1188
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040004A5 RID: 1189
		internal MethodDefinition add_method;

		// Token: 0x040004A6 RID: 1190
		internal MethodDefinition invoke_method;

		// Token: 0x040004A7 RID: 1191
		internal MethodDefinition remove_method;

		// Token: 0x040004A8 RID: 1192
		internal Collection<MethodDefinition> other_methods;
	}
}
