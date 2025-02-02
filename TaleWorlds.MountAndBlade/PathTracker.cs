using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000327 RID: 807
	public class PathTracker
	{
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000A77F4 File Offset: 0x000A59F4
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000A77FC File Offset: 0x000A59FC
		public float TotalDistanceTraveled { get; set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000A7805 File Offset: 0x000A5A05
		public bool HasChanged
		{
			get
			{
				return this._path != null && this._version < this._path.GetVersion();
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000A782A File Offset: 0x000A5A2A
		public bool IsValid
		{
			get
			{
				return this._path != null;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000A7838 File Offset: 0x000A5A38
		public bool HasReachedEnd
		{
			get
			{
				return this.TotalDistanceTraveled >= this._path.TotalDistance;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000A7850 File Offset: 0x000A5A50
		public float PathTraveledPercentage
		{
			get
			{
				return this.TotalDistanceTraveled / this._path.TotalDistance;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000A7864 File Offset: 0x000A5A64
		public MatrixFrame CurrentFrame
		{
			get
			{
				MatrixFrame frameForDistance = this._path.GetFrameForDistance(this.TotalDistanceTraveled);
				frameForDistance.rotation.RotateAboutUp(3.1415927f);
				frameForDistance.rotation.ApplyScaleLocal(this._initialScale);
				return frameForDistance;
			}
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000A78A7 File Offset: 0x000A5AA7
		public PathTracker(Path path, Vec3 initialScaleOfEntity)
		{
			this._path = path;
			this._initialScale = initialScaleOfEntity;
			if (path != null)
			{
				this.UpdateVersion();
			}
			this.Reset();
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000A78D9 File Offset: 0x000A5AD9
		public void UpdateVersion()
		{
			this._version = this._path.GetVersion();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000A78EC File Offset: 0x000A5AEC
		public bool PathExists()
		{
			return this._path != null;
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000A78FA File Offset: 0x000A5AFA
		public void Advance(float deltaDistance)
		{
			this.TotalDistanceTraveled += deltaDistance;
			this.TotalDistanceTraveled = MathF.Min(this.TotalDistanceTraveled, this._path.TotalDistance);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000A7926 File Offset: 0x000A5B26
		public float GetPathLength()
		{
			return this._path.TotalDistance;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000A7933 File Offset: 0x000A5B33
		public void CurrentFrameAndColor(out MatrixFrame frame, out Vec3 color)
		{
			this._path.GetFrameAndColorForDistance(this.TotalDistanceTraveled, out frame, out color);
			frame.rotation.RotateAboutUp(3.1415927f);
			frame.rotation.ApplyScaleLocal(this._initialScale);
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000A7969 File Offset: 0x000A5B69
		public void Reset()
		{
			this.TotalDistanceTraveled = 0f;
		}

		// Token: 0x040010C2 RID: 4290
		private readonly Path _path;

		// Token: 0x040010C3 RID: 4291
		private readonly Vec3 _initialScale;

		// Token: 0x040010C4 RID: 4292
		private int _version = -1;
	}
}
