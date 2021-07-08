using CommercialClientApplication.DataGridModels;
using CommercialClientApplication.Dtoes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public class StorageService : IStorageService
    {
        public void Insert(StorageDto storage)
        {
        }
        public void Update(StorageDto storage)
        {
        }
        public StorageDto Get(string name)
        {
            return new StorageDto();
        }
        public ObservableCollection<StorehouseItem> GetState(string name)
        {
            return new ObservableCollection<StorehouseItem>();
        }
    }
}
