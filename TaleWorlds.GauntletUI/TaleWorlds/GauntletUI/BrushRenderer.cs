using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000016 RID: 22
	public class BrushRenderer
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007623 File Offset: 0x00005823
		private float _brushTimer
		{
			get
			{
				if (!this.UseLocalTimer)
				{
					return this._globalTime;
				}
				return this._brushLocalTimer;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000763A File Offset: 0x0000583A
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007642 File Offset: 0x00005842
		public bool ForcePixelPerfectPlacement { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000764B File Offset: 0x0000584B
		public Style CurrentStyle
		{
			get
			{
				return this._styleOfCurrentState;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007653 File Offset: 0x00005853
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000765C File Offset: 0x0000585C
		public Brush Brush
		{
			get
			{
				return this._brush;
			}
			set
			{
				if (this._brush != value)
				{
					this._brush = value;
					this._brushLocalTimer = 0f;
					int capacity = (this._brush != null) ? this._brush.Layers.Count : 0;
					if (this._startBrushLayerState == null)
					{
						this._startBrushLayerState = new Dictionary<string, BrushLayerState>(capacity);
						this._currentBrushLayerState = new Dictionary<string, BrushLayerState>(capacity);
					}
					else
					{
						this._startBrushLayerState.Clear();
						this._currentBrushLayerState.Clear();
					}
					if (this._brush != null)
					{
						Style defaultStyle = this._brush.DefaultStyle;
						BrushState brushState = default(BrushState);
						brushState.FillFrom(defaultStyle);
						this._startBrushState = brushState;
						this._currentBrushState = brushState;
						foreach (BrushLayer brushLayer in this._brush.Layers)
						{
							BrushLayerState value2 = default(BrushLayerState);
							value2.FillFrom(brushLayer);
							this._startBrushLayerState[brushLayer.Name] = value2;
							this._currentBrushLayerState[brushLayer.Name] = value2;
						}
						if (!string.IsNullOrEmpty(this.CurrentState))
						{
							this._styleOfCurrentState = this.Brush.GetStyleOrDefault(this.CurrentState);
						}
					}
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000077B4 File Offset: 0x000059B4
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000077BC File Offset: 0x000059BC
		public string CurrentState
		{
			get
			{
				return this._currentState;
			}
			set
			{
				if (this._currentState != value)
				{
					string currentState = this._currentState;
					this._brushLocalTimer = 0f;
					this._currentState = value;
					this._startBrushState = this._currentBrushState;
					foreach (KeyValuePair<string, BrushLayerState> keyValuePair in this._currentBrushLayerState)
					{
						this._startBrushLayerState[keyValuePair.Key] = keyValuePair.Value;
					}
					if (this.Brush != null)
					{
						Style styleOrDefault = this.Brush.GetStyleOrDefault(this.CurrentState);
						this._styleOfCurrentState = styleOrDefault;
						this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.None;
						if (styleOrDefault.AnimationMode == StyleAnimationMode.BasicTransition)
						{
							if (!string.IsNullOrEmpty(currentState))
							{
								this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.PlayingBasicTranisition;
								return;
							}
						}
						else if (styleOrDefault.AnimationMode == StyleAnimationMode.Animation && (!string.IsNullOrEmpty(currentState) || !string.IsNullOrEmpty(styleOrDefault.AnimationToPlayOnBegin)))
						{
							this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.PlayingAnimation;
						}
					}
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000078C0 File Offset: 0x00005AC0
		public BrushRenderer()
		{
			this._startBrushState = default(BrushState);
			this._currentBrushState = default(BrushState);
			this._startBrushLayerState = new Dictionary<string, BrushLayerState>();
			this._currentBrushLayerState = new Dictionary<string, BrushLayerState>();
			this._brushLocalTimer = 0f;
			this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.None;
			this._randomXOffset = -1f;
			this._randomYOffset = -1f;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000792C File Offset: 0x00005B2C
		private float GetRandomXOffset()
		{
			if (this._randomXOffset < 0f)
			{
				Random random = new Random(this._offsetSeed);
				this._randomXOffset = (float)random.Next(0, 2048);
				this._randomYOffset = (float)random.Next(0, 2048);
			}
			return this._randomXOffset;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007980 File Offset: 0x00005B80
		private float GetRandomYOffset()
		{
			if (this._randomYOffset < 0f)
			{
				Random random = new Random(this._offsetSeed);
				this._randomXOffset = (float)random.Next(0, 2048);
				this._randomYOffset = (float)random.Next(0, 2048);
			}
			return this._randomYOffset;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000079D4 File Offset: 0x00005BD4
		public void Update(float globalAnimTime, float dt)
		{
			this._globalTime = globalAnimTime;
			this._brushLocalTimer += dt;
			if (this.Brush != null)
			{
				Style styleOfCurrentState = this._styleOfCurrentState;
				if ((this._brushRendererAnimationState == BrushRenderer.BrushRendererAnimationState.None || this._brushRendererAnimationState == BrushRenderer.BrushRendererAnimationState.Ended) && (!string.IsNullOrEmpty(styleOfCurrentState.AnimationToPlayOnBegin) || this._styleOfCurrentState.Version != this._latestStyleVersion))
				{
					this._latestStyleVersion = styleOfCurrentState.Version;
					BrushState brushState = default(BrushState);
					brushState.FillFrom(styleOfCurrentState);
					this._startBrushState = brushState;
					this._currentBrushState = brushState;
					foreach (StyleLayer styleLayer in styleOfCurrentState.GetLayers())
					{
						BrushLayerState value = default(BrushLayerState);
						value.FillFrom(styleLayer);
						this._currentBrushLayerState[styleLayer.Name] = value;
						this._startBrushLayerState[styleLayer.Name] = value;
					}
					return;
				}
				if (this._brushRendererAnimationState == BrushRenderer.BrushRendererAnimationState.PlayingBasicTranisition)
				{
					float num = this.UseLocalTimer ? this._brushLocalTimer : globalAnimTime;
					if (num >= this.Brush.TransitionDuration)
					{
						this.EndAnimation();
						return;
					}
					float num2 = num / this.Brush.TransitionDuration;
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					BrushState startBrushState = this._startBrushState;
					BrushState currentBrushState = default(BrushState);
					currentBrushState.LerpFrom(startBrushState, styleOfCurrentState, num2);
					this._currentBrushState = currentBrushState;
					foreach (StyleLayer styleLayer2 in styleOfCurrentState.GetLayers())
					{
						BrushLayerState start = this._startBrushLayerState[styleLayer2.Name];
						BrushLayerState value2 = default(BrushLayerState);
						value2.LerpFrom(start, styleLayer2, num2);
						this._currentBrushLayerState[styleLayer2.Name] = value2;
					}
					return;
				}
				else if (this._brushRendererAnimationState == BrushRenderer.BrushRendererAnimationState.PlayingAnimation)
				{
					string animationToPlayOnBegin = styleOfCurrentState.AnimationToPlayOnBegin;
					BrushAnimation animation = this.Brush.GetAnimation(animationToPlayOnBegin);
					if (animation == null || (!animation.Loop && this._brushTimer >= animation.Duration))
					{
						this.EndAnimation();
						return;
					}
					float brushStateTimer = this._brushTimer % animation.Duration;
					bool isFirstCycle = this._brushTimer < animation.Duration;
					BrushState startBrushState2 = this._startBrushState;
					BrushLayerAnimation styleAnimation = animation.StyleAnimation;
					BrushState currentBrushState2 = this.AnimateBrushState(animation, styleAnimation, brushStateTimer, isFirstCycle, startBrushState2, styleOfCurrentState);
					this._currentBrushState = currentBrushState2;
					foreach (StyleLayer styleLayer3 in styleOfCurrentState.GetLayers())
					{
						BrushLayerState startState = this._startBrushLayerState[styleLayer3.Name];
						BrushLayerAnimation layerAnimation = animation.GetLayerAnimation(styleLayer3.Name);
						BrushLayerState value3 = this.AnimateBrushLayerState(animation, layerAnimation, brushStateTimer, isFirstCycle, startState, styleLayer3);
						this._currentBrushLayerState[styleLayer3.Name] = value3;
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007C84 File Offset: 0x00005E84
		private BrushLayerState AnimateBrushLayerState(BrushAnimation animation, BrushLayerAnimation layerAnimation, float brushStateTimer, bool isFirstCycle, BrushLayerState startState, IBrushLayerData source)
		{
			BrushLayerState result = default(BrushLayerState);
			result.FillFrom(source);
			if (layerAnimation != null)
			{
				foreach (BrushAnimationProperty brushAnimationProperty in layerAnimation.Collections)
				{
					BrushAnimationProperty.BrushAnimationPropertyType propertyType = brushAnimationProperty.PropertyType;
					BrushAnimationKeyFrame brushAnimationKeyFrame = null;
					BrushAnimationKeyFrame brushAnimationKeyFrame2;
					if (animation.Loop)
					{
						BrushAnimationKeyFrame frameAt = brushAnimationProperty.GetFrameAt(0);
						if (isFirstCycle && this._brushTimer < frameAt.Time)
						{
							brushAnimationKeyFrame2 = frameAt;
						}
						else
						{
							brushAnimationKeyFrame2 = brushAnimationProperty.GetFrameAfter(brushStateTimer);
							if (brushAnimationKeyFrame2 == null)
							{
								brushAnimationKeyFrame2 = frameAt;
								brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationProperty.Count - 1);
							}
							else if (brushAnimationKeyFrame2 == frameAt)
							{
								brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationProperty.Count - 1);
							}
							else
							{
								brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationKeyFrame2.Index - 1);
							}
						}
					}
					else
					{
						brushAnimationKeyFrame2 = brushAnimationProperty.GetFrameAfter(brushStateTimer);
						if (brushAnimationKeyFrame2 != null)
						{
							brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationKeyFrame2.Index - 1);
						}
						else
						{
							brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationProperty.Count - 1);
						}
					}
					BrushAnimationKeyFrame brushAnimationKeyFrame3 = null;
					BrushLayerState brushLayerState = default(BrushLayerState);
					IBrushLayerData brushLayerData = null;
					BrushAnimationKeyFrame brushAnimationKeyFrame4 = null;
					float num3;
					if (brushAnimationKeyFrame2 != null)
					{
						if (brushAnimationKeyFrame != null)
						{
							float num;
							float num2;
							if (animation.Loop)
							{
								if (brushAnimationKeyFrame2.Index == 0)
								{
									num = brushAnimationKeyFrame2.Time + (animation.Duration - brushAnimationKeyFrame.Time);
									if (brushStateTimer >= brushAnimationKeyFrame.Time)
									{
										num2 = brushStateTimer - brushAnimationKeyFrame.Time;
									}
									else
									{
										num2 = animation.Duration - brushAnimationKeyFrame.Time + brushStateTimer;
									}
								}
								else
								{
									num = brushAnimationKeyFrame2.Time - brushAnimationKeyFrame.Time;
									num2 = brushStateTimer - brushAnimationKeyFrame.Time;
								}
							}
							else
							{
								num = brushAnimationKeyFrame2.Time - brushAnimationKeyFrame.Time;
								num2 = brushStateTimer - brushAnimationKeyFrame.Time;
							}
							num3 = num2 * (1f / num);
							brushAnimationKeyFrame3 = brushAnimationKeyFrame;
							brushAnimationKeyFrame4 = brushAnimationKeyFrame2;
						}
						else
						{
							num3 = brushStateTimer * (1f / brushAnimationKeyFrame2.Time);
							brushLayerState = startState;
							brushAnimationKeyFrame4 = brushAnimationKeyFrame2;
						}
					}
					else
					{
						num3 = (brushStateTimer - brushAnimationKeyFrame.Time) * (1f / (animation.Duration - brushAnimationKeyFrame.Time));
						brushAnimationKeyFrame3 = brushAnimationKeyFrame;
						brushLayerData = source;
					}
					switch (propertyType)
					{
					case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineAmount:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowRadius:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextBlur:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowAngle:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextColorFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextAlphaFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextHueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextSaturationFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextValueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.XOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.YOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverridenWidth:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverridenHeight:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendLeft:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendRight:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendTop:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendBottom:
					{
						float valueFrom = (brushAnimationKeyFrame3 != null) ? brushAnimationKeyFrame3.GetValueAsFloat() : brushLayerState.GetValueAsFloat(propertyType);
						float valueTo = (brushLayerData != null) ? brushLayerData.GetValueAsFloat(propertyType) : brushAnimationKeyFrame4.GetValueAsFloat();
						result.SetValueAsFloat(propertyType, MathF.Lerp(valueFrom, valueTo, num3, 1E-05f));
						break;
					}
					case BrushAnimationProperty.BrushAnimationPropertyType.Color:
					case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
					{
						Color start = (brushAnimationKeyFrame3 != null) ? brushAnimationKeyFrame3.GetValueAsColor() : brushLayerState.GetValueAsColor(propertyType);
						Color end = (brushLayerData != null) ? brushLayerData.GetValueAsColor(propertyType) : brushAnimationKeyFrame4.GetValueAsColor();
						BrushAnimationProperty.BrushAnimationPropertyType propertyType2 = propertyType;
						Color color = Color.Lerp(start, end, num3);
						result.SetValueAsColor(propertyType2, color);
						break;
					}
					case BrushAnimationProperty.BrushAnimationPropertyType.Sprite:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverlaySprite:
					{
						Sprite sprite = ((brushAnimationKeyFrame3 != null) ? brushAnimationKeyFrame3.GetValueAsSprite() : null) ?? brushLayerState.GetValueAsSprite(propertyType);
						Sprite sprite2 = ((brushLayerData != null) ? brushLayerData.GetValueAsSprite(propertyType) : null) ?? brushAnimationKeyFrame4.GetValueAsSprite();
						result.SetValueAsSprite(propertyType, ((double)num3 <= 0.9) ? sprite : sprite2);
						break;
					}
					}
				}
			}
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000805C File Offset: 0x0000625C
		public bool IsUpdateNeeded()
		{
			return this._brushRendererAnimationState == BrushRenderer.BrushRendererAnimationState.PlayingBasicTranisition || this._brushRendererAnimationState == BrushRenderer.BrushRendererAnimationState.PlayingAnimation || (this._styleOfCurrentState != null && this._styleOfCurrentState.Version != this._latestStyleVersion);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008094 File Offset: 0x00006294
		private BrushState AnimateBrushState(BrushAnimation animation, BrushLayerAnimation layerAnimation, float brushStateTimer, bool isFirstCycle, BrushState startState, Style source)
		{
			BrushState result = default(BrushState);
			result.FillFrom(source);
			if (layerAnimation != null)
			{
				foreach (BrushAnimationProperty brushAnimationProperty in layerAnimation.Collections)
				{
					BrushAnimationProperty.BrushAnimationPropertyType propertyType = brushAnimationProperty.PropertyType;
					BrushAnimationKeyFrame brushAnimationKeyFrame = null;
					BrushAnimationKeyFrame brushAnimationKeyFrame2;
					if (animation.Loop)
					{
						BrushAnimationKeyFrame frameAt = brushAnimationProperty.GetFrameAt(0);
						if (isFirstCycle && this._brushTimer < frameAt.Time)
						{
							brushAnimationKeyFrame2 = frameAt;
						}
						else
						{
							brushAnimationKeyFrame2 = brushAnimationProperty.GetFrameAfter(brushStateTimer);
							if (brushAnimationKeyFrame2 == null)
							{
								brushAnimationKeyFrame2 = frameAt;
								brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationProperty.Count - 1);
							}
							else if (brushAnimationKeyFrame2 == frameAt)
							{
								brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationProperty.Count - 1);
							}
							else
							{
								brushAnimationKeyFrame = brushAnimationProperty.GetFrameAt(brushAnimationKeyFrame2.Index - 1);
							}
						}
					}
					else
					{
						brushAnimationKeyFrame2 = brushAnimationProperty.GetFrameAfter(brushStateTimer);
						brushAnimationKeyFrame = ((brushAnimationKeyFrame2 != null) ? brushAnimationProperty.GetFrameAt(brushAnimationKeyFrame2.Index - 1) : brushAnimationProperty.GetFrameAt(brushAnimationProperty.Count - 1));
					}
					BrushAnimationKeyFrame brushAnimationKeyFrame3 = null;
					BrushState brushState = default(BrushState);
					Style style = null;
					BrushAnimationKeyFrame brushAnimationKeyFrame4 = null;
					float num3;
					if (brushAnimationKeyFrame2 != null)
					{
						if (brushAnimationKeyFrame != null)
						{
							float num;
							float num2;
							if (animation.Loop)
							{
								if (brushAnimationKeyFrame2.Index == 0)
								{
									num = brushAnimationKeyFrame2.Time + (animation.Duration - brushAnimationKeyFrame.Time);
									if (brushStateTimer >= brushAnimationKeyFrame.Time)
									{
										num2 = brushStateTimer - brushAnimationKeyFrame.Time;
									}
									else
									{
										num2 = animation.Duration - brushAnimationKeyFrame.Time + brushStateTimer;
									}
								}
								else
								{
									num = brushAnimationKeyFrame2.Time - brushAnimationKeyFrame.Time;
									num2 = brushStateTimer - brushAnimationKeyFrame.Time;
								}
							}
							else
							{
								num = brushAnimationKeyFrame2.Time - brushAnimationKeyFrame.Time;
								num2 = brushStateTimer - brushAnimationKeyFrame.Time;
							}
							num3 = num2 * (1f / num);
							brushAnimationKeyFrame3 = brushAnimationKeyFrame;
							brushAnimationKeyFrame4 = brushAnimationKeyFrame2;
						}
						else
						{
							num3 = brushStateTimer * (1f / brushAnimationKeyFrame2.Time);
							brushState = startState;
							brushAnimationKeyFrame4 = brushAnimationKeyFrame2;
						}
					}
					else
					{
						num3 = (brushStateTimer - brushAnimationKeyFrame.Time) * (1f / (animation.Duration - brushAnimationKeyFrame.Time));
						brushAnimationKeyFrame3 = brushAnimationKeyFrame;
						style = source;
					}
					num3 = MathF.Clamp(num3, 0f, 1f);
					switch (propertyType)
					{
					case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineAmount:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowRadius:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextBlur:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowAngle:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextColorFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextAlphaFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextHueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextSaturationFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextValueFactor:
					case BrushAnimationProperty.BrushAnimationPropertyType.XOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.YOffset:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverridenWidth:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverridenHeight:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendLeft:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendRight:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendTop:
					case BrushAnimationProperty.BrushAnimationPropertyType.ExtendBottom:
					{
						float valueFrom = (brushAnimationKeyFrame3 != null) ? brushAnimationKeyFrame3.GetValueAsFloat() : brushState.GetValueAsFloat(propertyType);
						float valueTo = (style != null) ? style.GetValueAsFloat(propertyType) : brushAnimationKeyFrame4.GetValueAsFloat();
						result.SetValueAsFloat(propertyType, MathF.Lerp(valueFrom, valueTo, num3, 1E-05f));
						break;
					}
					case BrushAnimationProperty.BrushAnimationPropertyType.Color:
					case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
					case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
					{
						Color start = (brushAnimationKeyFrame3 != null) ? brushAnimationKeyFrame3.GetValueAsColor() : brushState.GetValueAsColor(propertyType);
						Color end = (style != null) ? style.GetValueAsColor(propertyType) : brushAnimationKeyFrame4.GetValueAsColor();
						BrushAnimationProperty.BrushAnimationPropertyType propertyType2 = propertyType;
						Color color = Color.Lerp(start, end, num3);
						result.SetValueAsColor(propertyType2, color);
						break;
					}
					case BrushAnimationProperty.BrushAnimationPropertyType.Sprite:
					case BrushAnimationProperty.BrushAnimationPropertyType.OverlaySprite:
					{
						Sprite sprite = ((brushAnimationKeyFrame3 != null) ? brushAnimationKeyFrame3.GetValueAsSprite() : null) ?? brushState.GetValueAsSprite(propertyType);
						Sprite sprite2 = ((style != null) ? style.GetValueAsSprite(propertyType) : null) ?? brushAnimationKeyFrame4.GetValueAsSprite();
						result.SetValueAsSprite(propertyType, ((double)num3 <= 0.9) ? sprite : sprite2);
						break;
					}
					}
				}
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008474 File Offset: 0x00006674
		private void EndAnimation()
		{
			if (this.Brush != null)
			{
				Style styleOfCurrentState = this._styleOfCurrentState;
				BrushState brushState = default(BrushState);
				brushState.FillFrom(styleOfCurrentState);
				this._startBrushState = brushState;
				this._currentBrushState = brushState;
				if (this.Brush.TransitionDuration == 0f)
				{
					this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.None;
				}
				foreach (StyleLayer styleLayer in styleOfCurrentState.GetLayers())
				{
					BrushLayerState value = default(BrushLayerState);
					value.FillFrom(styleLayer);
					this._startBrushLayerState[styleLayer.Name] = value;
					this._currentBrushLayerState[styleLayer.Name] = value;
				}
				this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.Ended;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008524 File Offset: 0x00006724
		public void Render(TwoDimensionDrawContext drawContext, Vector2 targetPosition, Vector2 targetSize, float scale, float contextAlpha, Vector2 overlayOffset = default(Vector2))
		{
			if (this.Brush != null)
			{
				if (this.ForcePixelPerfectPlacement)
				{
					targetPosition.X = (float)MathF.Round(targetPosition.X);
					targetPosition.Y = (float)MathF.Round(targetPosition.Y);
				}
				Style styleOfCurrentState = this._styleOfCurrentState;
				for (int i = 0; i < styleOfCurrentState.LayerCount; i++)
				{
					StyleLayer layer = styleOfCurrentState.GetLayer(i);
					if (!layer.IsHidden)
					{
						BrushLayerState brushLayerState;
						if (this._currentBrushLayerState.Count == 1)
						{
							Dictionary<string, BrushLayerState>.ValueCollection.Enumerator enumerator = this._currentBrushLayerState.Values.GetEnumerator();
							enumerator.MoveNext();
							brushLayerState = enumerator.Current;
						}
						else
						{
							brushLayerState = this._currentBrushLayerState[layer.Name];
						}
						Sprite sprite = brushLayerState.Sprite;
						if (sprite != null)
						{
							Texture texture = sprite.Texture;
							if (texture != null)
							{
								float num = targetPosition.X + brushLayerState.XOffset * scale;
								float num2 = targetPosition.Y + brushLayerState.YOffset * scale;
								SimpleMaterial simpleMaterial = drawContext.CreateSimpleMaterial();
								simpleMaterial.OverlayEnabled = false;
								simpleMaterial.CircularMaskingEnabled = false;
								if (layer.OverlayMethod == BrushOverlayMethod.CoverWithTexture && layer.OverlaySprite != null)
								{
									Sprite overlaySprite = layer.OverlaySprite;
									Texture texture2 = overlaySprite.Texture;
									if (texture2 != null)
									{
										simpleMaterial.OverlayEnabled = true;
										simpleMaterial.StartCoordinate = new Vector2(num, num2);
										simpleMaterial.Size = targetSize;
										simpleMaterial.OverlayTexture = texture2;
										simpleMaterial.UseOverlayAlphaAsMask = layer.UseOverlayAlphaAsMask;
										float num3;
										float num4;
										if (layer.UseOverlayAlphaAsMask)
										{
											num3 = brushLayerState.XOffset;
											num4 = brushLayerState.YOffset;
										}
										else if (overlayOffset == default(Vector2))
										{
											num3 = brushLayerState.OverlayXOffset;
											num4 = brushLayerState.OverlayYOffset;
										}
										else
										{
											num3 = overlayOffset.X;
											num4 = overlayOffset.Y;
										}
										if (layer.UseRandomBaseOverlayXOffset)
										{
											num3 += this.GetRandomXOffset();
										}
										if (layer.UseRandomBaseOverlayYOffset)
										{
											num4 += this.GetRandomYOffset();
										}
										simpleMaterial.OverlayXOffset = num3 * scale;
										simpleMaterial.OverlayYOffset = num4 * scale;
										simpleMaterial.Scale = scale;
										simpleMaterial.OverlayTextureWidth = (layer.UseOverlayAlphaAsMask ? targetSize.X : ((float)overlaySprite.Width));
										simpleMaterial.OverlayTextureHeight = (layer.UseOverlayAlphaAsMask ? targetSize.Y : ((float)overlaySprite.Height));
									}
								}
								simpleMaterial.Texture = texture;
								simpleMaterial.Color = brushLayerState.Color * this.Brush.GlobalColor;
								simpleMaterial.ColorFactor = brushLayerState.ColorFactor * this.Brush.GlobalColorFactor;
								simpleMaterial.AlphaFactor = brushLayerState.AlphaFactor * this.Brush.GlobalAlphaFactor * contextAlpha;
								simpleMaterial.HueFactor = brushLayerState.HueFactor;
								simpleMaterial.SaturationFactor = brushLayerState.SaturationFactor;
								simpleMaterial.ValueFactor = brushLayerState.ValueFactor;
								float num5 = 0f;
								float num6 = 0f;
								if (layer.WidthPolicy == BrushLayerSizePolicy.StretchToTarget)
								{
									float num7 = layer.ExtendLeft;
									if (layer.HorizontalFlip)
									{
										num7 = layer.ExtendRight;
									}
									num5 = targetSize.X;
									num5 += (layer.ExtendRight + layer.ExtendLeft) * scale;
									num -= num7 * scale;
								}
								else if (layer.WidthPolicy == BrushLayerSizePolicy.Original)
								{
									num5 = (float)sprite.Width * scale;
								}
								else if (layer.WidthPolicy == BrushLayerSizePolicy.Overriden)
								{
									num5 = layer.OverridenWidth * scale;
								}
								if (layer.HeightPolicy == BrushLayerSizePolicy.StretchToTarget)
								{
									float num8 = layer.ExtendTop;
									if (layer.HorizontalFlip)
									{
										num8 = layer.ExtendBottom;
									}
									num6 = targetSize.Y;
									num6 += (layer.ExtendTop + layer.ExtendBottom) * scale;
									num2 -= num8 * scale;
								}
								else if (layer.HeightPolicy == BrushLayerSizePolicy.Original)
								{
									num6 = (float)sprite.Height * scale;
								}
								else if (layer.HeightPolicy == BrushLayerSizePolicy.Overriden)
								{
									num6 = layer.OverridenHeight * scale;
								}
								bool horizontalFlip = layer.HorizontalFlip;
								bool verticalFlip = layer.VerticalFlip;
								drawContext.DrawSprite(sprite, simpleMaterial, num, num2, scale, num5, num6, horizontalFlip, verticalFlip);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008918 File Offset: 0x00006B18
		public TextMaterial CreateTextMaterial(TwoDimensionDrawContext drawContext)
		{
			TextMaterial textMaterial = this._currentBrushState.CreateTextMaterial(drawContext);
			if (this.Brush != null)
			{
				textMaterial.ColorFactor *= this.Brush.GlobalColorFactor;
				textMaterial.AlphaFactor *= this.Brush.GlobalAlphaFactor;
				textMaterial.Color *= this.Brush.GlobalColor;
			}
			return textMaterial;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008988 File Offset: 0x00006B88
		public void RestartAnimation()
		{
			if (this.Brush != null)
			{
				this._brushLocalTimer = 0f;
				Style styleOfCurrentState = this._styleOfCurrentState;
				this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.None;
				if (styleOfCurrentState != null)
				{
					if (styleOfCurrentState.AnimationMode == StyleAnimationMode.BasicTransition)
					{
						this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.PlayingBasicTranisition;
						return;
					}
					if (styleOfCurrentState.AnimationMode == StyleAnimationMode.Animation)
					{
						this._brushRendererAnimationState = BrushRenderer.BrushRendererAnimationState.PlayingAnimation;
					}
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000089DA File Offset: 0x00006BDA
		public void SetSeed(int seed)
		{
			this._offsetSeed = seed;
		}

		// Token: 0x0400007A RID: 122
		private BrushState _startBrushState;

		// Token: 0x0400007B RID: 123
		private BrushState _currentBrushState;

		// Token: 0x0400007C RID: 124
		private Dictionary<string, BrushLayerState> _startBrushLayerState;

		// Token: 0x0400007D RID: 125
		private Dictionary<string, BrushLayerState> _currentBrushLayerState;

		// Token: 0x0400007E RID: 126
		public bool UseLocalTimer;

		// Token: 0x0400007F RID: 127
		private float _brushLocalTimer;

		// Token: 0x04000080 RID: 128
		private float _globalTime;

		// Token: 0x04000081 RID: 129
		private int _offsetSeed;

		// Token: 0x04000082 RID: 130
		private float _randomXOffset;

		// Token: 0x04000083 RID: 131
		private float _randomYOffset;

		// Token: 0x04000084 RID: 132
		private BrushRenderer.BrushRendererAnimationState _brushRendererAnimationState;

		// Token: 0x04000086 RID: 134
		private Brush _brush;

		// Token: 0x04000087 RID: 135
		private long _latestStyleVersion;

		// Token: 0x04000088 RID: 136
		private string _currentState;

		// Token: 0x04000089 RID: 137
		private Style _styleOfCurrentState;

		// Token: 0x02000079 RID: 121
		public enum BrushRendererAnimationState
		{
			// Token: 0x0400041B RID: 1051
			None,
			// Token: 0x0400041C RID: 1052
			PlayingAnimation,
			// Token: 0x0400041D RID: 1053
			PlayingBasicTranisition,
			// Token: 0x0400041E RID: 1054
			Ended
		}
	}
}
