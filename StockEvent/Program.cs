using System;
using static StockEvent.Program;

namespace StockEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stock Event Program");
            Console.Write("Enter Stock Name Please: ");
            Stock stock = new Stock(Console.ReadLine());
            Console.Write("Enter Stock Price Please: ");
            stock.Price = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Percent Please: ");
            decimal per =  Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("\nDo you want to subscribe in Stock Event Features? (y/n)");
            char ans =Convert.ToChar(Console.ReadLine());
            stock.DoYouWantToSubscribe(ans);
            
            stock.ChangeStockPriceBy(per);
            if (!stock.Check) { Console.WriteLine($"Stock Name : {stock.Name} ==> Price : {stock.Price}"); }
            Console.Write("\n\nGood Luck ^_^");
            Console.ReadKey();

        }


        public delegate void StockPriceChangeHandler(Stock stock, decimal oldprice);

        public class Stock
        {
            private string name;
            private decimal price;

            public event StockPriceChangeHandler onPriceChanged;

            public string Name => this.name;
            public decimal Price { get => this.price; set => this.price = value;}
            public bool Check = false;
            public Stock(string stockname)
            {
                this.name = stockname;
            }

            public void ChangeStockPriceBy(decimal percent)
            {
                decimal oldprice = this.price;  
                this.price += Math.Round(this.price * percent, 2);
                if(onPriceChanged != null)
                {
                    onPriceChanged(this, oldprice);
                }
            }

            public void DoYouWantToSubscribe(char ans)
            {
                if (char.ToLower(ans) == 'y') { 
                    this.Check = true;
                    Console.WriteLine("\nSuccess Subscribe\n\n");
                    this.onPriceChanged += (Stock stock, decimal oldprice) =>
                    {
                        string res = " ";
                        if (stock.Price < oldprice)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            res = "Down";
                        }
                        else if (stock.Price > oldprice)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            res = "Up";
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            res = "Balance";
                        }
                        Console.WriteLine($"Stock Name : {stock.Name} ==> Price : {stock.Price} ==> {res}");
                    };
                }
                else { Console.WriteLine("\nYou still don't have a subscribtion\n\n"); }
            }
             
        }
    }

}