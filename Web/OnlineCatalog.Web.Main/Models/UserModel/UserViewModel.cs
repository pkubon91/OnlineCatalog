using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Web.Main.Models.UserModel
{
    public class UserViewModel
    {
        public Guid UserGuid { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [DisplayName("User Rank")]
        public string UserRank { get; set; }

        public bool ShopAssignmentIsPossible => UserRank == UserRankDto.ShopAdministrator.ToString();
    }
}