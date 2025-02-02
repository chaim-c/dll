using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A5 RID: 165
	public sealed class FunctionPointerType : TypeSpecification, IMethodSignature, IMetadataTokenProvider
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00017BD7 File Offset: 0x00015DD7
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public bool HasThis
		{
			get
			{
				return this.function.HasThis;
			}
			set
			{
				this.function.HasThis = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00017BF2 File Offset: 0x00015DF2
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00017BFF File Offset: 0x00015DFF
		public bool ExplicitThis
		{
			get
			{
				return this.function.ExplicitThis;
			}
			set
			{
				this.function.ExplicitThis = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00017C0D File Offset: 0x00015E0D
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00017C1A File Offset: 0x00015E1A
		public MethodCallingConvention CallingConvention
		{
			get
			{
				return this.function.CallingConvention;
			}
			set
			{
				this.function.CallingConvention = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00017C28 File Offset: 0x00015E28
		public bool HasParameters
		{
			get
			{
				return this.function.HasParameters;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00017C35 File Offset: 0x00015E35
		public Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.function.Parameters;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00017C42 File Offset: 0x00015E42
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00017C54 File Offset: 0x00015E54
		public TypeReference ReturnType
		{
			get
			{
				return this.function.MethodReturnType.ReturnType;
			}
			set
			{
				this.function.MethodReturnType.ReturnType = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00017C67 File Offset: 0x00015E67
		public MethodReturnType MethodReturnType
		{
			get
			{
				return this.function.MethodReturnType;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00017C74 File Offset: 0x00015E74
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00017C81 File Offset: 0x00015E81
		public override string Name
		{
			get
			{
				return this.function.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00017C88 File Offset: 0x00015E88
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x00017C8F File Offset: 0x00015E8F
		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00017C96 File Offset: 0x00015E96
		public override ModuleDefinition Module
		{
			get
			{
				return this.ReturnType.Module;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00017CA3 File Offset: 0x00015EA3
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00017CB5 File Offset: 0x00015EB5
		public override IMetadataScope Scope
		{
			get
			{
				return this.function.ReturnType.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00017CBC File Offset: 0x00015EBC
		public override bool IsFunctionPointer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00017CBF File Offset: 0x00015EBF
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.function.ContainsGenericParameter;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00017CCC File Offset: 0x00015ECC
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.function.Name);
				stringBuilder.Append(" ");
				stringBuilder.Append(this.function.ReturnType.FullName);
				stringBuilder.Append(" *");
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00017D2D File Offset: 0x00015F2D
		public FunctionPointerType() : base(null)
		{
			this.function = new MethodReference();
			this.function.Name = "method";
			this.etype = Mono.Cecil.Metadata.ElementType.FnPtr;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00017D59 File Offset: 0x00015F59
		public override TypeDefinition Resolve()
		{
			return null;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00017D5C File Offset: 0x00015F5C
		public override TypeReference GetElementType()
		{
			return this;
		}

		// Token: 0x04000428 RID: 1064
		private readonly MethodReference function;
	}
}
