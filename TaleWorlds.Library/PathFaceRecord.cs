using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000072 RID: 114
	public struct PathFaceRecord
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000D034 File Offset: 0x0000B234
		public PathFaceRecord(int index, int groupIndex, int islandIndex)
		{
			this.FaceIndex = index;
			this.FaceGroupIndex = groupIndex;
			this.FaceIslandIndex = islandIndex;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D04B File Offset: 0x0000B24B
		public bool IsValid()
		{
			return this.FaceIndex != -1;
		}

		// Token: 0x04000125 RID: 293
		public int FaceIndex;

		// Token: 0x04000126 RID: 294
		public int FaceGroupIndex;

		// Token: 0x04000127 RID: 295
		public int FaceIslandIndex;

		// Token: 0x04000128 RID: 296
		public static readonly PathFaceRecord NullFaceRecord = new PathFaceRecord(-1, -1, -1);

		// Token: 0x020000D0 RID: 208
		public struct StackArray6PathFaceRecord
		{
			// Token: 0x170000F1 RID: 241
			public PathFaceRecord this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return this._element0;
					case 1:
						return this._element1;
					case 2:
						return this._element2;
					case 3:
						return this._element3;
					case 4:
						return this._element4;
					case 5:
						return this._element5;
					default:
						Debug.FailedAssert("Index out of range.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\PathFaceRecord.cs", "Item", 34);
						return PathFaceRecord.NullFaceRecord;
					}
				}
				set
				{
					switch (index)
					{
					case 0:
						this._element0 = value;
						return;
					case 1:
						this._element1 = value;
						return;
					case 2:
						this._element2 = value;
						return;
					case 3:
						this._element3 = value;
						return;
					case 4:
						this._element4 = value;
						return;
					case 5:
						this._element5 = value;
						return;
					default:
						Debug.FailedAssert("Index out of range.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\PathFaceRecord.cs", "Item", 50);
						return;
					}
				}
			}

			// Token: 0x0400028C RID: 652
			private PathFaceRecord _element0;

			// Token: 0x0400028D RID: 653
			private PathFaceRecord _element1;

			// Token: 0x0400028E RID: 654
			private PathFaceRecord _element2;

			// Token: 0x0400028F RID: 655
			private PathFaceRecord _element3;

			// Token: 0x04000290 RID: 656
			private PathFaceRecord _element4;

			// Token: 0x04000291 RID: 657
			private PathFaceRecord _element5;

			// Token: 0x04000292 RID: 658
			public const int Length = 6;
		}
	}
}
