using System;
using System.Text;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A6 RID: 166
	public class MethodReference : MemberReference, IMethodSignature, IGenericParameterProvider, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00017D5F File Offset: 0x00015F5F
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00017D67 File Offset: 0x00015F67
		public virtual bool HasThis
		{
			get
			{
				return this.has_this;
			}
			set
			{
				this.has_this = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00017D70 File Offset: 0x00015F70
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00017D78 File Offset: 0x00015F78
		public virtual bool ExplicitThis
		{
			get
			{
				return this.explicit_this;
			}
			set
			{
				this.explicit_this = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00017D81 File Offset: 0x00015F81
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00017D89 File Offset: 0x00015F89
		public virtual MethodCallingConvention CallingConvention
		{
			get
			{
				return this.calling_convention;
			}
			set
			{
				this.calling_convention = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00017D92 File Offset: 0x00015F92
		public virtual bool HasParameters
		{
			get
			{
				return !this.parameters.IsNullOrEmpty<ParameterDefinition>();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00017DA2 File Offset: 0x00015FA2
		public virtual Collection<ParameterDefinition> Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ParameterDefinitionCollection(this);
				}
				return this.parameters;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00017DC0 File Offset: 0x00015FC0
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				TypeReference declaringType = this.DeclaringType;
				GenericInstanceType genericInstanceType = declaringType as GenericInstanceType;
				if (genericInstanceType != null)
				{
					return genericInstanceType.ElementType;
				}
				return declaringType;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00017DE6 File Offset: 0x00015FE6
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00017DE9 File Offset: 0x00015FE9
		GenericParameterType IGenericParameterProvider.GenericParameterType
		{
			get
			{
				return GenericParameterType.Method;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00017DEC File Offset: 0x00015FEC
		public virtual bool HasGenericParameters
		{
			get
			{
				return !this.generic_parameters.IsNullOrEmpty<GenericParameter>();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00017DFC File Offset: 0x00015FFC
		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters;
				}
				return this.generic_parameters = new GenericParameterCollection(this);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00017E28 File Offset: 0x00016028
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x00017E48 File Offset: 0x00016048
		public TypeReference ReturnType
		{
			get
			{
				MethodReturnType methodReturnType = this.MethodReturnType;
				if (methodReturnType == null)
				{
					return null;
				}
				return methodReturnType.ReturnType;
			}
			set
			{
				MethodReturnType methodReturnType = this.MethodReturnType;
				if (methodReturnType != null)
				{
					methodReturnType.ReturnType = value;
				}
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00017E66 File Offset: 0x00016066
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x00017E6E File Offset: 0x0001606E
		public virtual MethodReturnType MethodReturnType
		{
			get
			{
				return this.return_type;
			}
			set
			{
				this.return_type = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00017E78 File Offset: 0x00016078
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.ReturnType.FullName).Append(" ").Append(base.MemberFullName());
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00017EBF File Offset: 0x000160BF
		public virtual bool IsGenericInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00017EC4 File Offset: 0x000160C4
		public override bool ContainsGenericParameter
		{
			get
			{
				if (this.ReturnType.ContainsGenericParameter || base.ContainsGenericParameter)
				{
					return true;
				}
				Collection<ParameterDefinition> collection = this.Parameters;
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i].ParameterType.ContainsGenericParameter)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00017F16 File Offset: 0x00016116
		internal MethodReference()
		{
			this.return_type = new MethodReturnType(this);
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00017F3A File Offset: 0x0001613A
		public MethodReference(string name, TypeReference returnType) : base(name)
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			this.return_type = new MethodReturnType(this);
			this.return_type.ReturnType = returnType;
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00017F79 File Offset: 0x00016179
		public MethodReference(string name, TypeReference returnType, TypeReference declaringType) : this(name, returnType)
		{
			if (declaringType == null)
			{
				throw new ArgumentNullException("declaringType");
			}
			this.DeclaringType = declaringType;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00017F98 File Offset: 0x00016198
		public virtual MethodReference GetElementMethod()
		{
			return this;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00017F9C File Offset: 0x0001619C
		public virtual MethodDefinition Resolve()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}

		// Token: 0x04000429 RID: 1065
		internal ParameterDefinitionCollection parameters;

		// Token: 0x0400042A RID: 1066
		private MethodReturnType return_type;

		// Token: 0x0400042B RID: 1067
		private bool has_this;

		// Token: 0x0400042C RID: 1068
		private bool explicit_this;

		// Token: 0x0400042D RID: 1069
		private MethodCallingConvention calling_convention;

		// Token: 0x0400042E RID: 1070
		internal Collection<GenericParameter> generic_parameters;
	}
}
