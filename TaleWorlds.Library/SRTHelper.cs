using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TaleWorlds.Library
{
	// Token: 0x02000089 RID: 137
	public static class SRTHelper
	{
		// Token: 0x020000D6 RID: 214
		public static class SrtParser
		{
			// Token: 0x06000717 RID: 1815 RVA: 0x000164DC File Offset: 0x000146DC
			public static List<SRTHelper.SubtitleItem> ParseStream(Stream subtitleStream, Encoding encoding)
			{
				if (!subtitleStream.CanRead || !subtitleStream.CanSeek)
				{
					throw new ArgumentException("Given subtitle file is not readable.");
				}
				subtitleStream.Position = 0L;
				TextReader reader = new StreamReader(subtitleStream, encoding, true);
				List<SRTHelper.SubtitleItem> list = new List<SRTHelper.SubtitleItem>();
				List<string> list2 = SRTHelper.SrtParser.GetSrtSubTitleParts(reader).ToList<string>();
				if (list2.Count <= 0)
				{
					throw new FormatException("Parsing as srt returned no srt part.");
				}
				foreach (string text in list2)
				{
					List<string> list3 = (from s in text.Split(new string[]
					{
						Environment.NewLine
					}, StringSplitOptions.None)
					select s.Trim() into l
					where !string.IsNullOrEmpty(l)
					select l).ToList<string>();
					SRTHelper.SubtitleItem subtitleItem = new SRTHelper.SubtitleItem();
					foreach (string text2 in list3)
					{
						if (subtitleItem.StartTime == 0 && subtitleItem.EndTime == 0)
						{
							int startTime;
							int endTime;
							if (SRTHelper.SrtParser.TryParseTimecodeLine(text2, out startTime, out endTime))
							{
								subtitleItem.StartTime = startTime;
								subtitleItem.EndTime = endTime;
							}
						}
						else
						{
							subtitleItem.Lines.Add(text2);
						}
					}
					if ((subtitleItem.StartTime != 0 || subtitleItem.EndTime != 0) && subtitleItem.Lines.Count > 0)
					{
						list.Add(subtitleItem);
					}
				}
				if (list.Count > 0)
				{
					return list;
				}
				throw new ArgumentException("Stream is not in a valid Srt format");
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x000166A8 File Offset: 0x000148A8
			private static IEnumerable<string> GetSrtSubTitleParts(TextReader reader)
			{
				MBStringBuilder sb = default(MBStringBuilder);
				sb.Initialize(16, "GetSrtSubTitleParts");
				string text;
				while ((text = reader.ReadLine()) != null)
				{
					if (string.IsNullOrEmpty(text.Trim()))
					{
						string text2 = sb.ToStringAndRelease().TrimEnd(Array.Empty<char>());
						if (!string.IsNullOrEmpty(text2))
						{
							yield return text2;
						}
						sb.Initialize(16, "GetSrtSubTitleParts");
					}
					else
					{
						sb.AppendLine<string>(text);
					}
				}
				if (sb.Length > 0)
				{
					yield return sb.ToStringAndRelease();
				}
				else
				{
					sb.Release();
				}
				yield break;
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x000166B8 File Offset: 0x000148B8
			private static bool TryParseTimecodeLine(string line, out int startTc, out int endTc)
			{
				string[] array = line.Split(SRTHelper.SrtParser._delimiters, StringSplitOptions.None);
				if (array.Length != 2)
				{
					startTc = -1;
					endTc = -1;
					return false;
				}
				startTc = SRTHelper.SrtParser.ParseSrtTimecode(array[0]);
				endTc = SRTHelper.SrtParser.ParseSrtTimecode(array[1]);
				return true;
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x000166F8 File Offset: 0x000148F8
			private static int ParseSrtTimecode(string s)
			{
				Match match = Regex.Match(s, "[0-9]+:[0-9]+:[0-9]+([,\\.][0-9]+)?");
				if (match.Success)
				{
					s = match.Value;
					TimeSpan timeSpan;
					if (TimeSpan.TryParse(s.Replace(',', '.'), out timeSpan))
					{
						return (int)timeSpan.TotalMilliseconds;
					}
				}
				return -1;
			}

			// Token: 0x040002A7 RID: 679
			private static readonly string[] _delimiters = new string[]
			{
				"-->",
				"- >",
				"->"
			};
		}

		// Token: 0x020000D7 RID: 215
		public static class StreamHelpers
		{
			// Token: 0x0600071C RID: 1820 RVA: 0x00016764 File Offset: 0x00014964
			public static Stream CopyStream(Stream inputStream)
			{
				MemoryStream memoryStream = new MemoryStream();
				int num;
				do
				{
					byte[] buffer = new byte[1024];
					num = inputStream.Read(buffer, 0, 1024);
					memoryStream.Write(buffer, 0, num);
				}
				while (inputStream.CanRead && num > 0);
				memoryStream.ToArray();
				return memoryStream;
			}
		}

		// Token: 0x020000D8 RID: 216
		public class SubtitleItem
		{
			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x000167AD File Offset: 0x000149AD
			// (set) Token: 0x0600071E RID: 1822 RVA: 0x000167B5 File Offset: 0x000149B5
			public int StartTime { get; set; }

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x000167BE File Offset: 0x000149BE
			// (set) Token: 0x06000720 RID: 1824 RVA: 0x000167C6 File Offset: 0x000149C6
			public int EndTime { get; set; }

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x000167CF File Offset: 0x000149CF
			// (set) Token: 0x06000722 RID: 1826 RVA: 0x000167D7 File Offset: 0x000149D7
			public List<string> Lines { get; set; }

			// Token: 0x06000723 RID: 1827 RVA: 0x000167E0 File Offset: 0x000149E0
			public SubtitleItem()
			{
				this.Lines = new List<string>();
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x000167F4 File Offset: 0x000149F4
			public override string ToString()
			{
				TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, this.StartTime);
				TimeSpan timeSpan2 = new TimeSpan(0, 0, 0, 0, this.EndTime);
				return string.Format("{0} --> {1}: {2}", timeSpan.ToString("G"), timeSpan2.ToString("G"), string.Join(Environment.NewLine, this.Lines));
			}
		}
	}
}
