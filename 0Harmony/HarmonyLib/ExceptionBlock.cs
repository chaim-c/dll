using System;

namespace HarmonyLib
{
	// Token: 0x02000037 RID: 55
	public class ExceptionBlock
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00009FD8 File Offset: 0x000081D8
		public ExceptionBlock(ExceptionBlockType blockType, Type catchType = null)
		{
		}

		// Token: 0x04000085 RID: 133
		public ExceptionBlockType blockType = blockType;

		// Token: 0x04000086 RID: 134
		public Type catchType = catchType ?? typeof(object);
	}
}
