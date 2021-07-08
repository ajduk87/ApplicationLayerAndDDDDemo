using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using CommercialApplicationCommand.DomainLayer.Entities.StorageEntities;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;
using System.Collections.Generic;
using System.Data;

namespace CommercialApplicationCommand.DomainLayer.Services.StorageServices
{
    internal interface IStorageService
    {
        IEnumerable<Storage> Select(IDbConnection connection, IDbTransaction transaction = null);
        Storage SelectById(IDbConnection connection, StorageId id, IDbTransaction transaction = null);
        Storage SelectByName(IDbConnection connection, Name name, IDbTransaction transaction = null);

        void Insert(IDbConnection connection, Storage storage, IDbTransaction transaction = null);

        void Update(IDbConnection connection, Storage storage, IDbTransaction transaction = null);

        void Delete(IDbConnection connection, Storage storage, IDbTransaction transaction = null);
    }
}