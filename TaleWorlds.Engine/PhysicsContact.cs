using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000078 RID: 120
	[EngineStruct("rglPhysics_contact", false)]
	public struct PhysicsContact
	{
		// Token: 0x17000070 RID: 112
		public PhysicsContactPair this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.ContactPair0;
				case 1:
					return this.ContactPair1;
				case 2:
					return this.ContactPair2;
				case 3:
					return this.ContactPair3;
				case 4:
					return this.ContactPair4;
				case 5:
					return this.ContactPair5;
				case 6:
					return this.ContactPair6;
				case 7:
					return this.ContactPair7;
				case 8:
					return this.ContactPair8;
				case 9:
					return this.ContactPair9;
				case 10:
					return this.ContactPair10;
				case 11:
					return this.ContactPair11;
				case 12:
					return this.ContactPair12;
				case 13:
					return this.ContactPair13;
				case 14:
					return this.ContactPair14;
				case 15:
					return this.ContactPair15;
				default:
					return default(PhysicsContactPair);
				}
			}
		}

		// Token: 0x0400016F RID: 367
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair0;

		// Token: 0x04000170 RID: 368
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair1;

		// Token: 0x04000171 RID: 369
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair2;

		// Token: 0x04000172 RID: 370
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair3;

		// Token: 0x04000173 RID: 371
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair4;

		// Token: 0x04000174 RID: 372
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair5;

		// Token: 0x04000175 RID: 373
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair6;

		// Token: 0x04000176 RID: 374
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair7;

		// Token: 0x04000177 RID: 375
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair8;

		// Token: 0x04000178 RID: 376
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair9;

		// Token: 0x04000179 RID: 377
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair10;

		// Token: 0x0400017A RID: 378
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair11;

		// Token: 0x0400017B RID: 379
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair12;

		// Token: 0x0400017C RID: 380
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair13;

		// Token: 0x0400017D RID: 381
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair14;

		// Token: 0x0400017E RID: 382
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly PhysicsContactPair ContactPair15;

		// Token: 0x0400017F RID: 383
		public readonly IntPtr body0;

		// Token: 0x04000180 RID: 384
		public readonly IntPtr body1;

		// Token: 0x04000181 RID: 385
		public readonly int NumberOfContactPairs;
	}
}
