// From https://forum.unity3d.com/threads/script-execution-order-manipulation.130805/

/* "Using UnityEditor" breaks the build if script is not in the "Editor" directory,
 * while placing this script in the an Editor directory breaks runtime scripts.
 * This fixes the issue by commenting the scipt when fully building the application.
 */
//#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Aerowand.Internal
{
    [InitializeOnLoad]
    public class ScriptOrderManager
    {
        static ScriptOrderManager()
        {
            foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
            {
                if (monoScript.GetClass() != null)
                {
                    foreach (var a in Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(ScriptOrder)))
                    {
                        var currentOrder = MonoImporter.GetExecutionOrder(monoScript);
                        var newOrder = ((ScriptOrder)a).order;
                        if (currentOrder != newOrder)
                        {
                            MonoImporter.SetExecutionOrder(monoScript, newOrder);
                        }
                    }
                }
            }
        }
    }
}
//#endif
