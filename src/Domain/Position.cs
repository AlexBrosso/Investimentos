using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Position
    {
        public string TickerSymbol { get; }
        public decimal TotalCost { get; private set; }
        public int Quantity { get; private set; }
        public decimal AveragePrice => Quantity > 0 ? TotalCost / Quantity : 0;

        public Position(string tickerSymbol)
        {
            TickerSymbol = tickerSymbol;
        }

        public void Buy(int quantity, decimal price)
        {
            Quantity += quantity;
            TotalCost += quantity * price;
        }

        public void Sell(int quantity, decimal price)
        {
            if(quantity > Quantity)
                throw new InvalidOperationException("Cannot sell more than the current quantity.");

            TotalCost -= quantity * AveragePrice;
            Quantity -= quantity;
        }
    }
}
