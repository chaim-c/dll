using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200000C RID: 12
	public class BrushAnimationKeyFrame
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003927 File Offset: 0x00001B27
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000392F File Offset: 0x00001B2F
		public float Time { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003938 File Offset: 0x00001B38
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003940 File Offset: 0x00001B40
		public int Index { get; private set; }

		// Token: 0x060000CA RID: 202 RVA: 0x00003951 File Offset: 0x00001B51
		public void InitializeAsFloat(float time, float value)
		{
			this.Time = time;
			this._valueType = BrushAnimationKeyFrame.ValueType.Float;
			this._valueAsFloat = value;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003968 File Offset: 0x00001B68
		public void InitializeAsColor(float time, Color value)
		{
			this.Time = time;
			this._valueType = BrushAnimationKeyFrame.ValueType.Color;
			this._valueAsColor = value;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000397F File Offset: 0x00001B7F
		public void InitializeAsSprite(float time, Sprite value)
		{
			this.Time = time;
			this._valueType = BrushAnimationKeyFrame.ValueType.Sprite;
			this._valueAsSprite = value;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003996 File Offset: 0x00001B96
		public void InitializeIndex(int index)
		{
			this.Index = index;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000399F File Offset: 0x00001B9F
		public float GetValueAsFloat()
		{
			return this._valueAsFloat;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000039A7 File Offset: 0x00001BA7
		public Color GetValueAsColor()
		{
			return this._valueAsColor;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000039AF File Offset: 0x00001BAF
		public Sprite GetValueAsSprite()
		{
			return this._valueAsSprite;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000039B8 File Offset: 0x00001BB8
		public object GetValueAsObject()
		{
			switch (this._valueType)
			{
			case BrushAnimationKeyFrame.ValueType.Float:
				return this._valueAsFloat;
			case BrushAnimationKeyFrame.ValueType.Color:
				return this._valueAsColor;
			case BrushAnimationKeyFrame.ValueType.Sprite:
				return this._valueAsSprite;
			default:
				return null;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003A00 File Offset: 0x00001C00
		public BrushAnimationKeyFrame Clone()
		{
			return new BrushAnimationKeyFrame
			{
				_valueType = this._valueType,
				_valueAsFloat = this._valueAsFloat,
				_valueAsColor = this._valueAsColor,
				_valueAsSprite = this._valueAsSprite,
				Time = this.Time,
				Index = this.Index
			};
		}

		// Token: 0x04000033 RID: 51
		private BrushAnimationKeyFrame.ValueType _valueType;

		// Token: 0x04000034 RID: 52
		private float _valueAsFloat;

		// Token: 0x04000035 RID: 53
		private Color _valueAsColor;

		// Token: 0x04000036 RID: 54
		private Sprite _valueAsSprite;

		// Token: 0x02000076 RID: 118
		public enum ValueType
		{
			// Token: 0x040003E9 RID: 1001
			Float,
			// Token: 0x040003EA RID: 1002
			Color,
			// Token: 0x040003EB RID: 1003
			Sprite
		}
	}
}
