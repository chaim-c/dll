using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000084 RID: 132
	public abstract class ScriptComponentBehavior : DotNetObject
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x0000AC7B File Offset: 0x00008E7B
		protected void InvalidateWeakPointersIfValid()
		{
			this._gameEntity.ManualInvalidate();
			this._scriptComponent.ManualInvalidate();
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0000AC93 File Offset: 0x00008E93
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public GameEntity GameEntity
		{
			get
			{
				WeakNativeObjectReference gameEntity = this._gameEntity;
				return ((gameEntity != null) ? gameEntity.GetNativeObject() : null) as GameEntity;
			}
			private set
			{
				this._gameEntity = new WeakNativeObjectReference(value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0000ACBA File Offset: 0x00008EBA
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0000ACD3 File Offset: 0x00008ED3
		public ManagedScriptComponent ScriptComponent
		{
			get
			{
				WeakNativeObjectReference scriptComponent = this._scriptComponent;
				return ((scriptComponent != null) ? scriptComponent.GetNativeObject() : null) as ManagedScriptComponent;
			}
			private set
			{
				this._scriptComponent = new WeakNativeObjectReference(value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0000ACE1 File Offset: 0x00008EE1
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		private protected ManagedScriptHolder ManagedScriptHolder { protected get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0000ACF2 File Offset: 0x00008EF2
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0000AD0B File Offset: 0x00008F0B
		public Scene Scene
		{
			get
			{
				WeakNativeObjectReference scene = this._scene;
				return ((scene != null) ? scene.GetNativeObject() : null) as Scene;
			}
			private set
			{
				this._scene = new WeakNativeObjectReference(value);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000AD19 File Offset: 0x00008F19
		static ScriptComponentBehavior()
		{
			if (ScriptComponentBehavior.CachedFields == null)
			{
				ScriptComponentBehavior.CachedFields = new Dictionary<string, string[]>();
				ScriptComponentBehavior.CacheEditableFieldsForAllScriptComponents();
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000AD4D File Offset: 0x00008F4D
		internal void Construct(GameEntity myEntity, ManagedScriptComponent scriptComponent)
		{
			this.GameEntity = myEntity;
			this.Scene = myEntity.Scene;
			this.ScriptComponent = scriptComponent;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000AD69 File Offset: 0x00008F69
		internal void SetOwnerManagedScriptHolder(ManagedScriptHolder managedScriptHolder)
		{
			this.ManagedScriptHolder = managedScriptHolder;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000AD74 File Offset: 0x00008F74
		private void SetScriptComponentToTickAux(ScriptComponentBehavior.TickRequirement value)
		{
			if (this._lastTickRequirement != value)
			{
				if (value.HasAnyFlag(ScriptComponentBehavior.TickRequirement.Tick) != this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.Tick))
				{
					if (this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.Tick))
					{
						this.ManagedScriptHolder.RemoveScriptComponentFromTickList(this);
					}
					else
					{
						this.ManagedScriptHolder.AddScriptComponentToTickList(this);
					}
				}
				if (value.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickOccasionally) != this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickOccasionally))
				{
					if (this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickOccasionally))
					{
						this.ManagedScriptHolder.RemoveScriptComponentFromTickOccasionallyList(this);
					}
					else
					{
						this.ManagedScriptHolder.AddScriptComponentToTickOccasionallyList(this);
					}
				}
				if (value.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickParallel) != this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickParallel))
				{
					if (this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickParallel))
					{
						this.ManagedScriptHolder.RemoveScriptComponentFromParallelTickList(this);
					}
					else
					{
						this.ManagedScriptHolder.AddScriptComponentToParallelTickList(this);
					}
				}
				if (value.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickParallel2) != this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickParallel2))
				{
					if (this._lastTickRequirement.HasAnyFlag(ScriptComponentBehavior.TickRequirement.TickParallel2))
					{
						this.ManagedScriptHolder.RemoveScriptComponentFromParallelTick2List(this);
					}
					else
					{
						this.ManagedScriptHolder.AddScriptComponentToParallelTick2List(this);
					}
				}
				this._lastTickRequirement = value;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000AE88 File Offset: 0x00009088
		public void SetScriptComponentToTick(ScriptComponentBehavior.TickRequirement value)
		{
			this.SetScriptComponentToTickAux(value);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000AE94 File Offset: 0x00009094
		public void SetScriptComponentToTickMT(ScriptComponentBehavior.TickRequirement value)
		{
			object addRemoveLockObject = ManagedScriptHolder.AddRemoveLockObject;
			lock (addRemoveLockObject)
			{
				this.SetScriptComponentToTickAux(value);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000AED4 File Offset: 0x000090D4
		[EngineCallback]
		internal void AddScriptComponentToTick()
		{
			List<ScriptComponentBehavior> prefabScriptComponents = ScriptComponentBehavior._prefabScriptComponents;
			lock (prefabScriptComponents)
			{
				if (!ScriptComponentBehavior._prefabScriptComponents.Contains(this))
				{
					ScriptComponentBehavior._prefabScriptComponents.Add(this);
				}
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000AF28 File Offset: 0x00009128
		[EngineCallback]
		internal void RegisterAsPrefabScriptComponent()
		{
			List<ScriptComponentBehavior> prefabScriptComponents = ScriptComponentBehavior._prefabScriptComponents;
			lock (prefabScriptComponents)
			{
				if (!ScriptComponentBehavior._prefabScriptComponents.Contains(this))
				{
					ScriptComponentBehavior._prefabScriptComponents.Add(this);
				}
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000AF7C File Offset: 0x0000917C
		[EngineCallback]
		internal void DeregisterAsPrefabScriptComponent()
		{
			List<ScriptComponentBehavior> prefabScriptComponents = ScriptComponentBehavior._prefabScriptComponents;
			lock (prefabScriptComponents)
			{
				ScriptComponentBehavior._prefabScriptComponents.Remove(this);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000AFC4 File Offset: 0x000091C4
		[EngineCallback]
		internal void RegisterAsUndoStackScriptComponent()
		{
			if (!ScriptComponentBehavior._undoStackScriptComponents.Contains(this))
			{
				ScriptComponentBehavior._undoStackScriptComponents.Add(this);
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000AFDE File Offset: 0x000091DE
		[EngineCallback]
		internal void DeregisterAsUndoStackScriptComponent()
		{
			if (ScriptComponentBehavior._undoStackScriptComponents.Contains(this))
			{
				ScriptComponentBehavior._undoStackScriptComponents.Remove(this);
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000AFF9 File Offset: 0x000091F9
		[EngineCallback]
		protected internal virtual void SetScene(Scene scene)
		{
			this.Scene = scene;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000B002 File Offset: 0x00009202
		[EngineCallback]
		protected internal virtual void OnInit()
		{
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000B004 File Offset: 0x00009204
		[EngineCallback]
		protected internal virtual void HandleOnRemoved(int removeReason)
		{
			this.OnRemoved(removeReason);
			this._scene = null;
			this._gameEntity = null;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000B01B File Offset: 0x0000921B
		protected virtual void OnRemoved(int removeReason)
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000B01D File Offset: 0x0000921D
		public virtual ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.None;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000B020 File Offset: 0x00009220
		protected internal virtual void OnTick(float dt)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnTick", 256);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000B03B File Offset: 0x0000923B
		protected internal virtual void OnTickParallel(float dt)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnTickParallel", 262);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000B056 File Offset: 0x00009256
		protected internal virtual void OnTickParallel2(float dt)
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000B058 File Offset: 0x00009258
		protected internal virtual void OnTickOccasionally(float currentFrameDeltaTime)
		{
			Debug.FailedAssert("This base function should never be called.", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\ScriptComponentBehavior.cs", "OnTickOccasionally", 274);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000B073 File Offset: 0x00009273
		[EngineCallback]
		protected internal virtual void OnPreInit()
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000B075 File Offset: 0x00009275
		[EngineCallback]
		protected internal virtual void OnEditorInit()
		{
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000B077 File Offset: 0x00009277
		[EngineCallback]
		protected internal virtual void OnEditorTick(float dt)
		{
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000B079 File Offset: 0x00009279
		[EngineCallback]
		protected internal virtual void OnEditorValidate()
		{
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000B07B File Offset: 0x0000927B
		[EngineCallback]
		protected internal virtual bool IsOnlyVisual()
		{
			return false;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000B07E File Offset: 0x0000927E
		[EngineCallback]
		protected internal virtual bool MovesEntity()
		{
			return true;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0000B081 File Offset: 0x00009281
		[EngineCallback]
		protected internal virtual bool DisablesOroCreation()
		{
			return true;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000B084 File Offset: 0x00009284
		[EngineCallback]
		protected internal virtual void OnEditorVariableChanged(string variableName)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000B086 File Offset: 0x00009286
		[EngineCallback]
		protected internal virtual void OnSceneSave(string saveFolder)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000B088 File Offset: 0x00009288
		[EngineCallback]
		protected internal virtual bool OnCheckForProblems()
		{
			return false;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000B08B File Offset: 0x0000928B
		[EngineCallback]
		protected internal virtual void OnPhysicsCollision(ref PhysicsContact contact)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000B08D File Offset: 0x0000928D
		[EngineCallback]
		protected internal virtual void OnEditModeVisibilityChanged(bool currentVisibility)
		{
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000B090 File Offset: 0x00009290
		private static void CacheEditableFieldsForAllScriptComponents()
		{
			foreach (KeyValuePair<string, Type> keyValuePair in Managed.ModuleTypes)
			{
				string key = keyValuePair.Key;
				ScriptComponentBehavior.CachedFields.Add(key, ScriptComponentBehavior.CollectEditableFields(key));
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000B0F4 File Offset: 0x000092F4
		private static string[] CollectEditableFields(string className)
		{
			List<string> list = new List<string>();
			Type type;
			if (Managed.ModuleTypes.TryGetValue(className, out type))
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < fields.Length; i++)
				{
					FieldInfo fieldInfo = fields[i];
					object[] customAttributesSafe = fieldInfo.GetCustomAttributesSafe(typeof(EditableScriptComponentVariable), true);
					bool flag = false;
					if (customAttributesSafe.Length != 0)
					{
						flag = ((EditableScriptComponentVariable)customAttributesSafe[0]).Visible;
					}
					else if (!fieldInfo.IsPrivate && !fieldInfo.IsFamily)
					{
						flag = true;
					}
					if (flag)
					{
						list.Add(fields[i].Name);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000B190 File Offset: 0x00009390
		[EngineCallback]
		internal static string[] GetEditableFields(string className)
		{
			string[] result;
			ScriptComponentBehavior.CachedFields.TryGetValue(className, out result);
			return result;
		}

		// Token: 0x0400019E RID: 414
		private static List<ScriptComponentBehavior> _prefabScriptComponents = new List<ScriptComponentBehavior>();

		// Token: 0x0400019F RID: 415
		private static List<ScriptComponentBehavior> _undoStackScriptComponents = new List<ScriptComponentBehavior>();

		// Token: 0x040001A0 RID: 416
		private WeakNativeObjectReference _gameEntity;

		// Token: 0x040001A1 RID: 417
		private WeakNativeObjectReference _scriptComponent;

		// Token: 0x040001A2 RID: 418
		private ScriptComponentBehavior.TickRequirement _lastTickRequirement;

		// Token: 0x040001A3 RID: 419
		private static readonly Dictionary<string, string[]> CachedFields;

		// Token: 0x040001A5 RID: 421
		private WeakNativeObjectReference _scene;

		// Token: 0x020000C6 RID: 198
		[Flags]
		public enum TickRequirement : uint
		{
			// Token: 0x0400040F RID: 1039
			None = 0U,
			// Token: 0x04000410 RID: 1040
			TickOccasionally = 1U,
			// Token: 0x04000411 RID: 1041
			Tick = 2U,
			// Token: 0x04000412 RID: 1042
			TickParallel = 4U,
			// Token: 0x04000413 RID: 1043
			TickParallel2 = 8U
		}
	}
}
