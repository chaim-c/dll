using System;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x0200000D RID: 13
	internal class ByteBuffer
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002B65 File Offset: 0x00000D65
		internal ByteBuffer(byte[] buffer)
		{
			this.buffer = buffer;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B74 File Offset: 0x00000D74
		internal byte ReadByte()
		{
			this.CheckCanRead(1);
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			return array[num];
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BA0 File Offset: 0x00000DA0
		internal byte[] ReadBytes(int length)
		{
			this.CheckCanRead(length);
			byte[] array = new byte[length];
			Buffer.BlockCopy(this.buffer, this.position, array, 0, length);
			this.position += length;
			return array;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BE0 File Offset: 0x00000DE0
		internal short ReadInt16()
		{
			this.CheckCanRead(2);
			short result = (short)((int)this.buffer[this.position] | (int)this.buffer[this.position + 1] << 8);
			this.position += 2;
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C24 File Offset: 0x00000E24
		internal int ReadInt32()
		{
			this.CheckCanRead(4);
			int result = (int)this.buffer[this.position] | (int)this.buffer[this.position + 1] << 8 | (int)this.buffer[this.position + 2] << 16 | (int)this.buffer[this.position + 3] << 24;
			this.position += 4;
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C90 File Offset: 0x00000E90
		internal long ReadInt64()
		{
			this.CheckCanRead(8);
			uint num = (uint)((int)this.buffer[this.position] | (int)this.buffer[this.position + 1] << 8 | (int)this.buffer[this.position + 2] << 16 | (int)this.buffer[this.position + 3] << 24);
			uint num2 = (uint)((int)this.buffer[this.position + 4] | (int)this.buffer[this.position + 5] << 8 | (int)this.buffer[this.position + 6] << 16 | (int)this.buffer[this.position + 7] << 24);
			long result = (long)((ulong)num2 << 32 | (ulong)num);
			this.position += 8;
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D4C File Offset: 0x00000F4C
		internal float ReadSingle()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = this.ReadBytes(4);
				Array.Reverse(array);
				return BitConverter.ToSingle(array, 0);
			}
			this.CheckCanRead(4);
			float result = BitConverter.ToSingle(this.buffer, this.position);
			this.position += 4;
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DA0 File Offset: 0x00000FA0
		internal double ReadDouble()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = this.ReadBytes(8);
				Array.Reverse(array);
				return BitConverter.ToDouble(array, 0);
			}
			this.CheckCanRead(8);
			double result = BitConverter.ToDouble(this.buffer, this.position);
			this.position += 8;
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DF4 File Offset: 0x00000FF4
		private void CheckCanRead(int count)
		{
			if (this.position + count > this.buffer.Length)
			{
				string paramName = "count";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
				defaultInterpolatedStringHandler.AppendLiteral("position(");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.position);
				defaultInterpolatedStringHandler.AppendLiteral(") + count(");
				defaultInterpolatedStringHandler.AppendFormatted<int>(count);
				defaultInterpolatedStringHandler.AppendLiteral(") > buffer.Length(");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.buffer.Length);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				throw new ArgumentOutOfRangeException(paramName, defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x0400000C RID: 12
		internal byte[] buffer;

		// Token: 0x0400000D RID: 13
		internal int position;
	}
}
