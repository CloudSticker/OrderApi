namespace OrdersApi.BusinessLayer.Utils;

public interface IDbConnectionFactory
{
    DatabaseWrapper CreateDatabase(CancellationToken? cancellationToken = default);
}
