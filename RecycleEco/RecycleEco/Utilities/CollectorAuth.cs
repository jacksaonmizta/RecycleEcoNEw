using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System;
using Xamarin.Forms;
using RecycleEco.Model;

namespace RecycleEco.Utilities
{
    class CollectorAuth
    {
        static readonly FirebaseClient Firebase = new FirebaseClient("https://ecorecycle-65d2d.firebaseio.com/");

        public static async Task<List<Collector>> GetAllCollector()
        {
            try
            {
                return (await Firebase
                    .Child("Users/Collectors")
                    .OnceAsync<Collector>()).Select(item => new Collector
                    {
                        Username = item.Object.Username,
                        Password = item.Object.Password,
                        FullName = item.Object.FullName,
                        Points = item.Object.Points,
                        Address = item.Object.Address,
                        MaterialCollection = item.Object.MaterialCollection
                    }).ToList();
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception CDA1", a.Message, "ok");
                return null;
            }
        }

        public static async Task<Collector> GetCollector (User user)
        {

            try
            {
                var allCollectors = await GetAllCollector();
                if (allCollectors !=null)
                {
                    return allCollectors.Where(a => a.Username == user.Username).FirstOrDefault();
                }
                return null;
            }
            catch(Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception CDA2", a.Message, "Ok");
                return null;
            }
        }

        public static async Task<ObservableCollection<Collector>> GetCollecterByUsername(List<string> collectors)
        {
            try
            {
                var allCollectors = await GetAllCollector();
                ObservableCollection<Collector> colletorList = new ObservableCollection<Collector>();
                if(allCollectors !=null)
                {
                    foreach (Collector collector in allCollectors)
                    {
                        if (collectors.Contains(collector.Username))
                            colletorList.Add(collector);
                    }
                    return colletorList;
                }
                return null;
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception CDA3", a.Message, "Ok");
                return null;
            }
        }

        public static async Task AddCollector(Collector collector)
        {
            try
            {
                if(collector != null)
                {
                    await Firebase.Child("Users/Collectors").PatchAsync(collector);
                }
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception CDA4", a.Message, "Ok");
                
            }
        }

        public static async Task UpdateCollector (Collector collector)
        {
            try
            {
                if(collector !=null)
                {
                    var toUpdateCollector = (await Firebase.Child("Users/Collectors")
                        .OnceAsync<Collector>()).Where(a => a.Object.Username == collector.Username).FirstOrDefault();
                    await Firebase.Child("Users/Collectors").Child(toUpdateCollector.Key).PutAsync(collector);
                }
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception CDA5", a.Message, "Ok");

            }
        }

        public static async Task<Collector> GetCollectorbyUsername(string username)
        {
            try
            {
                var allCollectors = await GetAllCollector();
                if (allCollectors !=null)
                {
                    return allCollectors.Where(a => a.Username == username).FirstOrDefault();
                }
                return null;
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception CDA6", a.Message, "Ok");
                return null;

            }
        }

            }
        }
   
