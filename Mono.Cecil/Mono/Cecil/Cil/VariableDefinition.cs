using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000028 RID: 40
	public sealed class VariableDefinition : VariableReference
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000076EA File Offset: 0x000058EA
		public bool IsPinned
		{
			get
			{
				return this.variable_type.IsPinned;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000076F7 File Offset: 0x000058F7
		public VariableDefinition(TypeReference variableType) : base(variableType)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007700 File Offset: 0x00005900
		public VariableDefinition(string name, TypeReference variableType) : base(name, variableType)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000770A File Offset: 0x0000590A
		public override VariableDefinition Resolve()
		{
			return this;
		}
	}
}
