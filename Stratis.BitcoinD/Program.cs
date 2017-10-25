using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Configuration;
using NBitcoin;
using Stratis.Bitcoin.Features.BlockStore;
using Stratis.Bitcoin.Features.Consensus;
using Stratis.Bitcoin.Features.MemoryPool;
using Stratis.Bitcoin.Features.Miner;
using Stratis.Bitcoin.Features.RPC;
using Stratis.Bitcoin.Utilities;
using Stratis.Bitcoin.Features.CoinTracker;

namespace Stratis.BitcoinD
{
    public class Program
    {
        public static void Main(string[] args)
        {

            NodeSettings nodeSettings = NodeSettings.FromArguments(args);

            var node = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings)
                .UseConsensus()
                .UseBlockStore()
                .UseMempool()
                .AddMining()
                .AddRPC()
                .UseCoinTracker()
                .Build();

            node.Run();
        }
    }
}
