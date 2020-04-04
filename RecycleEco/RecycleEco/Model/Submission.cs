using System;
using System.Collections.Generic;
using System.Text;

namespace RecycleEco.Model
{
    class Submission
    {
        public string SubmissionID { get; set; }
        public string Weight { get; set; }
        public string Date { get; set; }

        public string SubmittedDate { get; set; }

        public string Status { get; set; }
        public int Points { get; set; }
        public string Username { get; set; }
        public List<string> RecyclerList { get; set; }
    }
}
