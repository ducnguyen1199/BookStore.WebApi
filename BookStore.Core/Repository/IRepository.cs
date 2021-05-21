using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IRepository
	{
		Task Commit();
	}
}
