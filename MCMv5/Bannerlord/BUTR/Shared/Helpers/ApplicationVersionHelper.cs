using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;

namespace Bannerlord.BUTR.Shared.Helpers
{
	// Token: 0x0200012D RID: 301
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal static class ApplicationVersionHelper
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00017B7D File Offset: 0x00015D7D
		[NullableContext(2)]
		public static bool TryParse(string versionAsString, out ApplicationVersion version)
		{
			return ApplicationVersionHelper.TryParse(versionAsString, out version, true);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00017B88 File Offset: 0x00015D88
		[NullableContext(2)]
		public static bool TryParse(string versionAsString, out ApplicationVersion version, bool asMin)
		{
			version = default(ApplicationVersion);
			int major = asMin ? 0 : int.MaxValue;
			int minor = asMin ? 0 : int.MaxValue;
			int revision = asMin ? 0 : int.MaxValue;
			int changeSet = asMin ? 0 : int.MaxValue;
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
					bool skipCheck = false;
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
					ApplicationVersionType applicationVersionType = ApplicationVersion.ApplicationVersionTypeFromString(array[0][0].ToString());
					version = new ApplicationVersion(applicationVersionType, major, minor, revision, changeSet);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00017DB4 File Offset: 0x00015FB4
		public static string ToString(ApplicationVersion av)
		{
			string prefix = ApplicationVersion.GetPrefix(av.ApplicationVersionType);
			ApplicationVersion def = ApplicationVersion.FromParametersFile(null);
			return string.Format("{0}{1}.{2}.{3}{4}", new object[]
			{
				prefix,
				av.Major,
				av.Minor,
				av.Revision,
				(av.ChangeSet == def.ChangeSet) ? "" : string.Format(".{0}", av.ChangeSet)
			});
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00017E4C File Offset: 0x0001604C
		public static bool IsSame(this ApplicationVersion @this, ApplicationVersion other)
		{
			return @this.ApplicationVersionType == other.ApplicationVersionType && @this.Major == other.Major && @this.Minor == other.Minor && @this.Revision == other.Revision;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00017E9C File Offset: 0x0001609C
		public static bool IsSameWithChangeSet(this ApplicationVersion @this, ApplicationVersion other)
		{
			return @this.ApplicationVersionType == other.ApplicationVersionType && @this.Major == other.Major && @this.Minor == other.Minor && @this.Revision == other.Revision && @this.ChangeSet == other.ChangeSet;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00017EFC File Offset: 0x000160FC
		public static ApplicationVersion? GameVersion()
		{
			return new ApplicationVersion?(ApplicationVersion.FromParametersFile(null));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00017F0C File Offset: 0x0001610C
		public static string GameVersionStr()
		{
			return ApplicationVersionHelper.ToString(ApplicationVersionHelper.GameVersion() ?? ApplicationVersion.Empty);
		}
	}
}
