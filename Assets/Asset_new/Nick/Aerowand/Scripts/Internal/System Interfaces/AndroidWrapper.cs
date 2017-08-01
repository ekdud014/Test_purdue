using UnityEngine;

namespace Aerowand.Internal
{
    public class AndroidWrapper : AbstractDevice
    {
        static AndroidJavaObject head;
        static AndroidJavaObject hand;

        AndroidJavaObject aerowand;

        public static void InitializeDevices ()
        {
            AndroidJavaObject activityContext;
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                using (AndroidJavaClass aerowandDeviceFactory = new AndroidJavaClass("com.accups.aerowandandroid.AerowandDeviceFactory"))
                {
                    head = aerowandDeviceFactory.CallStatic<AndroidJavaObject>("getSingletonHead", activityContext);
                    hand = aerowandDeviceFactory.CallStatic<AndroidJavaObject>("getSingletonHand", activityContext);
                }
            }));
        }

        public AndroidWrapper(Tracker deviceType) : base(deviceType)
        {
            switch (tracker)
            {
                case Tracker.Head:
                    while (head == null) { } // XXX wait for ui thread to return
                    aerowand = head;
                    break;
                case Tracker.Pointer:
                    while (hand == null) { } // XXX wait for ui thread to return
                    aerowand = hand;
                    break;
                default:
                    break;
            }
        }

        override public AerowandData getAerowandData()
        {
            AerowandData aerowandData;
            aerowandData.positionXYZ = aerowand.Call<float[]>("getPositionXYZ");
            aerowandData.orientationRPY = aerowand.Call<float[]>("getOrientationRPY");
            aerowandData.buttonFlags = aerowand.Call<byte>("getButtonFlags");
            return aerowandData;
        }

        override public float getPacketHz()
        {
            return aerowand.Call<float>("getPacketHz");
        }
    }
}
