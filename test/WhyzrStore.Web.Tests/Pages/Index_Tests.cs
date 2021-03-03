using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace WhyzrStore.Pages
{
    public class Index_Tests : WhyzrStoreWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
