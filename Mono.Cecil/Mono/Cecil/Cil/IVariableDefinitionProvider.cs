using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200000F RID: 15
	public interface IVariableDefinitionProvider
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000D5 RID: 213
		bool HasVariables { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D6 RID: 214
		Collection<VariableDefinition> Variables { get; }
	}
}
