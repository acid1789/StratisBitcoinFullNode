using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Stratis.Bitcoin.Configuration;

namespace Stratis.Bitcoin.Features.BlockStore
{
    class CoinFile
    {
        string _fileName;
        int _height;
        Dictionary<string, double> _coinHolders;

        public CoinFile(DataFolder dataFolder)
        {
            _fileName = dataFolder.BasePath + "coins.dat";
            Load();
        }

        void Load()
        {
            if (File.Exists(_fileName))
            {
                _coinHolders = new Dictionary<string, double>();

                FileStream f = File.OpenRead(_fileName);
                BinaryReader br = new BinaryReader(f);

                int version = br.ReadInt32();
                _height = br.ReadInt32();

                ulong entries = br.ReadUInt64();
                for (ulong i = 0; i < entries; i++)
                {
                    ushort btc = br.ReadUInt16();
                    uint satoshi = br.ReadUInt32();
                    string address = br.ReadString();

                    double coins = (satoshi * (100000000)) + btc;
                    if (!_coinHolders.ContainsKey(address))
                        _coinHolders[address] = 0;
                    _coinHolders[address] += coins;
                }

                br.Close();
            }
        }

        public int Height { get { return _height; } }

    }
}
