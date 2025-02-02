using System;
using System.Text;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000095 RID: 149
	public sealed class CallSite : IMethodSignature, IMetadataTokenProvider
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000154C9 File Offset: 0x000136C9
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x000154D6 File Offset: 0x000136D6
		public bool HasThis
		{
			get
			{
				return this.signature.HasThis;
			}
			set
			{
				this.signature.HasThis = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000154E4 File Offset: 0x000136E4
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x000154F1 File Offset: 0x000136F1
		public bool ExplicitThis
		{
			get
			{
				return this.signature.ExplicitThis;
			}
			set
			{
				this.signature.ExplicitThis = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000154FF File Offset: 0x000136FF
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001550C File Offset: 0x0001370C
		public MethodCallingConvention CallingConvention
		{
			get
			{
				return this.signature.CallingConvention;
			}
			set
			{
				this.signature.CallingConvention = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001551A File Offset: 0x0001371A
		public bool HasParameters
		{
			get
			{
				return this.signature.HasParameters;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00015527 File Offset: 0x00013727
		public Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.signature.Parameters;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00015534 File Offset: 0x00013734
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00015546 File Offset: 0x00013746
		public TypeReference ReturnType
		{
			get
			{
				return this.signature.MethodReturnType.ReturnType;
			}
			set
			{
				this.signature.MethodReturnType.ReturnType = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00015559 File Offset: 0x00013759
		public MethodReturnType MethodReturnType
		{
			get
			{
				return this.signature.MethodReturnType;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00015566 File Offset: 0x00013766
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0001556D File Offset: 0x0001376D
		public string Name
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00015574 File Offset: 0x00013774
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001557B File Offset: 0x0001377B
		public string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00015582 File Offset: 0x00013782
		public ModuleDefinition Module
		{
			get
			{
				return this.ReturnType.Module;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001558F File Offset: 0x0001378F
		public IMetadataScope Scope
		{
			get
			{
				return this.signature.ReturnType.Scope;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000155A1 File Offset: 0x000137A1
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000155AE File Offset: 0x000137AE
		public MetadataToken MetadataToken
		{
			get
			{
				return this.signature.token;
			}
			set
			{
				this.signature.token = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000155BC File Offset: 0x000137BC
		public string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.ReturnType.FullName);
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000155EE File Offset: 0x000137EE
		internal CallSite()
		{
			this.signature = new MethodReference();
			this.signature.token = new MetadataToken(TokenType.Signature, 0);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015617 File Offset: 0x00013817
		public CallSite(TypeReference returnType) : this()
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			this.signature.ReturnType = returnType;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015639 File Offset: 0x00013839
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040003E3 RID: 995
		private readonly MethodReference signature;
	}
}
