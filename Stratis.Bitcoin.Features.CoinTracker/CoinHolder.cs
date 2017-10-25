using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Stratis.Bitcoin.Features.CoinTracker
{
    class CoinHolder
    {
        double _coins;
        byte[] _address;

        public CoinHolder(BinaryReader br)
        {
            ushort btc = br.ReadUInt16();
            uint satoshi = br.ReadUInt32();
            _address = br.ReadBytes(34);

            _coins = (satoshi * (100000000)) + btc;
        }
    }
}
