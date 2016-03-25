/************************************************************************************

Filename    :   DeviceInterface.cs
Content     :   Interface for the Three Glasses Device
Created     :   March 31, 2015
Authors     :   Xiong Fei

Copyright   :   Copyright 2015 ThreeGlasses VR, Inc. All Rights reserved.

************************************************************************************/
using UnityEngine;
using System.Runtime.InteropServices;

namespace SZVR_SDK
{
	public class DeviceInterface
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct MessageList
		{
			public byte isHMDSensorAttached;
			public byte isHMDAttached;
			public byte isLatencyTesterAttached;
			
			public MessageList(byte HMDSensor, byte HMD, byte LatencyTester)
			{
				isHMDSensorAttached = HMDSensor;
				isHMDAttached = HMD;
				isLatencyTesterAttached = LatencyTester;
			}
		};

		public const string vrLib = "SZVRPlugin";

		[DllImport (vrLib)]
		private static extern bool SZVR_Initialize();
		
		[DllImport (vrLib)]
		private static extern bool SZVR_Update(ref MessageList messageList);
		
		[DllImport (vrLib)]
		private static extern bool SZVR_Destroy();
		
		[DllImport (vrLib)]
		private static extern bool SZVR_GetCameraPositionOrientation(ref float px, ref float py, ref float pz, ref float ox, ref float oy, ref float oz, ref float ow);
		
		[DllImport (vrLib)]
		private static extern bool SZVR_ResetSensorOrientation();
		
		[DllImport (vrLib)]
		private static extern bool SZVR_GetInterpupillaryDistance(ref float interpupillaryDistance);
		
		[DllImport (vrLib)]	
		private static extern bool SZVR_GetPlayerEyeHeight(ref float eyeHeight);

		[DllImport (vrLib)]
		private static extern bool SZVR_SaveRunAs3Glasses(string szCmdLine);

		public static bool isInitFinish = false;
		public static float ipd = 0.064f;

		public static bool resetTrackerOnLoad = true;
		public static bool sensorAttached = false;
		public static bool hmdAttached = false;

		private static MessageList msgList = new MessageList(0,0,0);


		/// <summary>
		/// Device Initialization.
		/// </summary>
		public static void Initialize ()
		{
			isInitFinish = SZVR_Initialize();
			
			if(isInitFinish) GetIPD();
		}

		/// <summary>
		/// Gets the IPD.
		/// </summary>
		/// <returns><c>true</c>, if IP was gotten, <c>false</c> otherwise.</returns>
		private static bool GetIPD ()
		{
			return SZVR_GetInterpupillaryDistance(ref ipd);
		}

		/// <summary>
		/// Monitor sensor status.
		/// </summary>
		public static void UpdateDeviceTesting ()
		{
			MessageList oldMsgList = msgList;
			SZVR_Update(ref msgList);

			if((msgList.isHMDSensorAttached != 0) && 
			   (oldMsgList.isHMDSensorAttached == 0))
			{
				Debug.Log("HMD SENSOR ATTACHED");
				sensorAttached = true;
			}
			else if((msgList.isHMDSensorAttached == 0) && 
			        (oldMsgList.isHMDSensorAttached != 0))
			{
				Debug.Log("HMD SENSOR DETACHED");
				sensorAttached = false;
			}
			
			if((msgList.isHMDAttached != 0) && 
			   (oldMsgList.isHMDAttached == 0))
			{
				Debug.Log("HMD ATTACHED");
				hmdAttached = true;
			}
			else if((msgList.isHMDAttached == 0) && 
			        (oldMsgList.isHMDAttached != 0))
			{
				Debug.Log("HMD DETACHED");
				hmdAttached = false;
			}
			
			if((msgList.isLatencyTesterAttached != 0) && 
			   (oldMsgList.isLatencyTesterAttached == 0))
			{
				Debug.Log("LATENCY TESTER ATTACHED");
			}
			else if((msgList.isLatencyTesterAttached == 0) && 
			        (oldMsgList.isLatencyTesterAttached != 0))
			{
				Debug.Log("LATENCY TESTER DETACHED");
			}
		}

		/// <summary>
		/// Pass command line arguments.
		/// </summary>
		/// <returns><c>true</c>, if run as3 glasses was saved, <c>false</c> otherwise.</returns>
		/// <param name="cmd">Cmd.</param>
		public static bool SaveRunAs3Glasses(string cmd)
		{
			return SZVR_SaveRunAs3Glasses(cmd);
		}

		/// <summary>
		/// Remove Device.
		/// </summary>
		public static void DeleteDevice ()
		{
			if(resetTrackerOnLoad == true)
			{
				SZVR_Destroy();
				isInitFinish = false;
			}
		}

		/// <summary>
		/// Orients the sensor.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void OrientSensor(ref Quaternion q)
		{
			q.x = -q.x;
			q.y = -q.y;
		}

		/// <summary>
		/// Sensor data into the direction of the data.
		/// </summary>
		/// <returns>The camera orientation.</returns>
		public static Quaternion GetCameraOrientation()
		{
			Vector3 p = Vector3.zero;
			Quaternion o = Quaternion.identity;

			float px = 0, py = 0, pz = 0, ow = 0, ox = 0, oy = 0, oz = 0;
			
			bool result = SZVR_GetCameraPositionOrientation(ref  px, ref  py, ref  pz, ref  ox, ref  oy, ref  oz, ref  ow);
			
			p.x = px; p.y = py; p.z = -pz;
			o.w = ow; o.x = ox; o.y = oy; o.z = oz;
			
			OrientSensor(ref o);
			return o;
		}

		/// <summary>
		/// Resets the orientation.
		/// </summary>
		/// <returns><c>true</c>, if orientation was reset, <c>false</c> otherwise.</returns>
		public static bool ResetOrientation()
		{
			return SZVR_ResetSensorOrientation();
		}
	}
}
