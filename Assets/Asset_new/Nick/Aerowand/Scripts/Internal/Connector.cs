using UnityEngine;

namespace Aerowand.Internal {
    public class Connector
    {
        public static Device AerowandHead { get; private set; }
        public static Device AerowandHand { get; private set; }

        public static Connector instance { get; private set; }

        [RuntimeInitializeOnLoadMethod]
        static void InitializeAerowand()
        {
            instance = new Connector();
        }

        private Connector()
        {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
            WindowsWrapper.InitializeDevices();
            AerowandHead = new Device(new WindowsWrapper(Tracker.Head));
            AerowandHand = new Device(new WindowsWrapper(Tracker.Pointer));
#else
            AndroidWrapper.InitializeDevices();
            AerowandHead = new Device(new AndroidWrapper(Tracker.Head));
            AerowandHand = new Device(new AndroidWrapper(Tracker.Pointer));
#endif
        }

        ~Connector()
        {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
            WindowsWrapper.CloseDevices();
#endif
        }
    }
}