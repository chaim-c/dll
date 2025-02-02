using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005E9 RID: 1513
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct LogEventParamPairParamValueInternal : IGettable<LogEventParamPairParamValue>, ISettable<LogEventParamPairParamValue>, IDisposable
	{
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00039588 File Offset: 0x00037788
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x000395B0 File Offset: 0x000377B0
		public IntPtr? ClientHandle
		{
			get
			{
				IntPtr? result;
				Helper.Get<AntiCheatCommonEventParamType>(this.m_ClientHandle, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle);
				return result;
			}
			set
			{
				Helper.Set<IntPtr, AntiCheatCommonEventParamType>(value, ref this.m_ClientHandle, AntiCheatCommonEventParamType.ClientHandle, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000395D4 File Offset: 0x000377D4
		// (set) Token: 0x0600268A RID: 9866 RVA: 0x000395FC File Offset: 0x000377FC
		public Utf8String String
		{
			get
			{
				Utf8String result;
				Helper.Get<AntiCheatCommonEventParamType>(this.m_String, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.String);
				return result;
			}
			set
			{
				Helper.Set<AntiCheatCommonEventParamType>(value, ref this.m_String, AntiCheatCommonEventParamType.String, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x00039620 File Offset: 0x00037820
		// (set) Token: 0x0600268C RID: 9868 RVA: 0x00039648 File Offset: 0x00037848
		public uint? UInt32
		{
			get
			{
				uint? result;
				Helper.Get<uint, AntiCheatCommonEventParamType>(this.m_UInt32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt32);
				return result;
			}
			set
			{
				Helper.Set<uint, AntiCheatCommonEventParamType>(value, ref this.m_UInt32, AntiCheatCommonEventParamType.UInt32, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x0003966C File Offset: 0x0003786C
		// (set) Token: 0x0600268E RID: 9870 RVA: 0x00039694 File Offset: 0x00037894
		public int? Int32
		{
			get
			{
				int? result;
				Helper.Get<int, AntiCheatCommonEventParamType>(this.m_Int32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int32);
				return result;
			}
			set
			{
				Helper.Set<int, AntiCheatCommonEventParamType>(value, ref this.m_Int32, AntiCheatCommonEventParamType.Int32, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000396B8 File Offset: 0x000378B8
		// (set) Token: 0x06002690 RID: 9872 RVA: 0x000396E0 File Offset: 0x000378E0
		public ulong? UInt64
		{
			get
			{
				ulong? result;
				Helper.Get<ulong, AntiCheatCommonEventParamType>(this.m_UInt64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt64);
				return result;
			}
			set
			{
				Helper.Set<ulong, AntiCheatCommonEventParamType>(value, ref this.m_UInt64, AntiCheatCommonEventParamType.UInt64, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x00039704 File Offset: 0x00037904
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x0003972C File Offset: 0x0003792C
		public long? Int64
		{
			get
			{
				long? result;
				Helper.Get<long, AntiCheatCommonEventParamType>(this.m_Int64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int64);
				return result;
			}
			set
			{
				Helper.Set<long, AntiCheatCommonEventParamType>(value, ref this.m_Int64, AntiCheatCommonEventParamType.Int64, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x00039750 File Offset: 0x00037950
		// (set) Token: 0x06002694 RID: 9876 RVA: 0x00039778 File Offset: 0x00037978
		public Vec3f Vec3f
		{
			get
			{
				Vec3f result;
				Helper.Get<Vec3fInternal, Vec3f, AntiCheatCommonEventParamType>(ref this.m_Vec3f, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Vector3f);
				return result;
			}
			set
			{
				Helper.Set<Vec3f, AntiCheatCommonEventParamType, Vec3fInternal>(ref value, ref this.m_Vec3f, AntiCheatCommonEventParamType.Vector3f, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x0003979C File Offset: 0x0003799C
		// (set) Token: 0x06002696 RID: 9878 RVA: 0x000397C4 File Offset: 0x000379C4
		public Quat Quat
		{
			get
			{
				Quat result;
				Helper.Get<QuatInternal, Quat, AntiCheatCommonEventParamType>(ref this.m_Quat, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Quat);
				return result;
			}
			set
			{
				Helper.Set<Quat, AntiCheatCommonEventParamType, QuatInternal>(ref value, ref this.m_Quat, AntiCheatCommonEventParamType.Quat, ref this.m_ParamValueType, this);
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000397E8 File Offset: 0x000379E8
		public void Set(ref LogEventParamPairParamValue other)
		{
			this.ClientHandle = other.ClientHandle;
			this.String = other.String;
			this.UInt32 = other.UInt32;
			this.Int32 = other.Int32;
			this.UInt64 = other.UInt64;
			this.Int64 = other.Int64;
			this.Vec3f = other.Vec3f;
			this.Quat = other.Quat;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00039860 File Offset: 0x00037A60
		public void Set(ref LogEventParamPairParamValue? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientHandle = other.Value.ClientHandle;
				this.String = other.Value.String;
				this.UInt32 = other.Value.UInt32;
				this.Int32 = other.Value.Int32;
				this.UInt64 = other.Value.UInt64;
				this.Int64 = other.Value.Int64;
				this.Vec3f = other.Value.Vec3f;
				this.Quat = other.Value.Quat;
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00039928 File Offset: 0x00037B28
		public void Dispose()
		{
			Helper.Dispose<AntiCheatCommonEventParamType>(ref this.m_ClientHandle, this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle);
			Helper.Dispose<AntiCheatCommonEventParamType>(ref this.m_String, this.m_ParamValueType, AntiCheatCommonEventParamType.String);
			Helper.Dispose<Vec3fInternal>(ref this.m_Vec3f);
			Helper.Dispose<QuatInternal>(ref this.m_Quat);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x00039974 File Offset: 0x00037B74
		public void Get(out LogEventParamPairParamValue output)
		{
			output = default(LogEventParamPairParamValue);
			output.Set(ref this);
		}

		// Token: 0x04001141 RID: 4417
		[FieldOffset(0)]
		private AntiCheatCommonEventParamType m_ParamValueType;

		// Token: 0x04001142 RID: 4418
		[FieldOffset(8)]
		private IntPtr m_ClientHandle;

		// Token: 0x04001143 RID: 4419
		[FieldOffset(8)]
		private IntPtr m_String;

		// Token: 0x04001144 RID: 4420
		[FieldOffset(8)]
		private uint m_UInt32;

		// Token: 0x04001145 RID: 4421
		[FieldOffset(8)]
		private int m_Int32;

		// Token: 0x04001146 RID: 4422
		[FieldOffset(8)]
		private ulong m_UInt64;

		// Token: 0x04001147 RID: 4423
		[FieldOffset(8)]
		private long m_Int64;

		// Token: 0x04001148 RID: 4424
		[FieldOffset(8)]
		private Vec3fInternal m_Vec3f;

		// Token: 0x04001149 RID: 4425
		[FieldOffset(8)]
		private QuatInternal m_Quat;
	}
}
