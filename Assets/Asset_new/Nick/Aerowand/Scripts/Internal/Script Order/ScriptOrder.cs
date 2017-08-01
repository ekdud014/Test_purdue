// From https://forum.unity3d.com/threads/script-execution-order-manipulation.130805/

using System;

namespace Aerowand.Internal
{
    public class ScriptOrder : Attribute
    {
        public int order;

        public ScriptOrder(int order)
        {
            this.order = order;
        }
    }
}