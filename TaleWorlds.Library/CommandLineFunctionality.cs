using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaleWorlds.Library
{
	// Token: 0x02000021 RID: 33
	public static class CommandLineFunctionality
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004568 File Offset: 0x00002768
		private static bool CheckAssemblyReferencesThis(Assembly assembly)
		{
			Assembly assembly2 = typeof(CommandLineFunctionality).Assembly;
			if (assembly2.GetName().Name == assembly.GetName().Name)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].Name == assembly2.GetName().Name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000045D8 File Offset: 0x000027D8
		public static List<string> CollectCommandLineFunctions()
		{
			List<string> list = new List<string>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (CommandLineFunctionality.CheckAssemblyReferencesThis(assembly))
				{
					foreach (Type type in assembly.GetTypesSafe(null))
					{
						foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
						{
							object[] customAttributesSafe = methodInfo.GetCustomAttributesSafe(typeof(CommandLineFunctionality.CommandLineArgumentFunction), false);
							if (customAttributesSafe != null && customAttributesSafe.Length != 0)
							{
								CommandLineFunctionality.CommandLineArgumentFunction commandLineArgumentFunction = customAttributesSafe[0] as CommandLineFunctionality.CommandLineArgumentFunction;
								if (commandLineArgumentFunction != null && !(methodInfo.ReturnType != typeof(string)))
								{
									string name = commandLineArgumentFunction.Name;
									string text = commandLineArgumentFunction.GroupName + "." + name;
									list.Add(text);
									CommandLineFunctionality.CommandLineFunction value = new CommandLineFunctionality.CommandLineFunction((Func<List<string>, string>)Delegate.CreateDelegate(typeof(Func<List<string>, string>), methodInfo));
									CommandLineFunctionality.AllFunctions.Add(text, value);
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004720 File Offset: 0x00002920
		public static bool HasFunctionForCommand(string command)
		{
			CommandLineFunctionality.CommandLineFunction commandLineFunction;
			return CommandLineFunctionality.AllFunctions.TryGetValue(command, out commandLineFunction);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000473C File Offset: 0x0000293C
		public static string CallFunction(string concatName, string concatArguments, out bool found)
		{
			CommandLineFunctionality.CommandLineFunction commandLineFunction;
			if (CommandLineFunctionality.AllFunctions.TryGetValue(concatName, out commandLineFunction))
			{
				List<string> objects;
				if (concatArguments != string.Empty)
				{
					objects = new List<string>(concatArguments.Split(new char[]
					{
						' '
					}));
				}
				else
				{
					objects = new List<string>();
				}
				found = true;
				return commandLineFunction.Call(objects);
			}
			found = false;
			return "Could not find the command " + concatName;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000047A0 File Offset: 0x000029A0
		public static string CallFunction(string concatName, List<string> argList, out bool found)
		{
			CommandLineFunctionality.CommandLineFunction commandLineFunction;
			if (CommandLineFunctionality.AllFunctions.TryGetValue(concatName, out commandLineFunction))
			{
				found = true;
				return commandLineFunction.Call(argList);
			}
			found = false;
			return "Could not find the command " + concatName;
		}

		// Token: 0x04000066 RID: 102
		private static Dictionary<string, CommandLineFunctionality.CommandLineFunction> AllFunctions = new Dictionary<string, CommandLineFunctionality.CommandLineFunction>();

		// Token: 0x020000C3 RID: 195
		private class CommandLineFunction
		{
			// Token: 0x060006E2 RID: 1762 RVA: 0x000159D0 File Offset: 0x00013BD0
			public CommandLineFunction(Func<List<string>, string> commandlinefunc)
			{
				this.CommandLineFunc = commandlinefunc;
				this.Children = new List<CommandLineFunctionality.CommandLineFunction>();
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x000159EA File Offset: 0x00013BEA
			public string Call(List<string> objects)
			{
				return this.CommandLineFunc(objects);
			}

			// Token: 0x04000227 RID: 551
			public Func<List<string>, string> CommandLineFunc;

			// Token: 0x04000228 RID: 552
			public List<CommandLineFunctionality.CommandLineFunction> Children;
		}

		// Token: 0x020000C4 RID: 196
		public class CommandLineArgumentFunction : Attribute
		{
			// Token: 0x060006E4 RID: 1764 RVA: 0x000159F8 File Offset: 0x00013BF8
			public CommandLineArgumentFunction(string name, string groupname)
			{
				this.Name = name;
				this.GroupName = groupname;
			}

			// Token: 0x04000229 RID: 553
			public string Name;

			// Token: 0x0400022A RID: 554
			public string GroupName;
		}
	}
}
