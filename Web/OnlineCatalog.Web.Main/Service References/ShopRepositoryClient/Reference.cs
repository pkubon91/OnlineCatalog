﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCatalog.Web.Main.ShopRepositoryClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://online.catalog.com/", ConfigurationName="ShopRepositoryClient.IShopRepositoryService")]
    public interface IShopRepositoryService {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IShopRepositoryService/GetAllShops", ReplyAction="https://online.catalog.com/IShopRepositoryService/GetAllShopsResponse")]
        OnlineCatalog.Common.DataContracts.Groups.ShopDto[] GetAllShops();
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IShopRepositoryService/GetShopByName", ReplyAction="https://online.catalog.com/IShopRepositoryService/GetShopByNameResponse")]
        OnlineCatalog.Common.DataContracts.Groups.ShopDto GetShopByName(string shopName);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IShopRepositoryService/GetShopByUniqueId", ReplyAction="https://online.catalog.com/IShopRepositoryService/GetShopByUniqueIdResponse")]
        OnlineCatalog.Common.DataContracts.Groups.ShopDto GetShopByUniqueId(System.Guid uniqueId);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IShopRepositoryService/GetShopsAssignedToUser", ReplyAction="https://online.catalog.com/IShopRepositoryService/GetShopsAssignedToUserResponse")]
        OnlineCatalog.Common.DataContracts.Groups.ShopDto[] GetShopsAssignedToUser(System.Guid userGuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IShopRepositoryService/GetShopsAssignedToUserByLogin", ReplyAction="https://online.catalog.com/IShopRepositoryService/GetShopsAssignedToUserByLoginRe" +
            "sponse")]
        OnlineCatalog.Common.DataContracts.Groups.ShopDto[] GetShopsAssignedToUserByLogin(string login);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IShopRepositoryServiceChannel : OnlineCatalog.Web.Main.ShopRepositoryClient.IShopRepositoryService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ShopRepositoryServiceClient : System.ServiceModel.ClientBase<OnlineCatalog.Web.Main.ShopRepositoryClient.IShopRepositoryService>, OnlineCatalog.Web.Main.ShopRepositoryClient.IShopRepositoryService {
        
        public ShopRepositoryServiceClient() {
        }
        
        public ShopRepositoryServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ShopRepositoryServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShopRepositoryServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShopRepositoryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public OnlineCatalog.Common.DataContracts.Groups.ShopDto[] GetAllShops() {
            return base.Channel.GetAllShops();
        }
        
        public OnlineCatalog.Common.DataContracts.Groups.ShopDto GetShopByName(string shopName) {
            return base.Channel.GetShopByName(shopName);
        }
        
        public OnlineCatalog.Common.DataContracts.Groups.ShopDto GetShopByUniqueId(System.Guid uniqueId) {
            return base.Channel.GetShopByUniqueId(uniqueId);
        }
        
        public OnlineCatalog.Common.DataContracts.Groups.ShopDto[] GetShopsAssignedToUser(System.Guid userGuid) {
            return base.Channel.GetShopsAssignedToUser(userGuid);
        }
        
        public OnlineCatalog.Common.DataContracts.Groups.ShopDto[] GetShopsAssignedToUserByLogin(string login) {
            return base.Channel.GetShopsAssignedToUserByLogin(login);
        }
    }
}
