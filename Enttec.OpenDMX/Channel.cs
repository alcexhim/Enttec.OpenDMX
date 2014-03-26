using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enttec.OpenDMX
{
    public class Channel
    {
        public class ChannelCollection
            : System.Collections.ObjectModel.Collection<Channel>
        {
            private Fixture mvarParent = null;
            public ChannelCollection(Fixture parent)
            {
                mvarParent = parent;
            }

            public Channel Add(string channelName, int relativeAddress, byte minimumValue = 0, byte maximumValue = 255)
            {
                Channel channel = new Channel();
                channel.Name = channelName;
                channel.RelativeAddress = relativeAddress;
                channel.MinimumValue = minimumValue;
                channel.MaximumValue = maximumValue;
                Add(channel);
                return channel;
            }

            protected override void InsertItem(int index, Channel item)
            {
                base.InsertItem(index, item);
                item.Parent = mvarParent;
            }
            protected override void RemoveItem(int index)
            {
                this[index].Parent = null;
                base.RemoveItem(index);
            }
            protected override void SetItem(int index, Channel item)
            {
                base.SetItem(index, item);
                item.Parent = mvarParent;
            }
        }

        private Fixture mvarParent = null;
        public Fixture Parent { get { return mvarParent; } internal set { mvarParent = value; } }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private byte mvarMinimumValue = 0;
        public byte MinimumValue { get { return mvarMinimumValue; } set { mvarMinimumValue = value; } }

        private byte mvarMaximumValue = 255;
        public byte MaximumValue { get { return mvarMaximumValue; } set { mvarMaximumValue = value; } }

        private int mvarRelativeAddress = 1;
        public int RelativeAddress { get { return mvarRelativeAddress; } set { mvarRelativeAddress = value; } }

        private byte mvarValue = 0;
        public byte Value
        {
            get { return mvarValue; }
            set
            {
                try
                {
                    mvarParent.SetChannelValue(mvarRelativeAddress, value);
                    mvarValue = value;
                }
                catch (ArgumentException ex)
                {
                }
            }
        }
    }
}
