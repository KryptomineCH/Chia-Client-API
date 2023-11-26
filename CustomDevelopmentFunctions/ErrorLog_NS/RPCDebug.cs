using CHIA_RPC.ErrorInterface_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Objects_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomDevelopmentFunctions.ErrorLog_NS
{
    public class RPCDebug
    {
        [Fact]
        public async Task TestLoadError()
        {
            // set this variable
            string fileName = "1700042585529-Transaction_DictMemos-GetPrimaryCoins()";

            // fixed logic
            DirectoryInfo baseDir = new DirectoryInfo(Path.Combine("ErrorLogs","Rpc"));
            FileInfo file = new FileInfo(Path.Combine(baseDir.FullName, fileName));
            string content = File.ReadAllText(file.FullName);
            /// error object
            Error rpcError = JsonSerializer.Deserialize<Error>(content);

            // custom test logic
            ActionResult<Transaction_NoMemo> transaction = Transaction_DictMemos.LoadObjectFromString(rpcError.ObjectJson);
            transaction.Data.GetPrimaryCoins();
        }
    }
}
