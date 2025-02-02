using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028E RID: 654
	public struct MissionTime : IComparable<MissionTime>
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0007D050 File Offset: 0x0007B250
		public long NumberOfTicks
		{
			get
			{
				return this._numberOfTicks;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0007D058 File Offset: 0x0007B258
		public MissionTime(long numberOfTicks)
		{
			this._numberOfTicks = numberOfTicks;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x0007D061 File Offset: 0x0007B261
		private static long CurrentNumberOfTicks
		{
			get
			{
				return Mission.Current.MissionTimeTracker.NumberOfTicks;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x0007D072 File Offset: 0x0007B272
		public static MissionTime DeltaTime
		{
			get
			{
				return new MissionTime(Mission.Current.MissionTimeTracker.DeltaTimeInTicks);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0007D088 File Offset: 0x0007B288
		private static long DeltaTimeInTicks
		{
			get
			{
				return Mission.Current.MissionTimeTracker.DeltaTimeInTicks;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0007D099 File Offset: 0x0007B299
		public static MissionTime Now
		{
			get
			{
				return new MissionTime(Mission.Current.MissionTimeTracker.NumberOfTicks);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0007D0AF File Offset: 0x0007B2AF
		public bool IsFuture
		{
			get
			{
				return MissionTime.CurrentNumberOfTicks < this._numberOfTicks;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x0007D0BE File Offset: 0x0007B2BE
		public bool IsPast
		{
			get
			{
				return MissionTime.CurrentNumberOfTicks > this._numberOfTicks;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x0007D0CD File Offset: 0x0007B2CD
		public bool IsNow
		{
			get
			{
				return MissionTime.CurrentNumberOfTicks == this._numberOfTicks;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0007D0DC File Offset: 0x0007B2DC
		public float ElapsedHours
		{
			get
			{
				return (float)(MissionTime.CurrentNumberOfTicks - this._numberOfTicks) / 3.6E+10f;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x0007D0F1 File Offset: 0x0007B2F1
		public float ElapsedSeconds
		{
			get
			{
				return (float)(MissionTime.CurrentNumberOfTicks - this._numberOfTicks) * 1E-07f;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x0007D106 File Offset: 0x0007B306
		public float ElapsedMilliseconds
		{
			get
			{
				return (float)(MissionTime.CurrentNumberOfTicks - this._numberOfTicks) / 10000f;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0007D11B File Offset: 0x0007B31B
		public double ToHours
		{
			get
			{
				return (double)this._numberOfTicks / 36000000000.0;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0007D12E File Offset: 0x0007B32E
		public double ToMinutes
		{
			get
			{
				return (double)this._numberOfTicks / 600000000.0;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0007D141 File Offset: 0x0007B341
		public double ToSeconds
		{
			get
			{
				return (double)this._numberOfTicks * 1.0000000116860974E-07;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0007D154 File Offset: 0x0007B354
		public double ToMilliseconds
		{
			get
			{
				return (double)this._numberOfTicks / 10000.0;
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0007D167 File Offset: 0x0007B367
		public static MissionTime MillisecondsFromNow(float valueInMilliseconds)
		{
			return new MissionTime((long)(valueInMilliseconds * 10000f + (float)MissionTime.CurrentNumberOfTicks));
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0007D17D File Offset: 0x0007B37D
		public static MissionTime SecondsFromNow(float valueInSeconds)
		{
			return new MissionTime((long)(valueInSeconds * 10000000f + (float)MissionTime.CurrentNumberOfTicks));
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0007D193 File Offset: 0x0007B393
		public bool Equals(MissionTime other)
		{
			return this._numberOfTicks == other._numberOfTicks;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0007D1A3 File Offset: 0x0007B3A3
		public override bool Equals(object obj)
		{
			return obj != null && obj is MissionTime && this.Equals((MissionTime)obj);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0007D1C0 File Offset: 0x0007B3C0
		public override int GetHashCode()
		{
			return this._numberOfTicks.GetHashCode();
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0007D1DB File Offset: 0x0007B3DB
		public int CompareTo(MissionTime other)
		{
			if (this._numberOfTicks == other._numberOfTicks)
			{
				return 0;
			}
			if (this._numberOfTicks > other._numberOfTicks)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0007D1FE File Offset: 0x0007B3FE
		public static bool operator <(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks < y._numberOfTicks;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0007D20E File Offset: 0x0007B40E
		public static bool operator >(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks > y._numberOfTicks;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0007D21E File Offset: 0x0007B41E
		public static bool operator ==(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks == y._numberOfTicks;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0007D22E File Offset: 0x0007B42E
		public static bool operator !=(MissionTime x, MissionTime y)
		{
			return !(x == y);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0007D23A File Offset: 0x0007B43A
		public static bool operator <=(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks <= y._numberOfTicks;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0007D24D File Offset: 0x0007B44D
		public static bool operator >=(MissionTime x, MissionTime y)
		{
			return x._numberOfTicks >= y._numberOfTicks;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0007D260 File Offset: 0x0007B460
		public static MissionTime Milliseconds(float valueInMilliseconds)
		{
			return new MissionTime((long)(valueInMilliseconds * 10000f));
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0007D26F File Offset: 0x0007B46F
		public static MissionTime Seconds(float valueInSeconds)
		{
			return new MissionTime((long)(valueInSeconds * 10000000f));
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0007D27E File Offset: 0x0007B47E
		public static MissionTime Minutes(float valueInMinutes)
		{
			return new MissionTime((long)(valueInMinutes * 600000000f));
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0007D28D File Offset: 0x0007B48D
		public static MissionTime Hours(float valueInHours)
		{
			return new MissionTime((long)(valueInHours * 3.6E+10f));
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0007D29C File Offset: 0x0007B49C
		public static MissionTime Zero
		{
			get
			{
				return new MissionTime(0L);
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0007D2A5 File Offset: 0x0007B4A5
		public static MissionTime operator +(MissionTime g1, MissionTime g2)
		{
			return new MissionTime(g1._numberOfTicks + g2._numberOfTicks);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0007D2B9 File Offset: 0x0007B4B9
		public static MissionTime operator -(MissionTime g1, MissionTime g2)
		{
			return new MissionTime(g1._numberOfTicks - g2._numberOfTicks);
		}

		// Token: 0x04000CCB RID: 3275
		public const long TimeTicksPerMilliSecond = 10000L;

		// Token: 0x04000CCC RID: 3276
		public const long TimeTicksPerSecond = 10000000L;

		// Token: 0x04000CCD RID: 3277
		public const long TimeTicksPerMinute = 600000000L;

		// Token: 0x04000CCE RID: 3278
		public const long TimeTicksPerHour = 36000000000L;

		// Token: 0x04000CCF RID: 3279
		public const float InvTimeTicksPerSecond = 1E-07f;

		// Token: 0x04000CD0 RID: 3280
		private readonly long _numberOfTicks;
	}
}
