using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Helpers_NS
{
    public class ReportError_Tests
    {
        [Fact]
        public async Task TestUploadFileAsync()
        {
            WalletID_Response response = new WalletID_Response();
            response.success = false;
            response.error = "this is a test error message";
            response.RawContent = "{success: false, error: this is a test error message}";
            // Arrange
            Error testError = new Error(response); // Replace this line as per your Error class definition
            string testFilename = "TestUploadFileAsync";

            // Act
            await ReportError.UploadFileAsync(testError, testFilename);

            // Assert
            // Assuming you have some way to check that the file was actually uploaded
            // For example, you could directly check the SFTP directory,
            // or catch the success response from the SFTP server in your UploadFileAsync method and assert it here.
        }
    }
}
