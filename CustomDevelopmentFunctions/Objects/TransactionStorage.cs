using CHIA_RPC.Objects_NS;

namespace TransactionTypeTests.Objects
{
    internal class TransactionStorage
    {
        private static DirectoryInfo BaseDir = new DirectoryInfo("Transactions");
        static TransactionStorage()
        {
            if (!BaseDir.Exists)
                BaseDir.Create();
        }

        internal static void StoreTransactions(Transaction_DictMemos[] transactionsToStore, string description)
        {
            string subPath = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_" + description.Replace(" ", "");
            DirectoryInfo subDir = new DirectoryInfo(Path.Join(BaseDir.FullName, subPath));
            subDir.Create();

            for (int i = 0; i < transactionsToStore.Length; i++)
            {
                string numeratedFilename = "description" + $"_{i}";
                transactionsToStore[i].SaveObjectToFile(Path.Combine(subDir.FullName, numeratedFilename));
            }
        }
    }
}
