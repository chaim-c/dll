using System;
using System.Collections.Generic;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200000B RID: 11
	public class BrushAnimation
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000036F0 File Offset: 0x000018F0
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000036F8 File Offset: 0x000018F8
		public string Name { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003701 File Offset: 0x00001901
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003709 File Offset: 0x00001909
		public float Duration { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003712 File Offset: 0x00001912
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000371A File Offset: 0x0000191A
		public bool Loop { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003723 File Offset: 0x00001923
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000372B File Offset: 0x0000192B
		public BrushLayerAnimation StyleAnimation { get; set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00003734 File Offset: 0x00001934
		public BrushAnimation()
		{
			this._data = new Dictionary<string, BrushLayerAnimation>();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003748 File Offset: 0x00001948
		public void AddAnimationProperty(BrushAnimationProperty property)
		{
			BrushLayerAnimation brushLayerAnimation = null;
			if (string.IsNullOrEmpty(property.LayerName))
			{
				if (this.StyleAnimation == null)
				{
					this.StyleAnimation = new BrushLayerAnimation();
				}
				brushLayerAnimation = this.StyleAnimation;
			}
			else if (!this._data.TryGetValue(property.LayerName, out brushLayerAnimation))
			{
				brushLayerAnimation = new BrushLayerAnimation();
				brushLayerAnimation.LayerName = property.LayerName;
				this._data.Add(property.LayerName, brushLayerAnimation);
			}
			brushLayerAnimation.AddAnimationProperty(property);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000037C0 File Offset: 0x000019C0
		public void RemoveAnimationProperty(BrushAnimationProperty property)
		{
			BrushLayerAnimation brushLayerAnimation;
			if (string.IsNullOrEmpty(property.LayerName))
			{
				if (this.StyleAnimation == null)
				{
					this.StyleAnimation = new BrushLayerAnimation();
				}
				brushLayerAnimation = this.StyleAnimation;
			}
			else
			{
				if (!this._data.ContainsKey(property.LayerName))
				{
					return;
				}
				brushLayerAnimation = this._data[property.LayerName];
			}
			brushLayerAnimation.RemoveAnimationProperty(property);
			if (brushLayerAnimation.Collections.Count == 0)
			{
				this._data.Remove(property.LayerName);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003844 File Offset: 0x00001A44
		public void FillFrom(BrushAnimation animation)
		{
			this.Name = animation.Name;
			this.Duration = animation.Duration;
			this.Loop = animation.Loop;
			if (animation.StyleAnimation != null)
			{
				this.StyleAnimation = animation.StyleAnimation.Clone();
			}
			this._data = new Dictionary<string, BrushLayerAnimation>();
			foreach (KeyValuePair<string, BrushLayerAnimation> keyValuePair in animation._data)
			{
				string key = keyValuePair.Key;
				BrushLayerAnimation value = keyValuePair.Value.Clone();
				this._data.Add(key, value);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000038FC File Offset: 0x00001AFC
		public BrushLayerAnimation GetLayerAnimation(string name)
		{
			if (this._data.ContainsKey(name))
			{
				return this._data[name];
			}
			return null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000391A File Offset: 0x00001B1A
		public IEnumerable<BrushLayerAnimation> GetLayerAnimations()
		{
			return this._data.Values;
		}

		// Token: 0x04000031 RID: 49
		private Dictionary<string, BrushLayerAnimation> _data;
	}
}
