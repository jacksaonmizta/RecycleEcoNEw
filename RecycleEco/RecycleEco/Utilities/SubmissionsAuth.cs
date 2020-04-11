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
using RecycleEco.ViewModel;

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
                    .Child("Submissions")
                    .OnceAsync<Submission>()).Select(item => new Submission
                    {
                        SubmissionID = item.Object.SubmissionID,
                        SubmittedDate = Convert.ToDateTime(item.Object.SubmittedDate),
                        ApprovedDate = Convert.ToDateTime(item.Object.ApprovedDate),
                        Weight = item.Object.Weight,
                        Points = item.Object.Points,
                        Status = item.Object.Status,
                        Collector = item.Object.Collector,
                        Recycler = item.Object.Recycler,
                        Material = item.Object.Material,
                        MaterialName = item.Object.MaterialName
                    }).ToList();

                ObservableCollection<Submission> submissionsList = new ObservableCollection<Submission>();
                foreach (Submission submission in submissions)
                {
                    submissionsList.Add(submission);
                }
                return submissionsList;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA1", ex.Message, "OK");
                return null;
            }
        }

        public static async Task AddSubmission(Submission submission)
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
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA2", ex.Message, "OK");
            }
        }
        public static async Task UpdateSubmission(Submission submission)
        {
            try
            {
                if (submission != null)
                {
                    var toUpdateSubmission = (await Firebase.Child("Submissions")
                        .OnceAsync<Submission>()).Where(a => a.Object.SubmissionID == submission.SubmissionID).FirstOrDefault();
                    await Firebase.Child("Submissions").Child(toUpdateSubmission.Key).PutAsync(submission);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA3", ex.Message, "OK");
            }
        }

        public static async Task<ObservableCollection<Submission>> GetProposedSubmissionsByCollector(Collector collector)
        {
            try
            {
                var submissions = await GetAllSubmissions();

                ObservableCollection<Submission> submissionsList = new ObservableCollection<Submission>();
                if (submissions != null)
                {
                    foreach (Submission submission in submissions)
                    {
                        if (submission.Collector == collector.Username && submission.Status == SubmissionVM.StatusInitial)
                            submissionsList.Add(submission);
                    }
                    return submissionsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA4", ex.Message, "OK");
                return null;
            }
        }

        public static async Task<ObservableCollection<Submission>> GetSubmissionsByMaterial(Material material)
        {
            try
            {
                var submissions = await GetAllSubmissions();

                ObservableCollection<Submission> submissionsList = new ObservableCollection<Submission>();
                if (submissions != null)
                {
                    foreach (Submission submission in submissions)
                    {
                        if (submission.Material == material.MaterialID)
                            submissionsList.Add(submission);
                    }
                    return submissionsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA5", ex.Message, "OK");
                return null;
            }
        }

        public static async Task<ObservableCollection<Submission>> GetSubmissionsForRecycler(Material material, Recycler recycler)
        {
            try
            {
                var submissions = await GetSubmissionsByMaterial(material);

                ObservableCollection<Submission> submissionsList = new ObservableCollection<Submission>();
                if (submissions != null)
                {
                    foreach (Submission submission in submissions)
                    {
                        if (submission.Recycler == recycler.Username)
                            submissionsList.Add(submission);
                    }
                    return submissionsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA6", ex.Message, "OK");
                return null;
            }
        }

        public static async Task<ObservableCollection<Submission>> GetSubmissionsForCollector(Material material, Collector collector)
        {
            try
            {
                var submissions = await GetSubmissionsByMaterial(material);

                ObservableCollection<Submission> submissionsList = new ObservableCollection<Submission>();
                if (submissions != null)
                {
                    foreach (Submission submission in submissions)
                    {
                        if (submission.Collector == collector.Username)
                            submissionsList.Add(submission);
                    }
                    return submissionsList;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Firebase Exception SDA6", ex.Message, "OK");
                return null;
            }
        }
    }
}