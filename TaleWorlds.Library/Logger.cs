using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace TaleWorlds.Library
{
	// Token: 0x02000059 RID: 89
	public class Logger
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000794C File Offset: 0x00005B4C
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00007954 File Offset: 0x00005B54
		public bool LogOnlyErrors { get; set; }

		// Token: 0x06000299 RID: 665 RVA: 0x00007960 File Offset: 0x00005B60
		static Logger()
		{
			Logger._logFileEncoding = Encoding.UTF8;
			Logger.LogsFolder = Environment.CurrentDirectory + "\\logs";
			Logger._loggers = new List<Logger>();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000079B1 File Offset: 0x00005BB1
		public Logger(string name) : this(name, false, false, false, 1, -1, false)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000079C0 File Offset: 0x00005BC0
		public Logger(string name, bool writeErrorsToDifferentFile, bool logOnlyErrors, bool doNotUseProcessId, int numFiles = 1, int totalFileSize = -1, bool overwrite = false)
		{
			string text = AppDomain.CurrentDomain.FriendlyName;
			text = Path.GetFileNameWithoutExtension(text);
			this._name = name;
			this._writeErrorsToDifferentFile = writeErrorsToDifferentFile;
			this.LogOnlyErrors = logOnlyErrors;
			this._logQueue = new Queue<HTMLDebugData>();
			int id = Process.GetCurrentProcess().Id;
			DateTime now = DateTime.Now;
			string text2 = Logger.LogsFolder;
			if (!doNotUseProcessId)
			{
				string str = string.Concat(new object[]
				{
					text,
					"_",
					now.ToString("yyyyMMdd"),
					"_",
					now.ToString("hhmmss"),
					"_",
					id
				});
				text2 = text2 + "/" + str;
			}
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			this._fileManager = new Logger.FileManager(text2, this._name, numFiles, totalFileSize, overwrite, writeErrorsToDifferentFile);
			List<Logger> loggers = Logger._loggers;
			lock (loggers)
			{
				if (Logger._thread == null)
				{
					Logger._thread = new Thread(new ThreadStart(Logger.ThreadMain));
					Logger._thread.IsBackground = true;
					Logger._thread.Priority = ThreadPriority.BelowNormal;
					Logger._thread.Start();
				}
				Logger._loggers.Add(this);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007B20 File Offset: 0x00005D20
		private static void ThreadMain()
		{
			while (Logger._running)
			{
				try
				{
					Logger.Printer();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception on network debug thread: " + ex.Message);
				}
			}
			Logger._isOver = true;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007B6C File Offset: 0x00005D6C
		private static void Printer()
		{
			while ((Logger._running || Logger._printedOnThisCycle) && Logger._loggers.Count > 0)
			{
				Logger._printedOnThisCycle = false;
				List<Logger> loggers = Logger._loggers;
				lock (loggers)
				{
					using (List<Logger>.Enumerator enumerator = Logger._loggers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.DoLoggingJob())
							{
								Logger._printedOnThisCycle = true;
							}
						}
					}
				}
				if (!Logger._printedOnThisCycle)
				{
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007C1C File Offset: 0x00005E1C
		private bool DoLoggingJob()
		{
			bool result = false;
			HTMLDebugData htmldebugData = null;
			Queue<HTMLDebugData> logQueue = this._logQueue;
			lock (logQueue)
			{
				if (this._logQueue.Count > 0)
				{
					htmldebugData = this._logQueue.Dequeue();
				}
			}
			if (htmldebugData != null)
			{
				FileStream fileStream = this._fileManager.GetFileStream();
				result = true;
				htmldebugData.Print(fileStream, Logger._logFileEncoding, true);
				if ((htmldebugData.Info == HTMLDebugCategory.Error || htmldebugData.Info == HTMLDebugCategory.Warning) && this._writeErrorsToDifferentFile)
				{
					htmldebugData.Print(this._fileManager.GetErrorFileStream(), Logger._logFileEncoding, false);
				}
				this._fileManager.CheckForFileSize();
			}
			return result;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public void Print(string log, HTMLDebugCategory debugInfo = HTMLDebugCategory.General)
		{
			this.Print(log, debugInfo, true);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public void Print(string log, HTMLDebugCategory debugInfo, bool printOnGlobal)
		{
			if (!this.LogOnlyErrors || (this.LogOnlyErrors && debugInfo == HTMLDebugCategory.Error) || (this.LogOnlyErrors && debugInfo == HTMLDebugCategory.Warning))
			{
				HTMLDebugData item = new HTMLDebugData(log, debugInfo);
				Queue<HTMLDebugData> logQueue = this._logQueue;
				lock (logQueue)
				{
					this._logQueue.Enqueue(item);
				}
				if (printOnGlobal)
				{
					Debug.Print(log, 0, Debug.DebugColor.White, 17592186044416UL);
				}
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007D64 File Offset: 0x00005F64
		public static void FinishAndCloseAll()
		{
			List<Logger> loggers = Logger._loggers;
			lock (loggers)
			{
				Logger._running = false;
				Logger._printedOnThisCycle = true;
			}
			while (!Logger._isOver)
			{
			}
		}

		// Token: 0x040000DE RID: 222
		private Queue<HTMLDebugData> _logQueue;

		// Token: 0x040000DF RID: 223
		private static Encoding _logFileEncoding;

		// Token: 0x040000E0 RID: 224
		private string _name;

		// Token: 0x040000E1 RID: 225
		private bool _writeErrorsToDifferentFile;

		// Token: 0x040000E3 RID: 227
		private static List<Logger> _loggers;

		// Token: 0x040000E4 RID: 228
		private Logger.FileManager _fileManager;

		// Token: 0x040000E5 RID: 229
		private static Thread _thread;

		// Token: 0x040000E6 RID: 230
		private static bool _running = true;

		// Token: 0x040000E7 RID: 231
		private static bool _printedOnThisCycle = false;

		// Token: 0x040000E8 RID: 232
		private static bool _isOver = false;

		// Token: 0x040000E9 RID: 233
		public static string LogsFolder = "";

		// Token: 0x020000CB RID: 203
		private class FileManager
		{
			// Token: 0x060006E9 RID: 1769 RVA: 0x00015A48 File Offset: 0x00013C48
			public FileManager(string path, string name, int numFiles, int maxTotalSize, bool overwrite, bool logErrorsToDifferentFile)
			{
				if (maxTotalSize < numFiles * 64 * 1024)
				{
					this._numFiles = 1;
					this._isCheckingFileSize = false;
				}
				else
				{
					this._numFiles = numFiles;
					if (numFiles <= 0)
					{
						this._numFiles = 1;
						this._isCheckingFileSize = false;
					}
					this._maxFileSize = maxTotalSize / this._numFiles;
					this._isCheckingFileSize = true;
				}
				this._streams = new FileStream[this._numFiles];
				this._currentStreamIndex = 0;
				try
				{
					for (int i = 0; i < this._numFiles; i++)
					{
						string str = name + "_" + i;
						string path2 = path + "/" + str + ".html";
						this._streams[i] = (overwrite ? new FileStream(path2, FileMode.Create) : new FileStream(path2, FileMode.OpenOrCreate));
						this.FillEmptyStream(this._streams[i]);
					}
					if (logErrorsToDifferentFile)
					{
						string path3 = path + "/" + name + "_errors.html";
						this._errorStream = (overwrite ? new FileStream(path3, FileMode.Create) : new FileStream(path3, FileMode.OpenOrCreate));
						this.FillEmptyStream(this._errorStream);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error when creating log file(s): " + ex.GetBaseException().Message);
					for (int j = 0; j < this._numFiles; j++)
					{
						string str2 = name + "__" + j;
						string path4 = path + "/" + str2 + ".html";
						this._streams[j] = (overwrite ? new FileStream(path4, FileMode.Create) : new FileStream(path4, FileMode.OpenOrCreate));
						this.FillEmptyStream(this._streams[j]);
					}
					if (logErrorsToDifferentFile)
					{
						string path5 = path + "/" + name + "_errors.html";
						this._errorStream = (overwrite ? new FileStream(path5, FileMode.Create) : new FileStream(path5, FileMode.OpenOrCreate));
						this.FillEmptyStream(this._errorStream);
					}
				}
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x00015C3C File Offset: 0x00013E3C
			public FileStream GetFileStream()
			{
				return this._streams[this._currentStreamIndex];
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x00015C4B File Offset: 0x00013E4B
			public FileStream GetErrorFileStream()
			{
				return this._errorStream;
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x00015C54 File Offset: 0x00013E54
			public void CheckForFileSize()
			{
				if (this._isCheckingFileSize && this._streams[this._currentStreamIndex].Length > (long)this._maxFileSize)
				{
					this._currentStreamIndex = (this._currentStreamIndex + 1) % this._numFiles;
					this.ResetFileStream(this._streams[this._currentStreamIndex]);
				}
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x00015CAC File Offset: 0x00013EAC
			public void ShutDown()
			{
				for (int i = 0; i < this._numFiles; i++)
				{
					this._streams[i].Close();
					this._streams[i] = null;
				}
				if (this._errorStream != null)
				{
					this._errorStream.Close();
					this._errorStream = null;
				}
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x00015CFC File Offset: 0x00013EFC
			private void FillEmptyStream(FileStream stream)
			{
				if (stream.Length == 0L)
				{
					string s = "<table></table>";
					byte[] bytes = Logger._logFileEncoding.GetBytes(s);
					stream.Write(bytes, 0, bytes.Length);
				}
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x00015D2E File Offset: 0x00013F2E
			private void ResetFileStream(FileStream stream)
			{
				stream.SetLength(0L);
				this.FillEmptyStream(stream);
			}

			// Token: 0x04000271 RID: 625
			private bool _isCheckingFileSize;

			// Token: 0x04000272 RID: 626
			private int _maxFileSize;

			// Token: 0x04000273 RID: 627
			private int _numFiles;

			// Token: 0x04000274 RID: 628
			private FileStream[] _streams;

			// Token: 0x04000275 RID: 629
			private int _currentStreamIndex;

			// Token: 0x04000276 RID: 630
			private FileStream _errorStream;
		}
	}
}
