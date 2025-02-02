using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000AA RID: 170
	public sealed class GenericInstanceType : TypeSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00018188 File Offset: 0x00016388
		public bool HasGenericArguments
		{
			get
			{
				return !this.arguments.IsNullOrEmpty<TypeReference>();
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00018198 File Offset: 0x00016398
		public Collection<TypeReference> GenericArguments
		{
			get
			{
				Collection<TypeReference> result;
				if ((result = this.arguments) == null)
				{
					result = (this.arguments = new Collection<TypeReference>());
				}
				return result;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000181BD File Offset: 0x000163BD
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x000181CA File Offset: 0x000163CA
		public override TypeReference DeclaringType
		{
			get
			{
				return base.ElementType.DeclaringType;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000181D4 File Offset: 0x000163D4
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.FullName);
				this.GenericInstanceFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00018201 File Offset: 0x00016401
		public override bool IsGenericInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00018204 File Offset: 0x00016404
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.ContainsGenericParameter() || base.ContainsGenericParameter;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00018216 File Offset: 0x00016416
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return base.ElementType;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001821E File Offset: 0x0001641E
		public GenericInstanceType(TypeReference type) : base(type)
		{
			base.IsValueType = type.IsValueType;
			this.etype = Mono.Cecil.Metadata.ElementType.GenericInst;
		}

		// Token: 0x04000431 RID: 1073
		private Collection<TypeReference> arguments;
	}
}
