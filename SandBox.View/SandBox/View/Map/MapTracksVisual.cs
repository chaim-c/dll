using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.View.Map
{
	// Token: 0x02000051 RID: 81
	public class MapTracksVisual : CampaignEntityVisualComponent
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0001C538 File Offset: 0x0001A738
		public MapTracksVisual()
		{
			this._trackEntityPool = new List<GameEntity>();
			this._parallelUpdateTrackColorsPredicate = new TWParallel.ParallelForAuxPredicate(this.ParallelUpdateTrackColors);
			this._parallelMakeTrackPoolElementsInvisiblePredicate = new TWParallel.ParallelForAuxPredicate(this.ParallelMakeTrackPoolElementsInvisible);
			this._parallelUpdateTrackPoolPositionsPredicate = new TWParallel.ParallelForAuxPredicate(this.ParallelUpdateTrackPoolPositions);
			this._parallelUpdateVisibleTracksPredicate = new TWParallel.ParallelForAuxPredicate(this.ParallelUpdateVisibleTracks);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
		public Scene MapScene
		{
			get
			{
				if (this._mapScene == null && Campaign.Current != null && Campaign.Current.MapSceneWrapper != null)
				{
					this._mapScene = ((MapScene)Campaign.Current.MapSceneWrapper).Scene;
				}
				return this._mapScene;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001C5F6 File Offset: 0x0001A7F6
		protected override void OnInitialize()
		{
			base.OnInitialize();
			CampaignEvents.TrackDetectedEvent.AddNonSerializedListener(this, new Action<Track>(this.OnTrackDetected));
			CampaignEvents.TrackLostEvent.AddNonSerializedListener(this, new Action<Track>(this.OnTrackLost));
			this.InitializeObjectPoolWithDefaultCount();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001C632 File Offset: 0x0001A832
		private void OnTrackDetected(Track track)
		{
			this._tracksDirty = true;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001C63B File Offset: 0x0001A83B
		private void OnTrackLost(Track track)
		{
			this._tracksDirty = true;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001C644 File Offset: 0x0001A844
		private void ParallelUpdateTrackColors(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				(this._trackEntityPool[i].GetComponentAtIndex(0, GameEntity.ComponentType.Decal) as Decal).SetFactor1(Campaign.Current.Models.MapTrackModel.GetTrackColor(MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks[i]));
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		private void UpdateTrackMesh()
		{
			int num = this._trackEntityPool.Count - MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks.Count;
			if (num > 0)
			{
				int count = MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks.Count;
				TWParallel.For(count, count + num, this._parallelMakeTrackPoolElementsInvisiblePredicate, 16);
			}
			else
			{
				this.CreateNewTrackPoolElements(-num);
			}
			TWParallel.For(0, MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks.Count, this._parallelUpdateVisibleTracksPredicate, 16);
			TWParallel.For(0, MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks.Count, this._parallelUpdateTrackPoolPositionsPredicate, 16);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001C748 File Offset: 0x0001A948
		private void ParallelUpdateTrackPoolPositions(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				Track track = MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks[i];
				MatrixFrame matrixFrame = this.CalculateTrackFrame(track);
				this._trackEntityPool[i].SetFrame(ref matrixFrame);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001C794 File Offset: 0x0001A994
		private void ParallelUpdateVisibleTracks(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this._trackEntityPool[i].SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		private void ParallelMakeTrackPoolElementsInvisible(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this._trackEntityPool[i].SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001C7EC File Offset: 0x0001A9EC
		private void InitializeObjectPoolWithDefaultCount()
		{
			this.CreateNewTrackPoolElements(5);
			foreach (GameEntity gameEntity in this._trackEntityPool)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001C844 File Offset: 0x0001AA44
		private void CreateNewTrackPoolElements(int delta)
		{
			for (int i = 0; i < delta; i++)
			{
				GameEntity gameEntity = GameEntity.Instantiate(this.MapScene, "map_track_arrow", MatrixFrame.Identity);
				gameEntity.SetVisibilityExcludeParents(true);
				this._trackEntityPool.Add(gameEntity);
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001C886 File Offset: 0x0001AA86
		public override void OnVisualTick(MapScreen screen, float realDt, float dt)
		{
			if (this._tracksDirty)
			{
				this.UpdateTrackMesh();
				this._tracksDirty = false;
			}
			TWParallel.For(0, MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks.Count, this._parallelUpdateTrackColorsPredicate, 16);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001C8C0 File Offset: 0x0001AAC0
		public bool RaySphereIntersection(Ray ray, SphereData sphere, ref Vec3 intersectionPoint)
		{
			Vec3 origin = sphere.Origin;
			float radius = sphere.Radius;
			Vec3 v = origin - ray.Origin;
			float num = Vec3.DotProduct(ray.Direction, v);
			if (num > 0f)
			{
				Vec3 vec = ray.Origin + ray.Direction * num - origin;
				float num2 = radius * radius - vec.LengthSquared;
				if (num2 >= 0f)
				{
					float num3 = MathF.Sqrt(num2);
					float num4 = num - num3;
					if (num4 >= 0f && num4 <= ray.MaxDistance)
					{
						intersectionPoint = ray.Origin + ray.Direction * num4;
						return true;
					}
					if (num4 < 0f)
					{
						intersectionPoint = ray.Origin;
						return true;
					}
				}
			}
			else if ((ray.Origin - origin).LengthSquared < radius * radius)
			{
				intersectionPoint = ray.Origin;
				return true;
			}
			return false;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
		public Track GetTrackOnMouse(Ray mouseRay, Vec3 mouseIntersectionPoint)
		{
			Track result = null;
			for (int i = 0; i < MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks.Count; i++)
			{
				Track track = MapScreen.Instance.MapTracksCampaignBehavior.DetectedTracks[i];
				float trackScale = Campaign.Current.Models.MapTrackModel.GetTrackScale(track);
				MatrixFrame matrixFrame = this.CalculateTrackFrame(track);
				float lengthSquared = (matrixFrame.origin - mouseIntersectionPoint).LengthSquared;
				if (lengthSquared < 0.1f)
				{
					float num = MathF.Sqrt(lengthSquared);
					this._trackSphere.Origin = matrixFrame.origin;
					this._trackSphere.Radius = 0.05f + num * 0.01f + trackScale;
					Vec3 vec = default(Vec3);
					if (this.RaySphereIntersection(mouseRay, this._trackSphere, ref vec))
					{
						result = track;
					}
				}
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		private MatrixFrame CalculateTrackFrame(Track track)
		{
			Vec3 origin = track.Position.ToVec3(0f);
			float scale = track.Scale;
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = origin;
			float num;
			Vec3 u;
			Campaign.Current.MapSceneWrapper.GetTerrainHeightAndNormal(identity.origin.AsVec2, out num, out u);
			identity.origin.z = num + 0.01f;
			identity.rotation.u = u;
			Vec2 asVec = identity.rotation.f.AsVec2;
			asVec.RotateCCW(track.Direction);
			identity.rotation.f = new Vec3(asVec.x, asVec.y, identity.rotation.f.z, -1f);
			identity.rotation.s = Vec3.CrossProduct(identity.rotation.f, identity.rotation.u);
			identity.rotation.s.Normalize();
			identity.rotation.f = Vec3.CrossProduct(identity.rotation.u, identity.rotation.s);
			identity.rotation.f.Normalize();
			float f = scale;
			identity.rotation.s = identity.rotation.s * f;
			identity.rotation.f = identity.rotation.f * f;
			identity.rotation.u = identity.rotation.u * f;
			return identity;
		}

		// Token: 0x040001D1 RID: 465
		private const string TrackPrefabName = "map_track_arrow";

		// Token: 0x040001D2 RID: 466
		private const int DefaultObjectPoolCount = 5;

		// Token: 0x040001D3 RID: 467
		private readonly List<GameEntity> _trackEntityPool;

		// Token: 0x040001D4 RID: 468
		private SphereData _trackSphere;

		// Token: 0x040001D5 RID: 469
		private Scene _mapScene;

		// Token: 0x040001D6 RID: 470
		private bool _tracksDirty = true;

		// Token: 0x040001D7 RID: 471
		private readonly TWParallel.ParallelForAuxPredicate _parallelUpdateTrackColorsPredicate;

		// Token: 0x040001D8 RID: 472
		private readonly TWParallel.ParallelForAuxPredicate _parallelMakeTrackPoolElementsInvisiblePredicate;

		// Token: 0x040001D9 RID: 473
		private readonly TWParallel.ParallelForAuxPredicate _parallelUpdateTrackPoolPositionsPredicate;

		// Token: 0x040001DA RID: 474
		private readonly TWParallel.ParallelForAuxPredicate _parallelUpdateVisibleTracksPredicate;
	}
}
