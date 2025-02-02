using System;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x0200006E RID: 110
	public class ObjectInstanceTracker
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000C6A3 File Offset: 0x0000A8A3
		public static void RegisterTrackedInstance(string name, WeakReference instance)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public static bool CheckBlacklistedTypeCounts(Dictionary<string, int> typeNameCounts, ref string outputLog)
		{
			bool result = false;
			foreach (string text in typeNameCounts.Keys)
			{
				int num = 0;
				int num2 = typeNameCounts[text];
				List<WeakReference> list;
				if (ObjectInstanceTracker.TrackedInstances.TryGetValue(text, out list))
				{
					using (List<WeakReference>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.Target != null)
							{
								num++;
							}
						}
					}
				}
				if (num != num2)
				{
					result = true;
					outputLog = string.Concat(new object[]
					{
						outputLog,
						"Type(",
						text,
						") has ",
						num,
						" alive instance, but its should be ",
						num2,
						"\n"
					});
				}
			}
			return result;
		}

		// Token: 0x0400011E RID: 286
		private static Dictionary<string, List<WeakReference>> TrackedInstances = new Dictionary<string, List<WeakReference>>();
	}
}
