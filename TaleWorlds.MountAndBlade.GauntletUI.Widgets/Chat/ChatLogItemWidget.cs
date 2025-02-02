using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Chat
{
	// Token: 0x0200016F RID: 367
	public class ChatLogItemWidget : Widget
	{
		// Token: 0x060012F4 RID: 4852 RVA: 0x00033B87 File Offset: 0x00031D87
		public ChatLogItemWidget(UIContext context) : base(context)
		{
			this._fullyInsideAction = new Action<Widget>(this.UpdateWidgetFullyInside);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00033BC0 File Offset: 0x00031DC0
		private void UpdateWidgetFullyInside(Widget widget)
		{
			widget.DoNotRenderIfNotFullyInsideScissor = false;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00033BC9 File Offset: 0x00031DC9
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			base.ApplyActionOnAllChildren(this._fullyInsideAction);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00033BE0 File Offset: 0x00031DE0
		private void PostMessage(string message)
		{
			if (message.IndexOf(this._detailOpeningTag, StringComparison.Ordinal) > 0)
			{
				foreach (ChatLogItemWidget.ChatMultiLineElement chatMultiLineElement in this.GetFormattedLinesFromMessage(message))
				{
					RichTextWidget widget = new RichTextWidget(base.Context)
					{
						Id = "FormattedLineRichTextWidget",
						WidthSizePolicy = SizePolicy.StretchToParent,
						HeightSizePolicy = SizePolicy.CoverChildren,
						Brush = this.OneLineTextWidget.ReadOnlyBrush,
						MarginTop = -2f,
						MarginBottom = -2f,
						IsEnabled = false,
						Text = chatMultiLineElement.Line,
						MarginLeft = (float)(chatMultiLineElement.IdentModifier * this._defaultMarginLeftPerIndent) * base._inverseScaleToUse,
						ClipContents = false,
						DoNotRenderIfNotFullyInsideScissor = false
					};
					this.CollapsableWidget.AddChild(widget);
				}
				this.CollapsableWidget.IsVisible = true;
				this.OneLineTextWidget.IsVisible = false;
				return;
			}
			this.OneLineTextWidget.Text = message;
			this.CollapsableWidget.IsVisible = false;
			this.OneLineTextWidget.IsVisible = true;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00033D18 File Offset: 0x00031F18
		private List<ChatLogItemWidget.ChatMultiLineElement> GetFormattedLinesFromMessage(string message)
		{
			List<ChatLogItemWidget.ChatMultiLineElement> list = new List<ChatLogItemWidget.ChatMultiLineElement>();
			XmlDocument xmlDocument = new XmlDocument();
			int num = message.IndexOf(this._detailOpeningTag, StringComparison.Ordinal);
			string line = message.Substring(0, num);
			string text = message.Substring(num, message.Length - num);
			text = this._detailOpeningTag + text + this._detailClosingTag;
			list.Add(new ChatLogItemWidget.ChatMultiLineElement(line, 0));
			try
			{
				xmlDocument.LoadXml(text);
				this.AddLinesFromXMLRecur(xmlDocument.FirstChild, ref list, 0);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Couldn't parse chat log message: " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Chat\\ChatLogItemWidget.cs", "GetFormattedLinesFromMessage", 111);
			}
			return list;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00033DCC File Offset: 0x00031FCC
		private void AddLinesFromXMLRecur(XmlNode currentNode, ref List<ChatLogItemWidget.ChatMultiLineElement> lineList, int currentIndentModifier)
		{
			if (currentNode.NodeType == XmlNodeType.Text)
			{
				lineList.Add(new ChatLogItemWidget.ChatMultiLineElement(currentNode.InnerText, currentIndentModifier));
				for (int i = 0; i < currentNode.ChildNodes.Count; i++)
				{
					this.AddLinesFromXMLRecur(currentNode.ChildNodes.Item(i), ref lineList, currentIndentModifier + 1);
				}
				return;
			}
			for (int j = 0; j < currentNode.ChildNodes.Count; j++)
			{
				this.AddLinesFromXMLRecur(currentNode.ChildNodes.Item(j), ref lineList, currentIndentModifier + 1);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00033E4E File Offset: 0x0003204E
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00033E56 File Offset: 0x00032056
		[Editor(false)]
		public RichTextWidget OneLineTextWidget
		{
			get
			{
				return this._oneLineTextWidget;
			}
			set
			{
				if (this._oneLineTextWidget != value)
				{
					this._oneLineTextWidget = value;
				}
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00033E68 File Offset: 0x00032068
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x00033E70 File Offset: 0x00032070
		[Editor(false)]
		public ChatCollapsableListPanel CollapsableWidget
		{
			get
			{
				return this._collapsableWidget;
			}
			set
			{
				if (this._collapsableWidget != value)
				{
					this._collapsableWidget = value;
				}
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00033E82 File Offset: 0x00032082
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x00033E8A File Offset: 0x0003208A
		[Editor(false)]
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
					this.PostMessage(value);
				}
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00033EA8 File Offset: 0x000320A8
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x00033EB0 File Offset: 0x000320B0
		[Editor(false)]
		public ChatLogWidget ChatLogWidget
		{
			get
			{
				return this._chatLogWidget;
			}
			set
			{
				if (this._chatLogWidget != value)
				{
					this._chatLogWidget = value;
				}
			}
		}

		// Token: 0x04000899 RID: 2201
		private int _defaultMarginLeftPerIndent = 20;

		// Token: 0x0400089A RID: 2202
		private string _detailOpeningTag = "<Detail>";

		// Token: 0x0400089B RID: 2203
		private string _detailClosingTag = "</Detail>";

		// Token: 0x0400089C RID: 2204
		private Action<Widget> _fullyInsideAction;

		// Token: 0x0400089D RID: 2205
		private ChatLogWidget _chatLogWidget;

		// Token: 0x0400089E RID: 2206
		private string _chatLine;

		// Token: 0x0400089F RID: 2207
		private RichTextWidget _oneLineTextWidget;

		// Token: 0x040008A0 RID: 2208
		private ChatCollapsableListPanel _collapsableWidget;

		// Token: 0x020001BA RID: 442
		public struct ChatMultiLineElement
		{
			// Token: 0x060014A1 RID: 5281 RVA: 0x0003815F File Offset: 0x0003635F
			public ChatMultiLineElement(string line, int identModifier)
			{
				this.Line = line;
				this.IdentModifier = identModifier;
			}

			// Token: 0x040009E9 RID: 2537
			public string Line;

			// Token: 0x040009EA RID: 2538
			public int IdentModifier;
		}
	}
}
