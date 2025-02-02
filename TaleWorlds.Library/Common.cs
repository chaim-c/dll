using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaleWorlds.Library
{
	// Token: 0x02000022 RID: 34
	public static class Common
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000047E1 File Offset: 0x000029E1
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000047E8 File Offset: 0x000029E8
		public static IPlatformFileHelper PlatformFileHelper
		{
			get
			{
				return Common._fileHelper;
			}
			set
			{
				Common._fileHelper = value;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000047F0 File Offset: 0x000029F0
		public static byte[] CombineBytes(byte[] arr1, byte[] arr2, byte[] arr3 = null, byte[] arr4 = null, byte[] arr5 = null)
		{
			byte[] array = new byte[arr1.Length + arr2.Length + ((arr3 != null) ? arr3.Length : 0) + ((arr4 != null) ? arr4.Length : 0) + ((arr5 != null) ? arr5.Length : 0)];
			int num = 0;
			if (arr1.Length != 0)
			{
				Buffer.BlockCopy(arr1, 0, array, num, arr1.Length);
				num += arr1.Length;
			}
			if (arr2.Length != 0)
			{
				Buffer.BlockCopy(arr2, 0, array, num, arr2.Length);
				num += arr2.Length;
			}
			if (arr3 != null && arr3.Length != 0)
			{
				Buffer.BlockCopy(arr3, 0, array, num, arr3.Length);
				num += arr3.Length;
			}
			if (arr4 != null && arr4.Length != 0)
			{
				Buffer.BlockCopy(arr4, 0, array, num, arr4.Length);
				num += arr4.Length;
			}
			if (arr5 != null && arr5.Length != 0)
			{
				Buffer.BlockCopy(arr5, 0, array, num, arr5.Length);
			}
			return array;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000048A4 File Offset: 0x00002AA4
		public static string CalculateMD5Hash(string input)
		{
			MD5 md = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(input);
			byte[] array = md.ComputeHash(bytes);
			md.Dispose();
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(32, "CalculateMD5Hash");
			for (int i = 0; i < array.Length; i++)
			{
				mbstringBuilder.Append<string>(array[i].ToString("X2"));
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004914 File Offset: 0x00002B14
		public static string ToRoman(int number)
		{
			if (number < 0 || number > 3999)
			{
				Debug.FailedAssert("Requested roman number has to be between 1 and 3999. Fix number!", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Common.cs", "ToRoman", 88);
			}
			if (number < 1)
			{
				return string.Empty;
			}
			if (number >= 1000)
			{
				return "M" + Common.ToRoman(number - 1000);
			}
			if (number >= 900)
			{
				return "CM" + Common.ToRoman(number - 900);
			}
			if (number >= 500)
			{
				return "D" + Common.ToRoman(number - 500);
			}
			if (number >= 400)
			{
				return "CD" + Common.ToRoman(number - 400);
			}
			if (number >= 100)
			{
				return "C" + Common.ToRoman(number - 100);
			}
			if (number >= 90)
			{
				return "XC" + Common.ToRoman(number - 90);
			}
			if (number >= 50)
			{
				return "L" + Common.ToRoman(number - 50);
			}
			if (number >= 40)
			{
				return "XL" + Common.ToRoman(number - 40);
			}
			if (number >= 10)
			{
				return "X" + Common.ToRoman(number - 10);
			}
			if (number >= 9)
			{
				return "IX" + Common.ToRoman(number - 9);
			}
			if (number >= 5)
			{
				return "V" + Common.ToRoman(number - 5);
			}
			if (number >= 4)
			{
				return "IV" + Common.ToRoman(number - 4);
			}
			if (number >= 1)
			{
				return "I" + Common.ToRoman(number - 1);
			}
			Debug.FailedAssert("ToRoman error", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Common.cs", "ToRoman", 104);
			return "";
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public static int GetDJB2(string str)
		{
			int num = 5381;
			for (int i = 0; i < str.Length; i++)
			{
				num = (num << 5) + num + (int)str[i];
			}
			return num;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public static byte[] SerializeObjectAsJson(object o)
		{
			string s = JsonConvert.SerializeObject(o, Formatting.Indented);
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004B14 File Offset: 0x00002D14
		public static string SerializeObjectAsJsonString(object o)
		{
			return JsonConvert.SerializeObject(o, Formatting.Indented);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004B1D File Offset: 0x00002D1D
		public static T DeserializeObjectFromJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004B28 File Offset: 0x00002D28
		public static byte[] SerializeObject(object sObject)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				binaryFormatter.Serialize(memoryStream, sObject);
			}
			catch (Exception ex)
			{
				Debug.Print(ex.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004B7C File Offset: 0x00002D7C
		public static object DeserializeObject(byte[] serializeData)
		{
			return Common.DeserializeObject(serializeData, 0, serializeData.Length);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004B88 File Offset: 0x00002D88
		public static object DeserializeObject(byte[] serializeData, int index, int length)
		{
			MemoryStream serializationStream = new MemoryStream(serializeData, index, length, false);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			object result;
			try
			{
				result = binaryFormatter.Deserialize(serializationStream);
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public static byte[] FromUrlSafeBase64(string base64)
		{
			string text = base64.Replace('_', '/').Replace('-', '+');
			int num = base64.Length % 4;
			if (num != 2)
			{
				if (num == 3)
				{
					text += "=";
				}
			}
			else
			{
				text += "==";
			}
			return Convert.FromBase64String(text);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004C18 File Offset: 0x00002E18
		public static string ConfigName
		{
			get
			{
				return new DirectoryInfo(Directory.GetCurrentDirectory()).Name;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004C2C File Offset: 0x00002E2C
		public static Type FindType(string typeName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				Type type = assemblies[i].GetType(typeName);
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004C6A File Offset: 0x00002E6A
		public static void MemoryCleanupGC(bool forceTimer = false)
		{
			GC.Collect();
			Common.lastGCTime = DateTime.Now;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004C7C File Offset: 0x00002E7C
		public static object DynamicInvokeWithLog(this Delegate method, params object[] args)
		{
			object result = null;
			try
			{
				result = method.DynamicInvoke(args);
			}
			catch (Exception e)
			{
				Common.PrintDynamicInvokeDebugInfo(e, method.Method, method.Target, args);
				result = null;
				throw;
			}
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004CBC File Offset: 0x00002EBC
		public static object InvokeWithLog(this MethodInfo methodInfo, object obj, params object[] args)
		{
			object result = null;
			try
			{
				result = methodInfo.Invoke(obj, args);
			}
			catch (Exception e)
			{
				Common.PrintDynamicInvokeDebugInfo(e, methodInfo, obj, args);
				result = null;
				throw;
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public static object InvokeWithLog(this ConstructorInfo constructorInfo, params object[] args)
		{
			object result = null;
			try
			{
				result = constructorInfo.Invoke(args);
			}
			catch (Exception e)
			{
				MethodInfo methodInfo = Common.GetMethodInfo<object[]>((object[] a) => constructorInfo.Invoke(a));
				Common.PrintDynamicInvokeDebugInfo(e, methodInfo, null, args);
				result = null;
				throw;
			}
			return result;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004DA8 File Offset: 0x00002FA8
		private static string GetStackTraceRaw(Exception e, int skipCount = 0)
		{
			StackTrace stackTrace = new StackTrace(e, 0, false);
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "GetStackTraceRaw");
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				if (i >= skipCount)
				{
					string text = "unknown_module.dll";
					try
					{
						StackFrame frame = stackTrace.GetFrame(i);
						MethodBase method = frame.GetMethod();
						text = method.Module.Assembly.Location;
						int iloffset = frame.GetILOffset();
						int metadataToken = method.MetadataToken;
						mbstringBuilder.AppendLine<string>(string.Concat(new object[]
						{
							text,
							"@",
							metadataToken,
							"@",
							iloffset
						}));
					}
					catch
					{
						mbstringBuilder.AppendLine<string>(text + "@-1@-1");
					}
				}
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E94 File Offset: 0x00003094
		private static void WalkInnerExceptionRecursive(Exception InnerException, ref string StackStr)
		{
			if (InnerException == null)
			{
				return;
			}
			Common.WalkInnerExceptionRecursive(InnerException.InnerException, ref StackStr);
			string stackTraceRaw = Common.GetStackTraceRaw(InnerException, 0);
			StackStr += stackTraceRaw;
			StackStr += "---End of stack trace from previous location where exception was thrown ---";
			StackStr += Environment.NewLine;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004EE0 File Offset: 0x000030E0
		private static void PrintDynamicInvokeDebugInfo(Exception e, MethodInfo methodInfo, object obj, params object[] args)
		{
			string text = "Exception occurred inside invoke: " + methodInfo.Name;
			if (obj != null)
			{
				text = text + "\nTarget type: " + obj.GetType().FullName;
			}
			if (args != null)
			{
				text = text + "\nArgument count: " + args.Length;
				foreach (object obj2 in args)
				{
					if (obj2 == null)
					{
						text += "\nArgument is null";
					}
					else
					{
						text = text + "\nArgument type is " + obj2.GetType().FullName;
					}
				}
			}
			string crashReportCustomStack = "";
			if (e.InnerException != null)
			{
				Common.WalkInnerExceptionRecursive(e, ref crashReportCustomStack);
			}
			Exception ex = e;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			text = text + "\nInner message: " + ex.Message;
			Debug.SetCrashReportCustomString(text);
			Debug.SetCrashReportCustomStack(crashReportCustomStack);
			Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004FCC File Offset: 0x000031CC
		public static bool TextContainsSpecialCharacters(string text)
		{
			return text.Any((char x) => !char.IsWhiteSpace(x) && !char.IsLetterOrDigit(x) && !char.IsPunctuation(x));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004FF4 File Offset: 0x000031F4
		public static uint ParseIpAddress(string address)
		{
			byte[] addressBytes = IPAddress.Parse(address).GetAddressBytes();
			return (uint)(((int)addressBytes[0] << 24) + ((int)addressBytes[1] << 16) + ((int)addressBytes[2] << 8) + (int)addressBytes[3]);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005024 File Offset: 0x00003224
		public static bool IsAllLetters(string text)
		{
			if (text == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsLetter(text[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000505C File Offset: 0x0000325C
		public static bool IsAllLettersOrWhiteSpaces(string text)
		{
			if (text == null)
			{
				return false;
			}
			foreach (char c in text)
			{
				if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000509C File Offset: 0x0000329C
		public static bool IsCharAsian(char character)
		{
			return (character >= '一' && character <= '鿿') || (character >= '㐀' && character <= '䶿') || (character >= '㐀' && character <= '䶿') || ((int)character >= 131072 && (int)character <= 183983) || (character >= '⺀' && character <= '㇯') || (character >= '豈' && character <= '﫿') || (character >= '︰' && character <= '﹏') || ((int)character >= 993280 && (int)character <= 195103);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005130 File Offset: 0x00003330
		public static void SetInvariantCulture()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005164 File Offset: 0x00003364
		public static MethodInfo GetMethodInfo(Expression<Action> expression)
		{
			return Common.GetMethodInfo(expression);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000516C File Offset: 0x0000336C
		public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
		{
			return Common.GetMethodInfo(expression);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005174 File Offset: 0x00003374
		public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			return Common.GetMethodInfo(expression);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000517C File Offset: 0x0000337C
		public static MethodInfo GetMethodInfo(LambdaExpression expression)
		{
			MethodCallExpression methodCallExpression = expression.Body as MethodCallExpression;
			if (methodCallExpression == null)
			{
				throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
			}
			return methodCallExpression.Method;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000519C File Offset: 0x0000339C
		public static ParallelOptions ParallelOptions
		{
			get
			{
				if (Common._parallelOptions == null)
				{
					Common._parallelOptions = new ParallelOptions();
					Common._parallelOptions.MaxDegreeOfParallelism = MathF.Max(Environment.ProcessorCount - 2, 1);
					Debug.Print(string.Format("Max Dexree of Parallelism is set to: {0}", Common._parallelOptions.MaxDegreeOfParallelism), 0, Debug.DebugColor.White, 17592186044416UL);
				}
				return Common._parallelOptions;
			}
		}

		// Token: 0x04000067 RID: 103
		private static IPlatformFileHelper _fileHelper = null;

		// Token: 0x04000068 RID: 104
		private static DateTime lastGCTime = DateTime.Now;

		// Token: 0x04000069 RID: 105
		private static ParallelOptions _parallelOptions = null;
	}
}
