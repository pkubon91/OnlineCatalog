using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OnlineCatalog.Common.DataContracts.Products
{
    [DataContract(Namespace = "https://online.catalog.com/")]
    public class ProductCategoryDto
    {
        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public DateTime CreatedDateTime { get; set; }

        [DataMember]
        public DateTime UpdatedDateTime { get; set; }

        [DataMember]
        public IEnumerable<ProductDto> AssignedProducts { get; set; }
    }
}
