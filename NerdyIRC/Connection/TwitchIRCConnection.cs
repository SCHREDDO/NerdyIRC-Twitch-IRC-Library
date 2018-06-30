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
// Created On: 30.06.2018
// Last Edited On: 30.06.2018
// Language: C#
//
using System;

using SCHREDDO.Nerdy.Twitch.IRC.AbstractClasses;

namespace SCHREDDO.Nerdy.Twitch.IRC.Connection
{
    public class TwitchIRCConnection : IRCConnection
    {
        private string username;
        private string userPassword;

        public string Username { get; private set; }
        public string UserPassword { get; private set; }

        public TwitchIRCConnection(string username, string userPassword) 
        : this(username, userPassword, "irc.twitch.tv", 443) {}

        public TwitchIRCConnection(string username, string userPassword, bool useSSL) 
        : this(username, userPassword, "irc.twitch.tv", useSSL) {}

        public TwitchIRCConnection(string username, string userPassword, int serverPort) 
        : this(username, userPassword, "irc.twitch.tv", serverPort) {}

        public TwitchIRCConnection(string username, string userPassword, string serverAddress, bool useSSL) 
        : this(username, userPassword, serverAddress, 443)
        {
            if (!useSSL) { ServerPort = 6667; }
        }

        public TwitchIRCConnection(string username, string userPassword, string serverAddress, int serverPort) 
        : base(serverAddress, serverPort)
        {
            Username = username;
            UserPassword = userPassword;
        }
    }
}
