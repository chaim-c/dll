using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200001E RID: 30
	public sealed class Scope : IVariableDefinitionProvider
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007398 File Offset: 0x00005598
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000073A0 File Offset: 0x000055A0
		public Instruction Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000073A9 File Offset: 0x000055A9
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000073B1 File Offset: 0x000055B1
		public Instruction End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000073BA File Offset: 0x000055BA
		public bool HasScopes
		{
			get
			{
				return !this.scopes.IsNullOrEmpty<Scope>();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000073CA File Offset: 0x000055CA
		public Collection<Scope> Scopes
		{
			get
			{
				if (this.scopes == null)
				{
					this.scopes = new Collection<Scope>();
				}
				return this.scopes;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000073E5 File Offset: 0x000055E5
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDefinition>();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000073F5 File Offset: 0x000055F5
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

		// Token: 0x04000265 RID: 613
		private Instruction start;

		// Token: 0x04000266 RID: 614
		private Instruction end;

		// Token: 0x04000267 RID: 615
		private Collection<Scope> scopes;

		// Token: 0x04000268 RID: 616
		private Collection<VariableDefinition> variables;
	}
}
