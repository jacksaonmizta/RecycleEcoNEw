using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecycleEco.Model
{
    class Submission
    {
        public string SubmissionID { get; set; }
        public string Weight { get; set; }
        public DatePicker Date { get; set; }

        public string SubmittedDate { get; set; }

        public string Status { get; set; }
        public int Points { get; set; }
        public string Username { get; set; }
        public List<string> RecyclerList { get; set; }
    }
}
