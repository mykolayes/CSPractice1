using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
{
    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            //AddCommand = new AddAgeCommand(this);
            CurrentAge = DateTime.Today;
        }

        //class AddAgeCommand : ICommand
        //{
        //    Model parent;

        //    public AddAgeCommand(Model parent)
        //    {
        //        this.parent = parent;
        //        parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        //    }

        //    public event EventHandler CanExecuteChanged;

        //    public bool CanExecute(object parameter) { return !string.IsNullOrEmpty(parent.CurrentDate); }

        //    public void Execute(object parameter)
        //    {
        //        //parent.AddedNames.Add(parent.CurrentDate); ;
        //        //parent.CurrentAge = DateTime.Today;
        //    }
        //}

        //public ICommand AddCommand { get; private set; }

        #region CurrentDate

        public string CurrentDate
        {
            get { return _mCurrentDate; }
            set
            {
                if (value.Equals(_mCurrentDate))
                    return;

                _mCurrentDate = value;
                OnPropertyChanged(nameof(CurrentDate));

                //CurrentAge = CalcAge(value);

            }
        }

        string _mCurrentDate;

        #endregion

        //#region CurrentAgeDefault
        //public DateTime CurrentAgeDefault
        //{
        //    get { return _mCurrentAgeDefault; }
        //    set
        //    {
        //        //if (value == _mCurrentAge)
        //        //    return;
        //            _mCurrentAgeDefault = value;
        //            OnPropertyChanged(nameof(CurrentAgeDefault));
        //    }
        //}
        //DateTime _mCurrentAgeDefault;
        //#endregion

        #region CurrentAge

        public DateTime CurrentAge
        {
            get { return _mCurrentAge; }
            set
            {

                string age = CalcAge(value);
                if (!(value.Equals(DateTime.Today)))
                {
                    if (age.Equals("error"))
                    {
                        MessageBox.Show(
                            "This program works only for people aged 1-135. Please, re-enter your birth date.");
                        CurrentAgeStr = "";
                    }
                    else
                    {
                        CurrentAgeStr = age;
                        //_mCurrentAge = value;
                        //OnPropertyChanged(nameof(CurrentAge));
                    }
                }

                _mCurrentAge = value;
                OnPropertyChanged(nameof(CurrentAge));


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
                //if (value == _mCurrentAge)
                //    return;
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

        //public ObservableCollection<string> AddedNames { get; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //additional functions
        private string CalcAge(DateTime enteredDateTime)
        {
            string res;

            int year = DateTime.Now.Year - enteredDateTime.Year;

            if (year > 135 || year < 1)
            {
                res = "error";
            }
            else
            {
                if (DateTime.Now.Month.Equals(enteredDateTime.Month) &&
                    DateTime.Now.Day.Equals(enteredDateTime.Day))
                {
                    BdNote = "Happy bd!";
                }
                else
                {
                    BdNote = "";
                }

                ChZod = CalcChZod(enteredDateTime);
                EuZod = CalcEuZod(enteredDateTime);

                res = (year).ToString();
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