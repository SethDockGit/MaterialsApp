using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MaterialsApp.Logic;
using MaterialsApp.Data;
using MaterialsApp.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace Tests
{
    public class ManagerTests
    {
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
        [Theory]
        [InlineData("Timmy", true)]
        [InlineData("Tammy", false)]
        public void Manager_CheckResources_GetsCorrectResponse(string username, bool expectedSuccess)
        {

            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            User user = inMemory.Authenticate(username);
            string expectedMessage;

            if (user == null)
            {
                expectedMessage = $"Error: User {username} was not found. Press any key to return to the main menu.";
            }
            else
            {
                expectedMessage = null;
            }

            WorkflowResponse response = manager.CheckResources(username);

            Assert.Equal(response.Success, expectedSuccess);
            Assert.Equal(response.Message, expectedMessage);
            Assert.Equal(response.User, user);
        }
        [Fact]
        public void Manager_CheckResources_CanCheckResources()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);

            foreach(User user in inMemory.Users)
            {
                int woodCount = user.WoodCount;
                int stoneCount = user.StoneCount;
                int ironCount = user.IronCount;
                int goldCount = user.GoldCount;

                WorkflowResponse response = manager.CheckResources(user.Username);

                Assert.Equal(woodCount, response.User.WoodCount);
                Assert.Equal(stoneCount, response.User.StoneCount);
                Assert.Equal(ironCount, response.User.IronCount);
                Assert.Equal(goldCount, response.User.GoldCount);
            }
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Manager_DepositResource_CantDepositZeroOrNegative(int withdrawal)
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Gold;

            foreach (User user in inMemory.Users)
            {
                string expectedMessage = "Error: Deposit amount must be an integer greater than 0. Press any key to return to the main menu.";

                WorkflowResponse response = manager.DepositResource(user, resource, withdrawal);
                string actualMessage = response.Message;

                Assert.Equal(expectedMessage, actualMessage);
                Assert.False(response.Success);
            }
        }
        [Fact]
        public void Manager_DepositResource_CanDepositWood()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Wood;
            int deposit = 500;

            foreach (User user in inMemory.Users)
            {
            int expectedWoodCount = user.WoodCount + deposit;
            WorkflowResponse response = manager.DepositResource(user, resource, deposit);
            int actualWoodCount = response.User.WoodCount;

            Assert.Equal(expectedWoodCount, actualWoodCount);
            }
            //where to put response.User = user. In the method or the test?
        }
        [Fact]
        public void Manager_DepositResource_CanDepositStone()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Stone;
            int deposit = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedStoneCount = user.StoneCount + deposit;
                WorkflowResponse response = manager.DepositResource(user, resource, deposit);
                int actualStoneCount = response.User.StoneCount;

                Assert.Equal(expectedStoneCount, actualStoneCount);
            }
        }
        [Fact]
        public void Manager_DepositResource_CanDepositIron()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Iron;
            int deposit = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedIronCount = user.IronCount + deposit;
                WorkflowResponse response = manager.DepositResource(user, resource, deposit);
                int actualIronCount = response.User.IronCount;

                Assert.Equal(expectedIronCount, actualIronCount);
            }
            //what would happen if deposit was negative? The test would not route the deposit
            //and so the user's count would not be equal to the "expected" count
        }
        [Fact]
        public void Manager_DepositResource_CanDepositGold()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Gold;
            int deposit = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedGoldCount = user.GoldCount + deposit;
                WorkflowResponse response = manager.DepositResource(user, resource, deposit);
                int actualGoldCount = response.User.GoldCount;

                Assert.Equal(expectedGoldCount, actualGoldCount);
            }
        }
        [Fact]
        public void Manager_WithdrawResource_CanWithdrawWood()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Wood;
            int withdrawal = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedWoodCount = user.WoodCount - withdrawal;
                WorkflowResponse response = manager.WithdrawResource(user, resource, withdrawal);
                int actualWoodCount = response.User.WoodCount;

                Assert.Equal(expectedWoodCount, actualWoodCount);
            }
        }
        [Fact]
        public void Manager_WithdrawResource_CanWithdrawStone()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Stone;
            int withdrawal = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedStoneCount = user.StoneCount - withdrawal;
                WorkflowResponse response = manager.WithdrawResource(user, resource, withdrawal);
                int actualStoneCount = response.User.StoneCount;

                Assert.Equal(expectedStoneCount, actualStoneCount);
            }
        }
        [Fact]
        public void Manager_WithdrawResource_CanWithdrawIron()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Iron;
            int withdrawal = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedIronCount = user.IronCount - withdrawal;
                WorkflowResponse response = manager.WithdrawResource(user, resource, withdrawal);
                int actualIronCount = response.User.IronCount;

                Assert.Equal(expectedIronCount, actualIronCount);
            }
        }
        [Fact]
        public void Manager_WithdrawResource_CanWithdrawGold()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Gold;
            int withdrawal = 500;

            foreach (User user in inMemory.Users)
            {
                int expectedGoldCount = user.GoldCount - withdrawal;
                WorkflowResponse response = manager.WithdrawResource(user, resource, withdrawal);
                int actualGoldCount = response.User.GoldCount;

                Assert.Equal(expectedGoldCount, actualGoldCount);
            }
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Manager_WithdrawResource_CantWithdrawZeroOrNegative(int withdrawal)
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Gold;

            foreach (User user in inMemory.Users)
            {
                string expectedMessage = "Error: Withdrawal amount must be an integer greater than 0. Press any key to return to the main menu.";

                WorkflowResponse response = manager.WithdrawResource(user, resource, withdrawal);
                string actualMessage = response.Message;

                Assert.Equal(expectedMessage, actualMessage);
                Assert.False(response.Success);
            }
        }
        [Fact]
        public void Manager_WithdrawResource_CantOverdraw()
        {
            InMemoryDataSource inMemory = new InMemoryDataSource();
            Manager manager = new Manager(inMemory);
            ResourceType resource = ResourceType.Gold;

            int withdrawal = 1000000;
            foreach (User user in inMemory.Users)
            {
                string expectedMessage = $"Error: insufficient balance of {resource}. Press any key to return to the main menu.";

                WorkflowResponse response = manager.WithdrawResource(user, resource, withdrawal);
                string actualMessage = response.Message;

                Assert.Equal(expectedMessage, actualMessage);
                Assert.False(response.Success);
            }
        }
    }
}
