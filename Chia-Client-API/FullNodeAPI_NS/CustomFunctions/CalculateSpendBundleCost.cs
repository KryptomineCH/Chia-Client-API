using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chia_Client_API.FullNodeAPI_NS.CustomFunctions
{
    internal class CalculateSpendBundleCost
    {
        /* https://github.com/mindspice/chia-blockchain/blob/2.x-main-custom-endpoints/chia/rpc/full_node_rpc_api.py#L981
          async def get_spend_bundle_inclusion_cost(self, request: Dict):
        if "spend_bundle" not in request:
            raise ValueError("No spend_bundle in request")
        spend_bundle: SpendBundle = SpendBundle.from_json_dict(request["spend_bundle"])

        sb_cost = 0
        for spend in spend_bundle.coin_spends:
            cost, _ = spend.puzzle_reveal.run_with_cost(INFINITE_COST, spend.solution)
            sb_cost += cost

        # Using sb_cost * 2 to ensure overhead, gives about 5% for a spend bundle of 32 items
        room_in_mempool = False if self.service.mempool_manager.mempool.at_full_capacity(sb_cost * 2) else True
        fee_to_spend = self.service.mempool_manager.mempool.get_min_fee_rate(sb_cost)

        return {
            "cost": sb_cost,
            "room_in_mempool": room_in_mempool,
            "fee_to_spend": fee_to_spend,
            "min_valid_fee": sb_cost * 5,
        }
         */
    }
}
