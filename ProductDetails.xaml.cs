using App2.Model;
using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ((ProductDetailsViewModel)BindingContext).LoadProductDetailsCommand.Execute(null);

            //txtQuantity.Text = strQuantity;
            //txtQuantityOld.Text = strQuantityOld;
            //txtQuantity.Text = txtQuantityOld.Text;
            //txtQuantity.Text = "0";
            //txtQuantityOld.Text = "0";
            
            txtOrderNumber.Text = strOrderNumber1;


            //strQuantity = txtQuantity.Text;
            //strQuantityOld = txtQuantityOld.Text;





        }



        //private void minusbutton_Clicked(object sender, EventArgs e)
        //{
        //    if (sender is Xamarin.Forms.Button button1)
        //    {
        //        if (txtQuantity.Text != "0")
        //        {

        //            button1.IsEnabled = true;
        //            txtQuantity.Text = (Convert.ToInt32(txtQuantityOld.Text) - Convert.ToInt32(1)).ToString();
        //            txtQuantityOld.Text = txtQuantity.Text;
        //            strQuantity = txtQuantity.Text;
        //            strQuantityOld = txtQuantityOld.Text;
        //        }

        //        else
        //        {

        //            button1.IsEnabled = false;
        //            Task.Delay(1000);
        //            button1.IsEnabled = true;
        //        }


        //    }
        //}

        //private void Button_Clicked_1(object sender, EventArgs e)
        //{

        //    txtQuantity.Text = (Convert.ToInt32(txtQuantityOld.Text) + Convert.ToInt32(1)).ToString();
        //    txtQuantityOld.Text = txtQuantity.Text;

        //    strQuantity = txtQuantity.Text;
        //    strQuantityOld = txtQuantityOld.Text;
        //}



    }
}