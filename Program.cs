using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using WebScrappingECommerece;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=DESKTOP-UFB6KUK\\SQLEXPRESS01;Initial Catalog=ECommereceDB;Integrated Security=True";
        var dbHelper = new DatabaseHelper(connectionString);

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--disable-gpu");
        options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

        using (IWebDriver driver = new ChromeDriver(options))
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                driver.Navigate().GoToUrl("https://www.flipkart.com/");
                Thread.Sleep(3000);

                IWebElement electronicsDiv = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.CssSelector("div[aria-label='Electronics']"))
                );

                electronicsDiv.Click();
                Console.WriteLine("Clicked on Electronics category.");

                IWebElement audiAllOptions = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.CssSelector("a[class='_3490ry']"))
                );
                audiAllOptions.Click();
                Thread.Sleep(4000);
                Console.WriteLine("Clicked on Audio All Electronic List Category.");


                IWebElement product = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.CssSelector("a[class='VJA3rP']"))
                );
                product.Click();
                Console.WriteLine("Clicked on Audio Product Category.");
                Thread.Sleep(4000);

                wait.Until(d => d.WindowHandles.Count > 1);

                driver.SwitchTo().Window(driver.WindowHandles[1]);

                var productNameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1._6EBuvT > span.VU-ZEz")));
                string productName = productNameElement.Text;
                Console.WriteLine("Product Name: " + productName);

                var priceElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.Nx9bqj.CxhGGd")));

                string price = priceElement.Text;

                Console.WriteLine("Price: " + price);

                var ratingElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.XQDdHH")));
                string rating = ratingElement.Text;
                Console.WriteLine("Rating: " + rating);

                string description = "";
                dbHelper.InsertProduct(productName, price, rating, description);
                Console.WriteLine("Product details inserted into database successfully.");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
