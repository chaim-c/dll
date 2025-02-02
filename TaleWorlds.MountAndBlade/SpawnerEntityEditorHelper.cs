using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000344 RID: 836
	public class SpawnerEntityEditorHelper
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000BA14A File Offset: 0x000B834A
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000BA152 File Offset: 0x000B8352
		public bool IsValid { get; private set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000BA15B File Offset: 0x000B835B
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000BA163 File Offset: 0x000B8363
		public GameEntity SpawnedGhostEntity { get; private set; }

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000BA16C File Offset: 0x000B836C
		public SpawnerEntityEditorHelper(ScriptComponentBehavior spawner)
		{
			this.spawner_ = spawner;
			if (this.AddGhostEntity(this.spawner_.GameEntity, this.GetGhostName()) != null)
			{
				this.SyncMatrixFrames(true);
				this.IsValid = true;
				return;
			}
			Debug.FailedAssert("No prefab found. Spawner script will remove itself.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SpawnerEntityEditorHelper.cs", ".ctor", 75);
			spawner.GameEntity.RemoveScriptComponent(this.spawner_.ScriptComponent.Pointer, 11);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000BA210 File Offset: 0x000B8410
		public GameEntity GetGhostEntityOrChild(string name)
		{
			if (this.SpawnedGhostEntity.Name == name)
			{
				return this.SpawnedGhostEntity;
			}
			List<GameEntity> source = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref source);
			GameEntity gameEntity = source.FirstOrDefault((GameEntity x) => x.Name == name);
			if (gameEntity != null)
			{
				return gameEntity;
			}
			return null;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000BA27C File Offset: 0x000B847C
		public void Tick(float dt)
		{
			if (this.SpawnedGhostEntity.Parent != this.spawner_.GameEntity)
			{
				this.IsValid = false;
				this.spawner_.GameEntity.RemoveScriptComponent(this.spawner_.ScriptComponent.Pointer, 12);
			}
			if (this.IsValid)
			{
				if (this.LockGhostParent)
				{
					bool flag = this.SpawnedGhostEntity.GetFrame() != MatrixFrame.Identity;
					MatrixFrame identity = MatrixFrame.Identity;
					this.SpawnedGhostEntity.SetFrame(ref identity);
					if (flag)
					{
						this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
					}
				}
				this.SyncMatrixFrames(false);
				if (this._ghostMovementMode)
				{
					this.UpdateGhostMovement(dt);
				}
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000BA32A File Offset: 0x000B852A
		public void GivePermission(string childName, SpawnerEntityEditorHelper.Permission permission, Action<float> onChangeFunction)
		{
			this._stableChildrenPermissions.Add(Tuple.Create<string, SpawnerEntityEditorHelper.Permission, Action<float>>(childName, permission, onChangeFunction));
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000BA340 File Offset: 0x000B8540
		private void ApplyPermissions()
		{
			using (List<Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>>>.Enumerator enumerator = this._stableChildrenPermissions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>> item = enumerator.Current;
					KeyValuePair<string, MatrixFrame> keyValuePair = this.stableChildrenFrames.Find((KeyValuePair<string, MatrixFrame> x) => x.Key == item.Item1);
					MatrixFrame frame = this.GetGhostEntityOrChild(item.Item1).GetFrame();
					if (!frame.NearlyEquals(keyValuePair.Value, 1E-05f))
					{
						SpawnerEntityEditorHelper.PermissionType typeOfPermission = item.Item2.TypeOfPermission;
						if (typeOfPermission != SpawnerEntityEditorHelper.PermissionType.scale)
						{
							if (typeOfPermission == SpawnerEntityEditorHelper.PermissionType.rotation)
							{
								switch (item.Item2.PermittedAxis)
								{
								case SpawnerEntityEditorHelper.Axis.x:
									if (!frame.rotation.f.NearlyEquals(keyValuePair.Value.rotation.f, 1E-05f) && !frame.rotation.u.NearlyEquals(keyValuePair.Value.rotation.u, 1E-05f) && frame.rotation.s.NearlyEquals(keyValuePair.Value.rotation.s, 1E-05f))
									{
										this.ChangeStableChildMatrixFrame(item.Item1, frame);
										item.Item3(frame.rotation.GetEulerAngles().x);
									}
									break;
								case SpawnerEntityEditorHelper.Axis.y:
									if (!frame.rotation.s.NearlyEquals(keyValuePair.Value.rotation.s, 1E-05f) && !frame.rotation.u.NearlyEquals(keyValuePair.Value.rotation.u, 1E-05f) && frame.rotation.f.NearlyEquals(keyValuePair.Value.rotation.f, 1E-05f))
									{
										this.ChangeStableChildMatrixFrame(item.Item1, frame);
										item.Item3(frame.rotation.GetEulerAngles().y);
									}
									break;
								case SpawnerEntityEditorHelper.Axis.z:
									if (!frame.rotation.f.NearlyEquals(keyValuePair.Value.rotation.f, 1E-05f) && !frame.rotation.s.NearlyEquals(keyValuePair.Value.rotation.s, 1E-05f) && frame.rotation.u.NearlyEquals(keyValuePair.Value.rotation.u, 1E-05f))
									{
										this.ChangeStableChildMatrixFrame(item.Item1, frame);
										item.Item3(frame.rotation.GetEulerAngles().z);
									}
									break;
								}
							}
						}
						else if (frame.origin.NearlyEquals(keyValuePair.Value.origin, 0.0001f))
						{
							Vec3 vec = frame.rotation.f.NormalizedCopy();
							MatrixFrame value = keyValuePair.Value;
							if (vec.NearlyEquals(value.rotation.f.NormalizedCopy(), 0.0001f))
							{
								vec = frame.rotation.u.NormalizedCopy();
								value = keyValuePair.Value;
								if (vec.NearlyEquals(value.rotation.u.NormalizedCopy(), 0.0001f))
								{
									vec = frame.rotation.s.NormalizedCopy();
									value = keyValuePair.Value;
									if (vec.NearlyEquals(value.rotation.s.NormalizedCopy(), 0.0001f))
									{
										switch (item.Item2.PermittedAxis)
										{
										case SpawnerEntityEditorHelper.Axis.x:
											if (!frame.rotation.f.NearlyEquals(keyValuePair.Value.rotation.f, 1E-05f))
											{
												this.ChangeStableChildMatrixFrame(item.Item1, frame);
												item.Item3(frame.rotation.f.Length);
											}
											break;
										case SpawnerEntityEditorHelper.Axis.y:
											if (!frame.rotation.s.NearlyEquals(keyValuePair.Value.rotation.s, 1E-05f))
											{
												this.ChangeStableChildMatrixFrame(item.Item1, frame);
												item.Item3(frame.rotation.s.Length);
											}
											break;
										case SpawnerEntityEditorHelper.Axis.z:
											if (!frame.rotation.u.NearlyEquals(keyValuePair.Value.rotation.u, 1E-05f))
											{
												this.ChangeStableChildMatrixFrame(item.Item1, frame);
												item.Item3(frame.rotation.u.Length);
											}
											break;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000BA898 File Offset: 0x000B8A98
		private void ChangeStableChildMatrixFrame(string childName, MatrixFrame matrixFrame)
		{
			this.stableChildrenFrames.RemoveAll((KeyValuePair<string, MatrixFrame> x) => x.Key == childName);
			KeyValuePair<string, MatrixFrame> item = new KeyValuePair<string, MatrixFrame>(childName, matrixFrame);
			this.stableChildrenFrames.Add(item);
			if (SpawnerEntityEditorHelper.HasField(this.spawner_, childName, true))
			{
				SpawnerEntityEditorHelper.SetSpawnerMatrixFrame(this.spawner_, childName, matrixFrame);
			}
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000BA90B File Offset: 0x000B8B0B
		public void ChangeStableChildMatrixFrameAndApply(string childName, MatrixFrame matrixFrame, bool updateTriad = true)
		{
			this.ChangeStableChildMatrixFrame(childName, matrixFrame);
			this.GetGhostEntityOrChild(childName).SetFrame(ref matrixFrame);
			if (updateTriad)
			{
				this.SpawnedGhostEntity.UpdateTriadFrameForEditorForAllChildren();
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000BA934 File Offset: 0x000B8B34
		private GameEntity AddGhostEntity(GameEntity parent, List<string> possibleEntityNames)
		{
			this.spawner_.GameEntity.RemoveAllChildren();
			foreach (string text in possibleEntityNames)
			{
				if (GameEntity.PrefabExists(text))
				{
					this.SpawnedGhostEntity = GameEntity.Instantiate(parent.Scene, text, true);
					break;
				}
			}
			if (this.SpawnedGhostEntity == null)
			{
				return null;
			}
			this.SpawnedGhostEntity.SetMobility(GameEntity.Mobility.dynamic);
			this.SpawnedGhostEntity.EntityFlags |= EntityFlags.DontSaveToScene;
			parent.AddChild(this.SpawnedGhostEntity, false);
			MatrixFrame identity = MatrixFrame.Identity;
			this.SpawnedGhostEntity.SetFrame(ref identity);
			this.GetChildrenInitialFrames();
			this.SpawnedGhostEntity.UpdateTriadFrameForEditorForAllChildren();
			return this.SpawnedGhostEntity;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000BAA14 File Offset: 0x000B8C14
		private void SyncMatrixFrames(bool first)
		{
			this.ApplyPermissions();
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref list);
			using (List<GameEntity>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameEntity item = enumerator.Current;
					if (SpawnerEntityEditorHelper.HasField(this.spawner_, item.Name, false))
					{
						if (first)
						{
							MatrixFrame matrixFrame = (MatrixFrame)SpawnerEntityEditorHelper.GetFieldValue(this.spawner_, item.Name);
							if (!matrixFrame.IsZero)
							{
								item.SetFrame(ref matrixFrame);
							}
						}
						else
						{
							SpawnerEntityEditorHelper.SetSpawnerMatrixFrame(this.spawner_, item.Name, item.GetFrame());
						}
					}
					else
					{
						MatrixFrame value = this.stableChildrenFrames.Find((KeyValuePair<string, MatrixFrame> x) => x.Key == item.Name).Value;
						if (!value.NearlyEquals(item.GetFrame(), 1E-05f))
						{
							item.SetFrame(ref value);
							this.SpawnedGhostEntity.UpdateTriadFrameForEditorForAllChildren();
						}
					}
				}
			}
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000BAB50 File Offset: 0x000B8D50
		private void GetChildrenInitialFrames()
		{
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref list);
			foreach (GameEntity gameEntity in list)
			{
				if (!SpawnerEntityEditorHelper.HasField(this.spawner_, gameEntity.Name, false))
				{
					this.stableChildrenFrames.Add(new KeyValuePair<string, MatrixFrame>(gameEntity.Name, gameEntity.GetFrame()));
				}
			}
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000BABDC File Offset: 0x000B8DDC
		private List<string> GetGhostName()
		{
			string text = this.GetPrefabName();
			List<string> list = new List<string>();
			list.Add(text + "_ghost");
			text = text.Remove(text.Length - text.Split(new char[]
			{
				'_'
			}).Last<string>().Length - 1);
			list.Add(text + "_ghost");
			return list;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000BAC44 File Offset: 0x000B8E44
		public string GetPrefabName()
		{
			return this.spawner_.GameEntity.Name.Remove(this.spawner_.GameEntity.Name.Length - this.spawner_.GameEntity.Name.Split(new char[]
			{
				'_'
			}).Last<string>().Length - 1);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000BACA8 File Offset: 0x000B8EA8
		public void SetupGhostMovement(string pathName)
		{
			this._ghostMovementMode = true;
			this._pathName = pathName;
			Path pathWithName = this.SpawnedGhostEntity.Scene.GetPathWithName(pathName);
			Vec3 scaleVector = this.SpawnedGhostEntity.GetFrame().rotation.GetScaleVector();
			this._tracker = new PathTracker(pathWithName, scaleVector);
			this._ghostObjectPosition = ((pathWithName != null) ? pathWithName.GetTotalLength() : 0f);
			this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
			List<GameEntity> source = new List<GameEntity>();
			this.SpawnedGhostEntity.GetChildrenRecursive(ref source);
			this._wheels.Clear();
			this._wheels.AddRange(from x in source
			where x.HasTag("wheel")
			select x);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000BAD70 File Offset: 0x000B8F70
		public void SetEnableAutoGhostMovement(bool enableAutoGhostMovement)
		{
			this._enableAutoGhostMovement = enableAutoGhostMovement;
			if (!this._enableAutoGhostMovement && this._tracker.IsValid)
			{
				this._ghostObjectPosition = this._tracker.GetPathLength();
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000BADA0 File Offset: 0x000B8FA0
		private void UpdateGhostMovement(float dt)
		{
			if (this._tracker.HasChanged)
			{
				this.SetupGhostMovement(this._pathName);
				this._tracker.Advance(this._tracker.GetPathLength());
			}
			if (this.spawner_.GameEntity.IsSelectedOnEditor() || this.SpawnedGhostEntity.IsSelectedOnEditor())
			{
				if (this._tracker.IsValid)
				{
					float num = 10f;
					if (Input.DebugInput.IsShiftDown())
					{
						num = 1f;
					}
					if (Input.DebugInput.IsKeyDown(InputKey.MouseScrollUp))
					{
						this._ghostObjectPosition += dt * num;
					}
					else if (Input.DebugInput.IsKeyDown(InputKey.MouseScrollDown))
					{
						this._ghostObjectPosition -= dt * num;
					}
					if (this._enableAutoGhostMovement)
					{
						this._ghostObjectPosition += dt * num;
						if (this._ghostObjectPosition >= this._tracker.GetPathLength())
						{
							this._ghostObjectPosition = 0f;
						}
					}
					this._ghostObjectPosition = MBMath.ClampFloat(this._ghostObjectPosition, 0f, this._tracker.GetPathLength());
				}
				else
				{
					this._ghostObjectPosition = 0f;
				}
			}
			if (this._tracker.IsValid)
			{
				MatrixFrame globalFrame = this.spawner_.GameEntity.GetGlobalFrame();
				this._tracker.Advance(0f);
				MatrixFrame m;
				Vec3 vec;
				this._tracker.CurrentFrameAndColor(out m, out vec);
				if (globalFrame != m)
				{
					this.spawner_.GameEntity.SetGlobalFrame(m);
					this.spawner_.GameEntity.UpdateTriadFrameForEditor();
				}
				this._tracker.Advance(this._ghostObjectPosition);
				this._tracker.CurrentFrameAndColor(out m, out vec);
				if (this._wheels.Count == 2)
				{
					m = this.LinearInterpolatedIK(ref this._tracker);
				}
				if (globalFrame != m)
				{
					this.SpawnedGhostEntity.SetGlobalFrame(m);
					this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
				}
				this._tracker.Reset();
				return;
			}
			if (this.SpawnedGhostEntity.GetGlobalFrame() != this.spawner_.GameEntity.GetGlobalFrame())
			{
				GameEntity spawnedGhostEntity = this.SpawnedGhostEntity;
				MatrixFrame globalFrame2 = this.spawner_.GameEntity.GetGlobalFrame();
				spawnedGhostEntity.SetGlobalFrame(globalFrame2);
				this.SpawnedGhostEntity.UpdateTriadFrameForEditor();
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000BAFEC File Offset: 0x000B91EC
		private MatrixFrame LinearInterpolatedIK(ref PathTracker pathTracker)
		{
			MatrixFrame m;
			Vec3 vec;
			pathTracker.CurrentFrameAndColor(out m, out vec);
			MatrixFrame m2 = SiegeWeaponMovementComponent.FindGroundFrameForWheelsStatic(ref m, 2.45f, 1.3f, this.SpawnedGhostEntity, this._wheels, this.SpawnedGhostEntity.Scene);
			return MatrixFrame.Lerp(m, m2, vec.x);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000BB03A File Offset: 0x000B923A
		private static object GetFieldValue(object src, string propName)
		{
			return src.GetType().GetField(propName).GetValue(src);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000BB04E File Offset: 0x000B924E
		private static bool HasField(object obj, string propertyName, bool findRestricted)
		{
			return obj.GetType().GetField(propertyName) != null && (findRestricted || obj.GetType().GetField(propertyName).GetCustomAttribute<RestrictedAccess>() == null);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000BB080 File Offset: 0x000B9280
		private static bool SetSpawnerMatrixFrame(object target, string propertyName, MatrixFrame value)
		{
			value.Fill();
			FieldInfo field = target.GetType().GetField(propertyName);
			if (field != null)
			{
				field.SetValue(target, value);
				return true;
			}
			return false;
		}

		// Token: 0x04001313 RID: 4883
		private List<Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>>> _stableChildrenPermissions = new List<Tuple<string, SpawnerEntityEditorHelper.Permission, Action<float>>>();

		// Token: 0x04001314 RID: 4884
		private ScriptComponentBehavior spawner_;

		// Token: 0x04001315 RID: 4885
		private List<KeyValuePair<string, MatrixFrame>> stableChildrenFrames = new List<KeyValuePair<string, MatrixFrame>>();

		// Token: 0x04001318 RID: 4888
		public bool LockGhostParent = true;

		// Token: 0x04001319 RID: 4889
		private bool _ghostMovementMode;

		// Token: 0x0400131A RID: 4890
		private PathTracker _tracker;

		// Token: 0x0400131B RID: 4891
		private float _ghostObjectPosition;

		// Token: 0x0400131C RID: 4892
		private string _pathName;

		// Token: 0x0400131D RID: 4893
		private bool _enableAutoGhostMovement;

		// Token: 0x0400131E RID: 4894
		private readonly List<GameEntity> _wheels = new List<GameEntity>();

		// Token: 0x020005FF RID: 1535
		public enum Axis
		{
			// Token: 0x04001F47 RID: 8007
			x,
			// Token: 0x04001F48 RID: 8008
			y,
			// Token: 0x04001F49 RID: 8009
			z
		}

		// Token: 0x02000600 RID: 1536
		public enum PermissionType
		{
			// Token: 0x04001F4B RID: 8011
			scale,
			// Token: 0x04001F4C RID: 8012
			rotation
		}

		// Token: 0x02000601 RID: 1537
		public struct Permission
		{
			// Token: 0x06003BF7 RID: 15351 RVA: 0x000E99D6 File Offset: 0x000E7BD6
			public Permission(SpawnerEntityEditorHelper.PermissionType permission, SpawnerEntityEditorHelper.Axis axis)
			{
				this.TypeOfPermission = permission;
				this.PermittedAxis = axis;
			}

			// Token: 0x04001F4D RID: 8013
			public SpawnerEntityEditorHelper.PermissionType TypeOfPermission;

			// Token: 0x04001F4E RID: 8014
			public SpawnerEntityEditorHelper.Axis PermittedAxis;
		}
	}
}
