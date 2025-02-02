using System;

namespace Mono.Cecil
{
	// Token: 0x0200009B RID: 155
	public class ExportedType : IMetadataTokenProvider
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00017103 File Offset: 0x00015303
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0001710B File Offset: 0x0001530B
		public string Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00017114 File Offset: 0x00015314
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001711C File Offset: 0x0001531C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00017125 File Offset: 0x00015325
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001712D File Offset: 0x0001532D
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00017136 File Offset: 0x00015336
		public IMetadataScope Scope
		{
			get
			{
				if (this.declaring_type != null)
				{
					return this.declaring_type.Scope;
				}
				return this.scope;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00017152 File Offset: 0x00015352
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0001715A File Offset: 0x0001535A
		public ExportedType DeclaringType
		{
			get
			{
				return this.declaring_type;
			}
			set
			{
				this.declaring_type = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00017163 File Offset: 0x00015363
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0001716B File Offset: 0x0001536B
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00017174 File Offset: 0x00015374
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001717C File Offset: 0x0001537C
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00017185 File Offset: 0x00015385
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00017194 File Offset: 0x00015394
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000171AA File Offset: 0x000153AA
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x000171B9 File Offset: 0x000153B9
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000171CF File Offset: 0x000153CF
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x000171DE File Offset: 0x000153DE
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000171F4 File Offset: 0x000153F4
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00017203 File Offset: 0x00015403
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00017219 File Offset: 0x00015419
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00017228 File Offset: 0x00015428
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0001723E File Offset: 0x0001543E
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0001724D File Offset: 0x0001544D
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00017263 File Offset: 0x00015463
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00017272 File Offset: 0x00015472
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00017288 File Offset: 0x00015488
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00017297 File Offset: 0x00015497
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000172AD File Offset: 0x000154AD
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x000172BD File Offset: 0x000154BD
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000172D4 File Offset: 0x000154D4
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x000172E4 File Offset: 0x000154E4
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000172FB File Offset: 0x000154FB
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001730C File Offset: 0x0001550C
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00017324 File Offset: 0x00015524
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00017334 File Offset: 0x00015534
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001734B File Offset: 0x0001554B
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001735C File Offset: 0x0001555C
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00017374 File Offset: 0x00015574
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00017386 File Offset: 0x00015586
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001739F File Offset: 0x0001559F
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000173B1 File Offset: 0x000155B1
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000173CA File Offset: 0x000155CA
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x000173DC File Offset: 0x000155DC
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x000173F5 File Offset: 0x000155F5
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00017407 File Offset: 0x00015607
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00017420 File Offset: 0x00015620
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00017432 File Offset: 0x00015632
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001744B File Offset: 0x0001564B
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001745E File Offset: 0x0001565E
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00017478 File Offset: 0x00015678
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001748F File Offset: 0x0001568F
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000174AD File Offset: 0x000156AD
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x000174C4 File Offset: 0x000156C4
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000174E2 File Offset: 0x000156E2
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x000174F4 File Offset: 0x000156F4
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001750D File Offset: 0x0001570D
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0001751F File Offset: 0x0001571F
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00017538 File Offset: 0x00015738
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0001754A File Offset: 0x0001574A
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00017563 File Offset: 0x00015763
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00017575 File Offset: 0x00015775
		public bool IsForwarder
		{
			get
			{
				return this.attributes.GetAttributes(2097152U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2097152U, value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00017590 File Offset: 0x00015790
		public string FullName
		{
			get
			{
				string text = string.IsNullOrEmpty(this.@namespace) ? this.name : (this.@namespace + '.' + this.name);
				if (this.declaring_type != null)
				{
					return this.declaring_type.FullName + "/" + text;
				}
				return text;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000175EB File Offset: 0x000157EB
		public ExportedType(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
		{
			this.@namespace = @namespace;
			this.name = name;
			this.scope = scope;
			this.module = module;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00017610 File Offset: 0x00015810
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00017618 File Offset: 0x00015818
		public TypeDefinition Resolve()
		{
			return this.module.Resolve(this.CreateReference());
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001762C File Offset: 0x0001582C
		internal TypeReference CreateReference()
		{
			return new TypeReference(this.@namespace, this.name, this.module, this.scope)
			{
				DeclaringType = ((this.declaring_type != null) ? this.declaring_type.CreateReference() : null)
			};
		}

		// Token: 0x040003F6 RID: 1014
		private string @namespace;

		// Token: 0x040003F7 RID: 1015
		private string name;

		// Token: 0x040003F8 RID: 1016
		private uint attributes;

		// Token: 0x040003F9 RID: 1017
		private IMetadataScope scope;

		// Token: 0x040003FA RID: 1018
		private ModuleDefinition module;

		// Token: 0x040003FB RID: 1019
		private int identifier;

		// Token: 0x040003FC RID: 1020
		private ExportedType declaring_type;

		// Token: 0x040003FD RID: 1021
		internal MetadataToken token;
	}
}
