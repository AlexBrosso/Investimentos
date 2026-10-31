using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests
{
    public class PositionTests
    {
        [Fact]
        public void Buy_TwoPurchases_UpdatesQuantityCostAndAveragePrice()
        {
            // Arrange
            var position = new Position("PETR4");

            //Act
            position.Buy(100, 30);
            position.Buy(50, 36);

            // Assert
            Assert.Equal(32.00m, position.AveragePrice);
            Assert.Equal(4800m, position.TotalCost);
            Assert.Equal(150, position.Quantity);
        }

        [Fact]
        public void Operation_TwoPurchasesOneSale_UpdatesQuantityCostAndAveragePrice()
        {
            // Arrange
            var position = new Position("PETR4");

            //Act
            position.Buy(100, 30);
            position.Buy(50, 36);
            position.Sell(30, 40);

            // Assert
            Assert.Equal(32.00m, position.AveragePrice);
            Assert.Equal(3840m, position.TotalCost);
            Assert.Equal(120, position.Quantity);
        }
    }
}
