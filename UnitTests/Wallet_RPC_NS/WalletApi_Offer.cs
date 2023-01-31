using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using NFT.Storage.Net.ClientResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Wallet_RPC_NS
{
    public class WalletApi_Offer
    {
        [Fact]
        public void TestOffer()
        {
            Offer_RPC test = new Offer_RPC();
            test.offer.Add("3", 1);
            test.offer.Add("4", -1);
            string json = test.ToString();
            { }
        }
        [Fact]
        public void TestOfferConversion()
        {
            string serverResponse = "{\"offer\": \"offer1qqzh3wcuu2rykcmqvpsxygqqwc7hynr6hum6e0mnf72sn7uvvkpt68eyumkhelprk0adeg42nlelk2mpagr90qq0a37v8lc92dadalrzd9j6k7s6rruxnrj9vl4jc0da47r0x3fhaee6sunhj4uqya8rstlh7h803pz084evpxma3te4dkhdgpx8zj8rfzwfhsad5c5ud6pr7fl7d76n94t5vyq9huaeulufzjtl7l8x40l3xh7v9jelhvunllunn9m72kjcc0h6fu727hdue03lwq68dxhmcpndr9l263mh87m0dsmnwl0rcpv2xmcevunlemdvca9ks8p3cg3qj6tqda6r5er58tj0l6r5eqgfcr5p5nkstadt3jp00haalrsdu62wkexh0ykuwu0nn87sras3y2mww5aln3vkntul79uga0kc4937atvdpuw87tjh2s78fw3pn5fxfjdp54ujxm4jmy567atkls0mp7j548sppyra90h9rwknnd9uem92aw822nmm5fghjluj6wtm6pmzt4p2unpxxaqjscmqjcr5zfn0snkzsqfleqs9hmeu8jlugwtdswgcana7mk7qjntuu24edfvm9jd6rur0avwvjv8uruj9a8ls8ygey5jst32kltaynztfj4y6nx2fjhvln7ve8xuhj229cezun9tx5enxjwfxceukd2w9zhze2wg909vj260erxukv7wx49ykv3j9txn9222xyeafjptfc4a8j6f9c5jlj3vfax5h5wve08v6qyvh3szj5w70at7vjnl8jm4r979f6duvc425ssauv6m3udujhg0ynyxjv8r8lllkljj9wdzhv5t2ve3gv52jt9qhap2fvfdy542224gkuujxv4vhu7n2g9u4jkvw293yv62k24rxdxt34e56aytef2c4zm5eh9cjznjhxnjkpvfp88vmsg946rxzfweq8xnm3w9c6nvvgf9n9qlnrfdyhqkzjfelmu7r7tffhv42tv4j4qc2xv3f5vkrw2d89s2mkr0lpuzv8nqxft5k2964z62wfemxu6j75ft8apaj8ulzlhwy9c5a3dfpxjejedffmumnw0el8zhrr4954z3jjd9ykvhn70386v5n20ec55l4pffdys8w9m8xxp53y4xuct6qux2trq8agtt007qx348nv3umaj6puzm7tvdmus9w70jvtkxddwhpl4e4mfls0exh3sz5ad607gtl5zea65x2u7dy7jq276z9mrea4g6undkfyr8vnz4rv690sjjeyl6amelw4zc7ehet20m7deh9p40d2dnhvm2mhdug9nteaw2ukve0thhuv53y3a8wag0yhngun0w3ey7s9mhe8uatj3mv8ngkwus02tmp6h0a4fe062sxayg0t7ctf6w8ww54dlcm23d6utn7vs6pc4mfhm4f4dqet3txl3wnlsarms65v48ft06wv0mza2y6tmjkdc4su4nnu3vls2elvtq9y3ywf63g6n4zpkz5nd8lwnhd5t0qppekcw2jeslltwk6a3xmdvgdyakfln6chppddaxh7c6kd95lj50kapegdd3dspmjrmwgz6zed0vxu6cprw7xrw7pammgptu8thpt7fr5wpka344v7lltfs4fw62m3mkddlpzccmle7cfnkns3rxlkav47dc7vad0hldyaq54ngx634rlc2clvlcp4a5y4j9wj5mgvpjtwm79kc0e0nttfmvnpd2m99xzkghmh659e2cu7pamps62uptkxzc0jpnzcprqc9csr2ec2eq9vq8ergz2jrsugx8hmll07qhnleyyhp20jy952lxuqsv8sl6254ummndjpt09rwrtdcudlhym2vhvxj7q9vkek6n7mumvec4l9uuu66ndfuqgl3wpg4huahzgmrecpmlaa5k5l057nh505kyyu8jtkcl8de2jdjd6w977wvme9mtlatlssfr6h9jga9ndhmunxdnr6akm24mrhemdc2gh2r50ggx3dl330jlnundx26lkch5zyqqf4rcgxc9u4cfc\", \"success\": true, \"trade_record\": {\"accepted_at_time\": null, \"coins_of_interest\": [{\"amount\": 1, \"parent_coin_info\": \"0xf38cc7c492fdbd9a87d8f557d0e9fcbb09f3fe1c9ed4d65680fb62cf237fb636\", \"puzzle_hash\": \"0xf7e04133429de03ca2f47b65ee9d8fd8b73735ec40017c2f395e3e13f6cd48e9\"}], \"confirmed_at_index\": 0, \"created_at_time\": 1675174129, \"is_my_offer\": true, \"pending\": {\"172eba5b1bc5418088fc39e9d73141d215685b34022928d462b32ea21a9efd2e\": 1}, \"sent\": 0, \"sent_to\": [], \"status\": \"PENDING_ACCEPT\", \"summary\": {\"fees\": 0, \"infos\": {\"172eba5b1bc5418088fc39e9d73141d215685b34022928d462b32ea21a9efd2e\": {\"also\": {\"also\": {\"owner\": \"0x5d68fdaf984877455c1522f65bb3fc16276175473a46a8c17b086e384f23e200\", \"transfer_program\": {\"launcher_id\": \"0x172eba5b1bc5418088fc39e9d73141d215685b34022928d462b32ea21a9efd2e\", \"royalty_address\": \"0x08564184c4730f95d7269bb23d5d80c83de798a1a81d35fab7ac3639f323e1b7\", \"royalty_percentage\": \"190\", \"type\": \"royalty transfer program\"}, \"type\": \"ownership\"}, \"metadata\": \"((117 \\\"https://bafybeibykooilmndr3rcy656fl43n6esxsylpnjtfohm6n3eb62rj5zd24.ipfs.nftstorage.link/\\\") (104 . 0x24b9cd2cfc6f670b1a8a30f92c81c64515140b5c4aa381cf3a126399803408cd) (28021 \\\"https://bafkreiah2bvpox4afdzdzrmchyvogepwvvl2ahujzhiv3m5m27ds2mv73q.ipfs.nftstorage.link/\\\") (27765 \\\"https://bafkreicc3peq64kpclsssu344iroadtsvbloo7ofbkzdyyrqhybhvmblve.ipfs.nftstorage.link/\\\" \\\"https://nft.kryptomine.ch/Kryptomine.ch_NFT_CreativeCommons_Attribution_License.pdf\\\") (29550 . 3) (29556 . 1000) (28008 . 0x07d06af75f8028f23cc5823e2ae311f6ad57a01e89c9d15db3acd7c72d32bfdc) (27752 . 0x42dbc90f714f12e529537ce222e00e72a856e77dc50ab23c62303e027ab02ba9))\", \"type\": \"metadata\", \"updater_hash\": \"0xfe8a4b4e27a2e29a4d3fc7ce9d527adbcaccbab6ada3903ccf3ba9a769d2d78b\"}, \"launcher_id\": \"0x172eba5b1bc5418088fc39e9d73141d215685b34022928d462b32ea21a9efd2e\", \"launcher_ph\": \"0xeff07522495060c066f66f32acc2a77e3a3e737aca8baea4d1a64ea4cdc13da9\", \"type\": \"singleton\"}}, \"offered\": {\"172eba5b1bc5418088fc39e9d73141d215685b34022928d462b32ea21a9efd2e\": 1}, \"requested\": {\"xch\": 250000000000}}, \"taken_offer\": null, \"trade_id\": \"0x96a1735e89c6554c46deb29de879b9eb8e8d96dd50d7198a3c483e2b04791074\"}}";
            OfferFile offer = JsonSerializer.Deserialize<OfferFile>(serverResponse);
            { }
        }
    }
}
