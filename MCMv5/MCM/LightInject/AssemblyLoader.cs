using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000116 RID: 278
	[ExcludeFromCodeCoverage]
	internal class AssemblyLoader : IAssemblyLoader
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x00014F84 File Offset: 0x00013184
		public IEnumerable<Assembly> Load(string searchPattern)
		{
			string directory = Path.GetDirectoryName(new Uri(this.GetAssemblyCodeBasePath()).LocalPath);
			string[] searchPatterns = searchPattern.Split(new char[]
			{
				'|'
			});
			IEnumerable<string> source = searchPatterns;
			Func<string, IEnumerable<string>> <>9__0;
			Func<string, IEnumerable<string>> selector;
			if ((selector = <>9__0) == null)
			{
				selector = (<>9__0 = ((string sp) => Directory.GetFiles(directory, sp)));
			}
			foreach (string file in source.SelectMany(selector).Where(new Func<string, bool>(this.CanLoad)))
			{
				yield return this.LoadAssembly(file);
				file = null;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00014F9C File Offset: 0x0001319C
		protected virtual bool CanLoad(string fileName)
		{
			return true;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00014FB0 File Offset: 0x000131B0
		protected virtual Assembly LoadAssembly(string filename)
		{
			return Assembly.LoadFrom(filename);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00014FC8 File Offset: 0x000131C8
		protected virtual string GetAssemblyCodeBasePath()
		{
			return typeof(ServiceContainer).Assembly.Location;
		}
	}
}
