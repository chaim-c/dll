using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000058 RID: 88
	public class ContainerSaveId : SaveId
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public ContainerType ContainerType { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000AE01 File Offset: 0x00009001
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000AE09 File Offset: 0x00009009
		public SaveId KeyId { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000AE12 File Offset: 0x00009012
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000AE1A File Offset: 0x0000901A
		public SaveId ValueId { get; set; }

		// Token: 0x0600029F RID: 671 RVA: 0x0000AE23 File Offset: 0x00009023
		public ContainerSaveId(ContainerType containerType, SaveId elementId)
		{
			this.ContainerType = containerType;
			this.KeyId = elementId;
			this._stringId = this.CalculateStringId();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AE45 File Offset: 0x00009045
		public ContainerSaveId(ContainerType containerType, SaveId keyId, SaveId valueId)
		{
			this.ContainerType = containerType;
			this.KeyId = keyId;
			this.ValueId = valueId;
			this._stringId = this.CalculateStringId();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000AE70 File Offset: 0x00009070
		private string CalculateStringId()
		{
			string result;
			if (this.ContainerType == ContainerType.Dictionary)
			{
				string stringId = this.KeyId.GetStringId();
				string stringId2 = this.ValueId.GetStringId();
				result = string.Concat(new object[]
				{
					"C(",
					(int)this.ContainerType,
					")-(",
					stringId,
					",",
					stringId2,
					")"
				});
			}
			else
			{
				string stringId3 = this.KeyId.GetStringId();
				result = string.Concat(new object[]
				{
					"C(",
					(int)this.ContainerType,
					")-(",
					stringId3,
					")"
				});
			}
			return result;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000AF2B File Offset: 0x0000912B
		public override string GetStringId()
		{
			return this._stringId;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000AF33 File Offset: 0x00009133
		public override void WriteTo(IWriter writer)
		{
			writer.WriteByte(2);
			writer.WriteByte((byte)this.ContainerType);
			this.KeyId.WriteTo(writer);
			if (this.ContainerType == ContainerType.Dictionary)
			{
				this.ValueId.WriteTo(writer);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AF6C File Offset: 0x0000916C
		public static ContainerSaveId ReadFrom(IReader reader)
		{
			ContainerType containerType = (ContainerType)reader.ReadByte();
			int num = (containerType == ContainerType.Dictionary) ? 2 : 1;
			List<SaveId> list = new List<SaveId>();
			for (int i = 0; i < num; i++)
			{
				SaveId item = null;
				byte b = reader.ReadByte();
				if (b == 0)
				{
					item = TypeSaveId.ReadFrom(reader);
				}
				else if (b == 1)
				{
					item = GenericSaveId.ReadFrom(reader);
				}
				else if (b == 2)
				{
					item = ContainerSaveId.ReadFrom(reader);
				}
				list.Add(item);
			}
			SaveId keyId = list[0];
			SaveId valueId = (list.Count > 1) ? list[1] : null;
			return new ContainerSaveId(containerType, keyId, valueId);
		}

		// Token: 0x040000CA RID: 202
		private readonly string _stringId;
	}
}
