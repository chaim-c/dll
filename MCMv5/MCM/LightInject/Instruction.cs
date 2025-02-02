using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace MCM.LightInject
{
	// Token: 0x0200011C RID: 284
	[ExcludeFromCodeCoverage]
	internal class Instruction
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x000157CE File Offset: 0x000139CE
		public Instruction(OpCode code, Action<ILGenerator> emitAction)
		{
			this.Code = code;
			this.Emit = emitAction;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000157E8 File Offset: 0x000139E8
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x000157F0 File Offset: 0x000139F0
		public OpCode Code { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000157F9 File Offset: 0x000139F9
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00015801 File Offset: 0x00013A01
		public Action<ILGenerator> Emit { get; private set; }

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001580C File Offset: 0x00013A0C
		public override string ToString()
		{
			return this.Code.ToString();
		}
	}
}
