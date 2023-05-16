using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using MaterialsApp.Models;

namespace MaterialsApp.Data
{
    class TxtDataSource : IDataSource
    {

        public List<User> Users { get; set; }

        private string SaveFile { get; set; }

        public TxtDataSource()
        {
            SaveFile = @"C:\Data\MaterialsApp\data.txt";    
            Users = new List<User>();

            if(File.Exists(SaveFile))
            {
                PopulateItems();
            }
            else
            {
                throw new Exception("Data source nonexistent.");
            }
        }
        private void PopulateItems()
        {
            using(StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while((line = sr.ReadLine()) != null)
                {
                    string[] splitline = line.Split(',');

                    User user = new User()
                    {
                        Username = splitline[0],
                        WoodCount = int.Parse(splitline[1]),
                        StoneCount = int.Parse(splitline[2]),
                        IronCount = int.Parse(splitline[3]),
                        GoldCount = int.Parse(splitline[4])
                    };

                    Users.Add(user);
                }
            }
        }
        public User Authenticate(string username)
        {
            User user = Users.SingleOrDefault(user => user.Username.ToLower() == username);
            return user;
        }
        public User GetUser(User user)
        {
            return user;
        }
        public void ReWriteFile()
        {
            File.Delete(SaveFile);

            File.Create(SaveFile).Close();

            foreach (var user in Users)
            {
                if (Users.Count > 0)
                {
                    using (StreamWriter sw = File.AppendText(SaveFile))
                    {
                        if (user != Users.First())
                        {
                            sw.Write("\n");
                        }
                        sw.Write($"{user.Username},{user.WoodCount},{user.StoneCount},{user.IronCount},{user.GoldCount}");
                    }
                }
            }
        }
        public int DepositGold(User user, int amount)
        {
            user.GoldCount += amount;
            ReWriteFile();
            return user.GoldCount;
        }
        public int DepositIron(User user, int amount)
        {
            user.IronCount += amount;
            ReWriteFile();
            return user.IronCount;
        }
        public int DepositStone(User user, int amount)
        {
            user.StoneCount += amount;
            ReWriteFile();
            return user.StoneCount;
        }
        public int DepositWood(User user, int amount)
        {
            user.WoodCount += amount;
            ReWriteFile();
            return user.WoodCount;
        }
        public int WithdrawGold(User user, int amount)
        {
                user.GoldCount -= amount;
                ReWriteFile();
                return user.GoldCount;
        }
        public int WithdrawIron(User user, int amount)
        {
                user.IronCount -= amount;
                ReWriteFile();
                return user.IronCount;
        }
        public int WithdrawStone(User user, int amount)
        {

                user.StoneCount -= amount;
                ReWriteFile();
                return user.StoneCount;
        }
        public int WithdrawWood(User user, int amount)
        {

                user.WoodCount -= amount;
                ReWriteFile();
                return user.WoodCount;
        }
    }
}
