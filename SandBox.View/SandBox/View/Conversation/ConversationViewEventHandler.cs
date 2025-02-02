using System;

namespace SandBox.View.Conversation
{
	// Token: 0x02000058 RID: 88
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class ConversationViewEventHandler : Attribute
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0002259B File Offset: 0x0002079B
		public string Id { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x000225A3 File Offset: 0x000207A3
		public ConversationViewEventHandler.EventType Type { get; }

		// Token: 0x06000409 RID: 1033 RVA: 0x000225AB File Offset: 0x000207AB
		public ConversationViewEventHandler(string id, ConversationViewEventHandler.EventType type)
		{
			this.Id = id;
			this.Type = type;
		}

		// Token: 0x0200009A RID: 154
		public enum EventType
		{
			// Token: 0x0400035A RID: 858
			OnCondition,
			// Token: 0x0400035B RID: 859
			OnConsequence
		}
	}
}
