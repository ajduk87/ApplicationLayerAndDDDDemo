using Autofac;
using CommercialApplication.ApplicationLayer.Dtoes.Product;
using CommercialApplication.ApplicationLayer.Models.Product;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Product;
using CommercialApplicationCommand.ApplicationLayer.Models.Product;
using CommercialApplicationCommand.ApplicationLayer.Services.ProductServices;
using CommercialApplicationCommand.ApplicationLayer.Validation;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercialApplicationCommand.ApplicationLayer.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductAppService productAppService;

        public ProductController()
        {
            this.productAppService = this.registrationAppServices.Instance.Container.Resolve<IProductAppService>();
        }

        [HttpGet]
        [Route("api/product")]
        public IEnumerable<ProductViewModel> GetAll()
        {
            IEnumerable<ProductDto> productDtoes = productAppService.GetAll();
            IEnumerable<ProductViewModel> productViewModels = this.mapper.Map<IEnumerable<ProductViewModel>>(productDtoes);
            return productViewModels;
        }

        [HttpGet]
        [Route("api/productbyid/{id}")]
        public ProductViewModel GetById(int id)
        {
            ProductDto productDto = productAppService.GetById(id);
            ProductViewModel customerViewModel = this.mapper.Map<ProductViewModel>(productDto);
            return customerViewModel;
        }

        [HttpGet]
        [Route("api/product/{name}")]
        public ProductViewModel Get(string name)
        {
            ProductDto productDto = productAppService.Get(name);
            ProductViewModel customerViewModel = this.mapper.Map<ProductViewModel>(productDto);
            return customerViewModel;
        }

        [HttpPost]
        [Route("api/product")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Insert(ProductCreateModel productCreateModel)
        {
            ProductDto productDto = this.mapper.Map<ProductCreateModel, ProductDto>(productCreateModel);
            this.productAppService.CreateNewProduct(productDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/product")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Update(ProductUpdateModel productUpdateModel)
        {
            ProductDto productDto = this.mapper.Map<ProductUpdateModel, ProductDto>(productUpdateModel);
            this.productAppService.UpdateExistingProduct(productDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/decrease/unitcost/fruits")]
        [ValidateModelStateFilter]
        public HttpResponseMessage DecreaseUnitcostFruits(DecreaseFruitsUnitCostModel decreaseFruitsUnitCostModel)
        {
            DecreaseFruitsUnitCostDto decreaseFruitsUnitCostDto = this.mapper.Map<DecreaseFruitsUnitCostModel, DecreaseFruitsUnitCostDto>(decreaseFruitsUnitCostModel);
            this.productAppService.DecreaseUnitcostFruits(decreaseFruitsUnitCostDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/decrease/unitcost/vegetables")]
        [ValidateModelStateFilter]
        public HttpResponseMessage DecreaseUnitcostVegetables(DecreaseVegetablesUnitCostModel decreaseVegetablesUnitCostModel)
        {
            DecreaseVegetablesUnitCostDto decreaseVegetablesUnitCostDto = this.mapper.Map<DecreaseVegetablesUnitCostModel, DecreaseVegetablesUnitCostDto>(decreaseVegetablesUnitCostModel);
            this.productAppService.DecreaseUnitcostVegetables(decreaseVegetablesUnitCostDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/setproductstate")]
        [ValidateModelStateFilter]
        public HttpResponseMessage SetState(ProductStateModel productStateModel)
        {
            ProductStateDto productStateDto = this.mapper.Map<ProductStateModel, ProductStateDto>(productStateModel);
            this.productAppService.SetState(productStateDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [ValidateModelStateFilter]
        [Route("api/product")]
        public HttpResponseMessage Delete(ProductDeleteModel productDeleteModel)
        {
            ProductDto productDto = this.mapper.Map<ProductDeleteModel, ProductDto>(productDeleteModel);
            this.productAppService.RemoveExistingProduct(productDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}