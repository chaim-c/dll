using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000059 RID: 89
	public sealed class ManagedScriptHolder : DotNetObject
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x000062E8 File Offset: 0x000044E8
		[EngineCallback]
		internal static ManagedScriptHolder CreateManagedScriptHolder()
		{
			return new ManagedScriptHolder();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000062F0 File Offset: 0x000044F0
		public ManagedScriptHolder()
		{
			this.TickComponentsParallelAuxMTPredicate = new TWParallel.ParallelForWithDtAuxPredicate(this.TickComponentsParallelAuxMT);
			this.TickComponentsParallel2AuxMTPredicate = new TWParallel.ParallelForWithDtAuxPredicate(this.TickComponentsParallel2AuxMT);
			this.TickComponentsOccasionallyParallelAuxMTPredicate = new TWParallel.ParallelForWithDtAuxPredicate(this.TickComponentsOccasionallyParallelAuxMT);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000063F4 File Offset: 0x000045F4
		[EngineCallback]
		public void SetScriptComponentHolder(ScriptComponentBehavior sc)
		{
			sc.SetOwnerManagedScriptHolder(this);
			if (this._scriptComponentsToRemoveFromTickForEditor.IndexOf(sc) != -1)
			{
				this._scriptComponentsToRemoveFromTickForEditor.Remove(sc);
			}
			else
			{
				this._scriptComponentsToAddToTickForEditor.Add(sc);
			}
			sc.SetScriptComponentToTick(sc.GetTickRequirement());
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00006434 File Offset: 0x00004634
		public void AddScriptComponentToTickOccasionallyList(ScriptComponentBehavior sc)
		{
			int num = this._scriptComponentsToRemoveFromTickOccasionally.IndexOf(sc);
			if (num != -1)
			{
				this._scriptComponentsToRemoveFromTickOccasionally.RemoveAt(num);
				return;
			}
			this._scriptComponentsToAddToTickOccasionally.Add(sc);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000646C File Offset: 0x0000466C
		public void AddScriptComponentToTickList(ScriptComponentBehavior sc)
		{
			int num = this._scriptComponentsToRemoveFromTick.IndexOf(sc);
			if (num != -1)
			{
				this._scriptComponentsToRemoveFromTick.RemoveAt(num);
				return;
			}
			this._scriptComponentsToAddToTick.Add(sc);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000064A4 File Offset: 0x000046A4
		public void AddScriptComponentToParallelTickList(ScriptComponentBehavior sc)
		{
			int num = this._scriptComponentsToRemoveFromParallelTick.IndexOf(sc);
			if (num != -1)
			{
				this._scriptComponentsToRemoveFromParallelTick.RemoveAt(num);
				return;
			}
			this._scriptComponentsToAddToParallelTick.Add(sc);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000064DC File Offset: 0x000046DC
		public void AddScriptComponentToParallelTick2List(ScriptComponentBehavior sc)
		{
			int num = this._scriptComponentsToRemoveFromParallelTick2.IndexOf(sc);
			if (num != -1)
			{
				this._scriptComponentsToRemoveFromParallelTick2.RemoveAt(num);
				return;
			}
			this._scriptComponentsToAddToParallelTick2.Add(sc);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00006514 File Offset: 0x00004714
		[EngineCallback]
		public void RemoveScriptComponentFromAllTickLists(ScriptComponentBehavior sc)
		{
			object addRemoveLockObject = ManagedScriptHolder.AddRemoveLockObject;
			lock (addRemoveLockObject)
			{
				sc.SetScriptComponentToTickMT(ScriptComponentBehavior.TickRequirement.None);
				if (this._scriptComponentsToAddToTickForEditor.IndexOf(sc) != -1)
				{
					this._scriptComponentsToAddToTickForEditor.Remove(sc);
				}
				else if (this._scriptComponentsToRemoveFromTickForEditor.IndexOf(sc) == -1)
				{
					this._scriptComponentsToRemoveFromTickForEditor.Add(sc);
				}
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00006590 File Offset: 0x00004790
		public void RemoveScriptComponentFromTickList(ScriptComponentBehavior sc)
		{
			if (this._scriptComponentsToAddToTick.IndexOf(sc) >= 0)
			{
				this._scriptComponentsToAddToTick.Remove(sc);
				return;
			}
			if (this._scriptComponentsToRemoveFromTick.IndexOf(sc) == -1 && this._scriptComponentsToTick.IndexOf(sc) != -1)
			{
				this._scriptComponentsToRemoveFromTick.Add(sc);
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000065E4 File Offset: 0x000047E4
		public void RemoveScriptComponentFromParallelTickList(ScriptComponentBehavior sc)
		{
			if (this._scriptComponentsToAddToParallelTick.IndexOf(sc) >= 0)
			{
				this._scriptComponentsToAddToParallelTick.Remove(sc);
				return;
			}
			if (this._scriptComponentsToRemoveFromParallelTick.IndexOf(sc) == -1 && this._scriptComponentsToParallelTick.IndexOf(sc) != -1)
			{
				this._scriptComponentsToRemoveFromParallelTick.Add(sc);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00006638 File Offset: 0x00004838
		public void RemoveScriptComponentFromParallelTick2List(ScriptComponentBehavior sc)
		{
			if (this._scriptComponentsToAddToParallelTick2.IndexOf(sc) >= 0)
			{
				this._scriptComponentsToAddToParallelTick2.Remove(sc);
				return;
			}
			if (this._scriptComponentsToRemoveFromParallelTick2.IndexOf(sc) == -1 && this._scriptComponentsToParallelTick2.IndexOf(sc) != -1)
			{
				this._scriptComponentsToRemoveFromParallelTick2.Add(sc);
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000668C File Offset: 0x0000488C
		public void RemoveScriptComponentFromTickOccasionallyList(ScriptComponentBehavior sc)
		{
			if (this._scriptComponentsToAddToTickOccasionally.IndexOf(sc) >= 0)
			{
				this._scriptComponentsToAddToTickOccasionally.Remove(sc);
				return;
			}
			if (this._scriptComponentsToRemoveFromTickOccasionally.IndexOf(sc) == -1 && this._scriptComponentsToTickOccasionally.IndexOf(sc) != -1)
			{
				this._scriptComponentsToRemoveFromTickOccasionally.Add(sc);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000066E0 File Offset: 0x000048E0
		[EngineCallback]
		internal int GetNumberOfScripts()
		{
			return this._scriptComponentsToTick.Count;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000066F0 File Offset: 0x000048F0
		private void TickComponentsParallelAuxMT(int startInclusive, int endExclusive, float dt)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this._scriptComponentsToParallelTick[i].OnTickParallel(dt);
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000671C File Offset: 0x0000491C
		private void TickComponentsParallel2AuxMT(int startInclusive, int endExclusive, float dt)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this._scriptComponentsToParallelTick2[i].OnTickParallel2(dt);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00006748 File Offset: 0x00004948
		private void TickComponentsOccasionallyParallelAuxMT(int startInclusive, int endExclusive, float dt)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this._scriptComponentsToTickOccasionally[i].OnTickOccasionally(dt);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00006774 File Offset: 0x00004974
		[EngineCallback]
		internal void TickComponents(float dt)
		{
			foreach (ScriptComponentBehavior item in this._scriptComponentsToRemoveFromParallelTick)
			{
				this._scriptComponentsToParallelTick.Remove(item);
			}
			this._scriptComponentsToRemoveFromParallelTick.Clear();
			foreach (ScriptComponentBehavior item2 in this._scriptComponentsToAddToParallelTick)
			{
				this._scriptComponentsToParallelTick.Add(item2);
			}
			this._scriptComponentsToAddToParallelTick.Clear();
			TWParallel.For(0, this._scriptComponentsToParallelTick.Count, dt, this.TickComponentsParallelAuxMTPredicate, 1);
			foreach (ScriptComponentBehavior item3 in this._scriptComponentsToRemoveFromParallelTick2)
			{
				this._scriptComponentsToParallelTick2.Remove(item3);
			}
			this._scriptComponentsToRemoveFromParallelTick2.Clear();
			foreach (ScriptComponentBehavior item4 in this._scriptComponentsToAddToParallelTick2)
			{
				this._scriptComponentsToParallelTick2.Add(item4);
			}
			this._scriptComponentsToAddToParallelTick2.Clear();
			TWParallel.For(0, this._scriptComponentsToParallelTick2.Count, dt, this.TickComponentsParallel2AuxMTPredicate, 8);
			foreach (ScriptComponentBehavior item5 in this._scriptComponentsToRemoveFromTick)
			{
				this._scriptComponentsToTick.Remove(item5);
			}
			this._scriptComponentsToRemoveFromTick.Clear();
			foreach (ScriptComponentBehavior item6 in this._scriptComponentsToAddToTick)
			{
				this._scriptComponentsToTick.Add(item6);
			}
			this._scriptComponentsToAddToTick.Clear();
			foreach (ScriptComponentBehavior scriptComponentBehavior in this._scriptComponentsToTick)
			{
				scriptComponentBehavior.OnTick(dt);
			}
			foreach (ScriptComponentBehavior item7 in this._scriptComponentsToRemoveFromTickOccasionally)
			{
				this._scriptComponentsToTickOccasionally.Remove(item7);
			}
			this._nextIndexToTickOccasionally = MathF.Max(0, this._nextIndexToTickOccasionally - this._scriptComponentsToRemoveFromTickOccasionally.Count);
			this._scriptComponentsToRemoveFromTickOccasionally.Clear();
			foreach (ScriptComponentBehavior item8 in this._scriptComponentsToAddToTickOccasionally)
			{
				this._scriptComponentsToTickOccasionally.Add(item8);
			}
			this._scriptComponentsToAddToTickOccasionally.Clear();
			int num = this._scriptComponentsToTickOccasionally.Count / 10 + 1;
			int num2 = Math.Min(this._nextIndexToTickOccasionally + num, this._scriptComponentsToTickOccasionally.Count);
			if (this._nextIndexToTickOccasionally < num2)
			{
				TWParallel.For(this._nextIndexToTickOccasionally, num2, dt, this.TickComponentsOccasionallyParallelAuxMTPredicate, 8);
				this._nextIndexToTickOccasionally = ((num2 >= this._scriptComponentsToTickOccasionally.Count) ? 0 : num2);
				return;
			}
			this._nextIndexToTickOccasionally = 0;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00006B20 File Offset: 0x00004D20
		[EngineCallback]
		internal void TickComponentsEditor(float dt)
		{
			for (int i = 0; i < this._scriptComponentsToRemoveFromTickForEditor.Count; i++)
			{
				this._scriptComponentsToTickForEditor.Remove(this._scriptComponentsToRemoveFromTickForEditor[i]);
			}
			this._scriptComponentsToRemoveFromTickForEditor.Clear();
			for (int j = 0; j < this._scriptComponentsToAddToTickForEditor.Count; j++)
			{
				this._scriptComponentsToTickForEditor.Add(this._scriptComponentsToAddToTickForEditor[j]);
			}
			this._scriptComponentsToAddToTickForEditor.Clear();
			for (int k = 0; k < this._scriptComponentsToTickForEditor.Count; k++)
			{
				if (this._scriptComponentsToRemoveFromTickForEditor.IndexOf(this._scriptComponentsToTickForEditor[k]) == -1)
				{
					this._scriptComponentsToTickForEditor[k].OnEditorTick(dt);
				}
			}
		}

		// Token: 0x040000C7 RID: 199
		public static object AddRemoveLockObject = new object();

		// Token: 0x040000C8 RID: 200
		private readonly List<ScriptComponentBehavior> _scriptComponentsToTick = new List<ScriptComponentBehavior>(512);

		// Token: 0x040000C9 RID: 201
		private readonly List<ScriptComponentBehavior> _scriptComponentsToParallelTick = new List<ScriptComponentBehavior>(64);

		// Token: 0x040000CA RID: 202
		private readonly List<ScriptComponentBehavior> _scriptComponentsToParallelTick2 = new List<ScriptComponentBehavior>(512);

		// Token: 0x040000CB RID: 203
		private readonly List<ScriptComponentBehavior> _scriptComponentsToTickOccasionally = new List<ScriptComponentBehavior>(512);

		// Token: 0x040000CC RID: 204
		private readonly List<ScriptComponentBehavior> _scriptComponentsToTickForEditor = new List<ScriptComponentBehavior>(512);

		// Token: 0x040000CD RID: 205
		private int _nextIndexToTickOccasionally;

		// Token: 0x040000CE RID: 206
		private readonly List<ScriptComponentBehavior> _scriptComponentsToAddToTick = new List<ScriptComponentBehavior>();

		// Token: 0x040000CF RID: 207
		private readonly List<ScriptComponentBehavior> _scriptComponentsToRemoveFromTick = new List<ScriptComponentBehavior>();

		// Token: 0x040000D0 RID: 208
		private readonly List<ScriptComponentBehavior> _scriptComponentsToAddToParallelTick = new List<ScriptComponentBehavior>();

		// Token: 0x040000D1 RID: 209
		private readonly List<ScriptComponentBehavior> _scriptComponentsToRemoveFromParallelTick = new List<ScriptComponentBehavior>();

		// Token: 0x040000D2 RID: 210
		private readonly List<ScriptComponentBehavior> _scriptComponentsToAddToParallelTick2 = new List<ScriptComponentBehavior>();

		// Token: 0x040000D3 RID: 211
		private readonly List<ScriptComponentBehavior> _scriptComponentsToRemoveFromParallelTick2 = new List<ScriptComponentBehavior>();

		// Token: 0x040000D4 RID: 212
		private readonly List<ScriptComponentBehavior> _scriptComponentsToAddToTickOccasionally = new List<ScriptComponentBehavior>();

		// Token: 0x040000D5 RID: 213
		private readonly List<ScriptComponentBehavior> _scriptComponentsToRemoveFromTickOccasionally = new List<ScriptComponentBehavior>();

		// Token: 0x040000D6 RID: 214
		private readonly List<ScriptComponentBehavior> _scriptComponentsToAddToTickForEditor = new List<ScriptComponentBehavior>();

		// Token: 0x040000D7 RID: 215
		private readonly List<ScriptComponentBehavior> _scriptComponentsToRemoveFromTickForEditor = new List<ScriptComponentBehavior>();

		// Token: 0x040000D8 RID: 216
		private readonly TWParallel.ParallelForWithDtAuxPredicate TickComponentsParallelAuxMTPredicate;

		// Token: 0x040000D9 RID: 217
		private readonly TWParallel.ParallelForWithDtAuxPredicate TickComponentsParallel2AuxMTPredicate;

		// Token: 0x040000DA RID: 218
		private readonly TWParallel.ParallelForWithDtAuxPredicate TickComponentsOccasionallyParallelAuxMTPredicate;
	}
}
