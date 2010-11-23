﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Command;

namespace SuperSocket.SocketBase
{
    public interface IAppSession
    {
        ISocketSession SocketSession { get; }
        string SessionID { get; }
        string IdentityKey { get; }
        SocketContext Context { get; }
        IServerConfig Config { get; }
        IPEndPoint LocalEndPoint { get; }
        IPEndPoint RemoteEndPoint { get; }
        DateTime LastActiveTime { get; set; }
        DateTime StartTime { get; }
        void Close();
        void StartSession();
        void HandleExceptionalError(Exception e);
    }

    public interface IAppSession<TCommandInfo> : IAppSession
        where TCommandInfo : ICommandInfo
    {        
        void HandleUnknownCommand(TCommandInfo cmdInfo);
    }

    public interface IAppSession<TAppSession, TCommandInfo> : IAppSession<TCommandInfo>
        where TCommandInfo : ICommandInfo
        where TAppSession : IAppSession, IAppSession<TCommandInfo>, new()
    {
        void Initialize(IAppServer<TAppSession, TCommandInfo> server, ISocketSession socketSession);
        void ExecuteCommand(TAppSession session, TCommandInfo cmdInfo);
    }
}