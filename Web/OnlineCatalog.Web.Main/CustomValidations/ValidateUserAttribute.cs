using System;
using System.ComponentModel.DataAnnotations;
using OnlineCatalog.Web.Main.Models.UserModel;

namespace OnlineCatalog.Web.Main.CustomValidations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidateUserAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            RegisterViewModel model = value as RegisterViewModel;

            if (model == null) return base.IsValid(value);
            if (model.Password != model.RepeatPassword)
            {
                ErrorMessage = "Repate password must be the same as password";
                return false;
            }
            return true;
        }
    }
}