using System;
using RecycleEco.Model;
using RecycleEco.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class SubmissionVM : INotifyPropertyChanged
    {
        //create Submission object
        public static Submission Submit{ get; set; }

        //getter and setter
        public string Weight 
        {
            get
            {
              return Submit.Weight  ;
            }
            set
            {
              Submit.Weight=value;
               OnPropertyChanged();
            }

        }

        public int Points
        {
            get
            {
                return Submit.Points;
            }
            set
            {
                Submit.Points = value;
                OnPropertyChanged();
            }

        }

        public string ActualDate
        {
            get
            {
                return Submit.SubmittedDate;
            }
            set
            {
                String myDate = DateTime.Now.ToString();
                Submit.SubmittedDate = myDate;
                OnPropertyChanged();
            }

        }

        //check if record was succesffuly create 
        private bool createSubmit;
        public bool CreateSubmit
        {
            get
            {
                return createSubmit;
            }
            set
            {
                createSubmit = value;
                OnPropertyChanged();
            }
        }

         // check if record was successfully submitted
        private string recordSubmitStatus;
        public string RecordSubmitStatus
        {
            get
            {
                return recordSubmitStatus;
            }
            set
            {
                recordSubmitStatus = value;
                OnPropertyChanged();
            }
        }

        //variables//////////////////////////////////////////////////////////////////////////////
       

        // Interfaces///////////////////////////////////////////////////////////////////////////////
        public ICommand submitCreated { get; set; }

        // Important methods////////////////////////////////////////////////////////////////////////

        private bool CheckFields()
        {
            bool result = !string.IsNullOrWhiteSpace(Submit.SubmissionID)&&
                          !string.IsNullOrWhiteSpace(Submit.Weight) &&
                          !string.IsNullOrWhiteSpace(Submit.Date);
            return result;
        }


        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        //this method is important to save data and later can be used to update if necessary
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
