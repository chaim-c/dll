using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000031 RID: 49
	internal sealed class UserStringHeapBuffer : StringHeapBuffer
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00007C90 File Offset: 0x00005E90
		protected override void WriteString(string @string)
		{
			base.WriteCompressedUInt32((uint)(@string.Length * 2 + 1));
			byte b = 0;
			foreach (char c in @string)
			{
				base.WriteUInt16((ushort)c);
				if (b != 1 && (c < ' ' || c > '~') && (c > '~' || (c >= '\u0001' && c <= '\b') || (c >= '\u000e' && c <= '\u001f') || c == '\'' || c == '-'))
				{
					b = 1;
				}
			}
			base.WriteByte(b);
		}
	}
}
