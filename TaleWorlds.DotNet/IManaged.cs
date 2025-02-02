using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000018 RID: 24
	[LibraryInterfaceBase]
	internal interface IManaged
	{
		// Token: 0x06000062 RID: 98
		[EngineMethod("increase_reference_count", false)]
		void IncreaseReferenceCount(UIntPtr ptr);

		// Token: 0x06000063 RID: 99
		[EngineMethod("decrease_reference_count", false)]
		void DecreaseReferenceCount(UIntPtr ptr);

		// Token: 0x06000064 RID: 100
		[EngineMethod("get_class_type_definition_count", false)]
		int GetClassTypeDefinitionCount();

		// Token: 0x06000065 RID: 101
		[EngineMethod("get_class_type_definition", false)]
		void GetClassTypeDefinition(int index, ref EngineClassTypeDefinition engineClassTypeDefinition);

		// Token: 0x06000066 RID: 102
		[EngineMethod("release_managed_object", false)]
		void ReleaseManagedObject(UIntPtr ptr);
	}
}
