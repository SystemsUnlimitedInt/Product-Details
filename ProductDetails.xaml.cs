using App2.Model;
using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Ubiety.Dns.Core.Records;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ProductDetails : ContentPage
    {
        public static string strOrderNumber1 = SigninPage.strOrderNumber;

        public ProductDetails(string ProductName)
        {

            InitializeComponent();

            BindingContext = new ProductDetailsViewModel(ProductName);

            // Call the method to load the categories from the database
            //((ProductDetailsViewModel)BindingContext).LoadProductDetailsCommand.Execute(null);

           

            txtOrderNumber.Text = strOrderNumber1;
        }


           






            public string ProductName { get; set; }
        //public string Quantity { get; set; }

        private string _quantity;
        public string Quantity
        {
            set { SetProperty(ref _quantity, value); }
            get { return _quantity; }
        }

        // public string QuantityOld { get; set; }
        private string _quantityOld;
        public string QuantityOld
        {
            set { SetProperty(ref _quantityOld, value); }
            get { return _quantityOld; }
        }

        //public string QuantityNew { get; set; }
        private string _quantityNew;
        public string QuantityNew
        {
            set { SetProperty(ref _quantityNew, value); }
            get { return _quantityNew; }
        }


        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    } 

        


    
}
