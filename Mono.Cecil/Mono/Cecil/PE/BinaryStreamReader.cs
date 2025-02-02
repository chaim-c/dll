using System;
using System.IO;

namespace Mono.Cecil.PE
{
	// Token: 0x02000044 RID: 68
	internal class BinaryStreamReader : BinaryReader
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00009749 File Offset: 0x00007949
		public BinaryStreamReader(Stream stream) : base(stream)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009752 File Offset: 0x00007952
		protected void Advance(int bytes)
		{
			this.BaseStream.Seek((long)bytes, SeekOrigin.Current);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009763 File Offset: 0x00007963
		protected DataDirectory ReadDataDirectory()
		{
			return new DataDirectory(this.ReadUInt32(), this.ReadUInt32());
		}
	}
}
