using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200006D RID: 109
	public class TypeSaveId : SaveId
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000EB1A File Offset: 0x0000CD1A
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000EB22 File Offset: 0x0000CD22
		public int Id { get; private set; }

		// Token: 0x06000354 RID: 852 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public TypeSaveId(int id)
		{
			this.Id = id;
			this._stringId = this.Id.ToString();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000EB5A File Offset: 0x0000CD5A
		public override string GetStringId()
		{
			return this._stringId;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000EB62 File Offset: 0x0000CD62
		public override void WriteTo(IWriter writer)
		{
			writer.WriteByte(0);
			writer.WriteInt(this.Id);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000EB77 File Offset: 0x0000CD77
		public static TypeSaveId ReadFrom(IReader reader)
		{
			return new TypeSaveId(reader.ReadInt());
		}

		// Token: 0x0400010F RID: 271
		private readonly string _stringId;
	}
}
