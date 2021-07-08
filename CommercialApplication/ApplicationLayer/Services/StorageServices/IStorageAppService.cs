using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using System.Collections.Generic;

namespace CommercialApplicationCommand.ApplicationLayer.Services.StorageServices
{
    internal interface IStorageAppService
    {
        IEnumerable<StorageDto> GetAll();
        StorageDto GetById(int id);
        StorageDto Get(string name);
        void CreateNewStorage(StorageDto storageDto);

        void UpdateExistingStorage(StorageDto storageDto);

        void DeleteExistingStorage(StorageDto storageDto);

        void AddProductInStorage(ProductStorageDto productStorageDto);

        void RemoveProductFromStorage(ProductStorageDto productStorageDto);
        IEnumerable<ProductStorageDto> GetContent(string name);
    }
}