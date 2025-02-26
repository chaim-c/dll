﻿using System;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006BB RID: 1723
	internal enum CMS_ASSEMBLY_REFERENCE_FLAG
	{
		// Token: 0x040022A7 RID: 8871
		CMS_ASSEMBLY_REFERENCE_FLAG_OPTIONAL = 1,
		// Token: 0x040022A8 RID: 8872
		CMS_ASSEMBLY_REFERENCE_FLAG_VISIBLE,
		// Token: 0x040022A9 RID: 8873
		CMS_ASSEMBLY_REFERENCE_FLAG_FOLLOW = 4,
		// Token: 0x040022AA RID: 8874
		CMS_ASSEMBLY_REFERENCE_FLAG_IS_PLATFORM = 8,
		// Token: 0x040022AB RID: 8875
		CMS_ASSEMBLY_REFERENCE_FLAG_CULTURE_WILDCARDED = 16,
		// Token: 0x040022AC RID: 8876
		CMS_ASSEMBLY_REFERENCE_FLAG_PROCESSOR_ARCHITECTURE_WILDCARDED = 32,
		// Token: 0x040022AD RID: 8877
		CMS_ASSEMBLY_REFERENCE_FLAG_PREREQUISITE = 128
	}
}
