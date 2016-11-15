using System.Threading.Tasks;

namespace AppSale
{
	public interface IAuthenticate
	{
		Task<bool> AuthenticateAsync ();

		Task<bool> LogoutAsync ();
	}
}
