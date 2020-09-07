using DataCrawlingDemo.DataModels;
using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DataCrawlingDemo
{
    public partial class Form1 : Form
    {
        private Product _product;
        NumberStyles _numberStyles = NumberStyles.Float;
        IFormatProvider _formatProvider = CultureInfo.CreateSpecificCulture("fr-FR");

        public Form1()
        {
            InitializeComponent();
        }

        private void StartCrawling(string url)
        {  
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            HtmlNode productInfoNode = htmlDoc.DocumentNode
                                    .SelectNodes("//main")
                                    .First();

            string productName = productInfoNode.SelectNodes("//h1[@data-test-id='hero-info-title']").First().InnerText;

            HtmlNode productPriceNode = productInfoNode
                                .SelectNodes("//div[@data-test-id='product-primary-price']").First();

            string price = productPriceNode.SelectNodes(".//span").First().InnerText;
            string priceUnit = productPriceNode.SelectNodes(".//span").Last().InnerText;
            string imgSrc = productInfoNode.SelectNodes(".//img[@data-test-id='image']").First().Attributes["src"].Value;
            string imgSrcRefined = imgSrc.Substring(0, imgSrc.IndexOf('$'));

            _product = new Product()
            {
                CrawlId = new Guid(),
                ProductName = productName,
                Price = Double.Parse(price, _numberStyles, _formatProvider),
                PriceUnit = priceUnit,
                ImageUrl = imgSrcRefined
            };

        }

        private void btnCrawl_Click(object sender, System.EventArgs e)
        {
            StartCrawling(txtUrl.Text);

            if(_product != null)
            {
                txtProductName.Text = _product.ProductName;
                txtPrice.Text = _product.Price.ToString("0.00", _formatProvider);
                txtUnit.Text = _product.PriceUnit;
                txtImageUrl.Text = _product.ImageUrl;
            }
        }
    }
}
