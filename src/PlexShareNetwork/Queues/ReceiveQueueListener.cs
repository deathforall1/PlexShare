﻿/// <author> Anish Bhagavatula </author>
/// <summary>
/// This file contains the definition of the class 'Receive QueueHandler' which contains functions to spawn a thread to call module handlers
/// once packets appear in the receiving queue
/// </summary>

using System;
using System.Collections.Generic;
using System.Threading;

namespace Networking.Queues
{
    public class ReceiveQueueListener
    {
        private Dictionary<string, INotificationHandler> _modulesToNotificationHandlerMap;
        private IQueue _receivingQueue;
        private bool _isRunning;

        // Constructor which is called by the Communicator
        public ReceiveQueueListener(Dictionary<string, INotificationHandler> modulesToNotificationHandlerMap, IQueue receivingQueue)
        {
            this._modulesToNotificationHandlerMap = modulesToNotificationHandlerMap;
            this._receivingQueue = receivingQueue;
        }

        /// <summary>
        /// Called by the Communicator to start a thread for calling module handlers
        /// </summary>
        public void Start()
        {
            ThreadStart listeningThreadRef = new ThreadStart(ListenOnQueue);

            // Creating a thread
            Thread listeningThread = new Thread(listeningThreadRef);

            // Starting the thread
            listeningThread.Start();

            // Declaring that the queue is running
            _isRunning = true;
        }

        /// <summary>
        /// Keep listening on the receiving queue and call the module's notification handlers if a packet appears in the queue
        /// </summary>
        private void ListenOnQueue()
        {
            // Keep listening on the queue until the Communicator asks to stop
            while (_isRunning)
            {
                // Waiting for the receiving queue to contain a packet
                _receivingQueue.WaitForPacket();

                Packet packet = _receivingQueue.Dequeue();

                // Identifying the module which the packet belongs to
                string moduleName = packet.getModuleOfPacket();

                if (!_modulesToNotificationHandlerMap.ContainsKey(moduleName))
                {
                    Console.WriteLine("Module %s does not contain a handler.\n", moduleName);
                    continue;
                }

                INotificationHandler notificationHandler = _modulesToNotificationHandlerMap[moduleName];

                // Calling the method 'OnDataReceived' on the handler of the appropriate module
                notificationHandler.OnDataReceived(packet.getSerializedData());
            }
        }

        /// <summary>
        /// Called by the Communicator to stop the thread that was run by the 'Start' function
        /// </summary>
        public void Stop()
        {
            // Stating to the thread to stop running
            _isRunning = false;
        }
    }
}
