using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace NaUKMA.CS.Practice01
{
    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            CurrentAge = DateTime.Today;
        }

        //properties

        #region CurrentAge

        public DateTime CurrentAge
        {
            get { return _mCurrentAge; }
            set
            {              
                _mCurrentAge = value;
                OnPropertyChanged(nameof(CurrentAge));
                CurrentAgeTask(value);

            }
        }

        DateTime _mCurrentAge;

        #endregion

        #region CurrentAgeStr

        public string CurrentAgeStr
        {
            get { return _mCurrentAgeStr; }
            set
            {
                _mCurrentAgeStr = value;
                OnPropertyChanged(nameof(CurrentAgeStr));
            }
        }

        string _mCurrentAgeStr;

        #endregion

        #region BdNote

        public string BdNote
        {
            get { return _mBdNote; }
            set
            {
                if (value == _mBdNote)
                    return;

                _mBdNote = value;

                OnPropertyChanged(nameof(BdNote));

            }
        }

        string _mBdNote;

        #endregion

        #region ChZod

        public string ChZod
        {
            get { return _mChZod; }
            set
            {

                _mChZod = value;

                OnPropertyChanged(nameof(ChZod));

            }
        }

        string _mChZod;

        #endregion

        #region EuZod

        public string EuZod
        {
            get { return _mEuZod; }
            set
            {

                _mEuZod = value;

                OnPropertyChanged(nameof(EuZod));

            }
        }

        string _mEuZod;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //async tasks for properties
        private async Task CurrentAgeTask(DateTime value)
        {
            int result = DateTime.Compare(value, DateTime.Today);
            if (result > 0)
            {
                MessageBox.Show(
                    "This program works only for people aged 0-135, born yesterday or earlier. Please, re-enter your birth date.");
                CurrentAgeStr = "";
                BdNote = "";
                ChZod = "";
                EuZod = "";
            }
            else if (result == 0)
            {
                //do nothing; required for the first launch.
                CurrentAgeStr = "";
                BdNote = "";
                ChZod = "";
                EuZod = "";
            }
            else
            {
                CurrentAgeStr = CalcAge(value);

                if (DateTime.Now.Month.Equals(value.Month) &&
                    DateTime.Now.Day.Equals(value.Day))
                {
                    BdNote = "Happy bd!";
                }
                else
                {
                    BdNote = "";
                }

                ChZod = CalcChZod(value);
                EuZod = CalcEuZod(value);
            }

        }

        //additional logic functions
        private string CalcAge(DateTime enteredDateTime)
        {
            string res;

            int age = DateTime.Now.Year - enteredDateTime.Year;
            if (enteredDateTime > DateTime.Now.AddYears(-age)) age--;

            if (age > 135 || age < 0)
            {
                res = "error";
            }
            else
            {
                res = (age).ToString();
            }

            return res;
        }

        private string CalcChZod(DateTime enteredDateTime)
        {
            var c = new System.Globalization.ChineseLunisolarCalendar();
            var y = c.GetSexagenaryYear(enteredDateTime);
            var s = c.GetCelestialStem(y) - 1;
            return
                ",Rat,Ox,Tiger,Rabbit,Dragon,Snake,Horse,Goat,Monkey,Rooster,Dog,Pig".Split(',')[
                    c.GetTerrestrialBranch(y)]
                + " - "
                + "Wood,Fire,Earth,Metal,Water".Split(',')[s / 2]
                + " - Y" + (s % 2 > 0 ? "in" : "ang");
        }

        private string CalcEuZod(DateTime enteredDateTime)
        {
                int month = enteredDateTime.Month;
                int day = enteredDateTime.Day;
                switch (month)
                {
                    case 1:
                        if (day <= 19)
                            return "Capricorn";
                        else
                            return "Aquarius";

                    case 2:
                        if (day <= 18)
                            return "Aquarius";
                        else
                            return "Pisces";
                    case 3:
                        if (day <= 20)
                            return "Pisces";
                        else
                            return "Aries";
                    case 4:
                        if (day <= 19)
                            return "Aries";
                        else
                            return "Taurus";
                    case 5:
                        if (day <= 20)
                            return "Taurus";
                        else
                            return "Gemini";
                    case 6:
                        if (day <= 20)
                            return "Gemini";
                        else
                            return "Cancer";
                    case 7:
                        if (day <= 22)
                            return "Cancer";
                        else
                            return "Leo";
                    case 8:
                        if (day <= 22)
                            return "Leo";
                        else
                            return "Virgo";
                    case 9:
                        if (day <= 22)
                            return "Virgo";
                        else
                            return "Libra";
                    case 10:
                        if (day <= 22)
                            return "Libra";
                        else
                            return "Scorpio";
                    case 11:
                        if (day <= 21)
                            return "Scorpio";
                        else
                            return "Sagittarius";
                    case 12:
                        if (day <= 21)
                            return "Sagittarius";
                        else
                            return "Capricorn";
                }
                return "";          
        }
    }
}