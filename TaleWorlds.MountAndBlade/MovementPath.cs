using System;
using System.Diagnostics;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200014A RID: 330
	public class MovementPath
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00030729 File Offset: 0x0002E929
		private int LineCount
		{
			get
			{
				return this._navigationData.PointSize - 1;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00030738 File Offset: 0x0002E938
		public Vec2 InitialDirection { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00030740 File Offset: 0x0002E940
		public Vec2 FinalDirection { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00030748 File Offset: 0x0002E948
		public Vec3 Destination
		{
			get
			{
				return this._navigationData.EndPoint;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00030755 File Offset: 0x0002E955
		public MovementPath(NavigationData navigationData, Vec2 initialDirection, Vec2 finalDirection)
		{
			this._navigationData = navigationData;
			this.InitialDirection = initialDirection;
			this.FinalDirection = finalDirection;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00030772 File Offset: 0x0002E972
		public MovementPath(Vec3 currentPosition, Vec3 orderPosition, float agentRadius, Vec2 previousDirection, Vec2 finalDirection) : this(new NavigationData(currentPosition, orderPosition, agentRadius), previousDirection, finalDirection)
		{
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00030788 File Offset: 0x0002E988
		private void UpdateLineLengths()
		{
			if (this._lineLengthAccumulations == null)
			{
				this._lineLengthAccumulations = new float[this.LineCount];
				for (int i = 0; i < this.LineCount; i++)
				{
					this._lineLengthAccumulations[i] = (this._navigationData.Points[i + 1] - this._navigationData.Points[i]).Length;
					if (i > 0)
					{
						this._lineLengthAccumulations[i] += this._lineLengthAccumulations[i - 1];
					}
				}
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00030818 File Offset: 0x0002EA18
		private float GetPathProggress(Vec2 point, int lineIndex)
		{
			this.UpdateLineLengths();
			float num = this._lineLengthAccumulations[this.LineCount - 1];
			if (num == 0f)
			{
				return 1f;
			}
			return (((lineIndex > 0) ? this._lineLengthAccumulations[lineIndex - 1] : 0f) + (point - this._navigationData.Points[lineIndex]).Length) / num;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00030880 File Offset: 0x0002EA80
		private void GetClosestPointTo(Vec2 point, out Vec2 closest, out int lineIndex)
		{
			closest = Vec2.Invalid;
			lineIndex = -1;
			float num = float.MaxValue;
			for (int i = 0; i < this.LineCount; i++)
			{
				Vec2 closestPointInLineSegmentToPoint = MBMath.GetClosestPointInLineSegmentToPoint(point, this._navigationData.Points[i], this._navigationData.Points[i + 1]);
				float num2 = closestPointInLineSegmentToPoint.DistanceSquared(point);
				if (num2 < num)
				{
					num = num2;
					closest = closestPointInLineSegmentToPoint;
					lineIndex = i;
				}
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000308F8 File Offset: 0x0002EAF8
		[Conditional("DEBUG")]
		public void TickDebug(Vec2 position)
		{
			Vec2 point;
			int lineIndex;
			this.GetClosestPointTo(position, out point, out lineIndex);
			float pathProggress = this.GetPathProggress(point, lineIndex);
			Vec2.Slerp(this.InitialDirection, this.FinalDirection, pathProggress).Normalize();
		}

		// Token: 0x040003D8 RID: 984
		private float[] _lineLengthAccumulations;

		// Token: 0x040003D9 RID: 985
		private NavigationData _navigationData;
	}
}
