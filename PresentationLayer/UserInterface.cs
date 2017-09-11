using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class UserInterface
    {
        public void MainChoise()
        {
            var outKey = -1;
            do
            {
                string key;
                do
                {
                    Console.WriteLine("If you want know current rate press - '1',\n" +
                                      "If you want view currency rate statistic press - '2'");
                    key = Console.ReadLine();
                } while (!key.Equals("1") || !key.Equals("2"));

                outKey = key.Equals("2") ? GetStatictic() : CurrentRate();
            } while (outKey != 0);
        }

        private int GetStatictic()
        {
           ////Controller
            return -1;
        }

        private int CurrentRate()
        {
            string key = GetCurrencyKey();

            if(key.Equals("0")) return 0;

            if (key.Equals("4"))
            {
                GetStatictic();
                return 0;
            }
            string currency = GetCurrencyByKey(key);

            string cityKey = GetCityKey();

            if (key.Equals("0")) return 0;

            if (key.Equals("4"))
            {
                GetStatictic();
                return -1;
            }

            string city = GetCityByKey(cityKey);

            /////Controller
            /// + get Best Purchase/Sale
            return -1;
        }

        private string GetCityKey()
        {
            string key;

            do
            {
                Console.WriteLine("Select currency: \n" +
                                  "minsk - '1'\n" +
                                  "vitebsk - '2'\n" +
                                  "brest - '3' \n" +
                                  "If you want view currency rate statistic press - '4' \n" +
                                  "For out press - 0");
                key = Console.ReadLine();
            } while (!key.Equals("0") || !key.Equals("1") || !key.Equals("2") || !key.Equals("3") || !key.Equals("4"));

            return key;
        }

        private string GetCurrencyKey()
        {
            string key;

            do
            {
                Console.WriteLine("Select currency: \n" +
                                  "dollar - '1'\n" +
                                  "euro - '2'\n" +
                                  "russian ruble(by 100 rub) - '3' \n" +
                                  "If you want view currency rate statistic press - '4' \n" +
                                  "For out press - 0");
                key = Console.ReadLine();
            } while (!key.Equals("0") || !key.Equals("1") || !key.Equals("2") || !key.Equals("3") || !key.Equals("4"));

            return key;
        }

        private static string GetCurrencyByKey(string key)
        {
            var currency = "";
            switch (key)
            {
                case "1":
                    currency = "dolar";
                    break;
                case "2":
                    currency = "euro";
                    break;
                case "3":
                    currency = "rubl";
                    break;
            }
            return currency;
        }

        private static string GetCityByKey(string key)
        {
            var city = "";
            switch (key)
            {
                case "1":
                    city = "minsk";
                    break;
                case "2":
                    city = "vitebsk";
                    break;
                case "3":
                    city = "brest";
                    break;
            }
            return city;
        }
    }
}
