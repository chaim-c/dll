using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000011 RID: 17
	public static class CrashInformationCollector
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002D70 File Offset: 0x00000F70
		[EngineCallback]
		public static string CollectInformation()
		{
			List<CrashInformationCollector.CrashInformation> list = new List<CrashInformationCollector.CrashInformation>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					Type[] types = assembly.GetTypes();
					for (int j = 0; j < types.Length; j++)
					{
						foreach (MethodInfo methodInfo in types[j].GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
						{
							object[] customAttributesSafe = methodInfo.GetCustomAttributesSafe(typeof(CrashInformationCollector.CrashInformationProvider), false);
							if (customAttributesSafe != null && customAttributesSafe.Length != 0 && customAttributesSafe[0] is CrashInformationCollector.CrashInformationProvider)
							{
								CrashInformationCollector.CrashInformation crashInformation = methodInfo.Invoke(null, new object[0]) as CrashInformationCollector.CrashInformation;
								if (crashInformation != null)
								{
									list.Add(crashInformation);
								}
							}
						}
					}
				}
				catch (ReflectionTypeLoadException ex)
				{
					foreach (Exception ex2 in ex.LoaderExceptions)
					{
						MBDebug.Print("Unable to load types from assembly: " + ex2.Message, 0, Debug.DebugColor.White, 17592186044416UL);
					}
				}
				catch (Exception ex3)
				{
					MBDebug.Print("Exception while collecting crash information : " + ex3.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				}
			}
			string text = "";
			foreach (CrashInformationCollector.CrashInformation crashInformation2 in list)
			{
				foreach (ValueTuple<string, string> valueTuple in crashInformation2.Lines)
				{
					text = string.Concat(new string[]
					{
						text,
						"[",
						crashInformation2.Id,
						"][",
						valueTuple.Item1,
						"][",
						valueTuple.Item2,
						"]\n"
					});
				}
			}
			return text;
		}

		// Token: 0x020000A9 RID: 169
		public class CrashInformation
		{
			// Token: 0x06000C52 RID: 3154 RVA: 0x0000EE2F File Offset: 0x0000D02F
			public CrashInformation(string id, MBReadOnlyList<ValueTuple<string, string>> lines)
			{
				this.Id = id;
				this.Lines = lines;
			}

			// Token: 0x04000335 RID: 821
			public readonly string Id;

			// Token: 0x04000336 RID: 822
			public readonly MBReadOnlyList<ValueTuple<string, string>> Lines;
		}

		// Token: 0x020000AA RID: 170
		[AttributeUsage(AttributeTargets.Method)]
		public class CrashInformationProvider : Attribute
		{
		}
	}
}
