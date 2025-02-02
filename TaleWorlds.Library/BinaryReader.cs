using System;
using System.Text;

namespace TaleWorlds.Library
{
	// Token: 0x02000019 RID: 25
	public class BinaryReader : IReader
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002CF1 File Offset: 0x00000EF1
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public byte[] Data { get; private set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002D02 File Offset: 0x00000F02
		public BinaryReader(byte[] data)
		{
			this.Data = data;
			this._cursor = 0;
			this._buffer = new byte[4];
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D24 File Offset: 0x00000F24
		public int UnreadByteCount
		{
			get
			{
				return this.Data.Length - this._cursor;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D35 File Offset: 0x00000F35
		public ISerializableObject ReadSerializableObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D3C File Offset: 0x00000F3C
		public int Read3ByteInt()
		{
			this._buffer[0] = this.ReadByte();
			this._buffer[1] = this.ReadByte();
			this._buffer[2] = this.ReadByte();
			if (this._buffer[0] == 255 && this._buffer[1] == 255 && this._buffer[2] == 255)
			{
				this._buffer[3] = byte.MaxValue;
			}
			else
			{
				this._buffer[3] = 0;
			}
			return BitConverter.ToInt32(this._buffer, 0);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public int ReadInt()
		{
			int result = BitConverter.ToInt32(this.Data, this._cursor);
			this._cursor += 4;
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DE5 File Offset: 0x00000FE5
		public short ReadShort()
		{
			short result = BitConverter.ToInt16(this.Data, this._cursor);
			this._cursor += 2;
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E08 File Offset: 0x00001008
		public void ReadFloats(float[] output, int count)
		{
			int num = count * 4;
			Buffer.BlockCopy(this.Data, this._cursor, output, 0, num);
			this._cursor += num;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E3C File Offset: 0x0000103C
		public void ReadShorts(short[] output, int count)
		{
			int num = count * 2;
			Buffer.BlockCopy(this.Data, this._cursor, output, 0, num);
			this._cursor += num;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E70 File Offset: 0x00001070
		public string ReadString()
		{
			int num = this.ReadInt();
			string result = null;
			if (num >= 0)
			{
				result = Encoding.UTF8.GetString(this.Data, this._cursor, num);
				this._cursor += num;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EB4 File Offset: 0x000010B4
		public Color ReadColor()
		{
			float red = this.ReadFloat();
			float green = this.ReadFloat();
			float blue = this.ReadFloat();
			float alpha = this.ReadFloat();
			return new Color(red, green, blue, alpha);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EE6 File Offset: 0x000010E6
		public bool ReadBool()
		{
			int num = (int)this.Data[this._cursor];
			this._cursor++;
			return num == 1;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F06 File Offset: 0x00001106
		public float ReadFloat()
		{
			float result = BitConverter.ToSingle(this.Data, this._cursor);
			this._cursor += 4;
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F27 File Offset: 0x00001127
		public uint ReadUInt()
		{
			uint result = BitConverter.ToUInt32(this.Data, this._cursor);
			this._cursor += 4;
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F48 File Offset: 0x00001148
		public ulong ReadULong()
		{
			ulong result = BitConverter.ToUInt64(this.Data, this._cursor);
			this._cursor += 8;
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F69 File Offset: 0x00001169
		public long ReadLong()
		{
			long result = BitConverter.ToInt64(this.Data, this._cursor);
			this._cursor += 8;
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F8A File Offset: 0x0000118A
		public byte ReadByte()
		{
			byte result = this.Data[this._cursor];
			this._cursor++;
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FA8 File Offset: 0x000011A8
		public byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			Array.Copy(this.Data, this._cursor, array, 0, length);
			this._cursor += length;
			return array;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FE0 File Offset: 0x000011E0
		public Vec2 ReadVec2()
		{
			float a = this.ReadFloat();
			float b = this.ReadFloat();
			return new Vec2(a, b);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003000 File Offset: 0x00001200
		public Vec3 ReadVec3()
		{
			float x = this.ReadFloat();
			float y = this.ReadFloat();
			float z = this.ReadFloat();
			float w = this.ReadFloat();
			return new Vec3(x, y, z, w);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003030 File Offset: 0x00001230
		public Vec3i ReadVec3Int()
		{
			int x = this.ReadInt();
			int y = this.ReadInt();
			int z = this.ReadInt();
			return new Vec3i(x, y, z);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003058 File Offset: 0x00001258
		public sbyte ReadSByte()
		{
			sbyte result = (sbyte)this.Data[this._cursor];
			this._cursor++;
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003076 File Offset: 0x00001276
		public ushort ReadUShort()
		{
			ushort result = BitConverter.ToUInt16(this.Data, this._cursor);
			this._cursor += 2;
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003097 File Offset: 0x00001297
		public double ReadDouble()
		{
			double result = BitConverter.ToDouble(this.Data, this._cursor);
			this._cursor += 8;
			return result;
		}

		// Token: 0x04000051 RID: 81
		private int _cursor;

		// Token: 0x04000052 RID: 82
		private byte[] _buffer;
	}
}
