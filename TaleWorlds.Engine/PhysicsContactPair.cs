﻿using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000077 RID: 119
	[EngineStruct("rglPhysics_contact_pair", false)]
	public struct PhysicsContactPair
	{
		// Token: 0x1700006F RID: 111
		public PhysicsContactInfo this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Contact0;
				case 1:
					return this.Contact1;
				case 2:
					return this.Contact2;
				case 3:
					return this.Contact3;
				case 4:
					return this.Contact4;
				case 5:
					return this.Contact5;
				case 6:
					return this.Contact6;
				case 7:
					return this.Contact7;
				default:
					return default(PhysicsContactInfo);
				}
			}
		}

		// Token: 0x04000165 RID: 357
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact0;

		// Token: 0x04000166 RID: 358
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact1;

		// Token: 0x04000167 RID: 359
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact2;

		// Token: 0x04000168 RID: 360
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact3;

		// Token: 0x04000169 RID: 361
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact4;

		// Token: 0x0400016A RID: 362
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact5;

		// Token: 0x0400016B RID: 363
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact6;

		// Token: 0x0400016C RID: 364
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactInfo Contact7;

		// Token: 0x0400016D RID: 365
		[CustomEngineStructMemberData("type")]
		public readonly PhysicsEventType ContactEventType;

		// Token: 0x0400016E RID: 366
		public readonly int NumberOfContacts;
	}
}
