
using PromotIt.Entitey.azureCommands.Activist;
using PromotIt.Entitey.azureCommands.Business;
using PromotIt.Entitey.azureCommands.Campaigns;
using PromotIt.Entitey.azureCommands.Owner;
using PromotIt.Entitey.azureCommands.Users;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public interface ICommand
    {
        object ExecuteCommand(params object[] param);
    }

    public class CommandManager
    {

        public CommandManager()
        {

        }

        private Dictionary<string, ICommand> _CommandList;

        public Dictionary<string, ICommand> CommandList
        {
            get
            {
                if (_CommandList == null) Init();
                return _CommandList;
            }
        }

        private void Init()
        {
            try
            {
                MainManager.Instance.Log.AddLogItemToQueue("Command List Initialization", null, "Event");

                _CommandList = new Dictionary<string, ICommand>
                {
                    
                    // Campaign
                    { "Campaign.ADD", new AddCampaign() },
                    { "Campaign.GET", new GetPersonalCampaign() },
                    { "Campaign.DELETE", new DeleteOrDeactivateCampagin() },
                    { "Campaign.Update",new UpdateCampagin() },
                    { "Campaign.GETCAMPAIGNID", new GetCampaignID() },
                    { "Campaign.DONATIONAMOUNT",new CampaginDonationAmount() },
                    // Business
                    { "Business.Donate", new DonateToCampaign() },
                    { "Business.GET", new GetActiveCampaigns() },
                    { "Business.DELETEPRPDUCT", new DeleteProduct() },
                    { "Business.Update", new UpdateProduct() },
                    { "Business.GETPRODUCTID",new GetProductId() },
                    { "Business.GETPRODUCTS",new GetCampaignProducts() },
                    { "Business.PRODUCTS",new GetProducts() },
                    { "Business.GETORDERS" ,new GetProductOrders() },
                    { "Business.CONFIRMORDER" ,new ConfirmOrders() },
                    { "Business.REGISTER", new RegisterBusinessCompany() },
                    { "Business.GETCOMPANYNAME",new GetCompanyName() },
                    //Activist
                    { "Activist.USERID",new GetUserID() },
                    { "Activist.INITIATECAMPAIGN", new InitiateCampaigns() },
                    { "Activist.INITIATEPOINTS", new InitiateActivistPoints() },
                    { "Activist.GETPOINTS", new GetActivistPoints() },
                    { "Activist.DROPOINTS", new DecreaseActivistPoints() },
                    { "Activist.TWITTERMESSAGE", new SendTwitterMessage() },
                    //Owner
                    { "Owner.GET",new ReportUserCount() },
                    { "Owner.GETNONPROFIT",new GetNonprofitUsers() },
                    { "Owner.GETBUSINESS",new GetBusinessUsers() },
                    { "Owner.GETACTIVIST",new GetActivistUsers() },
                    { "Owner.CAMPAIGNSTATS",new CampaignStatistics() },
                    { "Owner.GETOTALCAMPAIGNS",new GetAllCampaigns() },
                    { "Owner.GETCAMPAIGNDONATION",new GetCampaignDonation() },
                    { "Owner.GETCAMPAIGNTWEETS",new GetCampaignTweets() },
                    //Users
                    { "Users.Order",new UserOrder() },
                    { "Users.GET",new GetAllCampaignInfo() },
                    { "Users.GETID",new GetUserId() },
                    { "Users.UpdateStock",new UpdateProductStock() },
                    { "Users.InitWallet", new InitiateUserWallet() },
                    { "Users.GETUSERMONEY",new GetUserMoney() },
                    { "Users.ADDMONEY",new AddMoneyToUser() },
                    { "Users.DECREASEMONEY",new DeacreaseUserMoney() },
                    { "Users.ROLES",new SetRole() }
                };

            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue("Faild To Initialie Command List", ex, "Error");
            }

        }

    }

}
