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

using SCHREDDO.Nerdy.Twitch.IRC.Connection;

namespace SCHREDDO.Nerdy.Twitch.IRC.Functions
{
    public static class TwitchIRCClient
    {
        public static int Connect(TwitchIRCConnection connection, bool extendedInformation) 
        {
            if (connection.ConnectionStatus == 0) 
            {
                int isConnected = connection.Connect();

                if (isConnected == 0) 
                {
                    connection.Send("PASS oauth:" + connection.UserPassword);
                    connection.Send("NICK " + connection.Username);

                    if (extendedInformation) 
                    {
                        connection.Send("CAP REQ :twitch.tv/membership");
                        connection.Send("CAP REQ :twitch.tv/commands");
                        connection.Send("CAP REQ :twitch.tv/tags");
                    }
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }

        public static int Disconnect(TwitchIRCConnection connection)
        {

            if (connection.ConnectionStatus == 1)
            {
                connection.Disconnect();
            }

            return 0;
        }

        public static int Restart(TwitchIRCConnection connection, bool extendedInformation)
        {

            if (connection.ConnectionStatus == 0 || connection.ConnectionStatus == 2)
            {
                TwitchIRCClient.Connect(connection, extendedInformation);
            }

            if (connection.ConnectionStatus == 1)
            {
                TwitchIRCClient.Disconnect(connection);
                TwitchIRCClient.Connect(connection, extendedInformation);
            }

            return 0;
        }

        public static int JoinChannel(String channelName, TwitchIRCConnection connection)
        {
            return TwitchIRCClient.SendServerMessage("JOIN #" + channelName, connection);
        }

        public static int DepartChannel(String channelName, TwitchIRCConnection connection)
        {
            return TwitchIRCClient.SendServerMessage("PART #" + channelName, connection);
        }

        public static String ReadMessage(TwitchIRCConnection connection)
        {
            String message = "";

            message = connection.Receive();

            if (message.ToLower().StartsWith("ping"))
            {
                TwitchIRCClient.SendServerMessage("PONG :tmi.twitch.tv", connection);
            }

            return message;
        }

        public static int SendServerMessage(String message, TwitchIRCConnection connection)
        {
            return connection.Send(message);
        }

        public static int SendChannelMessage(String channelName, String message, TwitchIRCConnection connection)
        {
            return TwitchIRCClient.SendServerMessage("PRIVMSG #" + channelName + " :" + message, connection);
        }

        public static int SendWisperMessage(String username, String message, TwitchIRCConnection connection)
        {
            return TwitchIRCClient.SendServerMessage(".w " + username + " " + message, connection);
        }
    }
}
