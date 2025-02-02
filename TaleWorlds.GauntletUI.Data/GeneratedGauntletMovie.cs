using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x02000004 RID: 4
	public class GeneratedGauntletMovie : IGauntletMovie
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000037C6 File Offset: 0x000019C6
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000037CE File Offset: 0x000019CE
		public UIContext Context { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000037D7 File Offset: 0x000019D7
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000037DF File Offset: 0x000019DF
		public Widget RootWidget { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000037E8 File Offset: 0x000019E8
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000037F0 File Offset: 0x000019F0
		public string MovieName { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000037F9 File Offset: 0x000019F9
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003801 File Offset: 0x00001A01
		public bool IsReleased { get; private set; }

		// Token: 0x06000062 RID: 98 RVA: 0x0000380C File Offset: 0x00001A0C
		public GeneratedGauntletMovie(string movieName, Widget rootWidget)
		{
			this.MovieName = movieName;
			this.RootWidget = rootWidget;
			this.Context = rootWidget.Context;
			this._root = (IGeneratedGauntletMovieRoot)rootWidget;
			this._movieRootNode = new Widget(this.Context);
			this.Context.Root.AddChild(this._movieRootNode);
			this._movieRootNode.WidthSizePolicy = SizePolicy.Fixed;
			this._movieRootNode.HeightSizePolicy = SizePolicy.Fixed;
			this._movieRootNode.ScaledSuggestedWidth = this.Context.TwoDimensionContext.Width;
			this._movieRootNode.ScaledSuggestedHeight = this.Context.TwoDimensionContext.Height;
			this._movieRootNode.DoNotAcceptEvents = true;
			this._movieRootNode.AddChild(rootWidget);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000038D2 File Offset: 0x00001AD2
		public void Update()
		{
			this._movieRootNode.ScaledSuggestedWidth = this.Context.TwoDimensionContext.Width;
			this._movieRootNode.ScaledSuggestedHeight = this.Context.TwoDimensionContext.Height;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000390C File Offset: 0x00001B0C
		public void Release()
		{
			this.IsReleased = true;
			this._movieRootNode.OnBeforeRemovedChild(this._movieRootNode);
			this._root.DestroyDataSource();
			this._movieRootNode.ParentWidget = null;
			this.Context.OnMovieReleased(this.MovieName);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003959 File Offset: 0x00001B59
		public void RefreshBindingWithChildren()
		{
			this._root.RefreshBindingWithChildren();
		}

		// Token: 0x04000018 RID: 24
		private Widget _movieRootNode;

		// Token: 0x0400001A RID: 26
		private IGeneratedGauntletMovieRoot _root;
	}
}
