using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Stratis.Bitcoin.Configuration.Logging;

namespace Stratis.Bitcoin.Features.CoinTracker
{
    class CoinFile
    {
        string _fileName;
        ulong _height;
        List<CoinHolder> _coinHolders;

        public CoinFile()
        {
            _fileName = LoggingConfiguration.DataFolder.BasePath + "coins.dat";
            Load();
        }

        void Load()
        {
            if (File.Exists(_fileName))
            {
                _coinHolders = new List<CoinHolder>();

                FileStream f = File.OpenRead(_fileName);
                BinaryReader br = new BinaryReader(f);

                int version = br.ReadInt32();
                _height = br.ReadUInt64();

                ulong entries = br.ReadUInt64();
                for (ulong i = 0; i < entries; i++)
                {
                    CoinHolder ch = new CoinHolder(br);
                    _coinHolders.Add(ch);
                }

                br.Close();
            }
        }

        public ulong Height { get { return _height; } }

    }
}
