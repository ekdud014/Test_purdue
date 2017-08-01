using UnityEngine;
using System;

namespace Aerowand.Internal
{
    [ScriptOrder(-32000)] // Earliest possible priority in execution order
    public class DeviceDriver : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        static void AddToScene ()
        {
            DontDestroyOnLoad(new GameObject("Aerowand Driver").AddComponent<DeviceDriver>());
        }

        public static event Action onEarlyUpdate = delegate {};

        void Update()
        {
            onEarlyUpdate();
        }
    }
}