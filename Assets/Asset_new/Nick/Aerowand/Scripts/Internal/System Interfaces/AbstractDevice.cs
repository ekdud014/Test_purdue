namespace Aerowand.Internal
{
    public abstract class AbstractDevice
    {
        protected AbstractDevice (Tracker deviceType)
        {
            tracker = deviceType;
        }
        abstract public AerowandData getAerowandData();
        abstract public float getPacketHz();
        public Tracker tracker { get; private set; }
    }
}
