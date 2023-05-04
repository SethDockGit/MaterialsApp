using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialsApp;
using MaterialsApp.Logic;
using MaterialsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialsController : ControllerBase
    {

        public Manager Manager { get; set; }


        [HttpGet]
        [Route("{username}")]
        public WorkflowResponse CheckResources(string username)
        {

            var managerFactory = new ManagerFactory();
            var manager = managerFactory.GetManager();

            WorkflowResponse response = manager.CheckResources(username);

            return response;

        }

        [HttpPost]
        [Route("deposit")]
        public WorkflowResponse DepositResource([FromBody]TransactionRequest transaction)
        {
            var username = transaction.Username;
            var amount = transaction.Amount;
            ResourceType resource;
            
            switch(transaction.ResourceType)
            {
                case "wood":
                    resource = ResourceType.Wood;
                    break;
                case "stone":
                    resource = ResourceType.Stone;
                    break;
                case "gold":
                    resource = ResourceType.Gold;
                    break;
                case "iron":
                    resource = ResourceType.Iron;
                    break;
                default:
                    throw new Exception("String failed to convert to resource type");
            }

            var managerFactory = new ManagerFactory();
            var manager = managerFactory.GetManager();
            var user = manager.IDataSource.Authenticate(username);

            WorkflowResponse response = manager.DepositResource(user, resource, amount);

            return response;
        }
        [HttpPost]
        [Route("withdraw")]
        public WorkflowResponse WithdrawResource([FromBody] TransactionRequest transaction)
        {
            var username = transaction.Username;
            var amount = transaction.Amount;
            ResourceType resource;

            switch (transaction.ResourceType)
            {
                case "wood":
                    resource = ResourceType.Wood;
                    break;
                case "stone":
                    resource = ResourceType.Stone;
                    break;
                case "gold":
                    resource = ResourceType.Gold;
                    break;
                case "iron":
                    resource = ResourceType.Iron;
                    break;
                default:
                    throw new Exception("String failed to convert to resource type");
            }

            var managerFactory = new ManagerFactory();
            var manager = managerFactory.GetManager();
            var user = manager.IDataSource.Authenticate(username);

            WorkflowResponse response = manager.WithdrawResource(user, resource, amount);

            return response;
        }
    }
}
