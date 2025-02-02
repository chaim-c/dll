using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000042 RID: 66
	public interface IWriter
	{
		// Token: 0x06000222 RID: 546
		void WriteSerializableObject(ISerializableObject serializableObject);

		// Token: 0x06000223 RID: 547
		void WriteByte(byte value);

		// Token: 0x06000224 RID: 548
		void WriteSByte(sbyte value);

		// Token: 0x06000225 RID: 549
		void WriteBytes(byte[] bytes);

		// Token: 0x06000226 RID: 550
		void WriteInt(int value);

		// Token: 0x06000227 RID: 551
		void WriteUInt(uint value);

		// Token: 0x06000228 RID: 552
		void WriteShort(short value);

		// Token: 0x06000229 RID: 553
		void WriteUShort(ushort value);

		// Token: 0x0600022A RID: 554
		void WriteString(string value);

		// Token: 0x0600022B RID: 555
		void WriteColor(Color value);

		// Token: 0x0600022C RID: 556
		void WriteBool(bool value);

		// Token: 0x0600022D RID: 557
		void WriteFloat(float value);

		// Token: 0x0600022E RID: 558
		void WriteDouble(double value);

		// Token: 0x0600022F RID: 559
		void WriteULong(ulong value);

		// Token: 0x06000230 RID: 560
		void WriteLong(long value);

		// Token: 0x06000231 RID: 561
		void WriteVec2(Vec2 vec2);

		// Token: 0x06000232 RID: 562
		void WriteVec3(Vec3 vec3);

		// Token: 0x06000233 RID: 563
		void WriteVec3Int(Vec3i vec3);
	}
}
