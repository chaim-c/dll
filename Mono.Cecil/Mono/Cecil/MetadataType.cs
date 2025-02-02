using System;

namespace Mono.Cecil
{
	// Token: 0x020000EE RID: 238
	public enum MetadataType : byte
	{
		// Token: 0x040005E8 RID: 1512
		Void = 1,
		// Token: 0x040005E9 RID: 1513
		Boolean,
		// Token: 0x040005EA RID: 1514
		Char,
		// Token: 0x040005EB RID: 1515
		SByte,
		// Token: 0x040005EC RID: 1516
		Byte,
		// Token: 0x040005ED RID: 1517
		Int16,
		// Token: 0x040005EE RID: 1518
		UInt16,
		// Token: 0x040005EF RID: 1519
		Int32,
		// Token: 0x040005F0 RID: 1520
		UInt32,
		// Token: 0x040005F1 RID: 1521
		Int64,
		// Token: 0x040005F2 RID: 1522
		UInt64,
		// Token: 0x040005F3 RID: 1523
		Single,
		// Token: 0x040005F4 RID: 1524
		Double,
		// Token: 0x040005F5 RID: 1525
		String,
		// Token: 0x040005F6 RID: 1526
		Pointer,
		// Token: 0x040005F7 RID: 1527
		ByReference,
		// Token: 0x040005F8 RID: 1528
		ValueType,
		// Token: 0x040005F9 RID: 1529
		Class,
		// Token: 0x040005FA RID: 1530
		Var,
		// Token: 0x040005FB RID: 1531
		Array,
		// Token: 0x040005FC RID: 1532
		GenericInstance,
		// Token: 0x040005FD RID: 1533
		TypedByReference,
		// Token: 0x040005FE RID: 1534
		IntPtr = 24,
		// Token: 0x040005FF RID: 1535
		UIntPtr,
		// Token: 0x04000600 RID: 1536
		FunctionPointer = 27,
		// Token: 0x04000601 RID: 1537
		Object,
		// Token: 0x04000602 RID: 1538
		MVar = 30,
		// Token: 0x04000603 RID: 1539
		RequiredModifier,
		// Token: 0x04000604 RID: 1540
		OptionalModifier,
		// Token: 0x04000605 RID: 1541
		Sentinel = 65,
		// Token: 0x04000606 RID: 1542
		Pinned = 69
	}
}
