using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NBitcoin;
using NBitcoin.Protocol;
using Stratis.Bitcoin.BlockPulling;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Builder.Feature;
using Stratis.Bitcoin.Configuration;
using Stratis.Bitcoin.Configuration.Logging;
using Stratis.Bitcoin.Connection;
using Stratis.Bitcoin.Interfaces;
using Stratis.Bitcoin.Utilities;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using Stratis.Bitcoin.Features.BlockStore;

namespace Stratis.Bitcoin.Features.CoinTracker
{
    public class CoinTrackerFeature : FullNodeFeature
    {
        CoinTrackerLoop _loop;
        IBlockRepository _blockRepository;

        public CoinTrackerFeature(/*IBlockRepository blockRepository*/)
        {
            //_blockRepository = blockRepository;
        }

        public override void Start()
        {
            if (_loop != null)
                _loop.Stop();

            _loop = new CoinTrackerLoop();
        }

        public override void Stop()
        {
            base.Stop();

            if (_loop != null)
                _loop.Stop();
        }
    }

    class Startup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
        }
    }

    /// <summary>
    /// A class providing extension methods for <see cref="IFullNodeBuilder"/>.
    /// </summary>
    public static partial class IFullNodeBuilderExtensions
    {
        public static IFullNodeBuilder UseCoinTracker(this IFullNodeBuilder fullNodeBuilder)
        {
            LoggingConfiguration.RegisterFeatureNamespace<CoinTrackerFeature>("ct");

            fullNodeBuilder.ConfigureFeature(features =>
            {
                features
                .AddFeature<CoinTrackerFeature>();/*
                .FeatureServices(services =>
                {
                    services.AddSingleton<IBlockRepository, BlockRepository>();
                });*/
            });

            return fullNodeBuilder;
        }
    }
}
