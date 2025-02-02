using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace MCM.LightInject
{
	// Token: 0x0200011D RID: 285
	[ExcludeFromCodeCoverage]
	internal class Instruction<T> : Instruction
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x0001582D File Offset: 0x00013A2D
		public Instruction(OpCode code, T argument, Action<ILGenerator> emitAction) : base(code, emitAction)
		{
			this.Argument = argument;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00015841 File Offset: 0x00013A41
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00015849 File Offset: 0x00013A49
		public T Argument { get; private set; }

		// Token: 0x060006C7 RID: 1735 RVA: 0x00015852 File Offset: 0x00013A52
		public override string ToString()
		{
			return string.Format("{0} {1}", base.ToString(), this.Argument);
		}
	}
}
