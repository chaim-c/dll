using System;
using System.Globalization;

namespace TaleWorlds.Library
{
	// Token: 0x0200008A RID: 138
	public class StringReader : IReader
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000FE11 File Offset: 0x0000E011
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000FE19 File Offset: 0x0000E019
		public string Data { get; private set; }

		// Token: 0x060004AB RID: 1195 RVA: 0x0000FE22 File Offset: 0x0000E022
		private string GetNextToken()
		{
			string result = this._tokens[this._currentIndex];
			this._currentIndex++;
			return result;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000FE3F File Offset: 0x0000E03F
		public StringReader(string data)
		{
			this.Data = data;
			this._tokens = data.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000FE66 File Offset: 0x0000E066
		public ISerializableObject ReadSerializableObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000FE6D File Offset: 0x0000E06D
		public int ReadInt()
		{
			return Convert.ToInt32(this.GetNextToken());
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000FE7A File Offset: 0x0000E07A
		public short ReadShort()
		{
			return Convert.ToInt16(this.GetNextToken());
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000FE88 File Offset: 0x0000E088
		public string ReadString()
		{
			int num = this.ReadInt();
			int i = 0;
			string text = "";
			while (i < num)
			{
				string nextToken = this.GetNextToken();
				text += nextToken;
				i = text.Length;
				if (i < num)
				{
					text += " ";
				}
			}
			if (text.Length != num)
			{
				throw new Exception("invalid string format, length does not match");
			}
			return text;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public Color ReadColor()
		{
			float red = this.ReadFloat();
			float green = this.ReadFloat();
			float blue = this.ReadFloat();
			float alpha = this.ReadFloat();
			return new Color(red, green, blue, alpha);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000FF1C File Offset: 0x0000E11C
		public bool ReadBool()
		{
			string nextToken = this.GetNextToken();
			return nextToken == "1" || (!(nextToken == "0") && Convert.ToBoolean(nextToken));
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000FF54 File Offset: 0x0000E154
		public float ReadFloat()
		{
			return Convert.ToSingle(this.GetNextToken(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000FF66 File Offset: 0x0000E166
		public uint ReadUInt()
		{
			return Convert.ToUInt32(this.GetNextToken());
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000FF73 File Offset: 0x0000E173
		public ulong ReadULong()
		{
			return Convert.ToUInt64(this.GetNextToken());
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000FF80 File Offset: 0x0000E180
		public long ReadLong()
		{
			return Convert.ToInt64(this.GetNextToken());
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000FF8D File Offset: 0x0000E18D
		public byte ReadByte()
		{
			return Convert.ToByte(this.GetNextToken());
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000FF9A File Offset: 0x0000E19A
		public byte[] ReadBytes(int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		public Vec2 ReadVec2()
		{
			float a = this.ReadFloat();
			float b = this.ReadFloat();
			return new Vec2(a, b);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public Vec3 ReadVec3()
		{
			float x = this.ReadFloat();
			float y = this.ReadFloat();
			float z = this.ReadFloat();
			float w = this.ReadFloat();
			return new Vec3(x, y, z, w);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		public Vec3i ReadVec3Int()
		{
			int x = this.ReadInt();
			int y = this.ReadInt();
			int z = this.ReadInt();
			return new Vec3i(x, y, z);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001001C File Offset: 0x0000E21C
		public sbyte ReadSByte()
		{
			return Convert.ToSByte(this.GetNextToken());
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00010029 File Offset: 0x0000E229
		public ushort ReadUShort()
		{
			return Convert.ToUInt16(this.GetNextToken());
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00010036 File Offset: 0x0000E236
		public double ReadDouble()
		{
			return Convert.ToDouble(this.GetNextToken());
		}

		// Token: 0x04000169 RID: 361
		private string[] _tokens;

		// Token: 0x0400016A RID: 362
		private int _currentIndex;
	}
}
