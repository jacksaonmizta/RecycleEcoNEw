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
        public static async Task<List<Recycler>> GetAllRecyclers()
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
                        TotalPoints = item.Object.TotalPoints,
                        EcoLevel = item.Object.EcoLevel
                    }).ToList();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Registered Recycler Firebase error", ex.Message, "OK");
                return null;
            }
        }
        public static async Task<Recycler> GetRecycler(User user)
        {
            try
            {
                var allRecyclers = await GetAllRecyclers();
                if (allRecyclers != null)
                {
                    return allRecyclers.Where(a => a.Username == user.Username).FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("GEt Recycler error", ex.Message, "OK");
                return null;
            }
        }
        public static async Task AddRecycler(Recycler recycler)
        {
            try
            {
                if (recycler != null)
                {
                    await Firebase.Child("Users/Recyclers").PostAsync(recycler);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Add Recycler Error", ex.Message, "OK");
            }
        }
        public static async Task UpdateRecycler(Recycler recycler)
        {
            try
            {
                if (recycler != null)
                {
                    var toUpdateRecycler = (await Firebase.Child("Users/Recyclers")
                        .OnceAsync<Recycler>()).Where(a => a.Object.Username == recycler.Username).FirstOrDefault();
                    await Firebase.Child("Users/Recyclers").Child(toUpdateRecycler.Key).PutAsync(recycler);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Update Recycler FIrebase Error", ex.Message, "OK");
            }
        }
        public static async Task<Recycler> GetRecyclerByUsername(string username)
        {
            try
            {
                var allRecyclers = await GetAllRecyclers();
                if (allRecyclers != null)
                {
                    return allRecyclers.Where(a => a.Username == username).FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Get Recycler by username Firebase error", ex.Message, "OK");
                return null;
            }
        }
    }
}