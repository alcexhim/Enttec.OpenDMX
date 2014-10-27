using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FTD2XX;

namespace Enttec.OpenDMX
{
	public class Interface
	{
		private Device device = null;
		public Interface(uint port)
		{
			device = new Device(port);

			device.Open();
			InitializeOpenDMX();
		}

		private const byte DMX_START_CODE = 0x7E;
		private const byte DMX_END_CODE = 0xE7;
		private const byte OFFSET = 0xFF;
		private const byte BYTE_LENGTH = 8;
		private byte[] APIKEY = new byte[] { 0xAD, 0x88, 0xD0, 0xC8 };

		private const byte ENTTEC_PRO_ENABLE_API2 = 0x0D;
		private const byte SET_PORT_ASSIGNMENT_LABEL = 0xCB;

		private void WritePacket(byte label, byte[] data)
		{
			int length = data.Length;
			byte[] packet = new byte[4 + data.Length + 1];
			packet[0] = DMX_START_CODE;
			packet[1] = label;
			packet[2] = (byte)(length & OFFSET);
			packet[3] = (byte)((length >> BYTE_LENGTH) & OFFSET);

			Array.Copy(data, 0, packet, 4, data.Length);
			packet[packet.Length - 1] = DMX_END_CODE;
			
			int bytesWritten = 0;
			bytesWritten = device.Write(packet);
			// if (bytesWritten != footer.Length) throw new System.IO.IOException();
		}

		private bool mvarIsOpenDMXInitialized = false;
		private void InitializeOpenDMX()
		{
			if (mvarIsOpenDMXInitialized) return;

			device.Reset();
			device.SetBaudRate(12);
			device.SetDataCharacteristics(BitsPerWord.Eight, StopBits.Two, Parity.None);
			device.SetFlowControl(FlowControl.None, 0, 0);
			device.ClearRTS();
			device.Purge();

			WritePacket(ENTTEC_PRO_ENABLE_API2, APIKEY);
			WritePacket(SET_PORT_ASSIGNMENT_LABEL, new byte[] { 1, 1 });
			mvarIsOpenDMXInitialized = true;
		}

		private byte[] buffer = new byte[513];
		public void Reset(int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				buffer[i] = 0;
			}
			WritePacket(6, buffer);
		}

		public void SetChannelValue(int initialAddress, int relativeAddress, byte value)
		{
			if (initialAddress < 1) throw new ArgumentException("must be greater than or equal to 1", "initialAddress");
			if (relativeAddress < 1) throw new ArgumentException("must be greater than or equal to 1", "relativeAddress");

			device.Break = true;
			buffer[initialAddress + relativeAddress - 1] = value;

			WritePacket(6, buffer);
			device.Break = false;
		}
		public void Write(byte[] data)
		{
			if (data.Length > 512) throw new ArgumentException("Data size must be less than or equal to 255 bytes");
			if (data.Length < 512)
			{
				byte[] realdata = new byte[512];
				Array.Copy(data, 0, realdata, 0, data.Length);
				data = realdata;
			}

			device.Break = true;
			buffer = data;
			WritePacket(6, buffer);
			device.Break = false;
		}

		public void Close()
		{
			device.Close();
		}

		public void Reset()
		{
			Reset(0, buffer.Length);
		}
	}
}
