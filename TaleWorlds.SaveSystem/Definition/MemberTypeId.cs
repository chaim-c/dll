using System;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000064 RID: 100
	public struct MemberTypeId
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000C355 File Offset: 0x0000A555
		public short SaveId
		{
			get
			{
				return (short)(this.TypeLevel << 8) + this.LocalSaveId;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C368 File Offset: 0x0000A568
		public static MemberTypeId Invalid
		{
			get
			{
				return new MemberTypeId(0, -1);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C374 File Offset: 0x0000A574
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.TypeLevel,
				",",
				this.LocalSaveId,
				")"
			});
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		public MemberTypeId(byte typeLevel, short localSaveId)
		{
			this.TypeLevel = typeLevel;
			this.LocalSaveId = localSaveId;
		}

		// Token: 0x040000E9 RID: 233
		public byte TypeLevel;

		// Token: 0x040000EA RID: 234
		public short LocalSaveId;
	}
}
