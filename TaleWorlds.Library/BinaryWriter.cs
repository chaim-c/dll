using System;
using System.Text;

namespace TaleWorlds.Library
{
	// Token: 0x0200001A RID: 26
	public class BinaryWriter : IWriter
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000030B8 File Offset: 0x000012B8
		public byte[] Data
		{
			get
			{
				byte[] array = new byte[this._availableIndex];
				Buffer.BlockCopy(this._data, 0, array, 0, this._availableIndex);
				return array;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030E6 File Offset: 0x000012E6
		public int Length
		{
			get
			{
				return this._availableIndex;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000030EE File Offset: 0x000012EE
		public BinaryWriter()
		{
			this._data = new byte[4096];
			this._availableIndex = 0;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000310D File Offset: 0x0000130D
		public BinaryWriter(int capacity)
		{
			this._data = new byte[capacity];
			this._availableIndex = 0;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003128 File Offset: 0x00001328
		public void Clear()
		{
			Array.Clear(this._data, 0, this._data.Length);
			this._availableIndex = 0;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003148 File Offset: 0x00001348
		public void EnsureLength(int added)
		{
			int num = this._availableIndex + added;
			if (num > this._data.Length)
			{
				int num2 = this._data.Length * 2;
				if (num > num2)
				{
					num2 = num;
				}
				byte[] array = new byte[num2];
				Buffer.BlockCopy(this._data, 0, array, 0, this._availableIndex);
				this._data = array;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000319C File Offset: 0x0000139C
		public void WriteSerializableObject(ISerializableObject serializableObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000031A3 File Offset: 0x000013A3
		public void WriteByte(byte value)
		{
			this.EnsureLength(1);
			this._data[this._availableIndex] = value;
			this._availableIndex++;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000031C8 File Offset: 0x000013C8
		public void WriteBytes(byte[] bytes)
		{
			this.EnsureLength(bytes.Length);
			Buffer.BlockCopy(bytes, 0, this._data, this._availableIndex, bytes.Length);
			this._availableIndex += bytes.Length;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031FC File Offset: 0x000013FC
		public void Write3ByteInt(int value)
		{
			this.EnsureLength(3);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
			byte[] data3 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data3[availableIndex] = (byte)(value >> 16);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003264 File Offset: 0x00001464
		public void WriteInt(int value)
		{
			this.EnsureLength(4);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
			byte[] data3 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data3[availableIndex] = (byte)(value >> 16);
			byte[] data4 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data4[availableIndex] = (byte)(value >> 24);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000032E8 File Offset: 0x000014E8
		public void WriteShort(short value)
		{
			this.EnsureLength(2);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003334 File Offset: 0x00001534
		public void WriteString(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				this.WriteInt(bytes.Length);
				this.WriteBytes(bytes);
				return;
			}
			this.WriteInt(0);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003370 File Offset: 0x00001570
		public void WriteFloats(float[] value, int count)
		{
			int num = count * 4;
			this.EnsureLength(num);
			Buffer.BlockCopy(value, 0, this._data, this._availableIndex, num);
			this._availableIndex += num;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033AC File Offset: 0x000015AC
		public void WriteShorts(short[] value, int count)
		{
			int num = count * 2;
			this.EnsureLength(num);
			Buffer.BlockCopy(value, 0, this._data, this._availableIndex, num);
			this._availableIndex += num;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000033E6 File Offset: 0x000015E6
		public void WriteColor(Color value)
		{
			this.WriteFloat(value.Red);
			this.WriteFloat(value.Green);
			this.WriteFloat(value.Blue);
			this.WriteFloat(value.Alpha);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003418 File Offset: 0x00001618
		public void WriteBool(bool value)
		{
			this.EnsureLength(1);
			this._data[this._availableIndex] = (value ? 1 : 0);
			this._availableIndex++;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003444 File Offset: 0x00001644
		public void WriteFloat(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			this.EnsureLength(bytes.Length);
			Buffer.BlockCopy(bytes, 0, this._data, this._availableIndex, bytes.Length);
			this._availableIndex += bytes.Length;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003488 File Offset: 0x00001688
		public void WriteUInt(uint value)
		{
			this.EnsureLength(4);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
			byte[] data3 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data3[availableIndex] = (byte)(value >> 16);
			byte[] data4 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data4[availableIndex] = (byte)(value >> 24);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000350C File Offset: 0x0000170C
		public void WriteULong(ulong value)
		{
			this.EnsureLength(8);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
			byte[] data3 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data3[availableIndex] = (byte)(value >> 16);
			byte[] data4 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data4[availableIndex] = (byte)(value >> 24);
			byte[] data5 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data5[availableIndex] = (byte)(value >> 32);
			byte[] data6 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data6[availableIndex] = (byte)(value >> 40);
			byte[] data7 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data7[availableIndex] = (byte)(value >> 48);
			byte[] data8 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data8[availableIndex] = (byte)(value >> 56);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003604 File Offset: 0x00001804
		public void WriteLong(long value)
		{
			this.EnsureLength(8);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
			byte[] data3 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data3[availableIndex] = (byte)(value >> 16);
			byte[] data4 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data4[availableIndex] = (byte)(value >> 24);
			byte[] data5 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data5[availableIndex] = (byte)(value >> 32);
			byte[] data6 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data6[availableIndex] = (byte)(value >> 40);
			byte[] data7 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data7[availableIndex] = (byte)(value >> 48);
			byte[] data8 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data8[availableIndex] = (byte)(value >> 56);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000036FC File Offset: 0x000018FC
		public void WriteVec2(Vec2 vec2)
		{
			this.WriteFloat(vec2.x);
			this.WriteFloat(vec2.y);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003716 File Offset: 0x00001916
		public void WriteVec3(Vec3 vec3)
		{
			this.WriteFloat(vec3.x);
			this.WriteFloat(vec3.y);
			this.WriteFloat(vec3.z);
			this.WriteFloat(vec3.w);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003748 File Offset: 0x00001948
		public void WriteVec3Int(Vec3i vec3)
		{
			this.WriteInt(vec3.X);
			this.WriteInt(vec3.Y);
			this.WriteInt(vec3.Z);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000376E File Offset: 0x0000196E
		public void WriteSByte(sbyte value)
		{
			this.EnsureLength(1);
			this._data[this._availableIndex] = (byte)value;
			this._availableIndex++;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003794 File Offset: 0x00001994
		public void WriteUShort(ushort value)
		{
			this.EnsureLength(2);
			byte[] data = this._data;
			int availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data[availableIndex] = (byte)value;
			byte[] data2 = this._data;
			availableIndex = this._availableIndex;
			this._availableIndex = availableIndex + 1;
			data2[availableIndex] = (byte)(value >> 8);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000037E0 File Offset: 0x000019E0
		public void WriteDouble(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			this.EnsureLength(bytes.Length);
			Buffer.BlockCopy(bytes, 0, this._data, this._availableIndex, bytes.Length);
			this._availableIndex += bytes.Length;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003823 File Offset: 0x00001A23
		public void AppendData(BinaryWriter writer)
		{
			this.EnsureLength(writer._availableIndex);
			Buffer.BlockCopy(writer._data, 0, this._data, this._availableIndex, writer._availableIndex);
			this._availableIndex += writer._availableIndex;
		}

		// Token: 0x04000054 RID: 84
		private byte[] _data;

		// Token: 0x04000055 RID: 85
		private int _availableIndex;
	}
}
