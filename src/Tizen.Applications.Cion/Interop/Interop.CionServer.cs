﻿/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Runtime.InteropServices;
using Tizen.Applications.Cion;

using ErrorCode = Interop.Cion.ErrorCode;

internal static partial class Interop
{
    internal static partial class CionServer
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool CionServerPeerInfoIterator(IntPtr peerInfo, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerPayloadAsyncResultCb(IntPtr result, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerConnectionResultCb(string serviceName, IntPtr peerInfo, IntPtr result, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerDataReceivedCb(string serviceName, IntPtr peerInfo, byte[] data, int dataSize, out IntPtr returnData, out int returnDataSize, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerPayloadReceivedCb(string serviceName, IntPtr peerInfo, IntPtr payload, int status, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerConnectionRequestCb(string serviceName, IntPtr peerInfo, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerErrorReportedCb(string serviceName, IntPtr peerInfo, int error, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CionServerDisconnectedCb(string serviceName, IntPtr peerInfo, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_create")]
        internal static extern ErrorCode CionServerCreate(out ServerSafeHandle server, string serviceName, string displayName, IntPtr security);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_destroy")]
        internal static extern ErrorCode CionServerDestroy(IntPtr server);
        
        [DllImport(Libraries.Cion, EntryPoint = "cion_server_listen")]
        internal static extern ErrorCode CionServerListen(ServerSafeHandle server, CionServerConnectionRequestCb cb, IntPtr userData);
        
        [DllImport(Libraries.Cion, EntryPoint = "cion_server_stop")]
        internal static extern ErrorCode CionServerStop(ServerSafeHandle server);
        
        [DllImport(Libraries.Cion, EntryPoint = "cion_server_accept")]
        internal static extern ErrorCode CionServerAccept(ServerSafeHandle server, PeerInfoSafeHandle peerInfo);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_reject")]
        internal static extern ErrorCode CionServerReject(ServerSafeHandle server, PeerInfoSafeHandle peerInfo, string reason);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_disconnect")]
        internal static extern ErrorCode CionServerDisconnect(ServerSafeHandle server, PeerInfoSafeHandle peerInfo);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_send_payload_async")]
        internal static extern ErrorCode CionServerSendPayloadAsync(ServerSafeHandle server, PeerInfoSafeHandle peerInfo, PayloadSafeHandle payload, CionServerPayloadAsyncResultCb cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_foreach_connected_peer_info")]
        internal static extern ErrorCode CionServerForeachConnectedPeerInfo(ServerSafeHandle server, CionServerPeerInfoIterator cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_add_connection_result_cb")]
        internal static extern ErrorCode CionServerAddConnectionResultCb(ServerSafeHandle server, CionServerConnectionResultCb cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_remove_connection_result_cb")]
        internal static extern ErrorCode CionServerRemoveConnectionResultCb(ServerSafeHandle server, CionServerConnectionResultCb cb);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_add_payload_received_cb")]
        internal static extern ErrorCode CionServerAddPayloadReceivedCb(ServerSafeHandle server, CionServerPayloadReceivedCb cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_remove_payload_received_cb")]
        internal static extern ErrorCode CionServerRemovePayloadReceivedCb(ServerSafeHandle server, CionServerPayloadReceivedCb cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_set_data_received_cb")]
        internal static extern ErrorCode CionServerSetDataReceivedCb(ServerSafeHandle server, CionServerDataReceivedCb cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_unset_data_received_cb")]
        internal static extern ErrorCode CionServerUnsetDataReceivedCb(ServerSafeHandle server, CionServerDataReceivedCb cb);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_add_disconnected_cb")]
        internal static extern ErrorCode CionServerAddDisconnectedCb(ServerSafeHandle server, CionServerDisconnectedCb cb, IntPtr userData);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_remove_disconnected_cb")]
        internal static extern ErrorCode CionServerRemoveDisconnectedCb(ServerSafeHandle server, CionServerDisconnectedCb cb);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_set_on_demand_launch_enabled")]
        internal static extern ErrorCode CionServerSetOnDemandLaunchEnabled(ServerSafeHandle server, bool enable);

        [DllImport(Libraries.Cion, EntryPoint = "cion_server_set_display_name")]
        internal static extern ErrorCode CionServerSetDisplayName(ServerSafeHandle server, string displayName);
    }
}
