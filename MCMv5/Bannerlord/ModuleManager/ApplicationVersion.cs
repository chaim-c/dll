using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000131 RID: 305
	[NullableContext(1)]
	[Nullable(0)]
	internal class ApplicationVersion : IComparable<ApplicationVersion>, IEquatable<ApplicationVersion>
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00018E14 File Offset: 0x00017014
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ApplicationVersion);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00018E20 File Offset: 0x00017020
		public static ApplicationVersion Empty { get; } = new ApplicationVersion();

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00018E27 File Offset: 0x00017027
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x00018E2F File Offset: 0x0001702F
		public ApplicationVersionType ApplicationVersionType { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00018E38 File Offset: 0x00017038
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00018E40 File Offset: 0x00017040
		public int Major { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00018E49 File Offset: 0x00017049
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00018E51 File Offset: 0x00017051
		public int Minor { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00018E5A File Offset: 0x0001705A
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00018E62 File Offset: 0x00017062
		public int Revision { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00018E6B File Offset: 0x0001706B
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00018E73 File Offset: 0x00017073
		public int ChangeSet { get; set; }

		// Token: 0x060007BD RID: 1981 RVA: 0x00018E7C File Offset: 0x0001707C
		public ApplicationVersion()
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00018E86 File Offset: 0x00017086
		public ApplicationVersion(ApplicationVersionType applicationVersionType, int major, int minor, int revision, int changeSet)
		{
			this.ApplicationVersionType = applicationVersionType;
			this.Major = major;
			this.Minor = minor;
			this.Revision = revision;
			this.ChangeSet = changeSet;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00018EBC File Offset: 0x000170BC
		[NullableContext(2)]
		public bool IsSame(ApplicationVersion other)
		{
			int major = this.Major;
			int? num = (other != null) ? new int?(other.Major) : null;
			return (major == num.GetValueOrDefault() & num != null) && this.Minor == other.Minor && this.Revision == other.Revision;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00018F1C File Offset: 0x0001711C
		[NullableContext(2)]
		public bool IsSameWithChangeSet(ApplicationVersion other)
		{
			int major = this.Major;
			int? num = (other != null) ? new int?(other.Major) : null;
			return (major == num.GetValueOrDefault() & num != null) && this.Minor == other.Minor && this.Revision == other.Revision && this.ChangeSet == other.ChangeSet;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00018F8C File Offset: 0x0001718C
		public override string ToString()
		{
			return string.Format("{0}{1}.{2}.{3}.{4}", new object[]
			{
				ApplicationVersion.GetPrefix(this.ApplicationVersionType),
				this.Major,
				this.Minor,
				this.Revision,
				this.ChangeSet
			});
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00018FF4 File Offset: 0x000171F4
		[NullableContext(2)]
		public int CompareTo(ApplicationVersion other)
		{
			return ApplicationVersionComparer.CompareStandard(this, other);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00018FFD File Offset: 0x000171FD
		public static bool operator <(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019009 File Offset: 0x00017209
		public static bool operator >(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019015 File Offset: 0x00017215
		public static bool operator <=(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00019024 File Offset: 0x00017224
		public static bool operator >=(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00019034 File Offset: 0x00017234
		public static char GetPrefix(ApplicationVersionType applicationVersionType)
		{
			if (!true)
			{
			}
			char result;
			switch (applicationVersionType)
			{
			case ApplicationVersionType.Alpha:
				result = 'a';
				break;
			case ApplicationVersionType.Beta:
				result = 'b';
				break;
			case ApplicationVersionType.EarlyAccess:
				result = 'e';
				break;
			case ApplicationVersionType.Release:
				result = 'v';
				break;
			case ApplicationVersionType.Development:
				result = 'd';
				break;
			default:
				result = 'i';
				break;
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00019084 File Offset: 0x00017284
		public static ApplicationVersionType FromPrefix(char applicationVersionType)
		{
			if (!true)
			{
			}
			ApplicationVersionType result;
			switch (applicationVersionType)
			{
			case 'a':
				result = ApplicationVersionType.Alpha;
				goto IL_42;
			case 'b':
				result = ApplicationVersionType.Beta;
				goto IL_42;
			case 'c':
				break;
			case 'd':
				result = ApplicationVersionType.Development;
				goto IL_42;
			case 'e':
				result = ApplicationVersionType.EarlyAccess;
				goto IL_42;
			default:
				if (applicationVersionType == 'v')
				{
					result = ApplicationVersionType.Release;
					goto IL_42;
				}
				break;
			}
			result = ApplicationVersionType.Invalid;
			IL_42:
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000190D8 File Offset: 0x000172D8
		public static bool TryParse([Nullable(2)] string versionAsString, out ApplicationVersion version)
		{
			return ApplicationVersion.TryParse(versionAsString, out version, true);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000190E4 File Offset: 0x000172E4
		public static bool TryParse([Nullable(2)] string versionAsString, out ApplicationVersion version, bool asMin)
		{
			int major = asMin ? 0 : int.MaxValue;
			int minor = asMin ? 0 : int.MaxValue;
			int revision = asMin ? 0 : int.MaxValue;
			int changeSet = asMin ? 0 : int.MaxValue;
			bool skipCheck = false;
			version = ApplicationVersion.Empty;
			bool flag = versionAsString == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string[] array = versionAsString.Split(new char[]
				{
					'.'
				});
				bool flag2 = array.Length != 3 && array.Length != 4 && array[0].Length == 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					ApplicationVersionType applicationVersionType = ApplicationVersion.FromPrefix(array[0][0]);
					bool flag3 = !skipCheck && !int.TryParse(array[0].Substring(1), out major);
					if (flag3)
					{
						bool flag4 = array[0].Substring(1) != "*";
						if (flag4)
						{
							return false;
						}
						major = int.MinValue;
						minor = int.MinValue;
						revision = int.MinValue;
						changeSet = int.MinValue;
						skipCheck = true;
					}
					bool flag5 = !skipCheck && !int.TryParse(array[1], out minor);
					if (flag5)
					{
						bool flag6 = array[1] != "*";
						if (flag6)
						{
							return false;
						}
						minor = (asMin ? 0 : int.MaxValue);
						revision = (asMin ? 0 : int.MaxValue);
						changeSet = (asMin ? 0 : int.MaxValue);
						skipCheck = true;
					}
					bool flag7 = !skipCheck && !int.TryParse(array[2], out revision);
					if (flag7)
					{
						bool flag8 = array[2] != "*";
						if (flag8)
						{
							return false;
						}
						revision = (asMin ? 0 : int.MaxValue);
						changeSet = (asMin ? 0 : int.MaxValue);
						skipCheck = true;
					}
					bool flag9 = !skipCheck && array.Length == 4 && !int.TryParse(array[3], out changeSet);
					if (flag9)
					{
						bool flag10 = array[3] != "*";
						if (flag10)
						{
							return false;
						}
						changeSet = (asMin ? 0 : int.MaxValue);
					}
					version = new ApplicationVersion
					{
						ApplicationVersionType = applicationVersionType,
						Major = major,
						Minor = minor,
						Revision = revision,
						ChangeSet = changeSet
					};
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00019324 File Offset: 0x00017524
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("ApplicationVersionType = ");
			builder.Append(this.ApplicationVersionType.ToString());
			builder.Append(", Major = ");
			builder.Append(this.Major.ToString());
			builder.Append(", Minor = ");
			builder.Append(this.Minor.ToString());
			builder.Append(", Revision = ");
			builder.Append(this.Revision.ToString());
			builder.Append(", ChangeSet = ");
			builder.Append(this.ChangeSet.ToString());
			return true;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000193FA File Offset: 0x000175FA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ApplicationVersion left, ApplicationVersion right)
		{
			return !(left == right);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00019406 File Offset: 0x00017606
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ApplicationVersion left, ApplicationVersion right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001941C File Offset: 0x0001761C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ApplicationVersionType>.Default.GetHashCode(this.<ApplicationVersionType>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Major>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Minor>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Revision>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ChangeSet>k__BackingField);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000194AC File Offset: 0x000176AC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ApplicationVersion);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000194BC File Offset: 0x000176BC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ApplicationVersion other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ApplicationVersionType>.Default.Equals(this.<ApplicationVersionType>k__BackingField, other.<ApplicationVersionType>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Major>k__BackingField, other.<Major>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Minor>k__BackingField, other.<Minor>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Revision>k__BackingField, other.<Revision>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ChangeSet>k__BackingField, other.<ChangeSet>k__BackingField));
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019570 File Offset: 0x00017770
		[CompilerGenerated]
		protected ApplicationVersion(ApplicationVersion original)
		{
			this.ApplicationVersionType = original.<ApplicationVersionType>k__BackingField;
			this.Major = original.<Major>k__BackingField;
			this.Minor = original.<Minor>k__BackingField;
			this.Revision = original.<Revision>k__BackingField;
			this.ChangeSet = original.<ChangeSet>k__BackingField;
		}
	}
}
