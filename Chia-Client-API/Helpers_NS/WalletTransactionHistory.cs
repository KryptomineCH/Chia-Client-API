using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Collections.Concurrent;

namespace Chia_Client_API.Helpers_NS
{
    public class WalletTransactionHistory
    {
        public WalletTransactionHistory(Wallet_RPC_Client client, ulong fingerprint ,DirectoryInfo storageDirectory)
        {
            Client = client;
            StorageDirectory = storageDirectory;
            if (!StorageDirectory.Exists) StorageDirectory.Create();
        }
        private Wallet_RPC_Client Client;
        /// <summary>
        /// the directory where to store the history Data
        /// </summary>
        public DirectoryInfo StorageDirectory { get; private set; }
        /// <summary>
        /// The amount of chunk files to hold in cache
        /// </summary>
        public int MaxCacheSize { get; set; } = 8;
        /// <summary>
        /// The Fingerprint which to pull data from
        /// </summary>
        public ulong FingerPrint { get; set; }
        private ConcurrentDictionary<ulong, (TransactionsChunk Chunk, DateTime LastAccessTime)> _cache = new ConcurrentDictionary<ulong, (TransactionsChunk, DateTime)>();
        
        /// <summary>
        /// Fetches the Chunkfile of a given Block. If it doesnt exist in cache or disk yet, creates a new Chunk
        /// </summary>
        /// <param name="blockHeight">the chunk id is beeing automatically calculated from it</param>
        /// <returns></returns>
        public TransactionsChunk GetOrCreateChunk(ulong blockHeight)
        {
            ulong chunkID = CalculateChunkID(blockHeight);
            // First, try to get the value from the cache
            if (!_cache.TryGetValue(chunkID, out var cachedValue))
            {
                // If not in cache, try loading from disk
                FileInfo file = new FileInfo(Path.Combine(StorageDirectory.FullName, chunkID.ToString()));
                TransactionsChunk chunk;
                if (TransactionsChunk.FileExists(file))
                {
                    chunk = TransactionsChunk.LoadObjectFromFile(file);
                }
                else
                {
                    // If file does not exist on disk, create new
                    chunk = new TransactionsChunk { StartBlock = chunkID };
                }

                // Add or update the cache with the new bundle and current time
                cachedValue = (chunk, DateTime.UtcNow);
                _cache[chunkID] = cachedValue;
            }
            else
            {
                // Update the LastAccessTime for the existing item
                cachedValue.LastAccessTime = DateTime.UtcNow;
                _cache[chunkID] = cachedValue;
            }

            // Evict items if cache is too large
            EvictIfNecessary();

            return cachedValue.Chunk;
        }

        /// <summary>
        /// checks if the cache bocomes too large and if so, removes the element which has been accessed the longest time ago
        /// </summary>
        private void EvictIfNecessary()
        {
            if (_cache.Count > MaxCacheSize)
            {
                var oldest = _cache.OrderBy(pair => pair.Value.LastAccessTime).First().Key;
                RemoveChunk(oldest);
            }
        }

        /// <summary>
        /// Saves a chunk to Disk (no matter if it has been changed or not)
        /// </summary>
        /// <param name="blockHeight">the chunk id is beeing automatically calculated from it</param>
        public void SaveChunk(ulong blockHeight)
        {
            ulong chunkID = CalculateChunkID(blockHeight);
            if (_cache.TryGetValue(chunkID, out (TransactionsChunk Bundle, DateTime LastAccessTime) chunk) && chunk.Bundle.Edited)
            {
                chunk.Bundle.SaveObjectToFile(Path.Combine(StorageDirectory.FullName, chunkID.ToString()));
                chunk.Bundle.Edited = false; // Reset the edited flag after saving
            }
        }

        /// <summary>
        /// removes a transaction chunk from cache.<br/>
        /// is primairly used to free up cache space (automatic process).
        /// </summary>
        /// <remarks>
        /// Changes made to the chunkfile will be saved per default.<br/>
        /// IMPORTANT: On changes other than <see cref="TransactionsChunk.AddTransaction(Transaction_DictMemos, bool)"/>,
        /// you need to set the <see cref="TransactionsChunk.Edited"/> flag Manually!
        /// </remarks>
        /// <param name="blockHeight">the chunk id is beeing automatically calculated from it</param>
        /// <param name="saveChanges">save the cache file to Disk if changes were made</param>
        public void RemoveChunk(ulong blockHeight, bool saveChanges = true)
        {
            ulong chunkID = CalculateChunkID(blockHeight);
            if (saveChanges)
            {
                SaveChunk(chunkID);
            }
            
            _cache.TryRemove(chunkID, out _);
        }

        /// <summary>
        /// Calculates the starting block number for the chunk containing the specified block.
        /// </summary>
        /// <param name="blockNumber">The block number to find the chunk for.</param>
        /// <returns>The starting block number of the chunk.</returns>
        public ulong CalculateChunkID(ulong blockNumber)
        {
            return blockNumber - (blockNumber % 1000);
        }

        /// <summary>
        /// adds a transaction to the transaction history
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction_DictMemos transaction)
        {
            if (transaction.confirmed_at_height == null || !(bool)transaction.confirmed)
            {
                return;
            }
            TransactionsChunk chunk = GetOrCreateChunk(transaction.confirmed_at_height.Value);
            chunk.AddTransaction(transaction, false);
        }

        /// <summary>
        /// adds a set of transactions efficiently
        /// </summary>
        /// <param name="transactions"></param>
        public void AddTransactions(Transaction_DictMemos[] transactions)
        {
            ulong lastChunkID =ulong.MaxValue;
            TransactionsChunk chunk = new TransactionsChunk();
            foreach (Transaction_DictMemos transaction in transactions)
            {
                if (transaction.confirmed_at_height == null || !(bool)transaction.confirmed)
                {
                    continue;
                }
                ulong chunkID = CalculateChunkID(transaction.confirmed_at_height.Value);
                if (chunkID != lastChunkID)
                {
                    chunk = GetOrCreateChunk(chunkID);
                }
                chunk.AddTransaction(transaction, false);
            }
        }

        /// <summary>
        /// fetches new transactions from the Blockchain
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task PullNewTransactions()
        {
            FingerPrint_Response? login = await Client.LogIn_Async(FingerPrint);
            if (login == null)
            {
                throw new Exception("login was unsuccessful for unknown reason!");
            }
            if (!(bool)login.success!)
            {
                throw new Exception("Login was unsuccessful: "+ login.error);
            }
            GetWallets_Response? walletsResponse = await Client.GetWallets_Async();
            foreach (Wallets_info wallet in walletsResponse.wallets)
            {
                GetTransactions_RPC rpc = new GetTransactions_RPC(wallet.id, start: 0, end: long.MaxValue, reverse: true);
                GetTransactions_Response transactions = await Client.GetTransactions_Async(rpc);
                foreach (Transaction_DictMemos transaction in transactions.transactions)
                {

                }
            }
        }
    }
}
