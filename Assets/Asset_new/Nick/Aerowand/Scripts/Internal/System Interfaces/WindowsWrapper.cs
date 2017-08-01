using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Aerowand.Internal
{
    public class WindowsWrapper : AbstractDevice
    {
        int deviceId;

        [DllImport("aerowand", EntryPoint = "InitializeDevices")]
        public static extern byte InitializeDevices();

        [DllImport("aerowand", EntryPoint = "CloseDevices")]
        public static extern void CloseDevices();

        [DllImport("aerowand", EntryPoint = "GetAerowandHead")]
        private static extern int GetAerowandHead();

        [DllImport("aerowand", EntryPoint = "GetAerowandHandDominant")]
        private static extern int GetAerowandHandDominant();

        [DllImport("aerowand", EntryPoint = "GetAerowandHandNondominant")]
        private static extern int GetAerowandHandNondominant();

        [DllImport("aerowand", EntryPoint = "GetAerowandData")]
        private static extern AerowandData GetAerowandData(int deviceId);

        public WindowsWrapper(Tracker deviceType) : base (deviceType)
        {
            switch (tracker)
            {
                case Tracker.Head:
                    deviceId = GetAerowandHead();
                    break;
                case Tracker.Pointer:
                    deviceId = GetAerowandHandDominant();
                    break;
                default:
                    break;
            }
        }

        override public AerowandData getAerowandData()
        {
            return GetAerowandData(deviceId);
        }

        override public float getPacketHz()
        {
            return 0;
        }
    }
}
