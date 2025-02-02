using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200001F RID: 31
	public struct InstructionSymbol
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00007418 File Offset: 0x00005618
		public InstructionSymbol(int offset, SequencePoint sequencePoint)
		{
			this.Offset = offset;
			this.SequencePoint = sequencePoint;
		}

		// Token: 0x04000269 RID: 617
		public readonly int Offset;

		// Token: 0x0400026A RID: 618
		public readonly SequencePoint SequencePoint;
	}
}
