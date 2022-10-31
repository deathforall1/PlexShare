/// <author>Mohammad Umar Sultan</author>
/// <created>16/10/2022</created>
/// <summary>
/// This file contains the class definition of SendQueueListenerClient.
/// </summary>

using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Networking
{
	public class SendQueueListenerClient
	{
		// the thread which will be running
		private Thread _thread;
		// boolean to tell whether thread is running or stopped
		private volatile bool _threadRun;

		// variable to store the send queue
		private readonly IQueue _queue;

		// variable to store the socket
		private readonly TcpCleint _socket;

		/// <summary>
		/// It is the Constructor which initializes the queue and socket
		/// </summary>
		/// <param name="queue"> The the send queue. </param>
		/// <param name="socket"> The socket to send the data. </param>
		public SendQueueListenerClient(IQueue queue, TcpCleint socket)
		{
			_queue = queue;
			_socket = socket;
		}

		/// <summary>
		/// This function starts the thread.
		/// </summary>
		/// <returns> void </returns>
		public void Start()
		{
			_thread = new Thread(Listen);
			_threadRun = true;
			_thread.Start();
			Trace.WriteLine("[Networking] SendQueueListenerClient thread started.");
		}

		/// <summary>
		/// This function stops the thread.
		/// </summary>
		/// <returns> void </returns>
		public void Stop()
		{
			_threadRun = false;
			Trace.WriteLine("[Networking] SendQueueListenerClient thread stopped.");
		}

		/// <summary>
		/// This function listens to send queue and when some packet comes in the send queue then
		/// it sends the packet to the server. The thread will be running this function.
		/// </summary>
		/// <returns> void </returns>
		private void Listen()
		{
			while (_threadRun)
			{
				_queue.WaitForPacket();
				while (!_queue.IsEmpty())
				{
					var packet = _queue.Dequeue();

					/// we put flag string at the start and end of the packet, and we need to put
					/// escape string before the flag and escape strings which are in the packet
					var pkt = packet.ModuleIdentifier + ":" + packet.SerializedData;
					pkt = pkt.Replace("[ESC]", "[ESC][ESC]");
					pkt = pkt.Replace("[FLAG]", "[ESC][FLAG]");
					pkt = "[FLAG]" + pkt + "[FLAG]";
					var bytes = Encoding.ASCII.GetBytes(pkt);
					try
					{
						_socket.Client.Send(bytes);
						Trace.WriteLine($"[Networking] Data sent from client to server by module {packet.ModuleIdentifier}.");
					}
					catch (exception e)
					{
						Trace.WriteLine($"[Networking] Error in SendQueueListenerClient thread: {e.Message}");
					}
				}
			}
		}
	}
}
