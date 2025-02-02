using System;
using System.Diagnostics;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200007A RID: 122
	[EngineClass("rglResource")]
	public abstract class Resource : NativeObject
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0000910A File Offset: 0x0000730A
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000911C File Offset: 0x0000731C
		protected Resource()
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00009124 File Offset: 0x00007324
		internal Resource(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00009133 File Offset: 0x00007333
		[Conditional("_RGL_KEEP_ASSERTS")]
		protected void CheckResourceParameter(Resource param, string paramName = "")
		{
			if (param == null)
			{
				throw new NullReferenceException(paramName);
			}
			if (!param.IsValid)
			{
				throw new ArgumentException(paramName);
			}
		}
	}
}
