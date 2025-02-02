using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200004E RID: 78
	[DataContract]
	[Serializable]
	public sealed class RestResponse : RestData
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000055E5 File Offset: 0x000037E5
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000055ED File Offset: 0x000037ED
		[DataMember]
		public bool Successful { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000055F6 File Offset: 0x000037F6
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000055FE File Offset: 0x000037FE
		[DataMember]
		public string SuccessfulReason { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005607 File Offset: 0x00003807
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000560F File Offset: 0x0000380F
		[DataMember]
		public RestFunctionResult FunctionResult { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005618 File Offset: 0x00003818
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00005620 File Offset: 0x00003820
		[DataMember]
		public byte[] UserCertificate { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005629 File Offset: 0x00003829
		public int RemainingMessageCount
		{
			get
			{
				if (this._responseMessages != null)
				{
					return this._responseMessages.Count;
				}
				return 0;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005640 File Offset: 0x00003840
		public RestResponse()
		{
			this._responseMessages = new List<RestResponseMessage>();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005653 File Offset: 0x00003853
		public void SetSuccessful(bool successful, string successfulReason)
		{
			this.Successful = successful;
			this.SuccessfulReason = successfulReason;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005663 File Offset: 0x00003863
		public RestResponseMessage TryDequeueMessage()
		{
			if (this._responseMessages != null && this._responseMessages.Count > 0)
			{
				RestResponseMessage result = this._responseMessages[0];
				this._responseMessages.RemoveAt(0);
				return result;
			}
			return null;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005695 File Offset: 0x00003895
		public void ClearMessageQueue()
		{
			this._responseMessages.Clear();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000056A2 File Offset: 0x000038A2
		public void EnqueueMessage(RestResponseMessage message)
		{
			this._responseMessages.Add(message);
		}

		// Token: 0x040000A0 RID: 160
		[DataMember]
		private List<RestResponseMessage> _responseMessages;
	}
}
