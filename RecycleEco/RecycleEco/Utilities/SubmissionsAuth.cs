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
    class SubmissionAuth
    {
        static readonly FirebaseClient Firebase = new FirebaseClient("https://ecorecycle-65d2d.firebaseio.com/");
        public static async Task<ObservableCollection<Submission>> GetAllSubmissions()
        {
            try
            {
                List<Submission> submissions = (await Firebase
                   .Child("Submission")
                   .OnceAsync<Submission>()).Select(item => new Submission
                   {
                       SubmissionID = item.Object.SubmissionID,
                        Weight = item.Object.Weight,
                        Date = item.Object.Date,
                        Status = item.Object.Status,
                        Points = item.Object.Points,
                        RecyclerList = item.Object.RecyclerList
                    }).ToList();
                ObservableCollection<Submission> materialsList = new ObservableCollection<Submission>();
                foreach (Submission submission in submissions)
                {
                    materialsList.Add(submission);
                }
                return materialsList;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception RDA1", ex.Message, "OK");
                return null;
            }
        }

        public static async Task<ObservableCollection<Submission>> GetSubmissionsById(List<string> submissionCollection)
        {
            try
            {
                var submissions = await GetAllSubmissions();

                ObservableCollection<Submission> submissionsList = new ObservableCollection<Submission>();
                if (submissions != null)
                {
                    foreach (Submission submission in submissions)
                    {
                        if (submissionCollection == null || !submissionCollection.Contains(submission.SubmissionID))
                            submissionsList.Add(submission);
                    }
                    return submissionsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception MDA2", ex.Message, "OK");
                return null;
            }
        }

        public static async Task AddSubmissions(Submission submission)
        {
            try
            {
                if (submission != null)
                {
                    await Firebase.Child("Submissions").PostAsync(submission);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception MDA3", ex.Message, "OK");
            }
        }

        public static async Task<Submission> GetSubmissionByID(string id)
        {
            try
            {
                var allSubmissions = await GetAllSubmissions();
                if (allSubmissions != null)
                {
                    return allSubmissions.Where(a => a.SubmissionID == id).FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception MDA6", ex.Message, "OK");
                return null;
            }
        }
    }
}