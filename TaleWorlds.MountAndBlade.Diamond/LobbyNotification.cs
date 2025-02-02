using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public class LobbyNotification
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0000C317 File Offset: 0x0000A517
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0000C31F File Offset: 0x0000A51F
		public int Id { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000C328 File Offset: 0x0000A528
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0000C330 File Offset: 0x0000A530
		public NotificationType Type { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0000C339 File Offset: 0x0000A539
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0000C341 File Offset: 0x0000A541
		public DateTime Date { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000C34A File Offset: 0x0000A54A
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0000C352 File Offset: 0x0000A552
		public string Message { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0000C35B File Offset: 0x0000A55B
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0000C363 File Offset: 0x0000A563
		public Dictionary<string, string> Parameters { get; set; }

		// Token: 0x06000862 RID: 2146 RVA: 0x0000C36C File Offset: 0x0000A56C
		public LobbyNotification()
		{
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0000C374 File Offset: 0x0000A574
		public LobbyNotification(NotificationType type, DateTime date, string message)
		{
			this.Id = -1;
			this.Type = type;
			this.Date = date;
			this.Message = message;
			this.Parameters = new Dictionary<string, string>();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		public LobbyNotification(int id, NotificationType type, DateTime date, string message, string serializedParameters)
		{
			this.Id = id;
			this.Type = type;
			this.Date = date;
			this.Message = message;
			try
			{
				this.Parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedParameters);
			}
			catch (Exception)
			{
				this.Parameters = new Dictionary<string, string>();
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000C404 File Offset: 0x0000A604
		public string GetParametersAsString()
		{
			string result = "{}";
			try
			{
				result = JsonConvert.SerializeObject(this.Parameters, Formatting.None);
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000C43C File Offset: 0x0000A63C
		public TextObject GetTextObjectOfMessage()
		{
			TextObject result;
			if (!GameTexts.TryGetText(this.Message, out result, null))
			{
				result = new TextObject("{=!}" + this.Message, null);
			}
			return result;
		}

		// Token: 0x04000350 RID: 848
		public const string BadgeIdParameterName = "badge_id";

		// Token: 0x04000351 RID: 849
		public const string FriendRequesterParameterName = "friend_requester";
	}
}
