using System;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class TestAccessObject : AccessObject
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000344E File Offset: 0x0000164E
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003456 File Offset: 0x00001656
		[JsonProperty]
		public string UserName { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000345F File Offset: 0x0000165F
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003467 File Offset: 0x00001667
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00003470 File Offset: 0x00001670
		public TestAccessObject()
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003478 File Offset: 0x00001678
		public TestAccessObject(string userName, string password)
		{
			base.Type = "Test";
			this.UserName = userName;
			this.Password = password;
		}
	}
}
