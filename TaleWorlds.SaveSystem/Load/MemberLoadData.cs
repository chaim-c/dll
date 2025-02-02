using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x0200003B RID: 59
	internal abstract class MemberLoadData : VariableLoadData
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009D7B File Offset: 0x00007F7B
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009D83 File Offset: 0x00007F83
		public ObjectLoadData ObjectLoadData { get; private set; }

		// Token: 0x06000217 RID: 535 RVA: 0x00009D8C File Offset: 0x00007F8C
		protected MemberLoadData(ObjectLoadData objectLoadData, IReader reader) : base(objectLoadData.Context, reader)
		{
			this.ObjectLoadData = objectLoadData;
		}
	}
}
