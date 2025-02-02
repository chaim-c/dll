using System;
using System.IO;
using System.Runtime.Serialization;

namespace Mono.Cecil
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	public class AssemblyResolutionException : FileNotFoundException
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00014C84 File Offset: 0x00012E84
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00014C8C File Offset: 0x00012E8C
		public AssemblyResolutionException(AssemblyNameReference reference) : base(string.Format("Failed to resolve assembly: '{0}'", reference))
		{
			this.reference = reference;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00014CA6 File Offset: 0x00012EA6
		protected AssemblyResolutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040003DE RID: 990
		private readonly AssemblyNameReference reference;
	}
}
