﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCatalog.Web.Main.UserRepositoryClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserRepositoryClient.IUserRepositoryService")]
    public interface IUserRepositoryService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRepositoryService/GetAllUsers", ReplyAction="http://tempuri.org/IUserRepositoryService/GetAllUsersResponse")]
        OnlineCatalog.Common.DataContracts.Administration.UserDto[] GetAllUsers(OnlineCatalog.Common.DataContracts.Administration.UserRankDto[] acceptableUserRanks);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRepositoryService/GetAllUsers", ReplyAction="http://tempuri.org/IUserRepositoryService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<OnlineCatalog.Common.DataContracts.Administration.UserDto[]> GetAllUsersAsync(OnlineCatalog.Common.DataContracts.Administration.UserRankDto[] acceptableUserRanks);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserRepositoryServiceChannel : OnlineCatalog.Web.Main.UserRepositoryClient.IUserRepositoryService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserRepositoryServiceClient : System.ServiceModel.ClientBase<OnlineCatalog.Web.Main.UserRepositoryClient.IUserRepositoryService>, OnlineCatalog.Web.Main.UserRepositoryClient.IUserRepositoryService {
        
        public UserRepositoryServiceClient() {
        }
        
        public UserRepositoryServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserRepositoryServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserRepositoryServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserRepositoryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public OnlineCatalog.Common.DataContracts.Administration.UserDto[] GetAllUsers(OnlineCatalog.Common.DataContracts.Administration.UserRankDto[] acceptableUserRanks) {
            return base.Channel.GetAllUsers(acceptableUserRanks);
        }
        
        public System.Threading.Tasks.Task<OnlineCatalog.Common.DataContracts.Administration.UserDto[]> GetAllUsersAsync(OnlineCatalog.Common.DataContracts.Administration.UserRankDto[] acceptableUserRanks) {
            return base.Channel.GetAllUsersAsync(acceptableUserRanks);
        }
    }
}
