using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.Datalayer_NS.DatalayerObjects_NS;
using CHIA_RPC.General_NS;
using System.Linq;
using Xunit;
using static CHIA_RPC.Datalayer_NS.DatalayerObjects_NS.DataStoreChange;

namespace CHIA_API_Tests.Datalayer_NS
{
    [Collection("Testnet_Datalayer_Wallet")]
    public class Datalayer_Subscription_Tests
    {
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetSubscriptions_Test()
        {
            Subscriptions_Response response = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            Assert.True(response.store_ids.Length > 0, "No subscriptions found!");
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void SubscribeUnsubscribe_Test()
        {
            string datastoreToSubscribeTo = "8f6ed792bbbf5216f8e55064793f74ce01286b9c1d542cc4a357cf7f8712df1d";
            Subscribe_RPC subscribeRPC = new Subscribe_RPC(datastoreToSubscribeTo);
            // make sure, we are not subscribed before
            Subscriptions_Response subscriptionsBefore = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(subscriptionsBefore.success, subscriptionsBefore.error);
            if (subscriptionsBefore.store_ids.Contains(datastoreToSubscribeTo))
            {
                Success_Response unsubscribe = Testnet_Datalayer.Datalayer_Client.Unsubscribe_Sync(subscribeRPC);
                Assert.True(unsubscribe.success, unsubscribe.error);
            }
            // subscribe
            Success_Response subscribeResponse = Testnet_Datalayer.Datalayer_Client.Subscribe_Sync(subscribeRPC);
            Assert.True(subscribeResponse.success, subscribeResponse.error);
            // test if subscription was successful
            Subscriptions_Response subscribed = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(subscribed.success, subscribed.error);
            Assert.Contains(datastoreToSubscribeTo, subscribed.store_ids);
            // unsubscribe
            Success_Response unsubscribeResponse = Testnet_Datalayer.Datalayer_Client.Unsubscribe_Sync(subscribeRPC);
            Assert.True(unsubscribeResponse.success, unsubscribeResponse.error);
            // test if unsubscribed
            Subscriptions_Response subscriptionsAfter = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(subscriptionsAfter.success, subscriptionsAfter.error);
            Assert.DoesNotContain(datastoreToSubscribeTo, subscriptionsAfter.store_ids);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void RemoveURLFromSubscription_Test()
        {
            string datastoreToSubscribeTo = "8f6ed792bbbf5216f8e55064793f74ce01286b9c1d542cc4a357cf7f8712df1d";
            Subscribe_RPC subscribeRPC = new Subscribe_RPC(datastoreToSubscribeTo, new string[] { "http://www.example.com:8575", "http://www.example2.com:8575" });
            // make sure, we are not subscribed before
            Subscriptions_Response subscriptionsBefore = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(subscriptionsBefore.success, subscriptionsBefore.error);
            if (subscriptionsBefore.store_ids.Contains(datastoreToSubscribeTo))
            {
                Success_Response unsubscribe = Testnet_Datalayer.Datalayer_Client.Unsubscribe_Sync(subscribeRPC);
                Assert.True(unsubscribe.success, unsubscribe.error);
            }
            // subscribe
            Success_Response subscribeResponse = Testnet_Datalayer.Datalayer_Client.Subscribe_Sync(subscribeRPC);
            Assert.True(subscribeResponse.success, subscribeResponse.error);
            // test if subscription was successful
            Subscriptions_Response subscribed = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(subscribed.success, subscribed.error);
            Assert.Contains(datastoreToSubscribeTo, subscribed.store_ids);
            // remove url
            Subscribe_RPC removeURL = new Subscribe_RPC(datastoreToSubscribeTo, new string[] { "http://www.example.com:8575" });
            Success_Response removeURLResponse = Testnet_Datalayer.Datalayer_Client.RemoveSubscriptions_Sync(removeURL);
            Assert.True(removeURLResponse.success, removeURLResponse.error);
            // test if successful
            GetMirrors_Response subscriptionMirrors = Testnet_Datalayer.Datalayer_Client.GetMirrors_Sync(removeURL);
            Assert.DoesNotContain("http://www.example.com:8575", subscriptionMirrors.mirrors[0].urls);
            Assert.Contains("http://www.example2.com:8575", subscriptionMirrors.mirrors[0].urls);
            // unsubscribe
            Success_Response unsubscribeResponse = Testnet_Datalayer.Datalayer_Client.Unsubscribe_Sync(subscribeRPC);
            Assert.True(unsubscribeResponse.success, unsubscribeResponse.error);
            // test if unsubscribed
            Subscriptions_Response subscriptionsAfter = Testnet_Datalayer.Datalayer_Client.Subscriptions_Sync();
            Assert.True(subscriptionsAfter.success, subscriptionsAfter.error);
            Assert.DoesNotContain(datastoreToSubscribeTo, subscriptionsAfter.store_ids);
            { }
        }
    }
}
