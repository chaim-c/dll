using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005E8 RID: 1512
	public struct LogEventParamPairParamValue
	{
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00039178 File Offset: 0x00037378
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x00039190 File Offset: 0x00037390
		public AntiCheatCommonEventParamType ParamValueType
		{
			get
			{
				return this.m_ParamValueType;
			}
			private set
			{
				this.m_ParamValueType = value;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x0003919C File Offset: 0x0003739C
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x000391C4 File Offset: 0x000373C4
		public IntPtr? ClientHandle
		{
			get
			{
				IntPtr? result;
				Helper.Get<IntPtr?, AntiCheatCommonEventParamType>(this.m_ClientHandle, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle);
				return result;
			}
			set
			{
				Helper.Set<IntPtr?, AntiCheatCommonEventParamType>(value, ref this.m_ClientHandle, AntiCheatCommonEventParamType.ClientHandle, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x000391DC File Offset: 0x000373DC
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x00039204 File Offset: 0x00037404
		public Utf8String String
		{
			get
			{
				Utf8String result;
				Helper.Get<Utf8String, AntiCheatCommonEventParamType>(this.m_String, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.String);
				return result;
			}
			set
			{
				Helper.Set<Utf8String, AntiCheatCommonEventParamType>(value, ref this.m_String, AntiCheatCommonEventParamType.String, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0003921C File Offset: 0x0003741C
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x00039244 File Offset: 0x00037444
		public uint? UInt32
		{
			get
			{
				uint? result;
				Helper.Get<uint?, AntiCheatCommonEventParamType>(this.m_UInt32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt32);
				return result;
			}
			set
			{
				Helper.Set<uint?, AntiCheatCommonEventParamType>(value, ref this.m_UInt32, AntiCheatCommonEventParamType.UInt32, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x0003925C File Offset: 0x0003745C
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x00039284 File Offset: 0x00037484
		public int? Int32
		{
			get
			{
				int? result;
				Helper.Get<int?, AntiCheatCommonEventParamType>(this.m_Int32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int32);
				return result;
			}
			set
			{
				Helper.Set<int?, AntiCheatCommonEventParamType>(value, ref this.m_Int32, AntiCheatCommonEventParamType.Int32, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x0003929C File Offset: 0x0003749C
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000392C4 File Offset: 0x000374C4
		public ulong? UInt64
		{
			get
			{
				ulong? result;
				Helper.Get<ulong?, AntiCheatCommonEventParamType>(this.m_UInt64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt64);
				return result;
			}
			set
			{
				Helper.Set<ulong?, AntiCheatCommonEventParamType>(value, ref this.m_UInt64, AntiCheatCommonEventParamType.UInt64, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000392DC File Offset: 0x000374DC
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x00039304 File Offset: 0x00037504
		public long? Int64
		{
			get
			{
				long? result;
				Helper.Get<long?, AntiCheatCommonEventParamType>(this.m_Int64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int64);
				return result;
			}
			set
			{
				Helper.Set<long?, AntiCheatCommonEventParamType>(value, ref this.m_Int64, AntiCheatCommonEventParamType.Int64, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x0003931C File Offset: 0x0003751C
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x00039344 File Offset: 0x00037544
		public Vec3f Vec3f
		{
			get
			{
				Vec3f result;
				Helper.Get<Vec3f, AntiCheatCommonEventParamType>(this.m_Vec3f, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Vector3f);
				return result;
			}
			set
			{
				Helper.Set<Vec3f, AntiCheatCommonEventParamType>(value, ref this.m_Vec3f, AntiCheatCommonEventParamType.Vector3f, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x0003935C File Offset: 0x0003755C
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x00039384 File Offset: 0x00037584
		public Quat Quat
		{
			get
			{
				Quat result;
				Helper.Get<Quat, AntiCheatCommonEventParamType>(this.m_Quat, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Quat);
				return result;
			}
			set
			{
				Helper.Set<Quat, AntiCheatCommonEventParamType>(value, ref this.m_Quat, AntiCheatCommonEventParamType.Quat, ref this.m_ParamValueType, null);
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0003939C File Offset: 0x0003759C
		public static implicit operator LogEventParamPairParamValue(IntPtr value)
		{
			return new LogEventParamPairParamValue
			{
				ClientHandle = new IntPtr?(value)
			};
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000393C8 File Offset: 0x000375C8
		public static implicit operator LogEventParamPairParamValue(Utf8String value)
		{
			return new LogEventParamPairParamValue
			{
				String = value
			};
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000393EC File Offset: 0x000375EC
		public static implicit operator LogEventParamPairParamValue(string value)
		{
			return new LogEventParamPairParamValue
			{
				String = value
			};
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00039418 File Offset: 0x00037618
		public static implicit operator LogEventParamPairParamValue(uint value)
		{
			return new LogEventParamPairParamValue
			{
				UInt32 = new uint?(value)
			};
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00039444 File Offset: 0x00037644
		public static implicit operator LogEventParamPairParamValue(int value)
		{
			return new LogEventParamPairParamValue
			{
				Int32 = new int?(value)
			};
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00039470 File Offset: 0x00037670
		public static implicit operator LogEventParamPairParamValue(ulong value)
		{
			return new LogEventParamPairParamValue
			{
				UInt64 = new ulong?(value)
			};
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0003949C File Offset: 0x0003769C
		public static implicit operator LogEventParamPairParamValue(long value)
		{
			return new LogEventParamPairParamValue
			{
				Int64 = new long?(value)
			};
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000394C8 File Offset: 0x000376C8
		public static implicit operator LogEventParamPairParamValue(Vec3f value)
		{
			return new LogEventParamPairParamValue
			{
				Vec3f = value
			};
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000394EC File Offset: 0x000376EC
		public static implicit operator LogEventParamPairParamValue(Quat value)
		{
			return new LogEventParamPairParamValue
			{
				Quat = value
			};
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00039510 File Offset: 0x00037710
		internal void Set(ref LogEventParamPairParamValueInternal other)
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

		// Token: 0x04001138 RID: 4408
		private AntiCheatCommonEventParamType m_ParamValueType;

		// Token: 0x04001139 RID: 4409
		private IntPtr? m_ClientHandle;

		// Token: 0x0400113A RID: 4410
		private Utf8String m_String;

		// Token: 0x0400113B RID: 4411
		private uint? m_UInt32;

		// Token: 0x0400113C RID: 4412
		private int? m_Int32;

		// Token: 0x0400113D RID: 4413
		private ulong? m_UInt64;

		// Token: 0x0400113E RID: 4414
		private long? m_Int64;

		// Token: 0x0400113F RID: 4415
		private Vec3f m_Vec3f;

		// Token: 0x04001140 RID: 4416
		private Quat m_Quat;
	}
}
