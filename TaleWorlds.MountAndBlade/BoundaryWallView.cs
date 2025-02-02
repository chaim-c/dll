using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000318 RID: 792
	public class BoundaryWallView : ScriptComponentBehavior
	{
		// Token: 0x06002ADF RID: 10975 RVA: 0x000A5D64 File Offset: 0x000A3F64
		protected internal override void OnInit()
		{
			throw new Exception("This should only be used in editor.");
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000A5D70 File Offset: 0x000A3F70
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.timer += dt;
			if (!MBEditor.BorderHelpersEnabled())
			{
				return;
			}
			if (this.timer < 0.2f)
			{
				return;
			}
			this.timer = 0f;
			if (base.Scene == null)
			{
				return;
			}
			bool flag = this.CalculateBoundaries("walk_area_vertex", ref this._lastPoints);
			bool flag2 = this.CalculateBoundaries("defender_area_vertex", ref this._lastDefenderPoints);
			bool flag3 = this.CalculateBoundaries("attacker_area_vertex", ref this._lastAttackerPoints);
			if (this._lastPoints.Count >= 3 || this._lastDefenderPoints.Count >= 3 || this._lastAttackerPoints.Count >= 3)
			{
				if (flag || flag2 || flag3)
				{
					base.GameEntity.ClearEntityComponents(true, false, true);
					base.GameEntity.Name = "editor_map_border";
					Mesh mesh = BoundaryWallView.CreateBoundaryMesh(base.Scene, this._lastPoints, 536918784U);
					if (mesh != null)
					{
						base.GameEntity.AddMesh(mesh, true);
					}
					Color color = new Color(0f, 0f, 0.8f, 1f);
					Mesh mesh2 = BoundaryWallView.CreateBoundaryMesh(base.Scene, this._lastDefenderPoints, color.ToUnsignedInteger());
					if (mesh2 != null)
					{
						base.GameEntity.AddMesh(mesh2, true);
					}
					Color color2 = new Color(0f, 0.8f, 0.8f, 1f);
					Mesh mesh3 = BoundaryWallView.CreateBoundaryMesh(base.Scene, this._lastAttackerPoints, color2.ToUnsignedInteger());
					if (mesh3 != null)
					{
						base.GameEntity.AddMesh(mesh3, true);
						return;
					}
				}
			}
			else
			{
				base.GameEntity.ClearEntityComponents(true, false, true);
			}
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000A5F24 File Offset: 0x000A4124
		private bool CalculateBoundaries(string vertexTag, ref List<Vec2> lastPoints)
		{
			IEnumerable<GameEntity> source = base.Scene.FindEntitiesWithTag(vertexTag);
			source = from e in source
			where !e.EntityFlags.HasAnyFlag(EntityFlags.DontSaveToScene)
			select e;
			int num = source.Count<GameEntity>();
			bool flag = false;
			if (num >= 3)
			{
				List<Vec2> list = (from e in source
				select e.GlobalPosition.AsVec2).ToList<Vec2>();
				Vec2 mid = new Vec2(list.Average((Vec2 p) => p.x), list.Average((Vec2 p) => p.y));
				list = (from p in list
				orderby MathF.Atan2(p.x - mid.x, p.y - mid.y)
				select p).ToList<Vec2>();
				if (lastPoints != null && lastPoints.Count == list.Count)
				{
					flag = true;
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] != lastPoints[i])
						{
							flag = false;
							break;
						}
					}
				}
				lastPoints = list;
				return !flag;
			}
			return false;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000A6060 File Offset: 0x000A4260
		public static Mesh CreateBoundaryMesh(Scene scene, ICollection<Vec2> boundaryPoints, uint meshColor = 536918784U)
		{
			if (boundaryPoints == null || boundaryPoints.Count < 3)
			{
				return null;
			}
			Mesh mesh = Mesh.CreateMesh(true);
			UIntPtr uintPtr = mesh.LockEditDataWrite();
			Vec3 vec;
			Vec3 vec2;
			scene.GetBoundingBox(out vec, out vec2);
			vec2.z += 50f;
			vec.z -= 50f;
			for (int i = 0; i < boundaryPoints.Count; i++)
			{
				Vec2 point = boundaryPoints.ElementAt(i);
				Vec2 point2 = boundaryPoints.ElementAt((i + 1) % boundaryPoints.Count);
				float z = 0f;
				float z2 = 0f;
				if (!scene.IsAtmosphereIndoor)
				{
					if (!scene.GetHeightAtPoint(point, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref z))
					{
						MBDebug.ShowWarning("GetHeightAtPoint failed at CreateBoundaryEntity!");
						return null;
					}
					if (!scene.GetHeightAtPoint(point2, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref z2))
					{
						MBDebug.ShowWarning("GetHeightAtPoint failed at CreateBoundaryEntity!");
						return null;
					}
				}
				else
				{
					z = vec.z;
					z2 = vec.z;
				}
				Vec3 vec3 = point.ToVec3(z);
				Vec3 vec4 = point2.ToVec3(z2);
				Vec3 v = Vec3.Up * 2f;
				Vec3 vec5 = vec3;
				Vec3 vec6 = vec4;
				Vec3 vec7 = vec3;
				Vec3 vec8 = vec4;
				vec5.z = MathF.Min(vec5.z, vec.z);
				vec6.z = MathF.Min(vec6.z, vec.z);
				vec7.z = MathF.Max(vec7.z, vec2.z);
				vec8.z = MathF.Max(vec8.z, vec2.z);
				vec5 -= v;
				vec6 -= v;
				vec7 += v;
				vec8 += v;
				mesh.AddTriangle(vec5, vec6, vec7, Vec2.Zero, Vec2.Side, Vec2.Forward, meshColor, uintPtr);
				mesh.AddTriangle(vec7, vec6, vec8, Vec2.Forward, Vec2.Side, Vec2.One, meshColor, uintPtr);
			}
			mesh.SetMaterial("editor_map_border");
			mesh.VisibilityMask = (VisibilityMaskFlags.Final | VisibilityMaskFlags.EditModeBorders);
			mesh.SetColorAlpha(150U);
			mesh.SetMeshRenderOrder(250);
			mesh.CullingMode = MBMeshCullingMode.None;
			float vectorArgument = 25f;
			if (MBEditor.IsEditModeOn && scene.IsEditorScene())
			{
				vectorArgument = 100000f;
			}
			IEnumerable<GameEntity> source = scene.FindEntitiesWithTag("walk_area_vertex");
			float num;
			if (!source.Any<GameEntity>())
			{
				num = 0f;
			}
			else
			{
				num = source.Average((GameEntity ent) => ent.GlobalPosition.z);
			}
			float vectorArgument2 = num;
			mesh.SetVectorArgument(vectorArgument, vectorArgument2, 0f, 0f);
			mesh.ComputeNormals();
			mesh.ComputeTangents();
			mesh.RecomputeBoundingBox();
			mesh.UnlockEditDataWrite(uintPtr);
			return mesh;
		}

		// Token: 0x04001080 RID: 4224
		private List<Vec2> _lastPoints = new List<Vec2>();

		// Token: 0x04001081 RID: 4225
		private List<Vec2> _lastAttackerPoints = new List<Vec2>();

		// Token: 0x04001082 RID: 4226
		private List<Vec2> _lastDefenderPoints = new List<Vec2>();

		// Token: 0x04001083 RID: 4227
		private float timer;
	}
}
