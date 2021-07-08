using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using CommercialApplicationCommand.DomainLayer.Entities.StorageEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using CommercialApplicationCommand.DomainLayer.Repositories.StorageRepositories;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.StorageServices
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository storageRepository = new StorageRepository();

        public void Delete(IDbConnection connection, Storage storage, IDbTransaction transaction = null) =>
            this.storageRepository.Delete(connection, storage);

        public void Insert(IDbConnection connection, Storage storage, IDbTransaction transaction = null) =>
            this.storageRepository.Insert(connection, storage);

        public IEnumerable<Storage> Select(IDbConnection connection, IDbTransaction transaction = null)
        {
            return this.storageRepository.Select(connection);
        }

        public Storage SelectById(IDbConnection connection, StorageId id, IDbTransaction transaction = null)
        {
            return this.storageRepository.SelectById(connection, id);
        }

        public Storage SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null)
        {
            return this.storageRepository.SelectByName(connection, name);
        }

        public void Update(IDbConnection connection, Storage storage, IDbTransaction transaction = null) =>
            this.storageRepository.Update(connection, storage);
    }
}