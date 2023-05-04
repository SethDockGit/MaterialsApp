using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace MaterialsApp.Data
{
    public class InMemoryDataSource : IDataSource
    {
        public List<User> Users { get; set; }     
        public InMemoryDataSource()
        {
            Users = new List<User>()
            {
                new User()
                {
                    Username = "Timmy",
                    WoodCount = 1000,
                    StoneCount = 1000,
                    IronCount = 1000,
                    GoldCount = 1000
                },
                new User()
                {
                    Username = "Doug",
                    WoodCount = 5000,
                    StoneCount = 1000,
                    IronCount = 3000,
                    GoldCount = 100000
                }
            };
        }
        public User GetUser(User user)
        {
            return user;
        }
        public int DepositWood(User user, int amount)
        {
            user.WoodCount += amount;
            return user.WoodCount;
        }
        public int DepositStone(User user, int amount)
        {
            user.StoneCount += amount;
            return user.StoneCount;
        }
        public int DepositIron(User user, int amount)
        {
            user.IronCount += amount;
            return user.IronCount;
        }
        public int DepositGold(User user, int amount)
        {
            user.GoldCount += amount;
            return user.GoldCount;
        }
        public int WithdrawWood(User user, int amount)
        {
            user.WoodCount -= amount;
            return user.WoodCount;            
        }
        public int WithdrawStone(User user, int amount)
        {
            user.StoneCount -= amount;
            return user.StoneCount;
        }
        public int WithdrawIron(User user, int amount)
        {

            user.IronCount -= amount;
            return user.IronCount;
        }
        public int WithdrawGold(User user, int amount)
        {
            user.GoldCount -= amount;
            return user.GoldCount;
        }
        public User Authenticate(string username)
        {
            User user = Users.SingleOrDefault(user => user.Username == username);
            return user;
        }
    }
}
