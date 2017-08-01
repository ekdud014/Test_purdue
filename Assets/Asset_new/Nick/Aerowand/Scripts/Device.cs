using UnityEngine;
using System;
using Aerowand.Internal;

namespace Aerowand
{
    public enum Button
    {
        Face = 1 << 0,
        Trigger = 1 << 1
    }
    public enum Tracker { Head, Pointer }

    public class Device
    {
        public static Device AerowandHead { get { return Connector.AerowandHead; } }
        public static Device AerowandHand { get { return Connector.AerowandHand; } }

        public Tracker tracker { get { return aerowandDevice.tracker; } }
        public Vector3 position { get; private set; }
        public Quaternion rotation { get; private set; }
        public Vector3 positionRaw { get; private set; }
        public Quaternion rotationRaw { get; private set; }
        public float packetHz { get; private set; }
        
        private AbstractDevice aerowandDevice = null;
        private byte buttonFlags;
        private byte thisFrameButtonFlags;
        private byte lastFrameButtonFlags;

        public bool GetButtonDown(Button button)
        {
            return ((thisFrameButtonFlags & ~lastFrameButtonFlags) & (byte)button) != 0;
        }
        public bool GetButtonUp(Button button)
        {
            return ((~thisFrameButtonFlags & lastFrameButtonFlags) & (byte)button) != 0;
        }
        public bool GetButton(Button button)
        {
            return (thisFrameButtonFlags & (byte)button) != 0;
        }

        public Device(AbstractDevice deviceInterface)
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
            positionRaw = Vector3.zero;
            rotationRaw = Quaternion.identity;
            packetHz = 0.0f;
            aerowandDevice = deviceInterface;
            // Allow buttons to be updated before MonoBehaviour calls Update()
            // for consistency with Unity native Input.
            DeviceDriver.onEarlyUpdate += UpdateRoutine;
        }

        void UpdateButtons()
        {
            lastFrameButtonFlags = thisFrameButtonFlags;
            thisFrameButtonFlags = buttonFlags;
            foreach (Button button in Enum.GetValues(typeof(Button)))
            {
                if (GetButtonDown(button))
                {
                    Debug.Log(button.ToString() + " pressed");
                }
                else if (GetButtonUp(button))
                {
                    Debug.Log(button.ToString() + " released");
                }
            }
        }

        void GetData()
        {
            AerowandData aerowandData = aerowandDevice.getAerowandData();
            aerowandData.positionXYZ[2] *= -1.0f;
            positionRaw = new Vector3(aerowandData.positionXYZ[0], aerowandData.positionXYZ[1], aerowandData.positionXYZ[2]);
            position = positionRaw;

            aerowandData.orientationRPY[0] *= -1.0f;
            var yaw = Quaternion.Euler(0.0f, aerowandData.orientationRPY[0], 0.0f);
            var pitch = Quaternion.Euler(aerowandData.orientationRPY[1], 0.0f, 0.0f);
            var roll = Quaternion.Euler(0.0f, 0.0f, -aerowandData.orientationRPY[2]);

            rotationRaw = yaw * pitch * roll;
            rotation = rotationRaw;

            buttonFlags = aerowandData.buttonFlags;
            packetHz = aerowandDevice.getPacketHz();
        }

        void UpdateRoutine ()
        {
            GetData();
            UpdateButtons();
        }

        public void ForceUpdate()
        {
            GetData();
        }
    }
}
