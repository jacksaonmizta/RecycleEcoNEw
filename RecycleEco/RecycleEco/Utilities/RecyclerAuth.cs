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
    class RecyclerAuth
    {
        static readonly FirebaseClient Firebase = new FirebaseClient("https://ecorecycle-65d2d.firebaseio.com/");
        public static async Task<List<Recycler>> GetAllRecycler()
        {
            try
            {
                return (await Firebase
                    .Child("Users/Recyclers")
                    .OnceAsync<Recycler>()).Select(item => new Recycler
                    {
                        Username = item.Object.Username,
                        Password = item.Object.Password,
                        FullName = item.Object.FullName,
                        Points = item.Object.Points,
                        EcoLevel = item.Object.EcoLevel
                    }).ToList();
            }
            catch(Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception RDA1", a.Message, "OK");
                return null;
            }
        }

        public static async Task<Recycler> GetRecycler(User user)
        {

            try
            {
                var allCollectors = await GetAllRecycler();
                if (allCollectors != null)
                {
                    return allCollectors.Where(a => a.Username == user.Username).FirstOrDefault();
                }
                return null;
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception RDA2", a.Message, "Ok");
                return null;
            }
        }

        public static async Task AddRecycler(Recycler recycler)
        {
            try
            {
                if (recycler != null)
                {
                    await Firebase.Child("Users/Recyclers").PatchAsync(recycler);
                }
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception RDA3", a.Message, "Ok");

            }
        }

        public static async Task UpdateRecycler(Recycler recycler)
        {
            try
            {
                if (recycler != null)
                {
                    var toUpdateCollector = (await Firebase.Child("Users/Recyclers")
                        .OnceAsync<Collector>()).Where(a => a.Object.Username == recycler.Username).FirstOrDefault();
                    await Firebase.Child("Users/Recyclers").Child(toUpdateCollector.Key).PutAsync(recycler);
                }
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception RDF4", a.Message, "Ok");

            }
        }

        public static async Task<Recycler> GetRecyclerbyUsername(string username)
        {
            try
            {
                var allRecycler = await GetAllRecycler();
                if (allRecycler != null)
                {
                    return allRecycler.Where(a => a.Username == username).FirstOrDefault();
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
