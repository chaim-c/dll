using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200002E RID: 46
	public class SpriteCategory
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007985 File Offset: 0x00005B85
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000798D File Offset: 0x00005B8D
		public string Name { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007996 File Offset: 0x00005B96
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000799E File Offset: 0x00005B9E
		public SpriteData SpriteData { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000079A7 File Offset: 0x00005BA7
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000079AF File Offset: 0x00005BAF
		public List<SpritePart> SpriteParts { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000079B8 File Offset: 0x00005BB8
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000079C0 File Offset: 0x00005BC0
		public List<SpritePart> SortedSpritePartList { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000079C9 File Offset: 0x00005BC9
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000079D1 File Offset: 0x00005BD1
		public List<Texture> SpriteSheets { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000079DA File Offset: 0x00005BDA
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000079E2 File Offset: 0x00005BE2
		public int SpriteSheetCount { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000079EB File Offset: 0x00005BEB
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000079F3 File Offset: 0x00005BF3
		public bool IsLoaded { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000079FC File Offset: 0x00005BFC
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00007A04 File Offset: 0x00005C04
		public Vec2i[] SheetSizes { get; set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x00007A10 File Offset: 0x00005C10
		public SpriteCategory(string name, SpriteData spriteData, int spriteSheetCount, bool alwaysLoad = false)
		{
			this.Name = name;
			this.SpriteData = spriteData;
			this.SpriteSheetCount = spriteSheetCount;
			this.AlwaysLoad = alwaysLoad;
			this.SpriteSheets = new List<Texture>();
			this.SpriteParts = new List<SpritePart>();
			this.SortedSpritePartList = new List<SpritePart>();
			this.SheetSizes = new Vec2i[spriteSheetCount];
			this._spritePartComparer = new SpriteCategory.SpriteSizeComparer();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007A78 File Offset: 0x00005C78
		public void Load(ITwoDimensionResourceContext resourceContext, ResourceDepot resourceDepot)
		{
			if (!this.IsLoaded)
			{
				this.IsLoaded = true;
				for (int i = 1; i <= this.SpriteSheetCount; i++)
				{
					Texture item = resourceContext.LoadTexture(resourceDepot, string.Concat(new object[]
					{
						"SpriteSheets\\",
						this.Name,
						"\\",
						this.Name,
						"_",
						i
					}));
					this.SpriteSheets.Add(item);
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public void Unload()
		{
			if (this.IsLoaded)
			{
				this.SpriteSheets.ForEach(delegate(Texture s)
				{
					s.PlatformTexture.Release();
				});
				this.SpriteSheets.Clear();
				this.IsLoaded = false;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007B4C File Offset: 0x00005D4C
		public void InitializePartialLoad()
		{
			if (!this.IsLoaded)
			{
				this.IsLoaded = true;
				for (int i = 1; i <= this.SpriteSheetCount; i++)
				{
					this.SpriteSheets.Add(null);
				}
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007B88 File Offset: 0x00005D88
		public void ReleasePartialLoad()
		{
			if (this.IsLoaded)
			{
				for (int i = 1; i < this.SpriteSheetCount; i++)
				{
					this.PartialUnloadAtIndex(i);
				}
				this.SpriteSheets.Clear();
				this.IsLoaded = false;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public void PartialLoadAtIndex(ITwoDimensionResourceContext resourceContext, ResourceDepot resourceDepot, int sheetIndex)
		{
			if (sheetIndex >= 1 && sheetIndex <= this.SpriteSheetCount && this.IsLoaded && this.SpriteSheets[sheetIndex - 1] == null)
			{
				Texture value = resourceContext.LoadTexture(resourceDepot, string.Concat(new object[]
				{
					"SpriteSheets\\",
					this.Name,
					"\\",
					this.Name,
					"_",
					sheetIndex
				}));
				this.SpriteSheets[sheetIndex - 1] = value;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007C50 File Offset: 0x00005E50
		public void PartialUnloadAtIndex(int sheetIndex)
		{
			if (sheetIndex >= 1 && sheetIndex <= this.SpriteSheetCount && this.IsLoaded && this.SpriteSheets[sheetIndex - 1] != null)
			{
				this.SpriteSheets[sheetIndex - 1].PlatformTexture.Release();
				this.SpriteSheets[sheetIndex - 1] = null;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007CA9 File Offset: 0x00005EA9
		public void SortList()
		{
			this.SortedSpritePartList.Clear();
			this.SortedSpritePartList.AddRange(this.SpriteParts);
			this.SortedSpritePartList.Sort(this._spritePartComparer);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public bool IsCategoryFullyLoaded()
		{
			for (int i = 0; i < this.SpriteSheets.Count; i++)
			{
				Texture texture = this.SpriteSheets[i];
				if (texture != null && !texture.IsLoaded())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000FB RID: 251
		public const int SpriteSheetSize = 4096;

		// Token: 0x04000103 RID: 259
		public readonly bool AlwaysLoad;

		// Token: 0x04000105 RID: 261
		private SpriteCategory.SpriteSizeComparer _spritePartComparer;

		// Token: 0x02000041 RID: 65
		protected class SpriteSizeComparer : IComparer<SpritePart>
		{
			// Token: 0x060002AE RID: 686 RVA: 0x0000A48B File Offset: 0x0000868B
			public int Compare(SpritePart x, SpritePart y)
			{
				return y.Width * y.Height - x.Width * x.Height;
			}
		}
	}
}
