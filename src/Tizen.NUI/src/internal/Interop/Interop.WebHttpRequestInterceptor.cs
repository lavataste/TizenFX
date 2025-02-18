﻿/*
 * Copyright(c) 2021 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System.Runtime.InteropServices;

namespace Tizen.NUI
{
    internal static partial class Interop
    {
        internal static partial class WebHttpRequestInterceptor
        {
            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_GetUrl")]
            public static extern string GetUrl(global::System.Runtime.InteropServices.HandleRef jarg1);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_GetMethod")]
            public static extern string GetMethod(global::System.Runtime.InteropServices.HandleRef jarg1);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_GetHeaders")]
            public static extern global::System.IntPtr GetHeaders(global::System.Runtime.InteropServices.HandleRef jarg1);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_Ignore")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool Ignore(global::System.Runtime.InteropServices.HandleRef jarg1);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_SetResponseStatus")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool SetResponseStatus(global::System.Runtime.InteropServices.HandleRef jarg1, int jarg2, string jarg3);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_AddResponseHeader")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool AddResponseHeader(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2, string jarg3);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_AddResponseHeaders")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool AddResponseHeaders(global::System.Runtime.InteropServices.HandleRef jarg1, global::System.Runtime.InteropServices.HandleRef jarg2);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_AddResponseBody")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool AddResponseBody(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2, uint jarg3);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "CSharp_Dali_WebRequestInterceptor_AddResponseBody")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool AddResponseBody(global::System.Runtime.InteropServices.HandleRef jarg1, [MarshalAs(UnmanagedType.LPArray)] byte[] jarg2, uint jarg3);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_AddResponse")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool AddResponse(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2, string jarg3, uint jarg4);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "CSharp_Dali_WebRequestInterceptor_AddResponse")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool AddResponse(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2, [MarshalAs(UnmanagedType.LPArray)] byte[] jarg3, uint jarg4);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, EntryPoint = "CSharp_Dali_WebRequestInterceptor_WriteResponseChunk")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool WriteResponseChunk(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2, uint jarg3);

            [global::System.Runtime.InteropServices.DllImport(NDalicPINVOKE.Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "CSharp_Dali_WebRequestInterceptor_WriteResponseChunk")]
            [return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.U1)]
            public static extern bool WriteResponseChunk(global::System.Runtime.InteropServices.HandleRef jarg1, [MarshalAs(UnmanagedType.LPArray)] byte[] jarg2, uint jarg3);
        }
    }
}

