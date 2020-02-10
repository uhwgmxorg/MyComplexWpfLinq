using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MyComplexWpfLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotify Changed Properties  
        private string message;
        public string Message
        {
            get { return message; }
            set { SetField(ref message, value, nameof(Message)); }
        }

        private ObservableCollection<KeyAndTime> setA;
        public ObservableCollection<KeyAndTime> SetA
        {
            get { return setA; }
            set { SetField(ref setA, value, nameof(SetA)); }
        }

        private ObservableCollection<KeyAndTime> setB;
        public ObservableCollection<KeyAndTime> SetB
        {
            get { return setB; }
            set { SetField(ref setB, value, nameof(SetB)); }
        }

        private ObservableCollection<KeyAndTime> setT;
        public ObservableCollection<KeyAndTime> SetT
        {
            get { return setT; }
            set { SetField(ref setT, value, nameof(SetT)); }
        }

        private ObservableCollection<KeyAndTime> intersectionAB;
        public ObservableCollection<KeyAndTime> IntersectionAB
        {
            get { return intersectionAB; }
            set { SetField(ref intersectionAB, value, nameof(IntersectionAB)); }
        }

        private ObservableCollection<KeyAndTime> unionAB;
        public ObservableCollection<KeyAndTime> UnionAB
        {
            get { return unionAB; }
            set { SetField(ref unionAB, value, nameof(UnionAB)); }
        }

        private ObservableCollection<KeyAndTime> relativeComplementAB;
        public ObservableCollection<KeyAndTime> RelativeComplementAB
        {
            get { return relativeComplementAB; }
            set { SetField(ref relativeComplementAB, value, nameof(RelativeComplementAB)); }
        }

        private bool subsetIndicatorT;
        public bool SubsetIndicatorT
        {
            get { return subsetIndicatorT; }
            set { SetField(ref subsetIndicatorT, value, nameof(SubsetIndicatorT)); }
        }

        private string input;
        public string Input
        {
            get { return input; }
            set { SetField(ref input, value, nameof(Input)); }
        }

        // Template for a new INotify Changed Property
        // for using CTRL-R-R
        private ObservableCollection<string> xxx;
        public ObservableCollection<string> Xxx
        {
            get { return xxx; }
            set { SetField(ref xxx, value, nameof(Xxx)); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            InitSets();
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            AddToSet(Input[0], SetA);
        }

        /// <summary>
        /// Button2_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            AddToSet(Input[0], SetB);
        }

        /// <summary>
        /// Button3_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            AddToSet(Input[0], SetT);
        }

        /// <summary>
        /// Button4_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            // check and indicate if T is subset of A
            SubsetIndicatorT = SetT.All(x => SetA.Contains(x, new KeyAndTimeComparer())); if (SubsetIndicatorT) Console.Beep(); else { Console.Beep(); Console.Beep(); }

            // execute the LINQ commands
            var ResultSet = SetA.Intersect(SetB, new KeyAndTimeComparer());
            IntersectionAB = new ObservableCollection<KeyAndTime>(ResultSet.ToList<KeyAndTime>());

            ResultSet = SetA.Union(SetB, new KeyAndTimeComparer());
            UnionAB = new ObservableCollection<KeyAndTime>(ResultSet.ToList<KeyAndTime>());

            ResultSet = SetA.Except(SetB, new KeyAndTimeComparer());
            RelativeComplementAB = new ObservableCollection<KeyAndTime>(ResultSet.ToList<KeyAndTime>());
        }

        /// <summary>
        /// Button5_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            ClearSets();
        }
        private void ClearSets()
        {
            SetA.Clear();
            SetB.Clear();
            SetT.Clear();
            if (IntersectionAB != null)
                IntersectionAB.Clear();
            if (UnionAB != null)
                UnionAB.Clear();
            if (RelativeComplementAB != null)
                RelativeComplementAB.Clear();
        }

        /// <summary>
        /// Button6_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            ClearSets();
            InitSets();
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// InitSets
        /// </summary>
        private void InitSets()
        {
            SetA = new ObservableCollection<KeyAndTime>();
            SetB = new ObservableCollection<KeyAndTime>();
            SetT = new ObservableCollection<KeyAndTime>();

            SetA.Add(new KeyAndTime(Key.A));
            SetA.Add(new KeyAndTime(Key.B));
            SetA.Add(new KeyAndTime(Key.C));
            SetA.Add(new KeyAndTime(Key.D));
            SetA.Add(new KeyAndTime(Key.E));
            SetA.Add(new KeyAndTime(Key.F));
            SetA.Add(new KeyAndTime(Key.G));

            SetB.Add(new KeyAndTime(Key.D));
            SetB.Add(new KeyAndTime(Key.F));
            SetB.Add(new KeyAndTime(Key.H));
            SetB.Add(new KeyAndTime(Key.J));
            SetB.Add(new KeyAndTime(Key.L));

            SetT.Add(new KeyAndTime(Key.A));
            SetT.Add(new KeyAndTime(Key.B));
            SetT.Add(new KeyAndTime(Key.C));

            Input = "I";

            SubsetIndicatorT = false;
        }

        /// <summary>
        /// AddToSet
        /// </summary>
        /// <param name="c"></param>
        /// <param name="set"></param>
        private void AddToSet(char c, ObservableCollection<KeyAndTime> set)
        {
            if (ConvertTGoKeyAndTime(c) != null && !set.Contains(ConvertTGoKeyAndTime(c), new KeyAndTimeComparer()))
                set.Add(ConvertTGoKeyAndTime(c));
            else
            {
                Console.Beep();
                Console.Beep();
            }
        }

        /// <summary>
        /// ConvertTGoKeyAndTime
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private KeyAndTime ConvertTGoKeyAndTime(char c)
        {
            Key k;
            int n = (int)Char.ToUpper(c) - 21;

            System.Diagnostics.Debug.WriteLine(String.Format("{0} {1}", c, n));

            if (n < 44 || 69 < n)
                return null;

            k = (Key)n;

            return new KeyAndTime(k);
        }

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    /// <summary>
    /// class KeyAndTime
    /// </summary>
    public class KeyAndTime
    {
        public Key EKey { get; set; }
        public DateTime? KeyTime { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        public KeyAndTime(Key key)
        {
            EKey = key;
            KeyTime = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public KeyAndTime(Key key, DateTime? t)
        {
            EKey = key;
            KeyTime = t;
        }
    }

    /// <summary>
    /// KeyAndTimeComparer
    /// </summary>
    class KeyAndTimeComparer : IEqualityComparer<KeyAndTime>
    {
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(KeyAndTime x, KeyAndTime y)
        {
            // If parameter is null return false:
            if (x == null || y == null)
                return false;
            if (x.EKey == y.EKey)
                return true;
            else
                return false;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(KeyAndTime obj)
        {
            return obj.EKey.GetHashCode();
        }
    }

    /// <summary>
    /// ValueConverter
    /// </summary>
    public class KeyToString : System.Windows.Data.IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rc;
            rc = value.ToString();
            return rc;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
