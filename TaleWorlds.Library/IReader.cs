using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000041 RID: 65
	public interface IReader
	{
		// Token: 0x06000210 RID: 528
		ISerializableObject ReadSerializableObject();

		// Token: 0x06000211 RID: 529
		int ReadInt();

		// Token: 0x06000212 RID: 530
		short ReadShort();

		// Token: 0x06000213 RID: 531
		string ReadString();

		// Token: 0x06000214 RID: 532
		Color ReadColor();

		// Token: 0x06000215 RID: 533
		bool ReadBool();

		// Token: 0x06000216 RID: 534
		float ReadFloat();

		// Token: 0x06000217 RID: 535
		uint ReadUInt();

		// Token: 0x06000218 RID: 536
		ulong ReadULong();

		// Token: 0x06000219 RID: 537
		long ReadLong();

		// Token: 0x0600021A RID: 538
		byte ReadByte();

		// Token: 0x0600021B RID: 539
		byte[] ReadBytes(int length);

		// Token: 0x0600021C RID: 540
		Vec2 ReadVec2();

		// Token: 0x0600021D RID: 541
		Vec3 ReadVec3();

		// Token: 0x0600021E RID: 542
		Vec3i ReadVec3Int();

		// Token: 0x0600021F RID: 543
		sbyte ReadSByte();

		// Token: 0x06000220 RID: 544
		ushort ReadUShort();

		// Token: 0x06000221 RID: 545
		double ReadDouble();
	}
}
