using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Multiplayer
{
	// Token: 0x02000036 RID: 54
	public class MPChatLineVM : ViewModel
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00014772 File Offset: 0x00012972
		public MPChatLineVM(string chatLine, Color color, string category)
		{
			this.ChatLine = chatLine;
			this.Color = color;
			this.Alpha = 1f;
			this.Category = category;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001479A File Offset: 0x0001299A
		public void HandleFading(float dt)
		{
			this._timeSinceCreation += dt;
			this.RefreshAlpha();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000147B0 File Offset: 0x000129B0
		private void RefreshAlpha()
		{
			if (this._forcedVisible)
			{
				this.Alpha = 1f;
				return;
			}
			this.Alpha = this.GetActualAlpha();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000147D2 File Offset: 0x000129D2
		public void ForceInvisible()
		{
			this._timeSinceCreation = 10.5f;
			this.Alpha = 0f;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000147EA File Offset: 0x000129EA
		private float GetActualAlpha()
		{
			if (this._timeSinceCreation >= 10f)
			{
				return MBMath.ClampFloat(1f - (this._timeSinceCreation - 10f) / 0.5f, 0f, 1f);
			}
			return 1f;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014826 File Offset: 0x00012A26
		public void ToggleForceVisible(bool visible)
		{
			this._forcedVisible = visible;
			this.RefreshAlpha();
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00014835 File Offset: 0x00012A35
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0001483D File Offset: 0x00012A3D
		[DataSourceProperty]
		public string ChatLine
		{
			get
			{
				return this._chatLine;
			}
			set
			{
				if (this._chatLine != value)
				{
					this._chatLine = value;
					base.OnPropertyChangedWithValue<string>(value, "ChatLine");
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00014860 File Offset: 0x00012A60
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00014868 File Offset: 0x00012A68
		[DataSourceProperty]
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (this._color != value)
				{
					this._color = value;
					base.OnPropertyChangedWithValue(value, "Color");
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001488B File Offset: 0x00012A8B
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00014893 File Offset: 0x00012A93
		[DataSourceProperty]
		public float Alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				if (this._alpha != value)
				{
					this._alpha = value;
					base.OnPropertyChangedWithValue(value, "Alpha");
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000148B1 File Offset: 0x00012AB1
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x000148B9 File Offset: 0x00012AB9
		[DataSourceProperty]
		public string Category
		{
			get
			{
				return this._category;
			}
			set
			{
				if (this._category != value)
				{
					this._category = value;
					base.OnPropertyChangedWithValue<string>(value, "Category");
				}
			}
		}

		// Token: 0x04000231 RID: 561
		private bool _forcedVisible;

		// Token: 0x04000232 RID: 562
		private string _category;

		// Token: 0x04000233 RID: 563
		private const float ChatVisibilityDuration = 10f;

		// Token: 0x04000234 RID: 564
		private const float ChatFadeOutDuration = 0.5f;

		// Token: 0x04000235 RID: 565
		private float _timeSinceCreation;

		// Token: 0x04000236 RID: 566
		private string _chatLine;

		// Token: 0x04000237 RID: 567
		private Color _color;

		// Token: 0x04000238 RID: 568
		private float _alpha;
	}
}
