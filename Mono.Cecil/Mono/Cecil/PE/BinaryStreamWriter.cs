using System;
using System.IO;

namespace Mono.Cecil.PE
{
	// Token: 0x02000045 RID: 69
	internal class BinaryStreamWriter : BinaryWriter
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00009776 File Offset: 0x00007976
		public BinaryStreamWriter(Stream stream) : base(stream)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000977F File Offset: 0x0000797F
		public void WriteByte(byte value)
		{
			this.Write(value);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009788 File Offset: 0x00007988
		public void WriteUInt16(ushort value)
		{
			this.Write(value);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009791 File Offset: 0x00007991
		public void WriteInt16(short value)
		{
			this.Write(value);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000979A File Offset: 0x0000799A
		public void WriteUInt32(uint value)
		{
			this.Write(value);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000097A3 File Offset: 0x000079A3
		public void WriteInt32(int value)
		{
			this.Write(value);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000097AC File Offset: 0x000079AC
		public void WriteUInt64(ulong value)
		{
			this.Write(value);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000097B5 File Offset: 0x000079B5
		public void WriteBytes(byte[] bytes)
		{
			this.Write(bytes);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000097BE File Offset: 0x000079BE
		public void WriteDataDirectory(DataDirectory directory)
		{
			this.Write(directory.VirtualAddress);
			this.Write(directory.Size);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000097DA File Offset: 0x000079DA
		public void WriteBuffer(ByteBuffer buffer)
		{
			this.Write(buffer.buffer, 0, buffer.length);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000097EF File Offset: 0x000079EF
		protected void Advance(int bytes)
		{
			this.BaseStream.Seek((long)bytes, SeekOrigin.Current);
		}
	}
}
