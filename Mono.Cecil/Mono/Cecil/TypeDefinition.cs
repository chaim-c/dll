using System;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000EC RID: 236
	public sealed class TypeDefinition : TypeReference, IMemberDefinition, ICustomAttributeProvider, ISecurityDeclarationProvider, IMetadataTokenProvider
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0001CCB8 File Offset: 0x0001AEB8
		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001CCC1 File Offset: 0x0001AEC1
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001CCC9 File Offset: 0x0001AEC9
		public TypeReference BaseType
		{
			get
			{
				return this.base_type;
			}
			set
			{
				this.base_type = value;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		private void ResolveLayout()
		{
			if (this.packing_size != -2 || this.class_size != -2)
			{
				return;
			}
			if (!base.HasImage)
			{
				this.packing_size = -1;
				this.class_size = -1;
				return;
			}
			Row<short, int> row = this.Module.Read<TypeDefinition, Row<short, int>>(this, (TypeDefinition type, MetadataReader reader) => reader.ReadTypeLayout(type));
			this.packing_size = row.Col1;
			this.class_size = row.Col2;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001CD59 File Offset: 0x0001AF59
		public bool HasLayoutInfo
		{
			get
			{
				if (this.packing_size >= 0 || this.class_size >= 0)
				{
					return true;
				}
				this.ResolveLayout();
				return this.packing_size >= 0 || this.class_size >= 0;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0001CDB5 File Offset: 0x0001AFB5
		public short PackingSize
		{
			get
			{
				if (this.packing_size >= 0)
				{
					return this.packing_size;
				}
				this.ResolveLayout();
				if (this.packing_size < 0)
				{
					return -1;
				}
				return this.packing_size;
			}
			set
			{
				this.packing_size = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001CDBE File Offset: 0x0001AFBE
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0001CDE7 File Offset: 0x0001AFE7
		public int ClassSize
		{
			get
			{
				if (this.class_size >= 0)
				{
					return this.class_size;
				}
				this.ResolveLayout();
				if (this.class_size < 0)
				{
					return -1;
				}
				return this.class_size;
			}
			set
			{
				this.class_size = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001CDFC File Offset: 0x0001AFFC
		public bool HasInterfaces
		{
			get
			{
				if (this.interfaces != null)
				{
					return this.interfaces.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasInterfaces(type));
				}
				return false;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001CE5C File Offset: 0x0001B05C
		public Collection<TypeReference> Interfaces
		{
			get
			{
				if (this.interfaces != null)
				{
					return this.interfaces;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<TypeReference>>(ref this.interfaces, this, (TypeDefinition type, MetadataReader reader) => reader.ReadInterfaces(type));
				}
				return this.interfaces = new Collection<TypeReference>();
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001CEC8 File Offset: 0x0001B0C8
		public bool HasNestedTypes
		{
			get
			{
				if (this.nested_types != null)
				{
					return this.nested_types.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasNestedTypes(type));
				}
				return false;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001CF28 File Offset: 0x0001B128
		public Collection<TypeDefinition> NestedTypes
		{
			get
			{
				if (this.nested_types != null)
				{
					return this.nested_types;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<TypeDefinition>>(ref this.nested_types, this, (TypeDefinition type, MetadataReader reader) => reader.ReadNestedTypes(type));
				}
				return this.nested_types = new MemberDefinitionCollection<TypeDefinition>(this);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001CF8B File Offset: 0x0001B18B
		public bool HasMethods
		{
			get
			{
				if (this.methods != null)
				{
					return this.methods.Count > 0;
				}
				return base.HasImage && this.methods_range.Length > 0U;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001CFC8 File Offset: 0x0001B1C8
		public Collection<MethodDefinition> Methods
		{
			get
			{
				if (this.methods != null)
				{
					return this.methods;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<MethodDefinition>>(ref this.methods, this, (TypeDefinition type, MetadataReader reader) => reader.ReadMethods(type));
				}
				return this.methods = new MemberDefinitionCollection<MethodDefinition>(this);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001D02B File Offset: 0x0001B22B
		public bool HasFields
		{
			get
			{
				if (this.fields != null)
				{
					return this.fields.Count > 0;
				}
				return base.HasImage && this.fields_range.Length > 0U;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001D068 File Offset: 0x0001B268
		public Collection<FieldDefinition> Fields
		{
			get
			{
				if (this.fields != null)
				{
					return this.fields;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<FieldDefinition>>(ref this.fields, this, (TypeDefinition type, MetadataReader reader) => reader.ReadFields(type));
				}
				return this.fields = new MemberDefinitionCollection<FieldDefinition>(this);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		public bool HasEvents
		{
			get
			{
				if (this.events != null)
				{
					return this.events.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasEvents(type));
				}
				return false;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001D134 File Offset: 0x0001B334
		public Collection<EventDefinition> Events
		{
			get
			{
				if (this.events != null)
				{
					return this.events;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<EventDefinition>>(ref this.events, this, (TypeDefinition type, MetadataReader reader) => reader.ReadEvents(type));
				}
				return this.events = new MemberDefinitionCollection<EventDefinition>(this);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
		public bool HasProperties
		{
			get
			{
				if (this.properties != null)
				{
					return this.properties.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, bool>(this, (TypeDefinition type, MetadataReader reader) => reader.HasProperties(type));
				}
				return false;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001D200 File Offset: 0x0001B400
		public Collection<PropertyDefinition> Properties
		{
			get
			{
				if (this.properties != null)
				{
					return this.properties;
				}
				if (base.HasImage)
				{
					return this.Module.Read<TypeDefinition, Collection<PropertyDefinition>>(ref this.properties, this, (TypeDefinition type, MetadataReader reader) => reader.ReadProperties(type));
				}
				return this.properties = new MemberDefinitionCollection<PropertyDefinition>(this);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001D263 File Offset: 0x0001B463
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001D288 File Offset: 0x0001B488
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.Module);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001D2A6 File Offset: 0x0001B4A6
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001D2CB File Offset: 0x0001B4CB
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001D2E9 File Offset: 0x0001B4E9
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

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001D30E File Offset: 0x0001B50E
		public override Collection<GenericParameter> GenericParameters
		{
			get
			{
				return this.generic_parameters ?? this.GetGenericParameters(ref this.generic_parameters, this.Module);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001D32C File Offset: 0x0001B52C
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0001D33B File Offset: 0x0001B53B
		public bool IsNotPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 0U, value);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001D351 File Offset: 0x0001B551
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0001D360 File Offset: 0x0001B560
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 1U, value);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001D376 File Offset: 0x0001B576
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0001D385 File Offset: 0x0001B585
		public bool IsNestedPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 2U, value);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001D39B File Offset: 0x0001B59B
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001D3AA File Offset: 0x0001B5AA
		public bool IsNestedPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 3U, value);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0001D3CF File Offset: 0x0001B5CF
		public bool IsNestedFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 4U, value);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001D3E5 File Offset: 0x0001B5E5
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		public bool IsNestedAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 5U, value);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001D40A File Offset: 0x0001B60A
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0001D419 File Offset: 0x0001B619
		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 6U, value);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001D42F File Offset: 0x0001B62F
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0001D43E File Offset: 0x0001B63E
		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 7U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 7U, value);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001D454 File Offset: 0x0001B654
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0001D464 File Offset: 0x0001B664
		public bool IsAutoLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 0U, value);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001D47B File Offset: 0x0001B67B
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0001D48B File Offset: 0x0001B68B
		public bool IsSequentialLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 8U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 8U, value);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001D4A2 File Offset: 0x0001B6A2
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0001D4B3 File Offset: 0x0001B6B3
		public bool IsExplicitLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 16U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 16U, value);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001D4CB File Offset: 0x0001B6CB
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001D4DB File Offset: 0x0001B6DB
		public bool IsClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 0U, value);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001D4F2 File Offset: 0x0001B6F2
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001D503 File Offset: 0x0001B703
		public bool IsInterface
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 32U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 32U, value);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001D51B File Offset: 0x0001B71B
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0001D52D File Offset: 0x0001B72D
		public bool IsAbstract
		{
			get
			{
				return this.attributes.GetAttributes(128U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128U, value);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001D546 File Offset: 0x0001B746
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001D558 File Offset: 0x0001B758
		public bool IsSealed
		{
			get
			{
				return this.attributes.GetAttributes(256U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256U, value);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0001D571 File Offset: 0x0001B771
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0001D583 File Offset: 0x0001B783
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024U, value);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001D59C File Offset: 0x0001B79C
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0001D5AE File Offset: 0x0001B7AE
		public bool IsImport
		{
			get
			{
				return this.attributes.GetAttributes(4096U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096U, value);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0001D5C7 File Offset: 0x0001B7C7
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0001D5D9 File Offset: 0x0001B7D9
		public bool IsSerializable
		{
			get
			{
				return this.attributes.GetAttributes(8192U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192U, value);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001D5F2 File Offset: 0x0001B7F2
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0001D604 File Offset: 0x0001B804
		public bool IsWindowsRuntime
		{
			get
			{
				return this.attributes.GetAttributes(16384U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16384U, value);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001D61D File Offset: 0x0001B81D
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0001D630 File Offset: 0x0001B830
		public bool IsAnsiClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 0U, value);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001D64A File Offset: 0x0001B84A
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0001D661 File Offset: 0x0001B861
		public bool IsUnicodeClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 65536U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 65536U, value);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001D67F File Offset: 0x0001B87F
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0001D696 File Offset: 0x0001B896
		public bool IsAutoClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 131072U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 131072U, value);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0001D6C6 File Offset: 0x0001B8C6
		public bool IsBeforeFieldInit
		{
			get
			{
				return this.attributes.GetAttributes(1048576U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1048576U, value);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001D6DF File Offset: 0x0001B8DF
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0001D6F1 File Offset: 0x0001B8F1
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(2048U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2048U, value);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001D70A File Offset: 0x0001B90A
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0001D71C File Offset: 0x0001B91C
		public bool HasSecurity
		{
			get
			{
				return this.attributes.GetAttributes(262144U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(262144U, value);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001D735 File Offset: 0x0001B935
		public bool IsEnum
		{
			get
			{
				return this.base_type != null && this.base_type.IsTypeOf("System", "Enum");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001D758 File Offset: 0x0001B958
		public override bool IsValueType
		{
			get
			{
				return this.base_type != null && (this.base_type.IsTypeOf("System", "Enum") || (this.base_type.IsTypeOf("System", "ValueType") && !this.IsTypeOf("System", "Enum")));
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001D7B4 File Offset: 0x0001B9B4
		public override bool IsPrimitive
		{
			get
			{
				ElementType elementType;
				return MetadataSystem.TryGetPrimitiveElementType(this, out elementType);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		public override MetadataType MetadataType
		{
			get
			{
				ElementType result;
				if (MetadataSystem.TryGetPrimitiveElementType(this, out result))
				{
					return (MetadataType)result;
				}
				return base.MetadataType;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001D7EB File Offset: 0x0001B9EB
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001D7EE File Offset: 0x0001B9EE
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0001D7FB File Offset: 0x0001B9FB
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

		// Token: 0x06000968 RID: 2408 RVA: 0x0001D804 File Offset: 0x0001BA04
		public TypeDefinition(string @namespace, string name, TypeAttributes attributes) : base(@namespace, name)
		{
			this.attributes = (uint)attributes;
			this.token = new MetadataToken(TokenType.TypeDef);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001D835 File Offset: 0x0001BA35
		public TypeDefinition(string @namespace, string name, TypeAttributes attributes, TypeReference baseType) : this(@namespace, name, attributes)
		{
			this.BaseType = baseType;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001D848 File Offset: 0x0001BA48
		public override TypeDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040005CC RID: 1484
		private uint attributes;

		// Token: 0x040005CD RID: 1485
		private TypeReference base_type;

		// Token: 0x040005CE RID: 1486
		internal Range fields_range;

		// Token: 0x040005CF RID: 1487
		internal Range methods_range;

		// Token: 0x040005D0 RID: 1488
		private short packing_size = -2;

		// Token: 0x040005D1 RID: 1489
		private int class_size = -2;

		// Token: 0x040005D2 RID: 1490
		private Collection<TypeReference> interfaces;

		// Token: 0x040005D3 RID: 1491
		private Collection<TypeDefinition> nested_types;

		// Token: 0x040005D4 RID: 1492
		private Collection<MethodDefinition> methods;

		// Token: 0x040005D5 RID: 1493
		private Collection<FieldDefinition> fields;

		// Token: 0x040005D6 RID: 1494
		private Collection<EventDefinition> events;

		// Token: 0x040005D7 RID: 1495
		private Collection<PropertyDefinition> properties;

		// Token: 0x040005D8 RID: 1496
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040005D9 RID: 1497
		private Collection<SecurityDeclaration> security_declarations;
	}
}
