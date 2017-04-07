using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using System.Reflection;
using System.Collections;

namespace AppSale
{
    class CRUDManager
    {
        static CRUDManager defaultInstance = new CRUDManager();
        MobileServiceClient client;
        IMobileServiceTable<Favourites> favouritesTable;
        IMobileServiceTable<Regions> regionsTable;
        Favourites MyFavouriteItem;

        private CRUDManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            this.favouritesTable = client.GetTable<Favourites>();
            this.regionsTable = client.GetTable<Regions>();
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }
    }
}
