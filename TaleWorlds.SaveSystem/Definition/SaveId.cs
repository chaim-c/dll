using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000068 RID: 104
	public abstract class SaveId
	{
		// Token: 0x06000324 RID: 804
		public abstract string GetStringId();

		// Token: 0x06000325 RID: 805 RVA: 0x0000E434 File Offset: 0x0000C634
		public override int GetHashCode()
		{
			return this.GetStringId().GetHashCode();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E441 File Offset: 0x0000C641
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this.GetStringId() == ((SaveId)obj).GetStringId();
		}

		// Token: 0x06000327 RID: 807
		public abstract void WriteTo(IWriter writer);

		// Token: 0x06000328 RID: 808 RVA: 0x0000E474 File Offset: 0x0000C674
		public static SaveId ReadSaveIdFrom(IReader reader)
		{
			byte b = reader.ReadByte();
			SaveId result = null;
			if (b == 0)
			{
				result = TypeSaveId.ReadFrom(reader);
			}
			else if (b == 1)
			{
				result = GenericSaveId.ReadFrom(reader);
			}
			else if (b == 2)
			{
				result = ContainerSaveId.ReadFrom(reader);
			}
			return result;
		}
	}
}
