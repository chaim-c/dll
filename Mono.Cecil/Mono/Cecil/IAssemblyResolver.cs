using System;

namespace Mono.Cecil
{
	// Token: 0x02000092 RID: 146
	public interface IAssemblyResolver
	{
		// Token: 0x060004E8 RID: 1256
		AssemblyDefinition Resolve(AssemblyNameReference name);

		// Token: 0x060004E9 RID: 1257
		AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters);

		// Token: 0x060004EA RID: 1258
		AssemblyDefinition Resolve(string fullName);

		// Token: 0x060004EB RID: 1259
		AssemblyDefinition Resolve(string fullName, ReaderParameters parameters);
	}
}
