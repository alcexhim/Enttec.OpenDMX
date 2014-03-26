using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using FTD2XX;


namespace Enttec.OpenDMX
{
    public class Fixture
    {
        private int mvarInitialAddress = 1;
        public int InitialAddress { get { return mvarInitialAddress; } set { mvarInitialAddress = value; } }

        private Channel.ChannelCollection mvarChannels = null;
        public Channel.ChannelCollection Channels { get { return mvarChannels; } }

        private Interface mvarInterface = null;
        public Fixture(Interface intf, int initialAddress)
        {
            mvarInterface = intf;
            mvarChannels = new Channel.ChannelCollection(this);
            mvarInitialAddress = initialAddress;
        }

        internal void SetChannelValue(int relativeAddress, byte value)
        {
            mvarInterface.SetChannelValue(mvarInitialAddress, relativeAddress, value);
        }

        public void Reset()
        {
            mvarInterface.Reset(mvarInitialAddress, 13);
        }
    }

}