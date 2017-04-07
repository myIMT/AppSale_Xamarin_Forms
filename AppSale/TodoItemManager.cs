/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Reflection;
using System.Collections;
//using Microsoft.WindowsAzure.MobileServices.Sync;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace AppSale
{
    public partial class TodoItemManager
    {
        static TodoItemManager defaultInstance = new TodoItemManager();
        MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<TodoItem> todoTable;
        IMobileServiceTable<Favourites> favouritesTable;
        IMobileServiceTable<Regions> regionsTable;
#else
        IMobileServiceTable<Favourites> favouritesTable;
        IMobileServiceTable<Regions> regionsTable;
        Favourites MyFavouriteItem;
#endif

        const string offlineDbPath = @"localstore.db";

        private TodoItemManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<TodoItem>();
            store.DefineTable<Favourites>();
            store.DefineTable<Regions>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.todoTable = client.GetSyncTable<TodoItem>();
            this.favouritesTable = client.GetSyncTable<Favourites>();
            this.regionsTable = client.GetSyncTable<Regions>();
#else
            this.favouritesTable = client.GetTable<Favourites>();
            this.regionsTable = client.GetTable<Regions>();
#endif
        }

        public static TodoItemManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsOfflineEnabled
        {
            get { return favouritesTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Favourites>; }
        }

        public async Task<ObservableCollection<Favourites>> GetFavouritesAsync()
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                //Task<IEnumerable<Favourites>> favouritesItem = null;
                IEnumerable<Favourites> items = await favouritesTable
                    .Where(todoItem => todoItem.FashionAndBeauty >5)
                    .ToEnumerableAsync();
                //IEnumerable<Favourites> items = await favouritesTable
                //    .Where(favouritesItem.item1> 3)
                //    .ToEnumerableAsync();

                return new ObservableCollection<Favourites>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<Favourites> RecordLookup(string ID)
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                //Task<IEnumerable<Favourites>> favouritesItem = null;
                //                IEnumerable<Favourites> items = await favouritesTable
                Favourites items = await favouritesTable.LookupAsync(ID);
                
                    //                .Where(favouritesItem => favouritesItem.UserId == Settings.UserId)
                    //.ToEnumerableAsync();
                //IEnumerable<Favourites> items = await favouritesTable
                //    .Where(favouritesItem.item1> 3)
                //    .ToEnumerableAsync();
                
                return items;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        //Settings.UserId
        public async Task<ObservableCollection<Favourites>> UserExistAsync()
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                //Task<IEnumerable<Favourites>> favouritesItem = null;
                IEnumerable<Favourites> items = await favouritesTable
                                    .Where(favouritesItem => favouritesItem.UserId == Settings.UserId)
                    .ToEnumerableAsync();
                //IEnumerable<Favourites> items = await favouritesTable
                //    .Where(favouritesItem.item1> 3)
                //    .ToEnumerableAsync();

                return new ObservableCollection<Favourites>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<Favourites> RecordExistAsync()
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                //Task<IEnumerable<Favourites>> favouritesItem = null;
                IEnumerable<Favourites> items = await favouritesTable
                                    .Where(favouritesItem => favouritesItem.UserId == Settings.UserId)
                    .ToEnumerableAsync();
                //IEnumerable<Favourites> items = await favouritesTable
                //    .Where(favouritesItem.item1> 3)
                //    .ToEnumerableAsync();
                return (Favourites)items;
                //return new ObservableCollection<Favourites>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Regions>> RegionUserExistAsync()
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                //Task<IEnumerable<Favourites>> favouritesItem = null;
                IEnumerable<Regions> items = await regionsTable
                                    .Where(regionsItem => regionsItem.UserId == Settings.UserId)
                    .ToEnumerableAsync();
                //IEnumerable<Favourites> items = await favouritesTable
                //    .Where(favouritesItem.item1> 3)
                //    .ToEnumerableAsync();

                return new ObservableCollection<Regions>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Favourites>> GetTodoItemsAsync(bool syncItems = false)
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<Favourites> items = await favouritesTable
                    .Where(todoItem => !todoItem.Done)
                    .ToEnumerableAsync();

                return new ObservableCollection<Favourites>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveTaskAsync(Favourites item)
        {
            //var tempItem = UserExistAsync();
            //ObservableCollection<Favourites> tempItem = await UserExistAsync();
            //if (tempItem != null)
            //{
            //    //item.Id = tempItem.ElementAt<Favourites>(0).Id;
            //    Type type = typeof(Favourites);
            //    IEnumerable props = type.GetRuntimeProperties();
            //    foreach (PropertyInfo prop in props)
            //    {

            //        //if (Int16.Equals(prop.GetValue(),1))//(String.Equals(prop.Name, idPropertyName, StringComparison.OrdinalIgnoreCase))
            //        //{
            //        //    idPropertyValue = prop.GetValue(item).ToString();
            //        //}
            //    }

                #region test
            //    if (item.FashionAndBeauty == 1)
            //    {

            //    }
            //    else if (item.SportsAndOutdoor == 1)
            //    {

            //    }
            //    else if (item.Pets == 1)
            //    {

            //    }
            //    else if (item.Vehicles == 1)
            //    {

            //    }
            //    else if (item.HomeImprovement == 1)
            //    {

            //    }
            //    else if (item.BabiesChildren == 1)
            //    {

            //    }
            //    else if (item.HobbiesInterests == 1)
            //    {

            //    }
            //    else if (item.MobilePhonesAndAccessories == 1)
            //    {

            //    }
            //    else if (item.HomeAppliances == 1)
            //    {

            //    }
            //    else if (item.Gaming == 1)
            //    {

            //    }
            //    else if (item.Books == 1)
            //    {

            //    }
            //    else if (item.Music == 1)
            //    {

            //    }
            //    else if (true)
            //    {

            //    }
            //    //item = tempItem;
            //}
            //else
            //{

            //} 
            #endregion

            if (item.Id == null)
            {
                await favouritesTable.InsertAsync(item);
            }
            else
            {
                await favouritesTable.UpdateAsync(item);
            }
        }

        public async Task SaveFavouriteAsync(Favourites item)
        {
            if (item.Id == null)
            {
                await favouritesTable.InsertAsync(item);
            }
            else
            {
                await favouritesTable.UpdateAsync(item);
            }
        }

        public async Task SaveRegionAsync(Regions item)
        {
            if (item.Id == null)
            {
                await regionsTable.InsertAsync(item);
            }
            else
            {
                await regionsTable.UpdateAsync(item);
            }
        }
#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.todoTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allTodoItems",
                    this.todoTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif
    }
}
