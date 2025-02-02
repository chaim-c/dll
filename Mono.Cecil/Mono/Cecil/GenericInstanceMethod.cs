using System;
using System.Text;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A9 RID: 169
	public sealed class GenericInstanceMethod : MethodSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000180AE File Offset: 0x000162AE
		public bool HasGenericArguments
		{
			get
			{
				return !this.arguments.IsNullOrEmpty<TypeReference>();
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000180C0 File Offset: 0x000162C0
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x000180E5 File Offset: 0x000162E5
		public override bool IsGenericInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000180E8 File Offset: 0x000162E8
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return base.ElementMethod;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000180F0 File Offset: 0x000162F0
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return base.ElementMethod.DeclaringType;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000180FD File Offset: 0x000162FD
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.ContainsGenericParameter() || base.ContainsGenericParameter;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00018110 File Offset: 0x00016310
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				MethodReference elementMethod = base.ElementMethod;
				stringBuilder.Append(elementMethod.ReturnType.FullName).Append(" ").Append(elementMethod.DeclaringType.FullName).Append("::").Append(elementMethod.Name);
				this.GenericInstanceFullName(stringBuilder);
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001817F File Offset: 0x0001637F
		public GenericInstanceMethod(MethodReference method) : base(method)
		{
		}

		// Token: 0x04000430 RID: 1072
		private Collection<TypeReference> arguments;
	}
}
