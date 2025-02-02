using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A7 RID: 167
	public abstract class MethodSpecification : MethodReference
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00017FC0 File Offset: 0x000161C0
		public MethodReference ElementMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00017FC8 File Offset: 0x000161C8
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00017FD5 File Offset: 0x000161D5
		public override string Name
		{
			get
			{
				return this.method.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00017FDC File Offset: 0x000161DC
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00017FE9 File Offset: 0x000161E9
		public override MethodCallingConvention CallingConvention
		{
			get
			{
				return this.method.CallingConvention;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00017FF0 File Offset: 0x000161F0
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00017FFD File Offset: 0x000161FD
		public override bool HasThis
		{
			get
			{
				return this.method.HasThis;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00018004 File Offset: 0x00016204
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00018011 File Offset: 0x00016211
		public override bool ExplicitThis
		{
			get
			{
				return this.method.ExplicitThis;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00018018 File Offset: 0x00016218
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00018025 File Offset: 0x00016225
		public override MethodReturnType MethodReturnType
		{
			get
			{
				return this.method.MethodReturnType;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001802C File Offset: 0x0001622C
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00018039 File Offset: 0x00016239
		public override TypeReference DeclaringType
		{
			get
			{
				return this.method.DeclaringType;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00018040 File Offset: 0x00016240
		public override ModuleDefinition Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001804D File Offset: 0x0001624D
		public override bool HasParameters
		{
			get
			{
				return this.method.HasParameters;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001805A File Offset: 0x0001625A
		public override Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.method.Parameters;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00018067 File Offset: 0x00016267
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.method.ContainsGenericParameter;
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00018074 File Offset: 0x00016274
		internal MethodSpecification(MethodReference method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.method = method;
			this.token = new MetadataToken(TokenType.MethodSpec);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000180A1 File Offset: 0x000162A1
		public sealed override MethodReference GetElementMethod()
		{
			return this.method.GetElementMethod();
		}

		// Token: 0x0400042F RID: 1071
		private readonly MethodReference method;
	}
}
