using Autofac;
using CommercialApplication.ApplicationLayer.Models.ProductStorage;
using CommercialApplication.ApplicationLayer.Models.Storage;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Storage;
using CommercialApplicationCommand.ApplicationLayer.Models.ProductStorage;
using CommercialApplicationCommand.ApplicationLayer.Models.Storage;
using CommercialApplicationCommand.ApplicationLayer.Services.StorageServices;
using CommercialApplicationCommand.ApplicationLayer.Validation;
using CommercialApplicationCommand.DomainLayer.Entities.ProductEntities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercialApplicationCommand.ApplicationLayer.Controllers
{
    public class StorageController : BaseController
    {
        private readonly IStorageAppService storageAppService;

        public StorageController()
        {
            this.storageAppService = this.registrationAppServices.Instance.Container.Resolve<IStorageAppService>();
        }

        [HttpGet]
        [Route("api/storage")]
        public IEnumerable<StorageViewModel> GetAll()
        {
            IEnumerable<StorageDto> storageDtoes = storageAppService.GetAll();
            IEnumerable<StorageViewModel> storageViewModels = this.mapper.Map<IEnumerable<StorageViewModel>>(storageDtoes);
            return storageViewModels;
        }

        [HttpGet]
        [Route("api/storagebyid/{id}")]
        public StorageViewModel GetById(int id)
        {
            StorageDto storageDto = storageAppService.GetById(id);
            StorageViewModel storageViewModel = this.mapper.Map<StorageViewModel>(storageDto);
            return storageViewModel;
        }

        [HttpGet]
        [Route("api/storage/{name}")]
        public StorageViewModel Get(string name)
        {
            StorageDto storageDto = storageAppService.Get(name);
            StorageViewModel storageViewModel = this.mapper.Map<StorageViewModel>(storageDto);
            return storageViewModel;
        }

        [HttpPost]
        [Route("api/storage")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Insert(StorageCreateModel storageCreateModel)
        {
            StorageDto storageDto = this.mapper.Map<StorageCreateModel, StorageDto>(storageCreateModel);
            this.storageAppService.CreateNewStorage(storageDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/storage")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Update(StorageUpdateModel storageUpdateModel)
        {
            StorageDto storageDto = this.mapper.Map<StorageUpdateModel, StorageDto>(storageUpdateModel);
            this.storageAppService.UpdateExistingStorage(storageDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/storage")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Delete(StorageDeleteModel storageDeleteModel)
        {
            StorageDto storageDto = this.mapper.Map<StorageDeleteModel, StorageDto>(storageDeleteModel);
            this.storageAppService.DeleteExistingStorage(storageDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/storage/content/{name}")]
        public IEnumerable<ProductStorageViewModel> GetStorageContent(string name)
        {
            IEnumerable<ProductStorageDto> storageItemDtoes = this.storageAppService.GetContent(name);
            IEnumerable<ProductStorageViewModel> storageItemViewModels = this.mapper.Map<IEnumerable<ProductStorageViewModel>>(storageItemDtoes);

            return storageItemViewModels;
        }

        [HttpPost]
        [Route("api/addproduct/storage")]
        [ValidateModelStateFilter]
        public HttpResponseMessage AddProductToStorage(ProductStorageCreateModel productStorageCreateModel)
        {
            ProductStorageDto productStorageDto = this.mapper.Map<ProductStorageCreateModel, ProductStorageDto>(productStorageCreateModel);
            this.storageAppService.AddProductInStorage(productStorageDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/deleteproduct/storage")]
        [ValidateModelStateFilter]
        public HttpResponseMessage DeleteProductFromStorage(ProductStorageDeleteModel productStorageDeleteModel)
        {
            ProductStorageDto productStorageDto = this.mapper.Map<ProductStorageDeleteModel, ProductStorageDto>(productStorageDeleteModel);
            storageAppService.RemoveProductFromStorage(productStorageDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}