using System.Threading.Tasks;

namespace MySpot.Tests.Integration.Controllers
{
    public interface IUserControllerTests
    {
        Task post_users_should_return_created_201_status_code();
    }
}