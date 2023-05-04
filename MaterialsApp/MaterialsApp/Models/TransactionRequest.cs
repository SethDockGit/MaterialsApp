using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsApp.Models
{
    public class TransactionRequest
    {
        public int Amount { get; set; }
        public string ResourceType { get; set; }
        public string Username { get; set; }
    }
}
