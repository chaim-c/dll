using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000066 RID: 102
	public class SaveCodeGenerationContext
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0000C675 File Offset: 0x0000A875
		public SaveCodeGenerationContext(DefinitionContext definitionContext)
		{
			this._definitionContext = definitionContext;
			this._assemblies = new Dictionary<Assembly, SaveCodeGenerationContextAssembly>();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C690 File Offset: 0x0000A890
		public void AddAssembly(Assembly assembly, string defaultNamespace, string location, string fileName)
		{
			SaveCodeGenerationContextAssembly value = new SaveCodeGenerationContextAssembly(this._definitionContext, assembly, defaultNamespace, location, fileName);
			this._assemblies.Add(assembly, value);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		internal SaveCodeGenerationContextAssembly FindAssemblyInformation(Assembly assembly)
		{
			SaveCodeGenerationContextAssembly result;
			this._assemblies.TryGetValue(assembly, out result);
			return result;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		internal void FillFiles()
		{
			List<Tuple<string, string>> list = new List<Tuple<string, string>>();
			foreach (SaveCodeGenerationContextAssembly saveCodeGenerationContextAssembly in this._assemblies.Values)
			{
				saveCodeGenerationContextAssembly.Generate();
				string item = saveCodeGenerationContextAssembly.GenerateText();
				list.Add(new Tuple<string, string>(saveCodeGenerationContextAssembly.Location + saveCodeGenerationContextAssembly.FileName, item));
			}
			foreach (Tuple<string, string> tuple in list)
			{
				File.WriteAllText(tuple.Item1, tuple.Item2, Encoding.UTF8);
			}
		}

		// Token: 0x040000F0 RID: 240
		private Dictionary<Assembly, SaveCodeGenerationContextAssembly> _assemblies;

		// Token: 0x040000F1 RID: 241
		private DefinitionContext _definitionContext;
	}
}
