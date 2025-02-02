using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000020 RID: 32
	public sealed class MethodSymbols
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00007428 File Offset: 0x00005628
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDefinition>();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007438 File Offset: 0x00005638
		public Collection<VariableDefinition> Variables
		{
			get
			{
				if (this.variables == null)
				{
					this.variables = new Collection<VariableDefinition>();
				}
				return this.variables;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007453 File Offset: 0x00005653
		public Collection<InstructionSymbol> Instructions
		{
			get
			{
				if (this.instructions == null)
				{
					this.instructions = new Collection<InstructionSymbol>();
				}
				return this.instructions;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000746E File Offset: 0x0000566E
		public int CodeSize
		{
			get
			{
				return this.code_size;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007476 File Offset: 0x00005676
		public string MethodName
		{
			get
			{
				return this.method_name;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000747E File Offset: 0x0000567E
		public MetadataToken MethodToken
		{
			get
			{
				return this.method_token;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007486 File Offset: 0x00005686
		public MetadataToken LocalVarToken
		{
			get
			{
				return this.local_var_token;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000748E File Offset: 0x0000568E
		internal MethodSymbols(string methodName)
		{
			this.method_name = methodName;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000749D File Offset: 0x0000569D
		public MethodSymbols(MetadataToken methodToken)
		{
			this.method_token = methodToken;
		}

		// Token: 0x0400026B RID: 619
		internal int code_size;

		// Token: 0x0400026C RID: 620
		internal string method_name;

		// Token: 0x0400026D RID: 621
		internal MetadataToken method_token;

		// Token: 0x0400026E RID: 622
		internal MetadataToken local_var_token;

		// Token: 0x0400026F RID: 623
		internal Collection<VariableDefinition> variables;

		// Token: 0x04000270 RID: 624
		internal Collection<InstructionSymbol> instructions;
	}
}
