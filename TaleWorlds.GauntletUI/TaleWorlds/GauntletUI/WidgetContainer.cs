using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000037 RID: 55
	internal class WidgetContainer
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000F205 File Offset: 0x0000D405
		internal int Count
		{
			get
			{
				return this._widgetLists[this._currentBufferIndex].Count;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000F219 File Offset: 0x0000D419
		internal int RealCount
		{
			get
			{
				return this._widgetLists[this._currentBufferIndex].Count - this._emptyCount;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F234 File Offset: 0x0000D434
		internal WidgetContainer(UIContext context, int initialCapacity, WidgetContainer.ContainerType type)
		{
			this._emptyWidget = new EmptyWidget(context);
			this._currentBufferIndex = 0;
			this._widgetLists = new List<Widget>[]
			{
				new List<Widget>(initialCapacity),
				new List<Widget>(initialCapacity)
			};
			this._containerType = type;
			this._emptyCount = 0;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000F286 File Offset: 0x0000D486
		internal List<Widget> GetCurrentList()
		{
			return this._widgetLists[this._currentBufferIndex];
		}

		// Token: 0x1700011F RID: 287
		internal Widget this[int index]
		{
			get
			{
				return this._widgetLists[this._currentBufferIndex][index];
			}
			set
			{
				this._widgetLists[this._currentBufferIndex][index] = value;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		internal int Add(Widget widget)
		{
			this._widgetLists[this._currentBufferIndex].Add(widget);
			return this._widgetLists[this._currentBufferIndex].Count - 1;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		internal void Remove(Widget widget)
		{
			int index = this._widgetLists[this._currentBufferIndex].IndexOf(widget);
			this._widgetLists[this._currentBufferIndex][index] = this._emptyWidget;
			this._emptyCount++;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F334 File Offset: 0x0000D534
		public void Clear()
		{
			for (int i = 0; i < this._widgetLists.Length; i++)
			{
				this._widgetLists[i].Clear();
			}
			this._widgetLists = null;
			this._emptyCount = 0;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000F36F File Offset: 0x0000D56F
		internal void RemoveFromIndex(int index)
		{
			this._widgetLists[this._currentBufferIndex][index] = this._emptyWidget;
			this._emptyCount++;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000F398 File Offset: 0x0000D598
		internal bool CheckFragmentation()
		{
			int count = this._widgetLists[this._currentBufferIndex].Count;
			return count > 32 && (int)((float)count * 0.1f) < this._emptyCount;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		internal void DoDefragmentation()
		{
			int count = this._widgetLists[this._currentBufferIndex].Count;
			int num = (this._currentBufferIndex + 1) % 2;
			List<Widget> list = this._widgetLists[this._currentBufferIndex];
			List<Widget> list2 = this._widgetLists[num];
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				Widget widget = list[i];
				if (widget != this._emptyWidget)
				{
					switch (this._containerType)
					{
					case WidgetContainer.ContainerType.Update:
						widget.OnUpdateListIndex -= num2;
						break;
					case WidgetContainer.ContainerType.ParallelUpdate:
						widget.OnParallelUpdateListIndex -= num2;
						break;
					case WidgetContainer.ContainerType.LateUpdate:
						widget.OnLateUpdateListIndex -= num2;
						break;
					case WidgetContainer.ContainerType.VisualDefinition:
						widget.OnVisualDefinitionListIndex -= num2;
						break;
					case WidgetContainer.ContainerType.TweenPosition:
						widget.OnTweenPositionListIndex -= num2;
						break;
					case WidgetContainer.ContainerType.UpdateBrushes:
						widget.OnUpdateBrushesIndex -= num2;
						break;
					}
					list2.Add(widget);
				}
				else
				{
					num2++;
				}
			}
			list.Clear();
			this._emptyCount = 0;
			this._currentBufferIndex = num;
		}

		// Token: 0x040001CE RID: 462
		private int _currentBufferIndex;

		// Token: 0x040001CF RID: 463
		private List<Widget>[] _widgetLists;

		// Token: 0x040001D0 RID: 464
		private EmptyWidget _emptyWidget;

		// Token: 0x040001D1 RID: 465
		private int _emptyCount;

		// Token: 0x040001D2 RID: 466
		private WidgetContainer.ContainerType _containerType;

		// Token: 0x02000081 RID: 129
		internal enum ContainerType
		{
			// Token: 0x04000451 RID: 1105
			None,
			// Token: 0x04000452 RID: 1106
			Update,
			// Token: 0x04000453 RID: 1107
			ParallelUpdate,
			// Token: 0x04000454 RID: 1108
			LateUpdate,
			// Token: 0x04000455 RID: 1109
			VisualDefinition,
			// Token: 0x04000456 RID: 1110
			TweenPosition,
			// Token: 0x04000457 RID: 1111
			UpdateBrushes
		}
	}
}
