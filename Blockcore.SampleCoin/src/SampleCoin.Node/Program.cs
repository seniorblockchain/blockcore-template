using System;
using System.Threading.Tasks;
using Blockcore.Builder;
using Blockcore.Configuration;
using Blockcore.Features.BlockStore;
using Blockcore.Features.ColdStaking;
using Blockcore.Features.Consensus;
using Blockcore.Features.Diagnostic;
using Blockcore.Features.MemoryPool;
using Blockcore.Features.Miner;
using Blockcore.Features.NodeHost;
using Blockcore.Features.RPC;
using Blockcore.Utilities;

namespace Blockcore.SampleCoin.Daemon
{
   public class Program
   {
      public static async Task Main(string[] args)
      {
         try
         {
            var nodeSettings = new NodeSettings(networksSelector: Networks.Networks.SampleCoin, args: args);

            IFullNodeBuilder nodeBuilder = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings)
                .UseBlockStore()
                .UsePosConsensus()
                .UseMempool()
                .UseColdStakingWallet()
                .AddPowPosMining()
                .UseNodeHost()
                .AddRPC()
                .UseDiagnosticFeature();

            IFullNode node = nodeBuilder.Build();

            if (node != null)
               await node.RunAsync();
         }
         catch (Exception ex)
         {
            Console.WriteLine("There was a problem initializing the node. Details: '{0}'", ex);
         }
      }
   }
}
