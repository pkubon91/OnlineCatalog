using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Common.DataContracts.Products
{
    [DataContract(Namespace = "https://online.catalog.com/")]
    public class ProductDto
    {
        [DataMember]
        public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal DefaultPrice { get; set; }

        [DataMember]
        public int Tax { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte[] ProductImage { get; set; }

        [DataMember]
        public UserDto CreatedBy { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public DateTime Updated { get; set; }
    }
}