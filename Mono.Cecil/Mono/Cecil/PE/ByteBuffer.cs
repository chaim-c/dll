using System;

namespace Mono.Cecil.PE
{
	// Token: 0x02000003 RID: 3
	internal class ByteBuffer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ByteBuffer()
		{
			this.buffer = Empty<byte>.Array;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E3 File Offset: 0x000002E3
		public ByteBuffer(int length)
		{
			this.buffer = new byte[length];
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F7 File Offset: 0x000002F7
		public ByteBuffer(byte[] buffer)
		{
			this.buffer = (buffer ?? Empty<byte>.Array);
			this.length = this.buffer.Length;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000211D File Offset: 0x0000031D
		public void Reset(byte[] buffer)
		{
			this.buffer = (buffer ?? Empty<byte>.Array);
			this.length = this.buffer.Length;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000213D File Offset: 0x0000033D
		public void Advance(int length)
		{
			this.position += length;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002150 File Offset: 0x00000350
		public byte ReadByte()
		{
			return this.buffer[this.position++];
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002175 File Offset: 0x00000375
		public sbyte ReadSByte()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
		public byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			Buffer.BlockCopy(this.buffer, this.position, array, 0, length);
			this.position += length;
			return array;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B8 File Offset: 0x000003B8
		public ushort ReadUInt16()
		{
			ushort result = (ushort)((int)this.buffer[this.position] | (int)this.buffer[this.position + 1] << 8);
			this.position += 2;
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F5 File Offset: 0x000003F5
		public short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002200 File Offset: 0x00000400
		public uint ReadUInt32()
		{
			uint result = (uint)((int)this.buffer[this.position] | (int)this.buffer[this.position + 1] << 8 | (int)this.buffer[this.position + 2] << 16 | (int)this.buffer[this.position + 3] << 24);
			this.position += 4;
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002262 File Offset: 0x00000462
		public int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000226C File Offset: 0x0000046C
		public ulong ReadUInt64()
		{
			uint num = this.ReadUInt32();
			uint num2 = this.ReadUInt32();
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000228F File Offset: 0x0000048F
		public long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002298 File Offset: 0x00000498
		public uint ReadCompressedUInt32()
		{
			byte b = this.ReadByte();
			if ((b & 128) == 0)
			{
				return (uint)b;
			}
			if ((b & 64) == 0)
			{
				return ((uint)b & 4294967167U) << 8 | (uint)this.ReadByte();
			}
			return (uint)(((int)b & -193) << 24 | (int)this.ReadByte() << 16 | (int)this.ReadByte() << 8 | (int)this.ReadByte());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F4 File Offset: 0x000004F4
		public int ReadCompressedInt32()
		{
			int num = (int)(this.ReadCompressedUInt32() >> 1);
			if ((num & 1) == 0)
			{
				return num;
			}
			if (num < 64)
			{
				return num - 64;
			}
			if (num < 8192)
			{
				return num - 8192;
			}
			if (num < 268435456)
			{
				return num - 268435456;
			}
			return num - 536870912;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002344 File Offset: 0x00000544
		public float ReadSingle()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = this.ReadBytes(4);
				Array.Reverse(array);
				return BitConverter.ToSingle(array, 0);
			}
			float result = BitConverter.ToSingle(this.buffer, this.position);
			this.position += 4;
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002390 File Offset: 0x00000590
		public double ReadDouble()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = this.ReadBytes(8);
				Array.Reverse(array);
				return BitConverter.ToDouble(array, 0);
			}
			double result = BitConverter.ToDouble(this.buffer, this.position);
			this.position += 8;
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023DC File Offset: 0x000005DC
		public void WriteByte(byte value)
		{
			if (this.position == this.buffer.Length)
			{
				this.Grow(1);
			}
			this.buffer[this.position++] = value;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002433 File Offset: 0x00000633
		public void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002440 File Offset: 0x00000640
		public void WriteUInt16(ushort value)
		{
			if (this.position + 2 > this.buffer.Length)
			{
				this.Grow(2);
			}
			this.buffer[this.position++] = (byte)value;
			this.buffer[this.position++] = (byte)(value >> 8);
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024B6 File Offset: 0x000006B6
		public void WriteInt16(short value)
		{
			this.WriteUInt16((ushort)value);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024C0 File Offset: 0x000006C0
		public void WriteUInt32(uint value)
		{
			if (this.position + 4 > this.buffer.Length)
			{
				this.Grow(4);
			}
			this.buffer[this.position++] = (byte)value;
			this.buffer[this.position++] = (byte)(value >> 8);
			this.buffer[this.position++] = (byte)(value >> 16);
			this.buffer[this.position++] = (byte)(value >> 24);
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002570 File Offset: 0x00000770
		public void WriteInt32(int value)
		{
			this.WriteUInt32((uint)value);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000257C File Offset: 0x0000077C
		public void WriteUInt64(ulong value)
		{
			if (this.position + 8 > this.buffer.Length)
			{
				this.Grow(8);
			}
			this.buffer[this.position++] = (byte)value;
			this.buffer[this.position++] = (byte)(value >> 8);
			this.buffer[this.position++] = (byte)(value >> 16);
			this.buffer[this.position++] = (byte)(value >> 24);
			this.buffer[this.position++] = (byte)(value >> 32);
			this.buffer[this.position++] = (byte)(value >> 40);
			this.buffer[this.position++] = (byte)(value >> 48);
			this.buffer[this.position++] = (byte)(value >> 56);
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026A8 File Offset: 0x000008A8
		public void WriteInt64(long value)
		{
			this.WriteUInt64((ulong)value);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026B4 File Offset: 0x000008B4
		public void WriteCompressedUInt32(uint value)
		{
			if (value < 128U)
			{
				this.WriteByte((byte)value);
				return;
			}
			if (value < 16384U)
			{
				this.WriteByte((byte)(128U | value >> 8));
				this.WriteByte((byte)(value & 255U));
				return;
			}
			this.WriteByte((byte)(value >> 24 | 192U));
			this.WriteByte((byte)(value >> 16 & 255U));
			this.WriteByte((byte)(value >> 8 & 255U));
			this.WriteByte((byte)(value & 255U));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000273C File Offset: 0x0000093C
		public void WriteCompressedInt32(int value)
		{
			if (value >= 0)
			{
				this.WriteCompressedUInt32((uint)((uint)value << 1));
				return;
			}
			if (value > -64)
			{
				value = 64 + value;
			}
			else if (value >= -8192)
			{
				value = 8192 + value;
			}
			else if (value >= -536870912)
			{
				value = 536870912 + value;
			}
			this.WriteCompressedUInt32((uint)(value << 1 | 1));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002794 File Offset: 0x00000994
		public void WriteBytes(byte[] bytes)
		{
			int num = bytes.Length;
			if (this.position + num > this.buffer.Length)
			{
				this.Grow(num);
			}
			Buffer.BlockCopy(bytes, 0, this.buffer, this.position, num);
			this.position += num;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027FC File Offset: 0x000009FC
		public void WriteBytes(int length)
		{
			if (this.position + length > this.buffer.Length)
			{
				this.Grow(length);
			}
			this.position += length;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000284C File Offset: 0x00000A4C
		public void WriteBytes(ByteBuffer buffer)
		{
			if (this.position + buffer.length > this.buffer.Length)
			{
				this.Grow(buffer.length);
			}
			Buffer.BlockCopy(buffer.buffer, 0, this.buffer, this.position, buffer.length);
			this.position += buffer.length;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void WriteSingle(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			this.WriteBytes(bytes);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028F0 File Offset: 0x00000AF0
		public void WriteDouble(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			this.WriteBytes(bytes);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002918 File Offset: 0x00000B18
		private void Grow(int desired)
		{
			byte[] array = this.buffer;
			int num = array.Length;
			byte[] dst = new byte[Math.Max(num + desired, num * 2)];
			Buffer.BlockCopy(array, 0, dst, 0, num);
			this.buffer = dst;
		}

		// Token: 0x040000DD RID: 221
		internal byte[] buffer;

		// Token: 0x040000DE RID: 222
		internal int length;

		// Token: 0x040000DF RID: 223
		internal int position;
	}
}
