using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using App2.Model;
using MySqlConnector;
using Xamarin.Forms;
using App2.Views;
using Xamarin.Forms.BehaviorsPack;
using System;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace App2.ViewModels
{
    public class ProductDetailsViewModel : BaseProductDetailsViewModel
    {

        public string strOrderNumber3 = SigninPage.strOrderNumber;
        public string strEmail1 = SigninPage.strEmail;
        private string _productname;
        public string QuantityNew1 = string.Empty;






        public ObservableCollection<ProductDetail> Items { get; set; }

        public ICommand QuantityAddCommand { get; set; }
        public ICommand QuantityMinusCommand { get; set; }

        public ProductDetailsViewModel(string productparamter)
        {
            _productname = productparamter;
            Items = new ObservableCollection<ProductDetail>();
            Items.Add(new ProductDetail { Name = "Product1", Quantity = "30", QuantityOld = "10", QuantityNew = "0" });
            Items.Add(new ProductDetail { Name = "Product1", Quantity = "40", QuantityOld = "10", QuantityNew = "0" });
            Items.Add(new ProductDetail { Name = "Product1", Quantity = "50", QuantityOld = "10", QuantityNew = "0" });

            QuantityAddCommand = new Command((object item) => {

                if (item != null && item is ProductDetail)
                {
                    ProductDetail productdetail = (ProductDetail)item;
                    productdetail.Quantity = (Convert.ToInt32(productdetail.QuantityOld) + Convert.ToInt32(1)).ToString();
                    productdetail.QuantityOld = productdetail.Quantity;

                }
            });

            QuantityMinusCommand = new Command((object item) => {
                if (item != null && item is ProductDetail)
                {

                    ProductDetail productdetail = (ProductDetail)item;

                    if (productdetail.Quantity != "0")
                    {
                        productdetail.Quantity = (Convert.ToInt32(productdetail.QuantityOld) - Convert.ToInt32(1)).ToString();
                        productdetail.QuantityOld = productdetail.Quantity;

                    }
                }
            });
            LoadProductDetails();
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
    







    

        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
        }

        //public ProductDetailsViewModel(string productparamter)
        //{
        //    _productname = productparamter;
            
        //}


        



        private ICommand _loadProductDetailsCommand;


        public ICommand LoadProductDetailsCommand
        {
            get
            {
                if (_loadProductDetailsCommand == null)
                {
                    _loadProductDetailsCommand = new Command(async () => await LoadProductDetails());
                }

                return _loadProductDetailsCommand;
            }
        }


        private List<ProductDetail> _productdetails;

        public List<ProductDetail> ProductDetails
        {
            get => _productdetails;
            set => SetProperty(ref _productdetails, value);
        }


        public async Task LoadProductDetails()
        {
            // Call the GetCategories method to load the categories from the database
            ProductDetails = await GetProductDetails();
        }

        // Method for downloading an image
        private async Task<byte[]> DownloadImageAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetByteArrayAsync(url);
            }
        }

        // Method for getting the categories from the database
        private async Task<List<ProductDetail>> GetProductDetails()
        {
            // Initialize the list of categories
            List<ProductDetail> productdetails = new List<ProductDetail>();


            // Connect to the database
            using (MySqlConnection connection = new MySqlConnection("MySQL string"))
            {
                connection.Open();

                // Create a command to select the categories from the database
                using (MySqlCommand command = new MySqlCommand("SELECT name, productdetails, price, image, quantity FROM products WHERE name = '" + ProductName + "' ", connection))
                {
                    // Execute the command and read the results
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount >= 5)
                            {
                                // Create a new category object
                                ProductDetail productdetail = new ProductDetail
                                {
                                    Name = reader.GetString(0),
                                    ProductDetails = reader.GetString(1),
                                    Price = "R " + reader.GetString(2),
                                    Price1 = reader.GetString(2),
                                    ImageUrl = reader.GetString(3),
                                    Quantity = reader.GetString(4),
                                    QuantityOld = reader.GetString(4),
                                    
                                };
                                
                                
                                // Download the image for the category
                                productdetail.ImageData = await DownloadImageAsync(productdetail.ImageUrl);

                                // Add the category to the list
                                productdetails.Add(productdetail);
                            }
                        }

                    }
                }
            }

            // Return the list of categories
            return productdetails;

        
        }


        








        private ICommand _productdetailCommand;

        public ICommand ProductDetailsCommand
        {
            get
            {
                if (_productdetailCommand == null)
                {
                    _productdetailCommand = new Command<ProductDetail>(async (productdetail) =>
                    {
                        await OnProductDetailSelected(productdetail);
                    });
                }

                return _productdetailCommand;
            }
        }

        

        private async Task OnProductDetailSelected(ProductDetail productdetail)
        {
            string constring = "server=vps49.heliohost.us;Port=3306;user id=woofwishlist_woofwishlist;password=Sui@951!!;database=woofwishlist_vetapp";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlTransaction transaction = con.BeginTransaction();

            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO cart (name, image, price, quantity, email, ordernumber) VALUES(@Name, @Image, @Price, @Quantity,  @Email, @OrderNumber)", con))
            {
                cmd.Parameters.AddWithValue("@Name", productdetail.Name);
                cmd.Parameters.AddWithValue("@Image", productdetail.ImageUrl);
                cmd.Parameters.AddWithValue("@Price", productdetail.Price1);
                cmd.Parameters.AddWithValue("@Quantity", productdetail.Quantity );
                cmd.Parameters.AddWithValue("@Email", strEmail1);
                cmd.Parameters.AddWithValue("@OrderNumber", strOrderNumber3 );
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();

                transaction.Commit();
                con.Close();
            }
            
         
        }
        
    }
}
