using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000607 RID: 1543
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterEventOptionsInternal : ISettable<RegisterEventOptions>, IDisposable
	{
		// Token: 0x17000BE2 RID: 3042
		// (set) Token: 0x060027A9 RID: 10153 RVA: 0x0003B1D3 File Offset: 0x000393D3
		public uint EventId
		{
			set
			{
				this.m_EventId = value;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x0003B1DD File Offset: 0x000393DD
		public Utf8String EventName
		{
			set
			{
				Helper.Set(value, ref this.m_EventName);
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (set) Token: 0x060027AB RID: 10155 RVA: 0x0003B1ED File Offset: 0x000393ED
		public AntiCheatCommonEventType EventType
		{
			set
			{
				this.m_EventType = value;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x0003B1F7 File Offset: 0x000393F7
		public RegisterEventParamDef[] ParamDefs
		{
			set
			{
				Helper.Set<RegisterEventParamDef, RegisterEventParamDefInternal>(ref value, ref this.m_ParamDefs, out this.m_ParamDefsCount);
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0003B20E File Offset: 0x0003940E
		public void Set(ref RegisterEventOptions other)
		{
			this.m_ApiVersion = 1;
			this.EventId = other.EventId;
			this.EventName = other.EventName;
			this.EventType = other.EventType;
			this.ParamDefs = other.ParamDefs;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x0003B24C File Offset: 0x0003944C
		public void Set(ref RegisterEventOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.EventId = other.Value.EventId;
				this.EventName = other.Value.EventName;
				this.EventType = other.Value.EventType;
				this.ParamDefs = other.Value.ParamDefs;
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0003B2C1 File Offset: 0x000394C1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_EventName);
			Helper.Dispose(ref this.m_ParamDefs);
		}

		// Token: 0x040011D2 RID: 4562
		private int m_ApiVersion;

		// Token: 0x040011D3 RID: 4563
		private uint m_EventId;

		// Token: 0x040011D4 RID: 4564
		private IntPtr m_EventName;

		// Token: 0x040011D5 RID: 4565
		private AntiCheatCommonEventType m_EventType;

		// Token: 0x040011D6 RID: 4566
		private uint m_ParamDefsCount;

		// Token: 0x040011D7 RID: 4567
		private IntPtr m_ParamDefs;
	}
}
