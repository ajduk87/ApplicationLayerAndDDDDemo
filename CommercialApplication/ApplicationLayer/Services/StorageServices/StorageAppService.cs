using Autofac;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using CommercialApplicationCommand.DomainLayer.Entities.StorageEntities;
using CommercialApplicationCommand.DomainLayer.Services.StorageServices;
using Npgsql;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.DomainLayer.Services.ProductServices;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using System.Collections.Generic;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.ProductStorage;

namespace CommercialApplicationCommand.ApplicationLayer.Services.StorageServices
{
    public class StorageAppService : BaseAppService, IStorageAppService
    {
        private readonly IProductStorageService productStorageService;
        private readonly IStorageService storageService;

        public StorageAppService()
        {
            this.storageService = this.registrationServices.Instance.Container.Resolve<IStorageService>();
            this.productStorageService = this.registrationServices.Instance.Container.Resolve<IProductStorageService>();
        }

        public IEnumerable<StorageDto> GetAll()
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<Storage> storages = this.storageService.Select(connection);
                IEnumerable<StorageDto> storageDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<Storage>, IEnumerable<StorageDto>>(storages);
                return storageDtoes;
            }
        }

        public StorageDto GetById(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Storage storage = this.storageService.SelectById(connection, new StorageId(id));
                StorageDto storageDto = this.dtoToEntityMapper.MapView<Storage, StorageDto>(storage);
                return storageDto;
            }
        }

        public StorageDto Get(string name)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Storage storage = this.storageService.SelectByName(connection, new Name(name));
                StorageDto storageDto = this.dtoToEntityMapper.MapView<Storage, StorageDto>(storage);
                return storageDto;
            }
        }

        public void CreateNewStorage(StorageDto storageDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Storage storage = this.dtoToEntityMapper.Map<StorageDto, Storage>(storageDto);
                this.storageService.Insert(connection, storage);
            }
        }

        public void UpdateExistingStorage(StorageDto storageDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Storage storage = this.dtoToEntityMapper.Map<StorageDto, Storage>(storageDto);
                this.storageService.Update(connection, storage);
            }
        }

        public void DeleteExistingStorage(StorageDto storageDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Storage storage = this.dtoToEntityMapper.Map<StorageDto, Storage>(storageDto);
                this.storageService.Delete(connection, storage);
            }
        }

        public void AddProductInStorage(ProductStorageDto productStorageDto)
        {
            using(NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                ProductStorage productStorage = this.dtoToEntityMapper.Map<ProductStorageDto, ProductStorage>(productStorageDto);
                this.productStorageService.Insert(connection, productStorage);
            }
        }

        public void RemoveProductFromStorage(ProductStorageDto productStorageDto)
        {
            using(NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                ProductStorage productStorage = this.dtoToEntityMapper.Map<ProductStorageDto, ProductStorage>(productStorageDto);
                this.productStorageService.Delete(connection, productStorage);
            }
        }

        public IEnumerable<ProductStorageDto> GetContent(string name)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Storage storage = this.storageService.SelectByName(connection, new Name(name));
                IEnumerable<ProductStorage> storageItems = this.productStorageService.SelectByStorageId(connection, storage.Id);
                IEnumerable<ProductStorageDto> storageItemDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<ProductStorage>, IEnumerable<ProductStorageDto>>(storageItems);
                return storageItemDtoes;
            }
        }
    }
}
