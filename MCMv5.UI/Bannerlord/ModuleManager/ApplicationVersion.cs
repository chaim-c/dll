using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000044 RID: 68
	[NullableContext(1)]
	[Nullable(0)]
	internal class ApplicationVersion : IComparable<ApplicationVersion>, IEquatable<ApplicationVersion>
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B150 File Offset: 0x00009350
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ApplicationVersion);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000B15C File Offset: 0x0000935C
		public static ApplicationVersion Empty { get; } = new ApplicationVersion();

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B163 File Offset: 0x00009363
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000B16B File Offset: 0x0000936B
		public ApplicationVersionType ApplicationVersionType { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B174 File Offset: 0x00009374
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000B17C File Offset: 0x0000937C
		public int Major { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B185 File Offset: 0x00009385
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B18D File Offset: 0x0000938D
		public int Minor { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B196 File Offset: 0x00009396
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B19E File Offset: 0x0000939E
		public int Revision { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B1A7 File Offset: 0x000093A7
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B1AF File Offset: 0x000093AF
		public int ChangeSet { get; set; }

		// Token: 0x060002AF RID: 687 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public ApplicationVersion()
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B1C2 File Offset: 0x000093C2
		public ApplicationVersion(ApplicationVersionType applicationVersionType, int major, int minor, int revision, int changeSet)
		{
			this.ApplicationVersionType = applicationVersionType;
			this.Major = major;
			this.Minor = minor;
			this.Revision = revision;
			this.ChangeSet = changeSet;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B1F8 File Offset: 0x000093F8
		[NullableContext(2)]
		public bool IsSame(ApplicationVersion other)
		{
			int major = this.Major;
			int? num = (other != null) ? new int?(other.Major) : null;
			return (major == num.GetValueOrDefault() & num != null) && this.Minor == other.Minor && this.Revision == other.Revision;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B258 File Offset: 0x00009458
		[NullableContext(2)]
		public bool IsSameWithChangeSet(ApplicationVersion other)
		{
			int major = this.Major;
			int? num = (other != null) ? new int?(other.Major) : null;
			return (major == num.GetValueOrDefault() & num != null) && this.Minor == other.Minor && this.Revision == other.Revision && this.ChangeSet == other.ChangeSet;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B2C8 File Offset: 0x000094C8
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

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B330 File Offset: 0x00009530
		[NullableContext(2)]
		public int CompareTo(ApplicationVersion other)
		{
			return ApplicationVersionComparer.CompareStandard(this, other);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B339 File Offset: 0x00009539
		public static bool operator <(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B345 File Offset: 0x00009545
		public static bool operator >(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B351 File Offset: 0x00009551
		public static bool operator <=(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B360 File Offset: 0x00009560
		public static bool operator >=(ApplicationVersion left, ApplicationVersion right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B370 File Offset: 0x00009570
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

		// Token: 0x060002BA RID: 698 RVA: 0x0000B3C0 File Offset: 0x000095C0
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

		// Token: 0x060002BB RID: 699 RVA: 0x0000B414 File Offset: 0x00009614
		public static bool TryParse([Nullable(2)] string versionAsString, out ApplicationVersion version)
		{
			return ApplicationVersion.TryParse(versionAsString, out version, true);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B420 File Offset: 0x00009620
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

		// Token: 0x060002BD RID: 701 RVA: 0x0000B660 File Offset: 0x00009860
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

		// Token: 0x060002BE RID: 702 RVA: 0x0000B736 File Offset: 0x00009936
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ApplicationVersion left, ApplicationVersion right)
		{
			return !(left == right);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B742 File Offset: 0x00009942
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ApplicationVersion left, ApplicationVersion right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B758 File Offset: 0x00009958
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ApplicationVersionType>.Default.GetHashCode(this.<ApplicationVersionType>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Major>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Minor>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Revision>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ChangeSet>k__BackingField);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B7E8 File Offset: 0x000099E8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ApplicationVersion);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B7F8 File Offset: 0x000099F8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ApplicationVersion other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ApplicationVersionType>.Default.Equals(this.<ApplicationVersionType>k__BackingField, other.<ApplicationVersionType>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Major>k__BackingField, other.<Major>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Minor>k__BackingField, other.<Minor>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Revision>k__BackingField, other.<Revision>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ChangeSet>k__BackingField, other.<ChangeSet>k__BackingField));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B8AC File Offset: 0x00009AAC
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
