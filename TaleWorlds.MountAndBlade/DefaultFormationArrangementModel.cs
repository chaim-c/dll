using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F7 RID: 503
	public class DefaultFormationArrangementModel : FormationArrangementModel
	{
		// Token: 0x06001C0D RID: 7181 RVA: 0x00061108 File Offset: 0x0005F308
		public override List<FormationArrangementModel.ArrangementPosition> GetBannerBearerPositions(Formation formation, int maxCount)
		{
			List<FormationArrangementModel.ArrangementPosition> list = new List<FormationArrangementModel.ArrangementPosition>();
			LineFormation lineFormation;
			if (formation == null || (lineFormation = (formation.Arrangement as LineFormation)) == null)
			{
				return list;
			}
			DefaultFormationArrangementModel.RelativeFormationPosition[] array = null;
			int fileCount;
			int rankCount;
			lineFormation.GetFormationInfo(out fileCount, out rankCount);
			LineFormation lineFormation2 = lineFormation;
			if (lineFormation2 != null)
			{
				if (lineFormation2 is CircularFormation)
				{
					array = DefaultFormationArrangementModel.BannerBearerCircularFormationPositions;
					goto IL_89;
				}
				if (lineFormation2 is SkeinFormation)
				{
					array = DefaultFormationArrangementModel.BannerBearerSkeinFormationPositions;
					goto IL_89;
				}
				if (lineFormation2 is SquareFormation)
				{
					array = DefaultFormationArrangementModel.BannerBearerSquareFormationPositions;
					goto IL_89;
				}
				if (lineFormation2 is TransposedLineFormation)
				{
					goto IL_89;
				}
				if (lineFormation2 is WedgeFormation)
				{
					goto IL_89;
				}
			}
			array = DefaultFormationArrangementModel.BannerBearerLineFormationPositions;
			IL_89:
			int num = 0;
			if (array != null)
			{
				foreach (DefaultFormationArrangementModel.RelativeFormationPosition relativeFormationPosition in array)
				{
					if (num >= maxCount)
					{
						break;
					}
					FormationArrangementModel.ArrangementPosition arrangementPosition = relativeFormationPosition.GetArrangementPosition(fileCount, rankCount);
					if (DefaultFormationArrangementModel.SearchOccupiedInLineFormation(lineFormation, arrangementPosition.FileIndex, arrangementPosition.RankIndex, fileCount, relativeFormationPosition.FromLeftFile, out arrangementPosition))
					{
						list.Add(arrangementPosition);
						num++;
					}
				}
			}
			return list;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00061208 File Offset: 0x0005F408
		private static bool SearchOccupiedInLineFormation(LineFormation lineFormation, int fileIndex, int rankIndex, int fileCount, bool searchLeftToRight, out FormationArrangementModel.ArrangementPosition foundPosition)
		{
			if (lineFormation.GetUnit(fileIndex, rankIndex) != null)
			{
				foundPosition = new FormationArrangementModel.ArrangementPosition(fileIndex, rankIndex);
				return true;
			}
			int fileIndex2 = MathF.Min(fileIndex + 1, fileCount - 1);
			int fileIndex3 = MathF.Max(fileIndex - 1, 0);
			foundPosition = FormationArrangementModel.ArrangementPosition.Invalid;
			if (searchLeftToRight)
			{
				if (DefaultFormationArrangementModel.SearchOccupiedFileLeftToRight(lineFormation, fileIndex2, rankIndex, fileCount, ref foundPosition))
				{
					return true;
				}
				if (DefaultFormationArrangementModel.SearchOccupiedFileRightToLeft(lineFormation, fileIndex3, rankIndex, ref foundPosition))
				{
					return true;
				}
			}
			else
			{
				if (DefaultFormationArrangementModel.SearchOccupiedFileRightToLeft(lineFormation, fileIndex3, rankIndex, ref foundPosition))
				{
					return true;
				}
				if (DefaultFormationArrangementModel.SearchOccupiedFileLeftToRight(lineFormation, fileIndex2, rankIndex, fileCount, ref foundPosition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00061290 File Offset: 0x0005F490
		private static bool SearchOccupiedFileRightToLeft(LineFormation lineFormation, int fileIndex, int rankIndex, ref FormationArrangementModel.ArrangementPosition foundPosition)
		{
			for (int i = fileIndex; i >= 0; i--)
			{
				if (lineFormation.GetUnit(i, rankIndex) != null)
				{
					foundPosition = new FormationArrangementModel.ArrangementPosition(i, rankIndex);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000612C4 File Offset: 0x0005F4C4
		private static bool SearchOccupiedFileLeftToRight(LineFormation lineFormation, int fileIndex, int rankIndex, int fileCount, ref FormationArrangementModel.ArrangementPosition foundPosition)
		{
			for (int i = fileIndex; i < fileCount; i++)
			{
				if (lineFormation.GetUnit(i, rankIndex) != null)
				{
					foundPosition = new FormationArrangementModel.ArrangementPosition(i, rankIndex);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000913 RID: 2323
		private static readonly DefaultFormationArrangementModel.RelativeFormationPosition[] BannerBearerLineFormationPositions = new DefaultFormationArrangementModel.RelativeFormationPosition[]
		{
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, true, 1, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(false, 0, false, 0, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(false, 0, true, 1, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, true, 1, 0.5f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0.5f, 0f)
		};

		// Token: 0x04000914 RID: 2324
		private static readonly DefaultFormationArrangementModel.RelativeFormationPosition[] BannerBearerCircularFormationPositions = new DefaultFormationArrangementModel.RelativeFormationPosition[]
		{
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0.5f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 1, 0.833f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 1, 0.167f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 2, 0.666f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 2, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 2, 0.333f, 0f)
		};

		// Token: 0x04000915 RID: 2325
		private static readonly DefaultFormationArrangementModel.RelativeFormationPosition[] BannerBearerSkeinFormationPositions = new DefaultFormationArrangementModel.RelativeFormationPosition[]
		{
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0.5f, 0.5f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(false, 0, false, 0, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0f, 0.5f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(false, 0, false, 0, 0f, 0.5f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, true, 1, 0.5f, 0f)
		};

		// Token: 0x04000916 RID: 2326
		private static readonly DefaultFormationArrangementModel.RelativeFormationPosition[] BannerBearerSquareFormationPositions = new DefaultFormationArrangementModel.RelativeFormationPosition[]
		{
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 0, 0.5f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 1, 0.833f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 1, 0.167f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 2, 0.666f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 2, 0f, 0f),
			new DefaultFormationArrangementModel.RelativeFormationPosition(true, 0, false, 2, 0.333f, 0f)
		};

		// Token: 0x020004E4 RID: 1252
		private struct RelativeFormationPosition
		{
			// Token: 0x060037A2 RID: 14242 RVA: 0x000E03C0 File Offset: 0x000DE5C0
			public RelativeFormationPosition(bool fromLeftFile, int fileOffset, bool fromFrontRank, int rankOffset, float fileFractionalOffset = 0f, float rankFractionalOffset = 0f)
			{
				this.FromLeftFile = fromLeftFile;
				this.FileOffset = fileOffset;
				this.FileFractionalOffset = MathF.Clamp(fileFractionalOffset, 0f, 1f);
				this.FromFrontRank = fromFrontRank;
				this.RankOffset = rankOffset;
				this.RankFractionalOffset = MathF.Clamp(rankFractionalOffset, 0f, 1f);
			}

			// Token: 0x060037A3 RID: 14243 RVA: 0x000E0418 File Offset: 0x000DE618
			public FormationArrangementModel.ArrangementPosition GetArrangementPosition(int fileCount, int rankCount)
			{
				if (fileCount <= 0 || rankCount <= 0)
				{
					return FormationArrangementModel.ArrangementPosition.Invalid;
				}
				int num;
				int num2;
				if (this.FromLeftFile)
				{
					num = 1;
					num2 = 0;
				}
				else
				{
					num = -1;
					num2 = fileCount - 1;
				}
				int num3 = MathF.Round((float)this.FileOffset + this.FileFractionalOffset * (float)(fileCount - 1));
				int fileIndex = MBMath.ClampIndex(num2 + num * num3, 0, fileCount);
				int num4;
				int num5;
				if (this.FromFrontRank)
				{
					num4 = 1;
					num5 = 0;
				}
				else
				{
					num4 = -1;
					num5 = rankCount - 1;
				}
				int num6 = MathF.Round((float)this.RankOffset + this.RankFractionalOffset * (float)(rankCount - 1));
				int rankIndex = MBMath.ClampIndex(num5 + num4 * num6, 0, rankCount);
				return new FormationArrangementModel.ArrangementPosition(fileIndex, rankIndex);
			}

			// Token: 0x04001B67 RID: 7015
			public readonly bool FromLeftFile;

			// Token: 0x04001B68 RID: 7016
			public readonly int FileOffset;

			// Token: 0x04001B69 RID: 7017
			public readonly float FileFractionalOffset;

			// Token: 0x04001B6A RID: 7018
			public readonly bool FromFrontRank;

			// Token: 0x04001B6B RID: 7019
			public readonly int RankOffset;

			// Token: 0x04001B6C RID: 7020
			public readonly float RankFractionalOffset;
		}
	}
}
