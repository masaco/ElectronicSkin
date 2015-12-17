///<summary>
/// Xsens Stream Reader Thread read from the stream and store the latest pose from 1 actor.
/// 
///</summary>
///<version>
/// 0.1, 2013.03.12 by Peter Heinen
/// 1.0, 2013.05.14 by Attila Odry, Daniël van Os
///</version>
///<remarks>
/// Copyright (c) 2013, Xsens Technologies B.V.
/// All rights reserved.
/// 
/// Redistribution and use in source and binary forms, with or without modification,
/// are permitted provided that the following conditions are met:
/// 
/// 	- Redistributions of source code must retain the above copyright notice, 
///		  this list of conditions and the following disclaimer.
/// 	- Redistributions in binary form must reproduce the above copyright notice, 
/// 	  this list of conditions and the following disclaimer in the documentation 
/// 	  and/or other materials provided with the distribution.
/// 
/// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
/// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
/// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS
/// BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
/// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
/// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
/// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
/// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
///</remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;

namespace xsens
{
	public enum StreamingProtocol 
	{
		SPPoseEuler	= 1, 
		SPPoseQuaternion = 2, 
		SPPosePositions = 3, 
		SPTagPositionsLegacy = 4, 
		SPPoseUnity3D = 5,
		SPMetaScalingLegacy = 10,
		SPMetaPropInfoLegacy = 11,
		SPMetaMoreMeta = 12,
		SPMetaScaling = 13
	};

    /// <summary>
    /// Xsens Stream Reader Thread.
    /// Every actor from MVN Stream has its own reader trhead.
    /// </summary>
    class XsStreamReaderThread
    {
		private XsDataPacket datapacket;
        private Mutex packetMutex = new Mutex(false, "PacketMutex");
        private Mutex poseMutex = new Mutex(false, "PoseMutex");

        private Thread thread;
        private byte[] lastPackets;
        private bool newData = false;
        private bool dataUpdated = true;

        private Vector3[] lastPosePositions;
        private Quaternion[] lastPoseOrientations;

        /// <summary>
        /// Initializes a new instance of the <see cref="xsens.XsStreamReaderThread"/> class.
        /// </summary>
        public XsStreamReaderThread()
        {
            //make sure we always have some date, even when no streaming
            lastPosePositions = new Vector3[XsMvnPose.MvnSegmentCount];
            lastPoseOrientations = new Quaternion[XsMvnPose.MvnSegmentCount];
            //start a new thread		
            thread = new Thread(new ThreadStart(start));
            thread.Start();
        }

        /// <summary>
        /// Start this instance.
        /// The datapacket will be set to one of the supported mode, based on its type.
        /// </summary>
        public void start()
        {
            dataUpdated = false;
            while (true)
            {
                if (newData)
                {
                    if (packetMutex.WaitOne(1000))
                    {
                        try
                        {
			                newData = false;

                            //create the proper Data Packet based on the packets type
							string ptypeString = System.String.Empty;
							ptypeString += (char)(lastPackets[4]);
							ptypeString += (char)(lastPackets[5]); // last two chars contain packet ID type
							int ptype = Convert.ToInt32(ptypeString);      
							switch ((StreamingProtocol)ptype)
							{
							case StreamingProtocol.SPPoseEuler:
									datapacket = new XsEulerPacket(lastPackets);
								break;
							case StreamingProtocol.SPPoseQuaternion:
							case StreamingProtocol.SPPoseUnity3D:
								// technically this one should be handled differently
								datapacket = new XsQuaternionPacket(lastPackets);
								break;
							case StreamingProtocol.SPMetaScalingLegacy:
							case StreamingProtocol.SPMetaPropInfoLegacy:
							case StreamingProtocol.SPMetaMoreMeta:
							case StreamingProtocol.SPMetaScaling:
								// ignored packet containing not useful meta-data information for the unity plug-in. 
								break;
							case StreamingProtocol.SPPosePositions:
							case StreamingProtocol.SPTagPositionsLegacy:
								Debug.LogError("[xsens] Wrong protocol identified. Try to change the protocol type in the Network Streamer preferences panel in MVN Studio.");
								break;
							default:
								Debug.LogError("[xsens] Not supported packet type ("+ ptype +")!");
								break;
							}
                        }
                        finally
                        {
                            packetMutex.ReleaseMutex();

                            if (poseMutex.WaitOne(1000))
                            {
                                try
                                {
                                    XsMvnPose pose = datapacket.getPose();
                                    if(pose != null)
                                    {
                                        lastPosePositions = pose.positions;
                                        lastPoseOrientations = pose.orientations;
                                        dataUpdated = true;
                                    }
                                }
                                finally
                                {
                                    poseMutex.ReleaseMutex();

                                }
                            }//if poseMutex
                        }
                    }//if packetMutex
                }

                Thread.Sleep(1);
            }//while
        }

        /// <summary>
        /// Check if there is data available.
        /// </summary>
        /// <returns>
        /// true if data is available
        /// </returns>
        public bool dataAvailable()
        {
            poseMutex.WaitOne();

            try
            {
                return dataUpdated;
            }
            finally
            {
                poseMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Get the latest pose info that is available
        /// </summary>
        /// <param name="positions">This will return the positions</param>
        /// <param name="orientations">This will return the orientations</param>
        /// <returns>True if a proper pose was available, false otherwise</returns>
        public bool getLatestPose(out Vector3[] positions, out Quaternion[] orientations)
        {
            if (poseMutex.WaitOne(100))
            {
                try
                {
                    positions = lastPosePositions;
                    orientations = lastPoseOrientations;
                    return true;
                }
                finally
                {
                    poseMutex.ReleaseMutex();
                }
            }

            positions = null;
            orientations = null;
            return false;
        }

        /// <summary>
        /// Kills the thread.
        /// </summary>
        public void killThread()
        {
            thread.Abort();
        }

        /// <summary>
        /// Sets the packet.
        /// </summary>
        /// <param name='incomingData'>
        /// _incoming data in array
        /// </param>
        public void setPacket(byte[] incomingData)
        {
            if (packetMutex.WaitOne())
            {
                try
                {
                    lastPackets = incomingData;
                    newData = true;
                }
                finally
                {
                    packetMutex.ReleaseMutex();
                }
            }
        }

    }//class XsStreamReaderThread
}//namespace xsens