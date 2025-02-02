using System;
using System.Collections.Generic;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000017 RID: 23
	internal class LibraryApplicationInterface
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002ECC File Offset: 0x000010CC
		private static T GetObject<T>() where T : class
		{
			object obj;
			if (LibraryApplicationInterface._objects.TryGetValue(typeof(T).FullName, out obj))
			{
				return obj as T;
			}
			return default(T);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F0C File Offset: 0x0000110C
		internal static void SetObjects(Dictionary<string, object> objects)
		{
			LibraryApplicationInterface._objects = objects;
			LibraryApplicationInterface.IManaged = LibraryApplicationInterface.GetObject<IManaged>();
			LibraryApplicationInterface.ITelemetry = LibraryApplicationInterface.GetObject<ITelemetry>();
			LibraryApplicationInterface.ILibrarySizeChecker = LibraryApplicationInterface.GetObject<ILibrarySizeChecker>();
			LibraryApplicationInterface.INativeArray = LibraryApplicationInterface.GetObject<INativeArray>();
			LibraryApplicationInterface.INativeObjectArray = LibraryApplicationInterface.GetObject<INativeObjectArray>();
			LibraryApplicationInterface.INativeStringHelper = LibraryApplicationInterface.GetObject<INativeStringHelper>();
			LibraryApplicationInterface.INativeString = LibraryApplicationInterface.GetObject<INativeString>();
		}

		// Token: 0x04000031 RID: 49
		internal static IManaged IManaged;

		// Token: 0x04000032 RID: 50
		internal static ITelemetry ITelemetry;

		// Token: 0x04000033 RID: 51
		internal static ILibrarySizeChecker ILibrarySizeChecker;

		// Token: 0x04000034 RID: 52
		internal static INativeArray INativeArray;

		// Token: 0x04000035 RID: 53
		internal static INativeObjectArray INativeObjectArray;

		// Token: 0x04000036 RID: 54
		internal static INativeStringHelper INativeStringHelper;

		// Token: 0x04000037 RID: 55
		internal static INativeString INativeString;

		// Token: 0x04000038 RID: 56
		private static Dictionary<string, object> _objects;
	}
}
