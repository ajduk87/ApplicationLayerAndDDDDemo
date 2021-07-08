using CommercialApplication.DomainLayer.Repositories.Sql;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using CommercialApplicationCommand.DomainLayer.Entities.StorageEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommercialApplicationCommand.DomainLayer.Repositories.StorageRepositories
{
    public class StorageRepository : IStorageRepository
    {
        public void Delete(IDbConnection connection, Storage storage, IDbTransaction transaction = null)
        {
            connection.Execute(StorageQueries.Delete, new { id = storage.Id });
        }

        public bool Exists(IDbConnection connection, long id, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<bool>(StorageQueries.Exists, new { id });
        }

        public void Insert(IDbConnection connection, Storage storage, IDbTransaction transaction = null)
        {
            connection.Execute(StorageQueries.Insert, new
            {
                Name = storage.Name.Content,
                Location = storage.Location.Content
            });
        }

        public IEnumerable<Storage> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<Storage>(StorageQueries.Select);
        }

        public Storage SelectById(IDbConnection connection, StorageId id, IDbTransaction transaction = null)
        {
            return connection.Query<Storage>(StorageQueries.SelectById, new { Name = id.Content }).Single();
        }

        public Storage SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null)
        {
            return connection.Query<Storage>(StorageQueries.SelectByName, new { Name = name.Content }).Single();
        }

        public void Update(IDbConnection connection, Storage storage, IDbTransaction transaction = null)
        {
            connection.Execute(StorageQueries.Update, new
            {
                id = storage.Id.Content,
                Name = storage.Name.Content,
                Location = storage.Location.Content
            });
        }
    }
}