using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public class ServerNotification
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0000F44B File Offset: 0x0000D64B
		public ServerNotificationType Type { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0000F453 File Offset: 0x0000D653
		public string Message { get; }

		// Token: 0x060009CB RID: 2507 RVA: 0x0000F45B File Offset: 0x0000D65B
		public ServerNotification(ServerNotificationType type, string message)
		{
			this.Type = type;
			this.Message = message;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000F474 File Offset: 0x0000D674
		public TextObject GetTextObjectOfMessage()
		{
			TextObject result;
			if (!GameTexts.TryGetText(this.Message, out result, null))
			{
				result = new TextObject("{=!}" + this.Message, null);
			}
			return result;
		}
	}
}
