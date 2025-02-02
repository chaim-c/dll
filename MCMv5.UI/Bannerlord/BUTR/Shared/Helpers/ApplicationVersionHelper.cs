using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;

namespace Bannerlord.BUTR.Shared.Helpers
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal static class ApplicationVersionHelper
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00009EB9 File Offset: 0x000080B9
		[NullableContext(2)]
		public static bool TryParse(string versionAsString, out ApplicationVersion version)
		{
			return ApplicationVersionHelper.TryParse(versionAsString, out version, true);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009EC4 File Offset: 0x000080C4
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

		// Token: 0x06000271 RID: 625 RVA: 0x0000A0F0 File Offset: 0x000082F0
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

		// Token: 0x06000272 RID: 626 RVA: 0x0000A188 File Offset: 0x00008388
		public static bool IsSame(this ApplicationVersion @this, ApplicationVersion other)
		{
			return @this.ApplicationVersionType == other.ApplicationVersionType && @this.Major == other.Major && @this.Minor == other.Minor && @this.Revision == other.Revision;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public static bool IsSameWithChangeSet(this ApplicationVersion @this, ApplicationVersion other)
		{
			return @this.ApplicationVersionType == other.ApplicationVersionType && @this.Major == other.Major && @this.Minor == other.Minor && @this.Revision == other.Revision && @this.ChangeSet == other.ChangeSet;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A238 File Offset: 0x00008438
		public static ApplicationVersion? GameVersion()
		{
			return new ApplicationVersion?(ApplicationVersion.FromParametersFile(null));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A248 File Offset: 0x00008448
		public static string GameVersionStr()
		{
			return ApplicationVersionHelper.ToString(ApplicationVersionHelper.GameVersion() ?? ApplicationVersion.Empty);
		}
	}
}
