using System;
using System.IO;
using System.Net;
using System.Text;

namespace TaleWorlds.Library
{
	// Token: 0x0200005B RID: 91
	internal class HTMLDebugData
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00007DB0 File Offset: 0x00005FB0
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00007DB8 File Offset: 0x00005FB8
		internal HTMLDebugCategory Info { get; private set; }

		// Token: 0x060002A4 RID: 676 RVA: 0x00007DC4 File Offset: 0x00005FC4
		internal HTMLDebugData(string log, HTMLDebugCategory info)
		{
			this._log = log;
			this.Info = info;
			this._currentTime = DateTime.Now.ToString("yyyy/M/d h:mm:ss.fff");
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007E00 File Offset: 0x00006000
		private string Color
		{
			get
			{
				string result = "000000";
				switch (this.Info)
				{
				case HTMLDebugCategory.General:
					result = "000000";
					break;
				case HTMLDebugCategory.Connection:
					result = "FF00FF";
					break;
				case HTMLDebugCategory.IncomingMessage:
					result = "EE8800";
					break;
				case HTMLDebugCategory.OutgoingMessage:
					result = "AA6600";
					break;
				case HTMLDebugCategory.Database:
					result = "00008B";
					break;
				case HTMLDebugCategory.Warning:
					result = "0000FF";
					break;
				case HTMLDebugCategory.Error:
					result = "FF0000";
					break;
				case HTMLDebugCategory.Other:
					result = "000000";
					break;
				}
				return result;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007E84 File Offset: 0x00006084
		private ConsoleColor ConsoleColor
		{
			get
			{
				ConsoleColor result = ConsoleColor.Green;
				HTMLDebugCategory info = this.Info;
				if (info != HTMLDebugCategory.Warning)
				{
					if (info == HTMLDebugCategory.Error)
					{
						result = ConsoleColor.Red;
					}
				}
				else
				{
					result = ConsoleColor.Yellow;
				}
				return result;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007EAC File Offset: 0x000060AC
		internal void Print(FileStream fileStream, Encoding encoding, bool writeToConsole = true)
		{
			if (writeToConsole)
			{
				Console.ForegroundColor = this.ConsoleColor;
				Console.WriteLine(this._log);
				Console.ForegroundColor = this.ConsoleColor;
			}
			int byteCount = encoding.GetByteCount("</table>");
			string color = this.Color;
			string s = string.Concat(new string[]
			{
				"<tr>",
				this.TableCell(this._log, color).Replace("\n", "<br/>"),
				this.TableCell(this.Info.ToString(), color),
				this.TableCell(this._currentTime, color),
				"</tr></table>"
			});
			byte[] bytes = encoding.GetBytes(s);
			fileStream.Seek((long)(-(long)byteCount), SeekOrigin.End);
			fileStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007F78 File Offset: 0x00006178
		private string TableCell(string innerText, string color)
		{
			return string.Concat(new string[]
			{
				"<td><font color='#",
				color,
				"'>",
				WebUtility.HtmlEncode(innerText),
				"</font></td><td>"
			});
		}

		// Token: 0x040000F3 RID: 243
		private string _log;

		// Token: 0x040000F5 RID: 245
		private string _currentTime;
	}
}
