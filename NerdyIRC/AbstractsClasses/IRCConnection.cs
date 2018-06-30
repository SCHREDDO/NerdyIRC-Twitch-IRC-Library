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
using System.IO;
using System.Net.Sockets;

namespace SCHREDDO.Nerdy.Twitch.IRC.AbstractClasses 
{
    public abstract class IRCConnection 
    {
        private string serverAddress;
        private int serverPort;

        private StreamReader connectionReceive;
        private StreamWriter connectionSend;
        private TcpClient connectionSocket;
        private int connectionStatus;

        public string ServerAddress { get; protected set; }
        public int ServerPort { get; protected set; }

        private StreamReader ConnectionReceive { get; set; }
        private StreamWriter ConnectionSend { get; set; }
        private TcpClient ConnectionSocket { get; set; }
        public int ConnectionStatus { get; protected set; }

        public IRCConnection(string serverAddress, int serverPort)
        {
            ServerAddress = serverAddress;
            ServerPort = serverPort;
            ConnectionReceive = null;
            ConnectionSend = null;
            ConnectionSocket = null;
            ConnectionStatus = 0;
        }

        public int Connect()
        {
            ConnectionStatus = 3;
            try
            {
                ConnectionSocket = new TcpClient(ServerAddress, ServerPort);
                ConnectionReceive = new StreamReader(ConnectionSocket.GetStream());
                ConnectionSend = new StreamWriter(ConnectionSocket.GetStream());
                ConnectionStatus = 1;
            }
            catch (Exception e)
            {
                ConnectionStatus = 2;
            }

            return 0;
        }

        public int Disconnect()
        {
            ConnectionStatus = 4;

            try
            {
                ConnectionReceive.Close();
                ConnectionSend.Close();
                ConnectionSocket.Close();
                ConnectionStatus = 0;
            }
            catch (Exception e)
            {
                ConnectionStatus = 2;
            }

            return 0;
        }

        public int Send(string message)
        {
            ConnectionSend.WriteLine(message);
            ConnectionSend.Flush();

            return 0;
        }

        public string Receive()
        {
            string message = "";

            message = ConnectionReceive.ReadLine();

            return message;
        }
    }
}
