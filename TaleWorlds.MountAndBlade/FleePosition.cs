using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000320 RID: 800
	public class FleePosition : ScriptComponentBehavior
	{
		// Token: 0x06002B15 RID: 11029 RVA: 0x000A6B2C File Offset: 0x000A4D2C
		protected internal override void OnInit()
		{
			this.CollectNodes();
			bool flag = false;
			if (this.Side == "both")
			{
				this._side = BattleSideEnum.None;
			}
			else if (this.Side == "attacker")
			{
				this._side = (flag ? BattleSideEnum.Defender : BattleSideEnum.Attacker);
			}
			else if (this.Side == "defender")
			{
				this._side = (flag ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
			}
			else
			{
				this._side = BattleSideEnum.None;
			}
			if (base.GameEntity.HasTag("sally_out"))
			{
				if (Mission.Current.IsSallyOutBattle)
				{
					Mission.Current.AddFleePosition(this);
					return;
				}
			}
			else
			{
				Mission.Current.AddFleePosition(this);
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000A6BDA File Offset: 0x000A4DDA
		public BattleSideEnum GetSide()
		{
			return this._side;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000A6BE2 File Offset: 0x000A4DE2
		protected internal override void OnEditorInit()
		{
			this.CollectNodes();
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000A6BEC File Offset: 0x000A4DEC
		private void CollectNodes()
		{
			this._nodes.Clear();
			int childCount = base.GameEntity.ChildCount;
			for (int i = 0; i < childCount; i++)
			{
				GameEntity child = base.GameEntity.GetChild(i);
				this._nodes.Add(child.GlobalPosition);
			}
			if (this._nodes.IsEmpty<Vec3>())
			{
				this._nodes.Add(base.GameEntity.GlobalPosition);
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000A6C60 File Offset: 0x000A4E60
		protected internal override void OnEditorTick(float dt)
		{
			this.CollectNodes();
			bool flag = base.GameEntity.IsSelectedOnEditor();
			int childCount = base.GameEntity.ChildCount;
			int num = 0;
			while (!flag && num < childCount)
			{
				flag = base.GameEntity.GetChild(num).IsSelectedOnEditor();
				num++;
			}
			if (flag)
			{
				for (int i = 0; i < this._nodes.Count; i++)
				{
					int num2 = this._nodes.Count - 1;
				}
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A6CD8 File Offset: 0x000A4ED8
		public Vec3 GetClosestPointToEscape(Vec2 position)
		{
			if (this._nodes.Count == 1)
			{
				return this._nodes[0];
			}
			float num = float.MaxValue;
			Vec3 result = this._nodes[0];
			for (int i = 0; i < this._nodes.Count - 1; i++)
			{
				Vec3 vec = this._nodes[i];
				Vec3 vec2 = this._nodes[i + 1];
				float num2 = vec.DistanceSquared(vec2);
				if (num2 > 0f)
				{
					float f = MathF.Max(0f, MathF.Min(1f, Vec2.DotProduct(position - vec.AsVec2, vec2.AsVec2 - vec.AsVec2) / num2));
					Vec3 vec3 = vec + f * (vec2 - vec);
					float num3 = vec3.AsVec2.DistanceSquared(position);
					if (num > num3)
					{
						num = num3;
						result = vec3;
					}
				}
				else
				{
					num = 0f;
					result = vec;
				}
			}
			return result;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A6DE0 File Offset: 0x000A4FE0
		protected internal override bool MovesEntity()
		{
			return true;
		}

		// Token: 0x040010A8 RID: 4264
		private List<Vec3> _nodes = new List<Vec3>();

		// Token: 0x040010A9 RID: 4265
		private BattleSideEnum _side;

		// Token: 0x040010AA RID: 4266
		public string Side = "both";
	}
}
