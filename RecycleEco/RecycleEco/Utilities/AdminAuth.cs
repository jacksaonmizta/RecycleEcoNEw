using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using System;
using Xamarin.Forms;
using RecycleEco.Model;

namespace RecycleEco.Utilities
{
    class AdminAuth
    {
        static readonly FirebaseClient Firebase = new FirebaseClient("https://ecorecycle-65d2d.firebaseio.com/");

        public static async Task<List<User>> GetAllAdmins()
        {
            try
            {
                return (await Firebase
                    .Child("Admin")
                    .OnceAsync<User>()).Select(item => new User
                    {
                        Username = item.Object.Username,
                        Password = item.Object.Password
                    }).ToList();
            }
            catch (Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception ADA1", a.Message, "OK");
                return null;
            }
        }

        public static async Task<User> GetAdmin(User user)
        {
            try
            {
                var AllAdmin = await GetAllAdmins();
                if (AllAdmin !=null)
                {
                    return AllAdmin.Where(a => a.Username == user.Username).FirstOrDefault();
                }
                return null;
            }
            catch(Exception a)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception ADA2", a.Message, "OK");
                return null;
            }
        }

    }
}
