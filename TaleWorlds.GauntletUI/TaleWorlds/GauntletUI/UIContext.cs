using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000030 RID: 48
	public class UIContext
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E7C9 File Offset: 0x0000C9C9
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000E7D1 File Offset: 0x0000C9D1
		public UIContext.MouseCursors ActiveCursorOfContext { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E7DA File Offset: 0x0000C9DA
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000E7E2 File Offset: 0x0000C9E2
		public bool IsDynamicScaleEnabled { get; set; } = true;

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E7EB File Offset: 0x0000C9EB
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000E7F3 File Offset: 0x0000C9F3
		public float ScaleModifier { get; set; } = 1f;

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000E804 File Offset: 0x0000CA04
		public float ContextAlpha { get; set; } = 1f;

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E80D File Offset: 0x0000CA0D
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000E815 File Offset: 0x0000CA15
		public float Scale { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E81E File Offset: 0x0000CA1E
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000E826 File Offset: 0x0000CA26
		public float CustomScale { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000E82F File Offset: 0x0000CA2F
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000E837 File Offset: 0x0000CA37
		public float CustomInverseScale { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000E840 File Offset: 0x0000CA40
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000E848 File Offset: 0x0000CA48
		public string CurrentLanugageCode { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000E851 File Offset: 0x0000CA51
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000E859 File Offset: 0x0000CA59
		public Random UIRandom { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000E862 File Offset: 0x0000CA62
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000E86A File Offset: 0x0000CA6A
		public float InverseScale { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000E873 File Offset: 0x0000CA73
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000E87B File Offset: 0x0000CA7B
		public EventManager EventManager { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000E884 File Offset: 0x0000CA84
		public Widget Root
		{
			get
			{
				return this.EventManager.Root;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000E891 File Offset: 0x0000CA91
		public ResourceDepot ResourceDepot
		{
			get
			{
				return this.TwoDimensionContext.ResourceDepot;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000E89E File Offset: 0x0000CA9E
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000E8A6 File Offset: 0x0000CAA6
		public TwoDimensionContext TwoDimensionContext { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000E8AF File Offset: 0x0000CAAF
		public IEnumerable<Brush> Brushes
		{
			get
			{
				return this.BrushFactory.Brushes;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000E8BC File Offset: 0x0000CABC
		public Brush DefaultBrush
		{
			get
			{
				return this.BrushFactory.DefaultBrush;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000E8C9 File Offset: 0x0000CAC9
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000E8D1 File Offset: 0x0000CAD1
		public SpriteData SpriteData { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000E8DA File Offset: 0x0000CADA
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		public BrushFactory BrushFactory { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000E8EB File Offset: 0x0000CAEB
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000E8F3 File Offset: 0x0000CAF3
		public FontFactory FontFactory { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000E904 File Offset: 0x0000CB04
		public IGamepadNavigationContext GamepadNavigation { get; private set; }

		// Token: 0x06000361 RID: 865 RVA: 0x0000E910 File Offset: 0x0000CB10
		public UIContext(TwoDimensionContext twoDimensionContext, IInputContext inputContext, IInputService inputService, SpriteData spriteData, FontFactory fontFactory, BrushFactory brushFactory)
		{
			this.TwoDimensionContext = twoDimensionContext;
			this._inputContext = inputContext;
			this._inputService = inputService;
			this.GamepadNavigation = new EmptyGamepadNavigationContext();
			this.SpriteData = spriteData;
			this.FontFactory = fontFactory;
			this.BrushFactory = brushFactory;
			this._initializedWithExistingResources = true;
			this.ReferenceHeight = twoDimensionContext.Platform.ReferenceHeight;
			this.InverseReferenceHeight = 1f / this.ReferenceHeight;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public UIContext(TwoDimensionContext twoDimensionContext, IInputContext inputContext, IInputService inputService)
		{
			this.TwoDimensionContext = twoDimensionContext;
			this._inputContext = inputContext;
			this._inputService = inputService;
			this.GamepadNavigation = new EmptyGamepadNavigationContext();
			this._initializedWithExistingResources = false;
			this.ReferenceHeight = twoDimensionContext.Platform.ReferenceHeight;
			this.InverseReferenceHeight = 1f / this.ReferenceHeight;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000EA1E File Offset: 0x0000CC1E
		public Brush GetBrush(string name)
		{
			return this.BrushFactory.GetBrush(name);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		public void Initialize()
		{
			if (!this._initializedWithExistingResources)
			{
				this.SpriteData = new SpriteData("SpriteData");
				this.SpriteData.Load(this.ResourceDepot);
				this.FontFactory = new FontFactory(this.ResourceDepot);
				this.FontFactory.LoadAllFonts(this.SpriteData);
				this.BrushFactory = new BrushFactory(this.ResourceDepot, "Brushes", this.SpriteData, this.FontFactory);
				this.BrushFactory.Initialize();
			}
			this.EventManager = new EventManager(this);
			this.EventManager.InputService = this._inputService;
			this.EventManager.InputContext = this._inputContext;
			this.EventManager.UpdateMousePosition(this._inputContext.GetPointerPosition());
			Widget root = this.Root;
			root.WidthSizePolicy = SizePolicy.Fixed;
			root.HeightSizePolicy = SizePolicy.Fixed;
			root.SuggestedWidth = this.TwoDimensionContext.Width;
			root.SuggestedHeight = this.TwoDimensionContext.Height;
			this.UIRandom = new Random();
			this.UpdateScale();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000EB3B File Offset: 0x0000CD3B
		public void InitializeGamepadNavigation(IGamepadNavigationContext context)
		{
			this.GamepadNavigation = context;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000EB44 File Offset: 0x0000CD44
		private void UpdateScale()
		{
			float num;
			if (this.TwoDimensionContext != null)
			{
				num = this.TwoDimensionContext.Height * this.InverseReferenceHeight;
				float num2 = this.TwoDimensionContext.Width / this.TwoDimensionContext.Height;
				if (num2 < 1.7422223f)
				{
					float num3 = num2 / 1.7422223f;
					num *= num3;
				}
			}
			else
			{
				num = 1f;
			}
			if (this.Scale != num || this.CustomScale != this.Scale * this.ScaleModifier)
			{
				this.Scale = num;
				this.CustomScale = this.Scale * this.ScaleModifier;
				this.InverseScale = 1f / num;
				this.CustomInverseScale = 1f / this.CustomScale;
				this.EventManager.UpdateLayout();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public void OnFinalize()
		{
			this.EventManager.OnFinalize();
			this.GamepadNavigation.OnFinalize();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public void Update(float dt)
		{
			this.ActiveCursorOfContext = UIContext.MouseCursors.Default;
			if (!this._initializedWithExistingResources)
			{
				this.BrushFactory.CheckForUpdates();
			}
			if (this.IsDynamicScaleEnabled)
			{
				this.UpdateScale();
			}
			Widget root = this.Root;
			root.SuggestedWidth = this.TwoDimensionContext.Width;
			root.SuggestedHeight = this.TwoDimensionContext.Height;
			this.EventManager.Update(dt);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000EC88 File Offset: 0x0000CE88
		public void LateUpdate(float dt)
		{
			Vector2 pageSize = new Vector2(this.TwoDimensionContext.Width, this.TwoDimensionContext.Height);
			this.EventManager.CalculateCanvas(pageSize, dt);
			this.EventManager.LateUpdate(dt);
			this.EventManager.RecalculateCanvas();
			this.EventManager.UpdateBrushes(dt);
			this.EventManager.Render(this.TwoDimensionContext);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public void OnOnScreenkeyboardTextInputDone(string inputText)
		{
			EditableTextWidget editableTextWidget;
			if ((editableTextWidget = (this.EventManager.FocusedWidget as EditableTextWidget)) != null)
			{
				editableTextWidget.SetAllText(inputText);
			}
			this.EventManager.ClearFocus();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000ED27 File Offset: 0x0000CF27
		public void OnOnScreenKeyboardCanceled()
		{
			this.EventManager.ClearFocus();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000ED34 File Offset: 0x0000CF34
		public bool HitTest(Widget root, Vector2 position)
		{
			return EventManager.HitTest(root, position);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000ED3D File Offset: 0x0000CF3D
		public bool HitTest(Widget root)
		{
			return EventManager.HitTest(root, this._inputContext.GetPointerPosition());
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000ED50 File Offset: 0x0000CF50
		public bool FocusTest(Widget root)
		{
			return this.EventManager.FocusTest(root);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000ED60 File Offset: 0x0000CF60
		public void UpdateInput(InputType handleInputs)
		{
			if (this._inputService.MouseEnabled)
			{
				this.EventManager.UpdateMousePosition(this._inputContext.GetPointerPosition());
				if (handleInputs.HasAnyFlag(InputType.MouseButton))
				{
					this.EventManager.MouseMove();
					foreach (InputKey key in this._inputContext.GetClickKeys())
					{
						if (this._inputContext.IsKeyPressed(key))
						{
							this.EventManager.MouseDown();
							break;
						}
					}
					InputKey[] clickKeys;
					foreach (InputKey key2 in clickKeys)
					{
						if (this._inputContext.IsKeyReleased(key2))
						{
							this.EventManager.MouseUp();
							break;
						}
					}
					if (this._inputContext.IsKeyPressed(InputKey.RightMouseButton))
					{
						this.EventManager.MouseAlternateDown();
					}
					if (this._inputContext.IsKeyReleased(InputKey.RightMouseButton))
					{
						this.EventManager.MouseAlternateUp();
					}
				}
				if (handleInputs.HasAnyFlag(InputType.MouseWheel))
				{
					this.EventManager.MouseScroll();
				}
				this.EventManager.RightStickMovement();
				this._previousFrameMouseEnabled = true;
				return;
			}
			if (this._previousFrameMouseEnabled)
			{
				this.EventManager.UpdateMousePosition(new Vector2(-5000f, -5000f));
				this.EventManager.MouseMove();
				this.EventManager.SetHoveredView(null);
				this._previousFrameMouseEnabled = false;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000EEB3 File Offset: 0x0000D0B3
		public void OnMovieLoaded(string movieName)
		{
			this.GamepadNavigation.OnMovieLoaded(movieName);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000EEC1 File Offset: 0x0000D0C1
		public void OnMovieReleased(string movieName)
		{
			this.GamepadNavigation.OnMovieReleased(movieName);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		public void DrawWidgetDebugInfo()
		{
			if (Input.IsKeyDown(InputKey.LeftShift) && Input.IsKeyPressed(InputKey.F))
			{
				this.IsDebugWidgetInformationFroze = !this.IsDebugWidgetInformationFroze;
				this._currentRootNode = new UIContext.DebugWidgetTreeNode(this.TwoDimensionContext, this.Root, 0);
			}
			if (this.IsDebugWidgetInformationFroze)
			{
				UIContext.DebugWidgetTreeNode currentRootNode = this._currentRootNode;
				if (currentRootNode == null)
				{
					return;
				}
				currentRootNode.DebugDraw();
			}
		}

		// Token: 0x04000191 RID: 401
		private readonly float ReferenceHeight;

		// Token: 0x04000192 RID: 402
		private readonly float InverseReferenceHeight;

		// Token: 0x04000193 RID: 403
		private const float ReferenceAspectRatio = 1.7777778f;

		// Token: 0x04000194 RID: 404
		private const float ReferenceAspectRatioCoeff = 0.98f;

		// Token: 0x040001A0 RID: 416
		private IInputContext _inputContext;

		// Token: 0x040001A1 RID: 417
		private IInputService _inputService;

		// Token: 0x040001A6 RID: 422
		private bool _initializedWithExistingResources;

		// Token: 0x040001A7 RID: 423
		private bool _previousFrameMouseEnabled;

		// Token: 0x040001A8 RID: 424
		private bool IsDebugWidgetInformationFroze;

		// Token: 0x040001A9 RID: 425
		private UIContext.DebugWidgetTreeNode _currentRootNode;

		// Token: 0x0200007F RID: 127
		public enum MouseCursors
		{
			// Token: 0x0400043D RID: 1085
			System,
			// Token: 0x0400043E RID: 1086
			Default,
			// Token: 0x0400043F RID: 1087
			Attack,
			// Token: 0x04000440 RID: 1088
			Move,
			// Token: 0x04000441 RID: 1089
			HorizontalResize,
			// Token: 0x04000442 RID: 1090
			VerticalResize,
			// Token: 0x04000443 RID: 1091
			DiagonalRightResize,
			// Token: 0x04000444 RID: 1092
			DiagonalLeftResize,
			// Token: 0x04000445 RID: 1093
			Rotate,
			// Token: 0x04000446 RID: 1094
			Custom,
			// Token: 0x04000447 RID: 1095
			Disabled,
			// Token: 0x04000448 RID: 1096
			RightClickLink
		}

		// Token: 0x02000080 RID: 128
		private class DebugWidgetTreeNode
		{
			// Token: 0x1700029F RID: 671
			// (get) Token: 0x060008ED RID: 2285 RVA: 0x00023403 File Offset: 0x00021603
			private string ID
			{
				get
				{
					return string.Format("{0}.{1}.{2}", this._depth, this._current.GetSiblingIndex(), this._fullIDPath);
				}
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x00023430 File Offset: 0x00021630
			public DebugWidgetTreeNode(TwoDimensionContext context, Widget current, int depth)
			{
				this._context = context;
				this._current = current;
				this._depth = depth;
				Widget current2 = this._current;
				this._fullIDPath = (((current2 != null) ? current2.GetFullIDPath() : null) ?? string.Empty);
				int num = this._fullIDPath.LastIndexOf('\\');
				if (num != -1)
				{
					this._displayedName = this._fullIDPath.Substring(num + 1);
				}
				if (string.IsNullOrEmpty(this._displayedName))
				{
					this._displayedName = this._current.Id;
				}
				this._children = new List<UIContext.DebugWidgetTreeNode>();
				this.AddChildren();
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x000234D0 File Offset: 0x000216D0
			private void AddChildren()
			{
				foreach (Widget widget in this._current.Children)
				{
					if (widget.ParentWidget == this._current)
					{
						UIContext.DebugWidgetTreeNode item = new UIContext.DebugWidgetTreeNode(this._context, widget, this._depth + 1);
						this._children.Add(item);
					}
				}
			}

			// Token: 0x060008F0 RID: 2288 RVA: 0x00023550 File Offset: 0x00021750
			public void DebugDraw()
			{
				if (this._context.DrawDebugTreeNode(this._displayedName + "###Root." + this.ID))
				{
					if (this._context.IsDebugItemHovered())
					{
						this.DrawArea();
					}
					this._context.DrawCheckbox("Show Area###Area." + this.ID, ref this._isShowingArea);
					if (this._isShowingArea)
					{
						this.DrawArea();
					}
					this.DrawProperties();
					this.DrawChildren();
					this._context.PopDebugTreeNode();
					return;
				}
				if (this._context.IsDebugItemHovered())
				{
					this.DrawArea();
				}
			}

			// Token: 0x060008F1 RID: 2289 RVA: 0x000235F0 File Offset: 0x000217F0
			private void DrawProperties()
			{
				if (this._context.DrawDebugTreeNode("Properties###Properties." + this.ID))
				{
					this._context.DrawDebugText("General");
					string str = string.IsNullOrEmpty(this._current.Id) ? "_No ID_" : this._current.Id;
					this._context.DrawDebugText("\tID: " + str);
					this._context.DrawDebugText("\tPath: " + this._current.GetFullIDPath());
					this._context.DrawDebugText(string.Format("\tVisible: {0}", this._current.IsVisible));
					this._context.DrawDebugText(string.Format("\tEnabled: {0}", this._current.IsEnabled));
					this._context.DrawDebugText("\nSize");
					this._context.DrawDebugText(string.Format("\tWidth Size Policy: {0}", this._current.WidthSizePolicy));
					this._context.DrawDebugText(string.Format("\tHeight Size Policy: {0}", this._current.HeightSizePolicy));
					this._context.DrawDebugText(string.Format("\tSize: {0}", this._current.Size));
					this._context.DrawDebugText("\nPosition");
					this._context.DrawDebugText(string.Format("\tGlobal Position: {0}", this._current.GlobalPosition));
					this._context.DrawDebugText(string.Format("\tLocal Position: {0}", this._current.LocalPosition));
					this._context.DrawDebugText(string.Format("\tPosition Offset: <{0}, {1}>", this._current.PositionXOffset, this._current.PositionYOffset));
					this._context.DrawDebugText("\nEvents");
					this._context.DrawDebugText("\tCurrent State: " + this._current.CurrentState);
					this._context.DrawDebugText(string.Format("\tCan Accept Events: {0}", this._current.CanAcceptEvents));
					this._context.DrawDebugText(string.Format("\tPasses Events To Children: {0}", !this._current.DoNotPassEventsToChildren));
					this._context.DrawDebugText("\nVisuals");
					BrushWidget brushWidget = this._current as BrushWidget;
					if (brushWidget != null)
					{
						this._context.DrawDebugText("\tBrush: " + brushWidget.Brush.Name);
					}
					TextWidget textWidget;
					RichTextWidget richTextWidget;
					if ((textWidget = (this._current as TextWidget)) != null)
					{
						this._context.DrawDebugText("\tText: " + textWidget.Text);
					}
					else if ((richTextWidget = (this._current as RichTextWidget)) != null)
					{
						this._context.DrawDebugText("\tText: " + richTextWidget.Text);
					}
					else if (brushWidget != null)
					{
						TwoDimensionContext context = this._context;
						string str2 = "\tSprite: ";
						BrushRenderer brushRenderer = brushWidget.BrushRenderer;
						string text;
						if (brushRenderer == null)
						{
							text = null;
						}
						else
						{
							Style currentStyle = brushRenderer.CurrentStyle;
							if (currentStyle == null)
							{
								text = null;
							}
							else
							{
								StyleLayer layer = currentStyle.GetLayer(brushWidget.BrushRenderer.CurrentState);
								if (layer == null)
								{
									text = null;
								}
								else
								{
									Sprite sprite = layer.Sprite;
									text = ((sprite != null) ? sprite.Name : null);
								}
							}
						}
						context.DrawDebugText(str2 + (text ?? "None"));
						TwoDimensionContext context2 = this._context;
						string str3 = "\tColor: ";
						Brush brush = brushWidget.Brush;
						string str4;
						if (brush == null)
						{
							str4 = null;
						}
						else
						{
							BrushLayer layer2 = brush.GetLayer(brushWidget.CurrentState);
							str4 = ((layer2 != null) ? layer2.ToString() : null);
						}
						context2.DrawDebugText(str3 + str4);
					}
					else
					{
						TwoDimensionContext context3 = this._context;
						string str5 = "\tSprite: ";
						Sprite sprite2 = this._current.Sprite;
						context3.DrawDebugText(str5 + (((sprite2 != null) ? sprite2.Name : null) ?? "None"));
						this._context.DrawDebugText("\tColor: " + this._current.Color.ToString());
					}
					this._context.PopDebugTreeNode();
				}
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x00023A0C File Offset: 0x00021C0C
			private void DrawChildren()
			{
				if (this._children.Count > 0 && this._context.DrawDebugTreeNode("Children###Children." + this.ID))
				{
					foreach (UIContext.DebugWidgetTreeNode debugWidgetTreeNode in this._children)
					{
						debugWidgetTreeNode.DebugDraw();
					}
					this._context.PopDebugTreeNode();
				}
			}

			// Token: 0x060008F3 RID: 2291 RVA: 0x00023A94 File Offset: 0x00021C94
			private void DrawArea()
			{
				float x = this._current.GlobalPosition.X;
				float y = this._current.GlobalPosition.Y;
				float num = this._current.GlobalPosition.X + this._current.Size.X;
				float num2 = this._current.GlobalPosition.Y + this._current.Size.Y;
				if (x == num || y == num2 || this._current.Size.X == 0f || this._current.Size.Y == 0f)
				{
					return;
				}
				float num3 = 2f;
				float num4 = num3 / 2f;
				float num5 = num3 / 2f;
				float num6 = num3 / 2f;
				float num7 = num3 / 2f;
				float num8 = num3 / 2f;
				float num9 = num3 / 2f;
				float num10 = num3 / 2f;
				float num11 = num3 / 2f;
			}

			// Token: 0x04000449 RID: 1097
			private readonly TwoDimensionContext _context;

			// Token: 0x0400044A RID: 1098
			private readonly Widget _current;

			// Token: 0x0400044B RID: 1099
			private readonly List<UIContext.DebugWidgetTreeNode> _children;

			// Token: 0x0400044C RID: 1100
			private readonly string _fullIDPath;

			// Token: 0x0400044D RID: 1101
			private readonly string _displayedName;

			// Token: 0x0400044E RID: 1102
			private readonly int _depth;

			// Token: 0x0400044F RID: 1103
			private bool _isShowingArea;
		}
	}
}
