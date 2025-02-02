using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000060 RID: 96
	internal class GenericSaveId : SaveId
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000C15F File Offset: 0x0000A35F
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000C167 File Offset: 0x0000A367
		public SaveId BaseId { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000C170 File Offset: 0x0000A370
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000C178 File Offset: 0x0000A378
		public SaveId[] GenericTypeIDs { get; set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C181 File Offset: 0x0000A381
		public GenericSaveId(TypeSaveId baseId, SaveId[] saveIds)
		{
			this.BaseId = baseId;
			this.GenericTypeIDs = saveIds;
			this._stringId = this.CalculateStringId();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		private string CalculateStringId()
		{
			string text = "";
			for (int i = 0; i < this.GenericTypeIDs.Length; i++)
			{
				if (i != 0)
				{
					text += ",";
				}
				SaveId saveId = this.GenericTypeIDs[i];
				text += saveId.GetStringId();
			}
			return string.Concat(new string[]
			{
				"G(",
				this.BaseId.GetStringId(),
				")-(",
				text,
				")"
			});
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C224 File Offset: 0x0000A424
		public override string GetStringId()
		{
			return this._stringId;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C22C File Offset: 0x0000A42C
		public override void WriteTo(IWriter writer)
		{
			writer.WriteByte(1);
			this.BaseId.WriteTo(writer);
			writer.WriteByte((byte)this.GenericTypeIDs.Length);
			for (int i = 0; i < this.GenericTypeIDs.Length; i++)
			{
				this.GenericTypeIDs[i].WriteTo(writer);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C27C File Offset: 0x0000A47C
		public static GenericSaveId ReadFrom(IReader reader)
		{
			reader.ReadByte();
			TypeSaveId baseId = TypeSaveId.ReadFrom(reader);
			byte b = reader.ReadByte();
			List<SaveId> list = new List<SaveId>();
			for (int i = 0; i < (int)b; i++)
			{
				SaveId item = null;
				byte b2 = reader.ReadByte();
				if (b2 == 0)
				{
					item = TypeSaveId.ReadFrom(reader);
				}
				else if (b2 == 1)
				{
					item = GenericSaveId.ReadFrom(reader);
				}
				else if (b2 == 2)
				{
					item = ContainerSaveId.ReadFrom(reader);
				}
				list.Add(item);
			}
			return new GenericSaveId(baseId, list.ToArray());
		}

		// Token: 0x040000E6 RID: 230
		private readonly string _stringId;
	}
}
