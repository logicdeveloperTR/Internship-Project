using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EmployeeTracker.Pages;

public class Index_Tests : EmployeeTrackerWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
