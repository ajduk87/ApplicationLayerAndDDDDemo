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
    public interface IStorageService
    {
        void Insert(StorageDto storage);
        void Update(StorageDto storage);
        StorageDto Get(string name);
        ObservableCollection<StorehouseItem> GetState(string name);
    }
}
