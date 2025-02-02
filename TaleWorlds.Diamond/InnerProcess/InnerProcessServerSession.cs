using System;
using System.Collections.Generic;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000055 RID: 85
	public class InnerProcessServerSession
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00005B3E File Offset: 0x00003D3E
		internal bool HasMessage
		{
			get
			{
				return this._messageTasks.Count > 0;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005B4E File Offset: 0x00003D4E
		public InnerProcessServerSession()
		{
			this._messageTasks = new Queue<InnerProcessMessageTask>();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005B61 File Offset: 0x00003D61
		public void SendMessage(Message message)
		{
			this._associatedClientSession.EnqueueMessage(message);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005B6F File Offset: 0x00003D6F
		internal void EnqueueMessageTask(InnerProcessMessageTask messageTask)
		{
			this._messageTasks.Enqueue(messageTask);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005B80 File Offset: 0x00003D80
		internal InnerProcessMessageTask DequeueMessage()
		{
			InnerProcessMessageTask result = null;
			if (this._messageTasks.Count > 0)
			{
				result = this._messageTasks.Dequeue();
			}
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005BAA File Offset: 0x00003DAA
		internal void HandleConnected(IInnerProcessClient clientSession)
		{
			this._associatedClientSession = clientSession;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005BB3 File Offset: 0x00003DB3
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00005BBB File Offset: 0x00003DBB
		public PeerId PeerId { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005BC4 File Offset: 0x00003DC4
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00005BCC File Offset: 0x00003DCC
		public SessionKey SessionKey { get; private set; }

		// Token: 0x060001FB RID: 507 RVA: 0x00005BD5 File Offset: 0x00003DD5
		public void AssignSession(PeerId peerId, SessionKey sessionKey)
		{
			this.PeerId = peerId;
			this.SessionKey = sessionKey;
		}

		// Token: 0x040000AF RID: 175
		private Queue<InnerProcessMessageTask> _messageTasks;

		// Token: 0x040000B0 RID: 176
		private IInnerProcessClient _associatedClientSession;
	}
}
