using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using MaterialsApp.Data;
using MaterialsApp.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Xunit;

namespace Tests
{
    public class InMemoryTests
    {
        //could you just have the data source as a property and instantiate in the constructor?
        //can you even use a constructor if InMemoryTests is not instantiated anywhere?

        //InMemoryDataSource InMemory { get; set; }

        public static List<User> TestUsers { get; set; } = new List<User>
        {
            new User()
            {
                Username = "Test1",
                WoodCount = 500,
                StoneCount = 500,
                IronCount = 500,
                GoldCount = 500
            },
            new User()
            {
                Username = "Test2",
                WoodCount = 5000,
                StoneCount = 1000,
                IronCount = 3000,
                GoldCount = 100000
            }
        };

        [Fact]
        public void InMemoryDataSource_GetUser_CanGetUser()
        {
            var dataSource = new InMemoryDataSource();

            User authenticatedUser = dataSource.Authenticate("Timmy");

            User user = dataSource.GetUser(authenticatedUser);

            Assert.Equal(authenticatedUser.Username, user.Username);
        }
        [Fact]
        public void InMemoryDataSource_Authenticate_CannotGetInvalidUser()
        {
            var dataSource = new InMemoryDataSource();

            User authenticatedUser = dataSource.Authenticate("Invalid");

            Assert.Null(authenticatedUser);
        }
        [Fact]
        public void InMemoryDataSource_Authenticate_CanAuthenticateUser()
        {
            var dataSource = new InMemoryDataSource();

            User authenticatedUser = dataSource.Authenticate("Timmy");

            Assert.Equal("Timmy", authenticatedUser.Username);
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_DepositWood_CanDepositWood(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.WoodCount += amount;
                int actual = dataSource.DepositWood(user, amount);
                Assert.Equal(user.WoodCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_DepositStone_CanDepositStone(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.StoneCount += amount;
                int actual = dataSource.DepositStone(user, amount);
                Assert.Equal(user.StoneCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_DepositIron_CanDepositIron(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.IronCount += amount;
                int actual = dataSource.DepositIron(user, amount);
                Assert.Equal(user.IronCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_DepositGold_CanDepositGold(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.GoldCount += amount;
                int actual = dataSource.DepositGold(user, amount);
                Assert.Equal(user.GoldCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_WithdrawWood_CanWithdrawWood(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.WoodCount -= amount;
                int actual = dataSource.WithdrawWood(user, amount);
                Assert.Equal(user.WoodCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_WithdrawStone_CanWithdrawStone(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.StoneCount -= amount;
                int actual = dataSource.WithdrawStone(user, amount);
                Assert.Equal(user.StoneCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_WithdrawIron_CanWithdrawIron(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.IronCount -= amount;
                int actual = dataSource.WithdrawIron(user, amount);
                Assert.Equal(user.IronCount, actual);
            }
        }
        [Theory]
        [InlineData(500)]
        [InlineData(0)]
        [InlineData(-10)]
        public void InMemoryDataSource_WithdrawGold_CanWithdrawGold(int amount)
        {
            var dataSource = new InMemoryDataSource();
            foreach (User user in TestUsers)
            {
                user.GoldCount -= amount;
                int actual = dataSource.WithdrawGold(user, amount);
                Assert.Equal(user.GoldCount, actual);
            }
        }
    }
}
