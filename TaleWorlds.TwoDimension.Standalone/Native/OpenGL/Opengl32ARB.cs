﻿using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace TaleWorlds.TwoDimension.Standalone.Native.OpenGL
{
	// Token: 0x02000037 RID: 55
	internal static class Opengl32ARB
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00004EB4 File Offset: 0x000030B4
		public static void LoadExtensions()
		{
			if (!Opengl32ARB._extensionsLoaded)
			{
				Opengl32ARB._extensionsLoaded = true;
				Opengl32ARB.ActiveTexture = Opengl32ARB.LoadFunction<Opengl32ARB.ActiveTextureDelegate>("glActiveTexture");
				Opengl32ARB.wglCreateContextAttribs = Opengl32ARB.LoadFunction<Opengl32ARB.wglCreateContextAttribsDelegate>("wglCreateContextAttribsARB");
				Opengl32ARB.CreateProgramObject = Opengl32ARB.LoadFunction<Opengl32ARB.CreateProgramObjectDelegate>("glCreateProgramObjectARB");
				Opengl32ARB.CreateShaderObject = Opengl32ARB.LoadFunction<Opengl32ARB.CreateShaderObjectDelegate>("glCreateShaderObjectARB");
				Opengl32ARB.CompileShader = Opengl32ARB.LoadFunction<Opengl32ARB.CompileShaderDelegate>("glCompileShaderARB");
				Opengl32ARB.ShaderSourceInternal = Opengl32ARB.LoadFunction<Opengl32ARB.ShaderSourceDelegate>("glShaderSourceARB");
				Opengl32ARB.AttachShader = Opengl32ARB.LoadFunction<Opengl32ARB.AttachShaderDelegate>("glAttachShader");
				Opengl32ARB.LinkProgram = Opengl32ARB.LoadFunction<Opengl32ARB.LinkProgramDelegate>("glLinkProgram");
				Opengl32ARB.DeleteProgram = Opengl32ARB.LoadFunction<Opengl32ARB.DeleteProgramDelegate>("glDeleteProgram");
				Opengl32ARB.UseProgram = Opengl32ARB.LoadFunction<Opengl32ARB.UseProgramDelegate>("glUseProgram");
				Opengl32ARB.UniformMatrix4fvInternal = Opengl32ARB.LoadFunction<Opengl32ARB.UniformMatrix4fvDelegate>("glUniformMatrix4fv");
				Opengl32ARB.Uniform4f = Opengl32ARB.LoadFunction<Opengl32ARB.Uniform4fDelegate>("glUniform4f");
				Opengl32ARB.Uniform1i = Opengl32ARB.LoadFunction<Opengl32ARB.Uniform1iDelegate>("glUniform1i");
				Opengl32ARB.Uniform1f = Opengl32ARB.LoadFunction<Opengl32ARB.Uniform1fDelegate>("glUniform1f");
				Opengl32ARB.Uniform2f = Opengl32ARB.LoadFunction<Opengl32ARB.Uniform2fDelegate>("glUniform2f");
				Opengl32ARB.GetShaderiv = Opengl32ARB.LoadFunction<Opengl32ARB.GetShaderivDelegate>("glGetShaderiv");
				Opengl32ARB.GetShaderInfoLog = Opengl32ARB.LoadFunction<Opengl32ARB.GetShaderInfoLogDelegate>("glGetShaderInfoLog");
				Opengl32ARB.GetProgramInfoLog = Opengl32ARB.LoadFunction<Opengl32ARB.GetProgramInfoLogDelegate>("glGetProgramInfoLog");
				Opengl32ARB.GetProgramiv = Opengl32ARB.LoadFunction<Opengl32ARB.GetProgramivDelegate>("glGetProgramiv");
				Opengl32ARB.GetUniformLocationInternal = Opengl32ARB.LoadFunction<Opengl32ARB.GetUniformLocationDelegate>("glGetUniformLocation");
				Opengl32ARB.DetachShader = Opengl32ARB.LoadFunction<Opengl32ARB.DetachShaderDelegate>("glDetachShader");
				Opengl32ARB.DeleteShader = Opengl32ARB.LoadFunction<Opengl32ARB.DeleteShaderDelegate>("glDeleteShader");
				Opengl32ARB.GenBuffers = Opengl32ARB.LoadFunction<Opengl32ARB.GenBuffersDelegate>("glGenBuffers");
				Opengl32ARB.BindBuffer = Opengl32ARB.LoadFunction<Opengl32ARB.BindBufferDelegate>("glBindBuffer");
				Opengl32ARB.BufferData = Opengl32ARB.LoadFunction<Opengl32ARB.BufferDataDelegate>("glBufferData");
				Opengl32ARB.BufferSubData = Opengl32ARB.LoadFunction<Opengl32ARB.BufferSubDataDelegate>("glBufferSubData");
				Opengl32ARB.EnableVertexAttribArray = Opengl32ARB.LoadFunction<Opengl32ARB.EnableVertexAttribArrayDelegate>("glEnableVertexAttribArray");
				Opengl32ARB.DisableVertexAttribArray = Opengl32ARB.LoadFunction<Opengl32ARB.DisableVertexAttribArrayDelegate>("glDisableVertexAttribArray");
				Opengl32ARB.VertexAttribPointer = Opengl32ARB.LoadFunction<Opengl32ARB.VertexAttribPointerDelegate>("glVertexAttribPointer");
				Opengl32ARB.GenVertexArrays = Opengl32ARB.LoadFunction<Opengl32ARB.GenVertexArraysDelegate>("glGenVertexArrays");
				Opengl32ARB.BindVertexArray = Opengl32ARB.LoadFunction<Opengl32ARB.BindVertexArrayDelegate>("glBindVertexArray");
				Opengl32ARB.BlendFuncSeparate = Opengl32ARB.LoadFunction<Opengl32ARB.BlendFuncSeparateDelegate>("glBlendFuncSeparate");
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000050B1 File Offset: 0x000032B1
		private static T LoadFunction<T>(string name) where T : class
		{
			return Marshal.GetDelegateForFunctionPointer(Opengl32.wglGetProcAddress(name), typeof(T)) as T;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000050D4 File Offset: 0x000032D4
		public static void ShaderSource(int shader, string shaderSource)
		{
			string[] array = shaderSource.Split(Environment.NewLine.ToCharArray());
			byte[][] array2 = new byte[array.Length][];
			int[] array3 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				byte[] array4 = new byte[Encoding.UTF8.GetByteCount(text) + 2];
				Encoding.UTF8.GetBytes(text, 0, text.Length, array4, 0);
				array4[array4.Length - 2] = 10;
				array4[array4.Length - 1] = 0;
				array2[i] = array4;
				array3[i] = array4.Length - 1;
			}
			AutoPinner[] array5 = new AutoPinner[array.Length];
			IntPtr[] array6 = new IntPtr[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				AutoPinner autoPinner = new AutoPinner(array2[j]);
				IntPtr intPtr = autoPinner;
				array6[j] = intPtr;
				array5[j] = autoPinner;
			}
			Opengl32ARB.ShaderSourceInternal(shader, array.Length, array6, array3);
			AutoPinner[] array7 = array5;
			for (int k = 0; k < array7.Length; k++)
			{
				array7[k].Dispose();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000051E8 File Offset: 0x000033E8
		public static int GetUniformLocation(int program, string parameter)
		{
			byte[] array = new byte[Encoding.ASCII.GetByteCount(parameter) + 1];
			Encoding.UTF8.GetBytes(parameter, 0, parameter.Length, array, 0);
			return Opengl32ARB.GetUniformLocationInternal(program, array);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000522C File Offset: 0x0000342C
		public static void UniformMatrix4fv(int location, int count, bool isTranspose, Matrix4x4 matrix)
		{
			float[] matrix2 = new float[]
			{
				matrix.M11,
				matrix.M12,
				matrix.M13,
				matrix.M14,
				matrix.M21,
				matrix.M22,
				matrix.M23,
				matrix.M24,
				matrix.M31,
				matrix.M32,
				matrix.M33,
				matrix.M34,
				matrix.M41,
				matrix.M42,
				matrix.M43,
				matrix.M44
			};
			Opengl32ARB.UniformMatrix4fvInternal(location, count, isTranspose ? 1 : 0, matrix2);
		}

		// Token: 0x0400025A RID: 602
		private static bool _extensionsLoaded;

		// Token: 0x0400025B RID: 603
		public static Opengl32ARB.BlendFuncSeparateDelegate BlendFuncSeparate;

		// Token: 0x0400025C RID: 604
		public static Opengl32ARB.ActiveTextureDelegate ActiveTexture;

		// Token: 0x0400025D RID: 605
		public static Opengl32ARB.BindVertexArrayDelegate BindVertexArray;

		// Token: 0x0400025E RID: 606
		public static Opengl32ARB.GenVertexArraysDelegate GenVertexArrays;

		// Token: 0x0400025F RID: 607
		public static Opengl32ARB.VertexAttribPointerDelegate VertexAttribPointer;

		// Token: 0x04000260 RID: 608
		public static Opengl32ARB.EnableVertexAttribArrayDelegate EnableVertexAttribArray;

		// Token: 0x04000261 RID: 609
		public static Opengl32ARB.DisableVertexAttribArrayDelegate DisableVertexAttribArray;

		// Token: 0x04000262 RID: 610
		public static Opengl32ARB.GenBuffersDelegate GenBuffers;

		// Token: 0x04000263 RID: 611
		public static Opengl32ARB.BindBufferDelegate BindBuffer;

		// Token: 0x04000264 RID: 612
		public static Opengl32ARB.BufferDataDelegate BufferData;

		// Token: 0x04000265 RID: 613
		public static Opengl32ARB.BufferSubDataDelegate BufferSubData;

		// Token: 0x04000266 RID: 614
		public static Opengl32ARB.DetachShaderDelegate DetachShader;

		// Token: 0x04000267 RID: 615
		public static Opengl32ARB.DeleteShaderDelegate DeleteShader;

		// Token: 0x04000268 RID: 616
		private static Opengl32ARB.GetUniformLocationDelegate GetUniformLocationInternal;

		// Token: 0x04000269 RID: 617
		public static Opengl32ARB.GetProgramInfoLogDelegate GetProgramInfoLog;

		// Token: 0x0400026A RID: 618
		public static Opengl32ARB.GetShaderInfoLogDelegate GetShaderInfoLog;

		// Token: 0x0400026B RID: 619
		public static Opengl32ARB.GetProgramivDelegate GetProgramiv;

		// Token: 0x0400026C RID: 620
		public static Opengl32ARB.GetShaderivDelegate GetShaderiv;

		// Token: 0x0400026D RID: 621
		private static Opengl32ARB.UniformMatrix4fvDelegate UniformMatrix4fvInternal;

		// Token: 0x0400026E RID: 622
		public static Opengl32ARB.Uniform4fDelegate Uniform4f;

		// Token: 0x0400026F RID: 623
		public static Opengl32ARB.Uniform1iDelegate Uniform1i;

		// Token: 0x04000270 RID: 624
		public static Opengl32ARB.Uniform1fDelegate Uniform1f;

		// Token: 0x04000271 RID: 625
		public static Opengl32ARB.Uniform2fDelegate Uniform2f;

		// Token: 0x04000272 RID: 626
		public static Opengl32ARB.UseProgramDelegate UseProgram;

		// Token: 0x04000273 RID: 627
		public static Opengl32ARB.DeleteProgramDelegate DeleteProgram;

		// Token: 0x04000274 RID: 628
		public static Opengl32ARB.LinkProgramDelegate LinkProgram;

		// Token: 0x04000275 RID: 629
		public static Opengl32ARB.AttachShaderDelegate AttachShader;

		// Token: 0x04000276 RID: 630
		private static Opengl32ARB.ShaderSourceDelegate ShaderSourceInternal;

		// Token: 0x04000277 RID: 631
		public static Opengl32ARB.CompileShaderDelegate CompileShader;

		// Token: 0x04000278 RID: 632
		public static Opengl32ARB.CreateProgramObjectDelegate CreateProgramObject;

		// Token: 0x04000279 RID: 633
		public static Opengl32ARB.CreateShaderObjectDelegate CreateShaderObject;

		// Token: 0x0400027A RID: 634
		public static Opengl32ARB.wglCreateContextAttribsDelegate wglCreateContextAttribs;

		// Token: 0x0400027B RID: 635
		public const int GL_COMPILE_STATUS = 35713;

		// Token: 0x0400027C RID: 636
		public const int GL_LINK_STATUS = 35714;

		// Token: 0x0400027D RID: 637
		public const int GL_INFO_LOG_LENGTH = 35716;

		// Token: 0x0400027E RID: 638
		public const int StaticDraw = 35044;

		// Token: 0x0400027F RID: 639
		public const int DynamicDraw = 35048;

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x06000164 RID: 356
		public delegate void BlendFuncSeparateDelegate(BlendingSourceFactor srcRGB, BlendingDestinationFactor dstRGB, BlendingSourceFactor srcAlpha, BlendingDestinationFactor dstAlpha);

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x06000168 RID: 360
		public delegate void ActiveTextureDelegate(TextureUnit textureUnit);

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x0600016C RID: 364
		public delegate void BindVertexArrayDelegate(uint buffer);

		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x06000170 RID: 368
		public delegate void GenVertexArraysDelegate(int size, uint[] buffers);

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x06000174 RID: 372
		public delegate void VertexAttribPointerDelegate(uint index, int size, DataType type, byte normalized, int stride, IntPtr pointer);

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x06000178 RID: 376
		public delegate void EnableVertexAttribArrayDelegate(uint index);

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x0600017C RID: 380
		public delegate void DisableVertexAttribArrayDelegate(int index);

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x06000180 RID: 384
		public delegate void GenBuffersDelegate(int size, uint[] buffers);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000184 RID: 388
		public delegate void BindBufferDelegate(BufferBindingTarget target, uint buffer);

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x06000188 RID: 392
		public delegate void BufferDataDelegate(BufferBindingTarget target, int size, IntPtr data, int usage);

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x0600018C RID: 396
		public delegate void BufferSubDataDelegate(BufferBindingTarget target, int offset, int size, IntPtr data);

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x06000190 RID: 400
		public delegate void DetachShaderDelegate(int program, int shader);

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x06000194 RID: 404
		public delegate int DeleteShaderDelegate(int shader);

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x06000198 RID: 408
		private delegate int GetUniformLocationDelegate(int program, byte[] parameter);

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x0600019C RID: 412
		public delegate void GetProgramInfoLogDelegate(int shader, int maxLength, out int length, byte[] output);

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x060001A0 RID: 416
		public delegate void GetShaderInfoLogDelegate(int shader, int maxLength, out int length, byte[] output);

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x060001A4 RID: 420
		public delegate void GetProgramivDelegate(int program, int paremeterName, out int returnValue);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060001A8 RID: 424
		public delegate void GetShaderivDelegate(int shader, int paremeterName, out int returnValue);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060001AC RID: 428
		private delegate void UniformMatrix4fvDelegate(int location, int count, byte isTranspose, float[] matrix);

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060001B0 RID: 432
		public delegate void Uniform4fDelegate(int location, float f1, float f2, float f3, float f4);

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x060001B4 RID: 436
		public delegate void Uniform1iDelegate(int location, int i);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060001B8 RID: 440
		public delegate void Uniform1fDelegate(int location, float f);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x060001BC RID: 444
		public delegate void Uniform2fDelegate(int location, float f1, float f2);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x060001C0 RID: 448
		public delegate void UseProgramDelegate(int program);

		// Token: 0x02000055 RID: 85
		// (Invoke) Token: 0x060001C4 RID: 452
		public delegate void DeleteProgramDelegate(int program);

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x060001C8 RID: 456
		public delegate void LinkProgramDelegate(int program);

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x060001CC RID: 460
		public delegate void AttachShaderDelegate(int program, int shader);

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x060001D0 RID: 464
		private delegate void ShaderSourceDelegate(int shader, int count, IntPtr[] shaderSource, int[] length);

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x060001D4 RID: 468
		public delegate int CompileShaderDelegate(int shader);

		// Token: 0x0200005A RID: 90
		// (Invoke) Token: 0x060001D8 RID: 472
		public delegate int CreateProgramObjectDelegate();

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x060001DC RID: 476
		public delegate int CreateShaderObjectDelegate(ShaderType shaderType);

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x060001E0 RID: 480
		public delegate IntPtr wglCreateContextAttribsDelegate(IntPtr hDC, IntPtr hShareContext, int[] attribList);
	}
}
