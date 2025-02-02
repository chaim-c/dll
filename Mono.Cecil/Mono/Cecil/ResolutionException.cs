using System;
using System.Runtime.Serialization;

namespace Mono.Cecil
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class ResolutionException : Exception
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001885E File Offset: 0x00016A5E
		public MemberReference Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00018868 File Offset: 0x00016A68
		public IMetadataScope Scope
		{
			get
			{
				TypeReference typeReference = this.member as TypeReference;
				if (typeReference != null)
				{
					return typeReference.Scope;
				}
				TypeReference declaringType = this.member.DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Scope;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000188A6 File Offset: 0x00016AA6
		public ResolutionException(MemberReference member) : base("Failed to resolve " + member.FullName)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.member = member;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000188D3 File Offset: 0x00016AD3
		protected ResolutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000454 RID: 1108
		private readonly MemberReference member;
	}
}
