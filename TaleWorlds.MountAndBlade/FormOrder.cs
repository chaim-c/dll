using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000151 RID: 337
	public struct FormOrder
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00035333 File Offset: 0x00033533
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x0003533B File Offset: 0x0003353B
		public float CustomFlankWidth
		{
			get
			{
				return this._customFlankWidth;
			}
			set
			{
				this._customFlankWidth = value;
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00035344 File Offset: 0x00033544
		private FormOrder(FormOrder.FormOrderEnum orderEnum, float customFlankWidth = -1f)
		{
			this.OrderEnum = orderEnum;
			this._customFlankWidth = customFlankWidth;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00035354 File Offset: 0x00033554
		public static FormOrder FormOrderCustom(float customWidth)
		{
			return new FormOrder(FormOrder.FormOrderEnum.Custom, customWidth);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00035360 File Offset: 0x00033560
		public OrderType OrderType
		{
			get
			{
				switch (this.OrderEnum)
				{
				case FormOrder.FormOrderEnum.Wide:
					return OrderType.FormWide;
				case FormOrder.FormOrderEnum.Wider:
					return OrderType.FormWider;
				case FormOrder.FormOrderEnum.Custom:
					return OrderType.FormCustom;
				default:
					return OrderType.FormDeep;
				}
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00035395 File Offset: 0x00033595
		public void OnApply(Formation formation)
		{
			this.OnApplyToArrangement(formation, formation.Arrangement);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000353A4 File Offset: 0x000335A4
		public static int GetUnitCountOf(Formation formation)
		{
			if (formation.OverridenUnitCount == null)
			{
				return formation.CountOfUnitsWithoutDetachedOnes;
			}
			return formation.OverridenUnitCount.Value;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000353D6 File Offset: 0x000335D6
		public bool OnApplyToCustomArrangement(Formation formation, IFormationArrangement arrangement)
		{
			return false;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000353DC File Offset: 0x000335DC
		private void OnApplyToArrangement(Formation formation, IFormationArrangement arrangement)
		{
			if (!this.OnApplyToCustomArrangement(formation, arrangement))
			{
				if (arrangement is ColumnFormation)
				{
					ColumnFormation columnFormation = arrangement as ColumnFormation;
					if (FormOrder.GetUnitCountOf(formation) > 0)
					{
						columnFormation.FormFromWidth((float)this.GetRankVerticalFormFileCount(formation));
						return;
					}
				}
				else
				{
					if (arrangement is RectilinearSchiltronFormation)
					{
						(arrangement as RectilinearSchiltronFormation).Form();
						return;
					}
					if (arrangement is CircularSchiltronFormation)
					{
						(arrangement as CircularSchiltronFormation).Form();
						return;
					}
					if (arrangement is CircularFormation)
					{
						CircularFormation circularFormation = arrangement as CircularFormation;
						int unitCountOf = FormOrder.GetUnitCountOf(formation);
						int? fileCount = this.GetFileCount(unitCountOf);
						float circumference;
						if (fileCount != null)
						{
							int rankCount = MathF.Max(1, MathF.Ceiling((float)unitCountOf * 1f / (float)fileCount.Value));
							circumference = circularFormation.GetCircumferenceFromRankCount(rankCount);
						}
						else
						{
							circumference = 3.1415927f * this.CustomFlankWidth;
						}
						circularFormation.FormFromCircumference(circumference);
						return;
					}
					if (arrangement is SquareFormation)
					{
						SquareFormation squareFormation = arrangement as SquareFormation;
						int unitCountOf2 = FormOrder.GetUnitCountOf(formation);
						int? fileCount2 = this.GetFileCount(unitCountOf2);
						if (fileCount2 != null)
						{
							int rankCount2 = MathF.Max(1, MathF.Ceiling((float)unitCountOf2 * 1f / (float)fileCount2.Value));
							squareFormation.FormFromRankCount(rankCount2);
							return;
						}
						squareFormation.FormFromBorderSideWidth(this.CustomFlankWidth);
						return;
					}
					else if (arrangement is SkeinFormation)
					{
						SkeinFormation skeinFormation = arrangement as SkeinFormation;
						int unitCountOf3 = FormOrder.GetUnitCountOf(formation);
						int? fileCount3 = this.GetFileCount(unitCountOf3);
						if (fileCount3 != null)
						{
							skeinFormation.FormFromFlankWidth(fileCount3.Value, false);
							return;
						}
						skeinFormation.FlankWidth = this.CustomFlankWidth;
						return;
					}
					else if (arrangement is WedgeFormation)
					{
						WedgeFormation wedgeFormation = arrangement as WedgeFormation;
						int unitCountOf4 = FormOrder.GetUnitCountOf(formation);
						int? fileCount4 = this.GetFileCount(unitCountOf4);
						if (fileCount4 != null)
						{
							wedgeFormation.FormFromFlankWidth(fileCount4.Value, false);
							return;
						}
						wedgeFormation.FlankWidth = this.CustomFlankWidth;
						return;
					}
					else if (arrangement is TransposedLineFormation)
					{
						TransposedLineFormation transposedLineFormation = arrangement as TransposedLineFormation;
						int unitCountOf5 = FormOrder.GetUnitCountOf(formation);
						if (unitCountOf5 > 0)
						{
							int? fileCount5 = this.GetFileCount(unitCountOf5);
							if (fileCount5 == null)
							{
								fileCount5 = new int?(transposedLineFormation.GetFileCountFromWidth(this.CustomFlankWidth));
							}
							MathF.Ceiling((float)unitCountOf5 * 1f / (float)fileCount5.Value);
							transposedLineFormation.FormFromFlankWidth(this.GetRankVerticalFormFileCount(formation), false);
							return;
						}
					}
					else if (arrangement is LineFormation)
					{
						LineFormation lineFormation = arrangement as LineFormation;
						int unitCountOf6 = FormOrder.GetUnitCountOf(formation);
						int? fileCount6 = this.GetFileCount(unitCountOf6);
						if (fileCount6 != null)
						{
							lineFormation.FormFromFlankWidth(fileCount6.Value, unitCountOf6 > 40);
							return;
						}
						lineFormation.FlankWidth = this.CustomFlankWidth;
						return;
					}
					else
					{
						Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Orders\\FormOrder.cs", "OnApplyToArrangement", 224);
					}
				}
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00035681 File Offset: 0x00033881
		private int? GetFileCount(int unitCount)
		{
			return FormOrder.GetFileCountStatic(this.OrderEnum, unitCount);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003568F File Offset: 0x0003388F
		public static int? GetFileCountStatic(FormOrder.FormOrderEnum order, int unitCount)
		{
			return FormOrder.GetFileCountAux(order, unitCount);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00035698 File Offset: 0x00033898
		private int GetRankVerticalFormFileCount(IFormation formation)
		{
			switch (this.OrderEnum)
			{
			case FormOrder.FormOrderEnum.Deep:
				return 1;
			case FormOrder.FormOrderEnum.Wide:
				return 3;
			case FormOrder.FormOrderEnum.Wider:
				return 5;
			case FormOrder.FormOrderEnum.Custom:
				return MathF.Floor((this._customFlankWidth + formation.Interval) / (formation.UnitDiameter + formation.Interval));
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Orders\\FormOrder.cs", "GetRankVerticalFormFileCount", 265);
				return 1;
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00035708 File Offset: 0x00033908
		private static int? GetFileCountAux(FormOrder.FormOrderEnum order, int unitCount)
		{
			switch (order)
			{
			case FormOrder.FormOrderEnum.Deep:
				return new int?(MathF.Max(MathF.Round(MathF.Sqrt((float)unitCount / 4f)), 1) * 4);
			case FormOrder.FormOrderEnum.Wide:
				return new int?(MathF.Max(MathF.Round(MathF.Sqrt((float)unitCount / 16f)), 1) * 16);
			case FormOrder.FormOrderEnum.Wider:
				return new int?(MathF.Max(MathF.Round(MathF.Sqrt((float)unitCount / 64f)), 1) * 64);
			case FormOrder.FormOrderEnum.Custom:
				return null;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Orders\\FormOrder.cs", "GetFileCountAux", 285);
				return null;
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000357BC File Offset: 0x000339BC
		public override bool Equals(object obj)
		{
			if (obj is FormOrder)
			{
				FormOrder f = (FormOrder)obj;
				return f == this;
			}
			return false;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000357E8 File Offset: 0x000339E8
		public override int GetHashCode()
		{
			return (int)this.OrderEnum;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000357F0 File Offset: 0x000339F0
		public static bool operator !=(FormOrder f1, FormOrder f2)
		{
			return f1.OrderEnum != f2.OrderEnum;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00035803 File Offset: 0x00033A03
		public static bool operator ==(FormOrder f1, FormOrder f2)
		{
			return f1.OrderEnum == f2.OrderEnum;
		}

		// Token: 0x04000440 RID: 1088
		private float _customFlankWidth;

		// Token: 0x04000441 RID: 1089
		public readonly FormOrder.FormOrderEnum OrderEnum;

		// Token: 0x04000442 RID: 1090
		public static readonly FormOrder FormOrderDeep = new FormOrder(FormOrder.FormOrderEnum.Deep, -1f);

		// Token: 0x04000443 RID: 1091
		public static readonly FormOrder FormOrderWide = new FormOrder(FormOrder.FormOrderEnum.Wide, -1f);

		// Token: 0x04000444 RID: 1092
		public static readonly FormOrder FormOrderWider = new FormOrder(FormOrder.FormOrderEnum.Wider, -1f);

		// Token: 0x02000446 RID: 1094
		public enum FormOrderEnum
		{
			// Token: 0x040018E5 RID: 6373
			Deep,
			// Token: 0x040018E6 RID: 6374
			Wide,
			// Token: 0x040018E7 RID: 6375
			Wider,
			// Token: 0x040018E8 RID: 6376
			Custom
		}
	}
}
