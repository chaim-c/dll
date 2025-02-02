using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public struct CustomBattleId
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00007922 File Offset: 0x00005B22
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000792A File Offset: 0x00005B2A
		public Guid Guid { get; private set; }

		// Token: 0x060005F3 RID: 1523 RVA: 0x00007933 File Offset: 0x00005B33
		public CustomBattleId(Guid guid)
		{
			this.Guid = guid;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000793C File Offset: 0x00005B3C
		public static CustomBattleId NewGuid()
		{
			return new CustomBattleId(Guid.NewGuid());
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00007948 File Offset: 0x00005B48
		public override string ToString()
		{
			return this.Guid.ToString();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000796C File Offset: 0x00005B6C
		public byte[] ToByteArray()
		{
			return this.Guid.ToByteArray();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00007987 File Offset: 0x00005B87
		public static bool operator ==(CustomBattleId a, CustomBattleId b)
		{
			return a.Guid == b.Guid;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000799C File Offset: 0x00005B9C
		public static bool operator !=(CustomBattleId a, CustomBattleId b)
		{
			return a.Guid != b.Guid;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000079B4 File Offset: 0x00005BB4
		public override bool Equals(object o)
		{
			if (o != null && o is CustomBattleId)
			{
				CustomBattleId customBattleId = (CustomBattleId)o;
				return this.Guid.Equals(customBattleId.Guid);
			}
			return false;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000079EC File Offset: 0x00005BEC
		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}
	}
}
