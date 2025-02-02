using System;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000CD RID: 205
	public sealed class MethodDefinition : MethodReference, IMemberDefinition, ICustomAttributeProvider, ISecurityDeclarationProvider, IMetadataTokenProvider
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001A247 File Offset: 0x00018447
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001A24F File Offset: 0x0001844F
		public MethodAttributes Attributes
		{
			get
			{
				return (MethodAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001A258 File Offset: 0x00018458
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001A260 File Offset: 0x00018460
		public MethodImplAttributes ImplAttributes
		{
			get
			{
				return (MethodImplAttributes)this.impl_attributes;
			}
			set
			{
				this.impl_attributes = (ushort)value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001A269 File Offset: 0x00018469
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001A2A7 File Offset: 0x000184A7
		public MethodSemanticsAttributes SemanticsAttributes
		{
			get
			{
				if (this.sem_attrs_ready)
				{
					return this.sem_attrs;
				}
				if (base.HasImage)
				{
					this.ReadSemantics();
					return this.sem_attrs;
				}
				this.sem_attrs = MethodSemanticsAttributes.None;
				this.sem_attrs_ready = true;
				return this.sem_attrs;
			}
			set
			{
				this.sem_attrs = value;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001A2BC File Offset: 0x000184BC
		internal void ReadSemantics()
		{
			if (this.sem_attrs_ready)
			{
				return;
			}
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				return;
			}
			if (!module.HasImage)
			{
				return;
			}
			module.Read<MethodDefinition, MethodSemanticsAttributes>(this, (MethodDefinition method, MetadataReader reader) => reader.ReadAllSemantics(method));
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001A30D File Offset: 0x0001850D
		public bool HasSecurityDeclarations
		{
			get
			{
				if (this.security_declarations != null)
				{
					return this.security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(this.Module);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001A332 File Offset: 0x00018532
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.Module);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001A350 File Offset: 0x00018550
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001A375 File Offset: 0x00018575
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001A393 File Offset: 0x00018593
		public int RVA
		{
			get
			{
				return (int)this.rva;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001A39C File Offset: 0x0001859C
		public bool HasBody
		{
			get
			{
				return (this.attributes & 1024) == 0 && (this.attributes & 8192) == 0 && (this.impl_attributes & 4096) == 0 && (this.impl_attributes & 1) == 0 && (this.impl_attributes & 4) == 0 && (this.impl_attributes & 3) == 0;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001A400 File Offset: 0x00018600
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0001A474 File Offset: 0x00018674
		public MethodBody Body
		{
			get
			{
				MethodBody methodBody = this.body;
				if (methodBody != null)
				{
					return methodBody;
				}
				if (!this.HasBody)
				{
					return null;
				}
				if (base.HasImage && this.rva != 0U)
				{
					return this.Module.Read<MethodDefinition, MethodBody>(ref this.body, this, (MethodDefinition method, MetadataReader reader) => reader.ReadMethodBody(method));
				}
				return this.body = new MethodBody(this);
			}
			set
			{
				ModuleDefinition module = this.Module;
				if (module == null)
				{
					this.body = value;
					return;
				}
				lock (module.SyncRoot)
				{
					this.body = value;
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001A4C0 File Offset: 0x000186C0
		public bool HasPInvokeInfo
		{
			get
			{
				return this.pinvoke != null || this.IsPInvokeImpl;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001A4DC File Offset: 0x000186DC
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001A539 File Offset: 0x00018739
		public PInvokeInfo PInvokeInfo
		{
			get
			{
				if (this.pinvoke != null)
				{
					return this.pinvoke;
				}
				if (base.HasImage && this.IsPInvokeImpl)
				{
					return this.Module.Read<MethodDefinition, PInvokeInfo>(ref this.pinvoke, this, (MethodDefinition method, MetadataReader reader) => reader.ReadPInvokeInfo(method));
				}
				return null;
			}
			set
			{
				this.IsPInvokeImpl = true;
				this.pinvoke = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001A554 File Offset: 0x00018754
		public bool HasOverrides
		{
			get
			{
				if (this.overrides != null)
				{
					return this.overrides.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<MethodDefinition, bool>(this, (MethodDefinition method, MetadataReader reader) => reader.HasOverrides(method));
				}
				return false;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001A5B4 File Offset: 0x000187B4
		public Collection<MethodReference> Overrides
		{
			get
			{
				if (this.overrides != null)
				{
					return this.overrides;
				}
				if (base.HasImage)
				{
					return this.Module.Read<MethodDefinition, Collection<MethodReference>>(ref this.overrides, this, (MethodDefinition method, MetadataReader reader) => reader.ReadOverrides(method));
				}
				return this.overrides = new Collection<MethodReference>();
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001A616 File Offset: 0x00018816
		public override bool HasGenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters.Count > 0;
				}
				return this.GetHasGenericParameters(this.Module);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001A63B File Offset: 0x0001883B
		public override Collection<GenericParameter> GenericParameters
		{
			get
			{
				return this.generic_parameters ?? this.GetGenericParameters(ref this.generic_parameters, this.Module);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001A659 File Offset: 0x00018859
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0001A668 File Offset: 0x00018868
		public bool IsCompilerControlled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 0U, value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001A67E File Offset: 0x0001887E
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0001A68D File Offset: 0x0001888D
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 1U, value);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001A6A3 File Offset: 0x000188A3
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0001A6B2 File Offset: 0x000188B2
		public bool IsFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 2U, value);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001A6C8 File Offset: 0x000188C8
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0001A6D7 File Offset: 0x000188D7
		public bool IsAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 3U, value);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001A6ED File Offset: 0x000188ED
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0001A6FC File Offset: 0x000188FC
		public bool IsFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 4U, value);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001A712 File Offset: 0x00018912
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0001A721 File Offset: 0x00018921
		public bool IsFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 5U, value);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001A737 File Offset: 0x00018937
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0001A746 File Offset: 0x00018946
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 6U, value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001A75C File Offset: 0x0001895C
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0001A76B File Offset: 0x0001896B
		public bool IsStatic
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001A781 File Offset: 0x00018981
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001A790 File Offset: 0x00018990
		public bool IsFinal
		{
			get
			{
				return this.attributes.GetAttributes(32);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001A7A6 File Offset: 0x000189A6
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001A7B5 File Offset: 0x000189B5
		public bool IsVirtual
		{
			get
			{
				return this.attributes.GetAttributes(64);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001A7CB File Offset: 0x000189CB
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001A7DD File Offset: 0x000189DD
		public bool IsHideBySig
		{
			get
			{
				return this.attributes.GetAttributes(128);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001A7F6 File Offset: 0x000189F6
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001A809 File Offset: 0x00018A09
		public bool IsReuseSlot
		{
			get
			{
				return this.attributes.GetMaskedAttributes(256, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(256, 0U, value);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001A823 File Offset: 0x00018A23
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001A83A File Offset: 0x00018A3A
		public bool IsNewSlot
		{
			get
			{
				return this.attributes.GetMaskedAttributes(256, 256U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(256, 256U, value);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001A858 File Offset: 0x00018A58
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0001A86A File Offset: 0x00018A6A
		public bool IsCheckAccessOnOverride
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001A883 File Offset: 0x00018A83
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001A895 File Offset: 0x00018A95
		public bool IsAbstract
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001A8AE File Offset: 0x00018AAE
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(2048);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2048, value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001A8D9 File Offset: 0x00018AD9
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0001A8EB File Offset: 0x00018AEB
		public bool IsPInvokeImpl
		{
			get
			{
				return this.attributes.GetAttributes(8192);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192, value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001A904 File Offset: 0x00018B04
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0001A912 File Offset: 0x00018B12
		public bool IsUnmanagedExport
		{
			get
			{
				return this.attributes.GetAttributes(8);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001A927 File Offset: 0x00018B27
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001A939 File Offset: 0x00018B39
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001A952 File Offset: 0x00018B52
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001A964 File Offset: 0x00018B64
		public bool HasSecurity
		{
			get
			{
				return this.attributes.GetAttributes(16384);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16384, value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001A97D File Offset: 0x00018B7D
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0001A98C File Offset: 0x00018B8C
		public bool IsIL
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(3, 0U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(3, 0U, value);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001A9A2 File Offset: 0x00018BA2
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0001A9B1 File Offset: 0x00018BB1
		public bool IsNative
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(3, 1U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(3, 1U, value);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001A9C7 File Offset: 0x00018BC7
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0001A9D6 File Offset: 0x00018BD6
		public bool IsRuntime
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(3, 3U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(3, 3U, value);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001A9EC File Offset: 0x00018BEC
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0001A9FB File Offset: 0x00018BFB
		public bool IsUnmanaged
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(4, 4U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(4, 4U, value);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001AA11 File Offset: 0x00018C11
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001AA20 File Offset: 0x00018C20
		public bool IsManaged
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(4, 0U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(4, 0U, value);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001AA36 File Offset: 0x00018C36
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001AA45 File Offset: 0x00018C45
		public bool IsForwardRef
		{
			get
			{
				return this.impl_attributes.GetAttributes(16);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001AA5B File Offset: 0x00018C5B
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001AA6D File Offset: 0x00018C6D
		public bool IsPreserveSig
		{
			get
			{
				return this.impl_attributes.GetAttributes(128);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001AA86 File Offset: 0x00018C86
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0001AA98 File Offset: 0x00018C98
		public bool IsInternalCall
		{
			get
			{
				return this.impl_attributes.GetAttributes(4096);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001AAB1 File Offset: 0x00018CB1
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		public bool IsSynchronized
		{
			get
			{
				return this.impl_attributes.GetAttributes(32);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001AAD6 File Offset: 0x00018CD6
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		public bool NoInlining
		{
			get
			{
				return this.impl_attributes.GetAttributes(8);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001AAF9 File Offset: 0x00018CF9
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001AB08 File Offset: 0x00018D08
		public bool NoOptimization
		{
			get
			{
				return this.impl_attributes.GetAttributes(64);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001AB1E File Offset: 0x00018D1E
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001AB27 File Offset: 0x00018D27
		public bool IsSetter
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Setter);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Setter, value);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001AB31 File Offset: 0x00018D31
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0001AB3A File Offset: 0x00018D3A
		public bool IsGetter
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Getter);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Getter, value);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001AB44 File Offset: 0x00018D44
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001AB4D File Offset: 0x00018D4D
		public bool IsOther
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Other);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Other, value);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001AB57 File Offset: 0x00018D57
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001AB60 File Offset: 0x00018D60
		public bool IsAddOn
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.AddOn);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.AddOn, value);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001AB6A File Offset: 0x00018D6A
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001AB74 File Offset: 0x00018D74
		public bool IsRemoveOn
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.RemoveOn);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.RemoveOn, value);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001AB7F File Offset: 0x00018D7F
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001AB89 File Offset: 0x00018D89
		public bool IsFire
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Fire);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Fire, value);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0001AB94 File Offset: 0x00018D94
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0001ABA1 File Offset: 0x00018DA1
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001ABAA File Offset: 0x00018DAA
		public bool IsConstructor
		{
			get
			{
				return this.IsRuntimeSpecialName && this.IsSpecialName && (this.Name == ".cctor" || this.Name == ".ctor");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0001ABE2 File Offset: 0x00018DE2
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		internal MethodDefinition()
		{
			this.token = new MetadataToken(TokenType.Method);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001ABFD File Offset: 0x00018DFD
		public MethodDefinition(string name, MethodAttributes attributes, TypeReference returnType) : base(name, returnType)
		{
			this.attributes = (ushort)attributes;
			this.HasThis = !this.IsStatic;
			this.token = new MetadataToken(TokenType.Method);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001AC2D File Offset: 0x00018E2D
		public override MethodDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040004E9 RID: 1257
		private ushort attributes;

		// Token: 0x040004EA RID: 1258
		private ushort impl_attributes;

		// Token: 0x040004EB RID: 1259
		internal volatile bool sem_attrs_ready;

		// Token: 0x040004EC RID: 1260
		internal MethodSemanticsAttributes sem_attrs;

		// Token: 0x040004ED RID: 1261
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040004EE RID: 1262
		private Collection<SecurityDeclaration> security_declarations;

		// Token: 0x040004EF RID: 1263
		internal uint rva;

		// Token: 0x040004F0 RID: 1264
		internal PInvokeInfo pinvoke;

		// Token: 0x040004F1 RID: 1265
		private Collection<MethodReference> overrides;

		// Token: 0x040004F2 RID: 1266
		internal MethodBody body;
	}
}
