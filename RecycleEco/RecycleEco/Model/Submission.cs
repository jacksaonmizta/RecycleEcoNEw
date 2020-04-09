using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecycleEco.Model
{
    class Submission
    {
        public string SubmissionID { get; set; }
        public int Weight { get; set; }
        public DateTime ApprovedDate { get; set; } //collector
        public DateTime SubmittedDate { get; set; } //recycler
        public string Status { get; set; }
        public int Points { get; set; }
      
        public string Collector { get; set; }
        public string Recycler { get; set; }
        public string Material { get; set; }
       
    }
}
