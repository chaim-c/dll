using System;
using System.Text;

namespace TaleWorlds.Library
{
	// Token: 0x0200008B RID: 139
	public class StringWriter : IWriter
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00010043 File Offset: 0x0000E243
		public string Data
		{
			get
			{
				return this._stringBuilder.ToString();
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00010050 File Offset: 0x0000E250
		public StringWriter()
		{
			this._stringBuilder = new StringBuilder();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00010063 File Offset: 0x0000E263
		private void AddToken(string token)
		{
			this._stringBuilder.Append(token);
			this._stringBuilder.Append(" ");
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00010083 File Offset: 0x0000E283
		public void WriteSerializableObject(ISerializableObject serializableObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001008A File Offset: 0x0000E28A
		public void WriteByte(byte value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00010098 File Offset: 0x0000E298
		public void WriteBytes(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001009F File Offset: 0x0000E29F
		public void WriteInt(int value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000100AD File Offset: 0x0000E2AD
		public void WriteShort(short value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000100BB File Offset: 0x0000E2BB
		public void WriteString(string value)
		{
			this.WriteInt(value.Length);
			this.AddToken(value);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public void WriteColor(Color value)
		{
			this.WriteFloat(value.Red);
			this.WriteFloat(value.Green);
			this.WriteFloat(value.Blue);
			this.WriteFloat(value.Alpha);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00010102 File Offset: 0x0000E302
		public void WriteBool(bool value)
		{
			this.AddToken(value ? "1" : "0");
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00010119 File Offset: 0x0000E319
		public void WriteFloat(float value)
		{
			this.AddToken((value == 0f) ? "0" : Convert.ToString(value));
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00010136 File Offset: 0x0000E336
		public void WriteUInt(uint value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00010144 File Offset: 0x0000E344
		public void WriteULong(ulong value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00010152 File Offset: 0x0000E352
		public void WriteLong(long value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00010160 File Offset: 0x0000E360
		public void WriteVec2(Vec2 vec2)
		{
			this.WriteFloat(vec2.x);
			this.WriteFloat(vec2.y);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001017A File Offset: 0x0000E37A
		public void WriteVec3(Vec3 vec3)
		{
			this.WriteFloat(vec3.x);
			this.WriteFloat(vec3.y);
			this.WriteFloat(vec3.z);
			this.WriteFloat(vec3.w);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000101AC File Offset: 0x0000E3AC
		public void WriteVec3Int(Vec3i vec3)
		{
			this.WriteInt(vec3.X);
			this.WriteInt(vec3.Y);
			this.WriteInt(vec3.Z);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000101D2 File Offset: 0x0000E3D2
		public void WriteSByte(sbyte value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000101E0 File Offset: 0x0000E3E0
		public void WriteUShort(ushort value)
		{
			this.AddToken(Convert.ToString(value));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000101EE File Offset: 0x0000E3EE
		public void WriteDouble(double value)
		{
			this.AddToken((value == 0.0) ? "0" : Convert.ToString(value));
		}

		// Token: 0x0400016B RID: 363
		private StringBuilder _stringBuilder;
	}
}
