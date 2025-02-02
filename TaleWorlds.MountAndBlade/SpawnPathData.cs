using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000212 RID: 530
	public struct SpawnPathData
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0006693A File Offset: 0x00064B3A
		public bool IsValid
		{
			get
			{
				return this.Path != null && this.Path.NumberOfPoints > 1;
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0006695A File Offset: 0x00064B5A
		public SpawnPathData(Path path = null, SpawnPathOrientation orientation = SpawnPathOrientation.PathCenter, float centerRatio = 0f, bool isInverted = false)
		{
			this.Path = path;
			this.Orientation = orientation;
			this.CenterRatio = MathF.Clamp(centerRatio, 0f, 1f);
			this.IsInverted = isInverted;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00066988 File Offset: 0x00064B88
		public SpawnPathData Invert()
		{
			return new SpawnPathData(this.Path, this.Orientation, MathF.Max(1f - this.CenterRatio, 0f), !this.IsInverted);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000669BC File Offset: 0x00064BBC
		public MatrixFrame GetSpawnFrame(float offset = 0f)
		{
			MatrixFrame matrixFrame = MatrixFrame.Identity;
			if (this.IsValid)
			{
				float num = MathF.Clamp(this.CenterRatio + offset, 0f, 1f);
				num = (this.IsInverted ? (1f - num) : num);
				float distance = this.Path.GetTotalLength() * num;
				bool isInverted = this.IsInverted;
				matrixFrame = this.Path.GetNearestFrameWithValidAlphaForDistance(distance, isInverted, 0.5f);
				matrixFrame.rotation.f = (this.IsInverted ? (-matrixFrame.rotation.f) : matrixFrame.rotation.f);
				matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			}
			return matrixFrame;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00066A74 File Offset: 0x00064C74
		public void GetOrientedSpawnPathPosition(out Vec2 spawnPathPosition, out Vec2 spawnPathDirection, float pathOffset = 0f)
		{
			if (!this.IsValid)
			{
				spawnPathPosition = Vec2.Invalid;
				spawnPathDirection = Vec2.Invalid;
				return;
			}
			spawnPathPosition = this.GetSpawnFrame(pathOffset).origin.AsVec2;
			if (this.Orientation == SpawnPathOrientation.PathCenter)
			{
				Vec2 asVec = this.Invert().GetSpawnFrame(pathOffset).origin.AsVec2;
				spawnPathDirection = (asVec - spawnPathPosition).Normalized();
				return;
			}
			float offset = ((pathOffset >= 0f) ? 1f : 0f) * MathF.Max(0.01f, Math.Abs(pathOffset));
			Vec2 asVec2 = this.GetSpawnFrame(offset).origin.AsVec2;
			spawnPathDirection = (asVec2 - spawnPathPosition).Normalized();
		}

		// Token: 0x04000970 RID: 2416
		public static readonly SpawnPathData Invalid = new SpawnPathData(null, SpawnPathOrientation.PathCenter, 0f, false);

		// Token: 0x04000971 RID: 2417
		public readonly Path Path;

		// Token: 0x04000972 RID: 2418
		public readonly bool IsInverted;

		// Token: 0x04000973 RID: 2419
		public readonly float CenterRatio;

		// Token: 0x04000974 RID: 2420
		public readonly SpawnPathOrientation Orientation;
	}
}
