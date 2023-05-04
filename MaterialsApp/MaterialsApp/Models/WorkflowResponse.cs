using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsApp.Models
{
    public class WorkflowResponse
    {
        public bool Success { get; set; }   
        public string Message { get; set; }
        public User User { get; set; }
    }
}
