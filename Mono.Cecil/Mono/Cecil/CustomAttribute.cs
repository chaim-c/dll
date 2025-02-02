using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A2 RID: 162
	public sealed class CustomAttribute : ICustomAttribute
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000178B9 File Offset: 0x00015AB9
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x000178C1 File Offset: 0x00015AC1
		public MethodReference Constructor
		{
			get
			{
				return this.constructor;
			}
			set
			{
				this.constructor = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000178CA File Offset: 0x00015ACA
		public TypeReference AttributeType
		{
			get
			{
				return this.constructor.DeclaringType;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000178D7 File Offset: 0x00015AD7
		public bool IsResolved
		{
			get
			{
				return this.resolved;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000178DF File Offset: 0x00015ADF
		public bool HasConstructorArguments
		{
			get
			{
				this.Resolve();
				return !this.arguments.IsNullOrEmpty<CustomAttributeArgument>();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000178F8 File Offset: 0x00015AF8
		public Collection<CustomAttributeArgument> ConstructorArguments
		{
			get
			{
				this.Resolve();
				Collection<CustomAttributeArgument> result;
				if ((result = this.arguments) == null)
				{
					result = (this.arguments = new Collection<CustomAttributeArgument>());
				}
				return result;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00017923 File Offset: 0x00015B23
		public bool HasFields
		{
			get
			{
				this.Resolve();
				return !this.fields.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001793C File Offset: 0x00015B3C
		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				this.Resolve();
				Collection<CustomAttributeNamedArgument> result;
				if ((result = this.fields) == null)
				{
					result = (this.fields = new Collection<CustomAttributeNamedArgument>());
				}
				return result;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00017967 File Offset: 0x00015B67
		public bool HasProperties
		{
			get
			{
				this.Resolve();
				return !this.properties.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00017980 File Offset: 0x00015B80
		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				this.Resolve();
				Collection<CustomAttributeNamedArgument> result;
				if ((result = this.properties) == null)
				{
					result = (this.properties = new Collection<CustomAttributeNamedArgument>());
				}
				return result;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000179AB File Offset: 0x00015BAB
		internal bool HasImage
		{
			get
			{
				return this.constructor != null && this.constructor.HasImage;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000179C2 File Offset: 0x00015BC2
		internal ModuleDefinition Module
		{
			get
			{
				return this.constructor.Module;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000179CF File Offset: 0x00015BCF
		internal CustomAttribute(uint signature, MethodReference constructor)
		{
			this.signature = signature;
			this.constructor = constructor;
			this.resolved = false;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000179EC File Offset: 0x00015BEC
		public CustomAttribute(MethodReference constructor)
		{
			this.constructor = constructor;
			this.resolved = true;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00017A02 File Offset: 0x00015C02
		public CustomAttribute(MethodReference constructor, byte[] blob)
		{
			this.constructor = constructor;
			this.resolved = false;
			this.blob = blob;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00017A30 File Offset: 0x00015C30
		public byte[] GetBlob()
		{
			if (this.blob != null)
			{
				return this.blob;
			}
			if (!this.HasImage)
			{
				throw new NotSupportedException();
			}
			return this.Module.Read<CustomAttribute, byte[]>(ref this.blob, this, (CustomAttribute attribute, MetadataReader reader) => reader.ReadCustomAttributeBlob(attribute.signature));
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00017B00 File Offset: 0x00015D00
		private void Resolve()
		{
			if (this.resolved || !this.HasImage)
			{
				return;
			}
			this.Module.Read<CustomAttribute, CustomAttribute>(this, delegate(CustomAttribute attribute, MetadataReader reader)
			{
				try
				{
					reader.ReadCustomAttributeSignature(attribute);
					this.resolved = true;
				}
				catch (ResolutionException)
				{
					if (this.arguments != null)
					{
						this.arguments.Clear();
					}
					if (this.fields != null)
					{
						this.fields.Clear();
					}
					if (this.properties != null)
					{
						this.properties.Clear();
					}
					this.resolved = false;
				}
				return this;
			});
		}

		// Token: 0x0400041C RID: 1052
		internal readonly uint signature;

		// Token: 0x0400041D RID: 1053
		internal bool resolved;

		// Token: 0x0400041E RID: 1054
		private MethodReference constructor;

		// Token: 0x0400041F RID: 1055
		private byte[] blob;

		// Token: 0x04000420 RID: 1056
		internal Collection<CustomAttributeArgument> arguments;

		// Token: 0x04000421 RID: 1057
		internal Collection<CustomAttributeNamedArgument> fields;

		// Token: 0x04000422 RID: 1058
		internal Collection<CustomAttributeNamedArgument> properties;
	}
}
