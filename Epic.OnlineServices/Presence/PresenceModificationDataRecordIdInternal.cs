using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000249 RID: 585
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationDataRecordIdInternal : IGettable<PresenceModificationDataRecordId>, ISettable<PresenceModificationDataRecordId>, IDisposable
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00018174 File Offset: 0x00016374
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x00018195 File Offset: 0x00016395
		public Utf8String Key
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Key, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000181A5 File Offset: 0x000163A5
		public void Set(ref PresenceModificationDataRecordId other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000181BC File Offset: 0x000163BC
		public void Set(ref PresenceModificationDataRecordId? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000181F2 File Offset: 0x000163F2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00018201 File Offset: 0x00016401
		public void Get(out PresenceModificationDataRecordId output)
		{
			output = default(PresenceModificationDataRecordId);
			output.Set(ref this);
		}

		// Token: 0x04000742 RID: 1858
		private int m_ApiVersion;

		// Token: 0x04000743 RID: 1859
		private IntPtr m_Key;
	}
}
