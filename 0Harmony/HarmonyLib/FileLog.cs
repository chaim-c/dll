using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace HarmonyLib
{
	// Token: 0x02000050 RID: 80
	public static class FileLog
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000130D0 File Offset: 0x000112D0
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x000130D7 File Offset: 0x000112D7
		public static StreamWriter LogWriter { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000130E0 File Offset: 0x000112E0
		public static string LogPath
		{
			get
			{
				object obj = FileLog.fileLock;
				string logPath;
				lock (obj)
				{
					if (!FileLog._logPathInited)
					{
						FileLog._logPathInited = true;
						string environmentVariable = Environment.GetEnvironmentVariable("HARMONY_NO_LOG");
						if (!string.IsNullOrEmpty(environmentVariable))
						{
							return null;
						}
						FileLog._logPath = Environment.GetEnvironmentVariable("HARMONY_LOG_FILE");
						if (string.IsNullOrEmpty(FileLog._logPath))
						{
							string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
							Directory.CreateDirectory(folderPath);
							FileLog._logPath = Path.Combine(folderPath, "harmony.log.txt");
						}
					}
					logPath = FileLog._logPath;
				}
				return logPath;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013180 File Offset: 0x00011380
		private static string IndentString()
		{
			return new string(FileLog.indentChar, FileLog.indentLevel);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013194 File Offset: 0x00011394
		public static void ChangeIndent(int delta)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.indentLevel = Math.Max(0, FileLog.indentLevel + delta);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000131E0 File Offset: 0x000113E0
		public static void LogBuffered(string str)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.buffer.Add(FileLog.IndentString() + str);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013230 File Offset: 0x00011430
		public static void LogBuffered(List<string> strings)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.buffer.AddRange(strings);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013274 File Offset: 0x00011474
		public static List<string> GetBuffer(bool clear)
		{
			object obj = FileLog.fileLock;
			List<string> result;
			lock (obj)
			{
				List<string> list = FileLog.buffer;
				if (clear)
				{
					FileLog.buffer = new List<string>();
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000132C4 File Offset: 0x000114C4
		public static void SetBuffer(List<string> buffer)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.buffer = buffer;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013304 File Offset: 0x00011504
		public static void FlushBuffer()
		{
			if (FileLog.LogWriter != null)
			{
				foreach (string value in FileLog.buffer)
				{
					FileLog.LogWriter.WriteLine(value);
				}
				FileLog.buffer.Clear();
				return;
			}
			if (FileLog.LogPath == null)
			{
				return;
			}
			object obj = FileLog.fileLock;
			lock (obj)
			{
				if (FileLog.buffer.Count > 0)
				{
					using (StreamWriter streamWriter = File.AppendText(FileLog.LogPath))
					{
						foreach (string value2 in FileLog.buffer)
						{
							streamWriter.WriteLine(value2);
						}
						FileLog.buffer.Clear();
					}
				}
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00013420 File Offset: 0x00011620
		public static void Log(string str)
		{
			if (FileLog.LogWriter != null)
			{
				FileLog.LogWriter.WriteLine(FileLog.IndentString() + str);
				return;
			}
			if (FileLog.LogPath == null)
			{
				return;
			}
			object obj = FileLog.fileLock;
			lock (obj)
			{
				using (StreamWriter streamWriter = File.AppendText(FileLog.LogPath))
				{
					streamWriter.WriteLine(FileLog.IndentString() + str);
				}
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000134B0 File Offset: 0x000116B0
		public static void Debug(string str)
		{
			if (Harmony.DEBUG)
			{
				FileLog.Log(str);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000134C0 File Offset: 0x000116C0
		public static void Reset()
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				defaultInterpolatedStringHandler.AppendFormatted<char>(Path.DirectorySeparatorChar);
				defaultInterpolatedStringHandler.AppendLiteral("harmony.log.txt");
				string path = defaultInterpolatedStringHandler.ToStringAndClear();
				File.Delete(path);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00013538 File Offset: 0x00011738
		public unsafe static void LogBytes(long ptr, int len)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				byte* ptr2 = ptr;
				string text = "";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				for (int i = 1; i <= len; i++)
				{
					if (text.Length == 0)
					{
						text = "#  ";
					}
					string str = text;
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
					defaultInterpolatedStringHandler.AppendFormatted<byte>(*ptr2, "X2");
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					text = str + defaultInterpolatedStringHandler.ToStringAndClear();
					if (i > 1 || len == 1)
					{
						if (i % 8 == 0 || i == len)
						{
							FileLog.Log(text);
							text = "";
						}
						else if (i % 4 == 0)
						{
							text += " ";
						}
					}
					ptr2++;
				}
				byte[] destination = new byte[len];
				Marshal.Copy((IntPtr)ptr, destination, 0, len);
				MD5 md = MD5.Create();
				byte[] array = md.ComputeHash(destination);
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < array.Length; j++)
				{
					stringBuilder.Append(array[j].ToString("X2"));
				}
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
				defaultInterpolatedStringHandler.AppendLiteral("HASH: ");
				defaultInterpolatedStringHandler.AppendFormatted<StringBuilder>(stringBuilder);
				FileLog.Log(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x040000E4 RID: 228
		private static readonly object fileLock = new object();

		// Token: 0x040000E5 RID: 229
		private static bool _logPathInited;

		// Token: 0x040000E6 RID: 230
		private static string _logPath;

		// Token: 0x040000E8 RID: 232
		public static char indentChar = '\t';

		// Token: 0x040000E9 RID: 233
		public static int indentLevel = 0;

		// Token: 0x040000EA RID: 234
		private static List<string> buffer = new List<string>();
	}
}
