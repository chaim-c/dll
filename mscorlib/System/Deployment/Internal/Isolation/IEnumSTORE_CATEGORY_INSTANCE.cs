﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068D RID: 1677
	[Guid("5ba7cb30-8508-4114-8c77-262fcda4fadb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_INSTANCE
	{
		// Token: 0x06004F76 RID: 20342
		[SecurityCritical]
		uint Next([In] uint ulElements, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_INSTANCE[] rgInstances);

		// Token: 0x06004F77 RID: 20343
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004F78 RID: 20344
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F79 RID: 20345
		[SecurityCritical]
		IEnumSTORE_CATEGORY_INSTANCE Clone();
	}
}
