using System;
using System.IO;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000013 RID: 19
	internal static class InlineSignatureParser
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000488C File Offset: 0x00002A8C
		internal static InlineSignature ImportCallSite(Module moduleFrom, byte[] data)
		{
			InlineSignatureParser.<>c__DisplayClass0_0 CS$<>8__locals1 = new InlineSignatureParser.<>c__DisplayClass0_0();
			CS$<>8__locals1.moduleFrom = moduleFrom;
			InlineSignature inlineSignature = new InlineSignature();
			InlineSignature result;
			using (MemoryStream memoryStream = new MemoryStream(data, false))
			{
				CS$<>8__locals1.reader = new BinaryReader(memoryStream);
				try
				{
					CS$<>8__locals1.<ImportCallSite>g__ReadMethodSignature|0(inlineSignature);
					result = inlineSignature;
				}
				finally
				{
					if (CS$<>8__locals1.reader != null)
					{
						((IDisposable)CS$<>8__locals1.reader).Dispose();
					}
				}
			}
			return result;
		}
	}
}
