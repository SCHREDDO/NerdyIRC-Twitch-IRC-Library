// Copyright (C) 2018 Sebastian Lühnen
//
//
// This file is part of NerdyIRC.
// 
// NerdyIRC is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// NerdyIRC is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with NerdyIRC. If not, see <http://www.gnu.org/licenses/>.
//
//
// Created By: Sebastian Lühnen
// Created On: 28.06.2018
// Last Edited On: 30.06.2018
// Language: C#
//
using System;

using SCHREDDO.Nerdy.Twitch.IRC.Connection;
using SCHREDDO.Nerdy.Twitch.IRC.Functions;

namespace SCHREDDO.Nerdy.Twitch.IRC
{
    public class NerdyIRC
    {
        private TwitchIRCConnection connection;
        private bool extendedInformation;

        protected TwitchIRCConnection Connection { get; set; }
        public bool ExtendedInformation { get; protected set; }

        public NerdyIRC(string username, string userPassword)
        : this(username, userPassword, true) {}

        public NerdyIRC(string username, string userPassword, bool extendedInformation)
        : this(username, userPassword, extendedInformation, true) {}

        public NerdyIRC(string username, string userPassword, bool extendedInformation, bool useSSL)
        {
            Connection = new TwitchIRCConnection(username, userPassword, useSSL);
            ExtendedInformation = extendedInformation;
        }

        public NerdyIRC(string username, string userPassword, bool extendedInformation, string serverAddress, int serverPort)
        {
            Connection = new TwitchIRCConnection(username, userPassword, serverAddress, serverPort);
            ExtendedInformation = extendedInformation;
        }

        public void SetConnectionInformation(string username, string userPassword)
        {
            SetConnectionInformation(username, userPassword, true);
        }

        public void SetConnectionInformation(string username, string userPassword, bool extendedInformation)
        {
            SetConnectionInformation(username, userPassword, true, true);
        }

        public void SetConnectionInformation(string username, string userPassword, bool extendedInformation, bool useSSL)
        {
            Connection = new TwitchIRCConnection(username, userPassword, useSSL);
            ExtendedInformation = extendedInformation;
        }

        public void SetConnectionInformation(string username, string userPassword, bool extendedInformation, string serverAddress, int serverPort)
        {
            Connection = new TwitchIRCConnection(username, userPassword, serverAddress, serverPort);
            ExtendedInformation = extendedInformation;
        }

        public int Connect()
        {
            return TwitchIRCClient.Connect(Connection, ExtendedInformation);
        }

        public int Disconnect()
        {
            return TwitchIRCClient.Disconnect(Connection);
        }
        
        public int Restart()
        {
            return TwitchIRCClient.Restart(Connection, ExtendedInformation);
        }
        
        public int JoinChannel(string channelName)
        {
            return TwitchIRCClient.JoinChannel(channelName, Connection);
        }
        
        public int LeaveChannel(string channelName)
        {
            return TwitchIRCClient.DepartChannel(channelName, Connection);
        }
        
        public string ReadMessage()
        {
            return TwitchIRCClient.ReadMessage(Connection);
        }
        
        public int SendServerMessage(string message)
        {
            return TwitchIRCClient.SendServerMessage(message, Connection);
        }
        
        public int SendChannelMessage(string channelName, string message)
        {
            return TwitchIRCClient.SendChannelMessage(channelName, message, Connection);
        }
        
        public int SendWisperMessage(string username, string message)
        {
            return TwitchIRCClient.SendWisperMessage(username, message, Connection);
        }
    }
}
