﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCatalog.Web.Main.BasketClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://online.catalog.com/", ConfigurationName="BasketClient.IBasketService")]
    public interface IBasketService {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IBasketService/AddProductToBasket", ReplyAction="https://online.catalog.com/IBasketService/AddProductToBasketResponse")]
        OnlineCatalog.Common.DataContracts.ServiceActionResult AddProductToBasket(System.Guid shopGuid, System.Guid productGuid, string loginUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://online.catalog.com/IBasketService/RemoveProductFromBasket", ReplyAction="https://online.catalog.com/IBasketService/RemoveProductFromBasketResponse")]
        OnlineCatalog.Common.DataContracts.ServiceActionResult RemoveProductFromBasket(System.Guid basketGuid, System.Guid productGuid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBasketServiceChannel : OnlineCatalog.Web.Main.BasketClient.IBasketService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BasketServiceClient : System.ServiceModel.ClientBase<OnlineCatalog.Web.Main.BasketClient.IBasketService>, OnlineCatalog.Web.Main.BasketClient.IBasketService {
        
        public BasketServiceClient() {
        }
        
        public BasketServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BasketServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BasketServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BasketServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public OnlineCatalog.Common.DataContracts.ServiceActionResult AddProductToBasket(System.Guid shopGuid, System.Guid productGuid, string loginUser) {
            return base.Channel.AddProductToBasket(shopGuid, productGuid, loginUser);
        }
        
        public OnlineCatalog.Common.DataContracts.ServiceActionResult RemoveProductFromBasket(System.Guid basketGuid, System.Guid productGuid) {
            return base.Channel.RemoveProductFromBasket(basketGuid, productGuid);
        }
    }
}
