using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaManager.Areas.Identity.Pages.Account.Manage
{
    // Classe auxiliar para determinar qual link do menu lateral está ativo
    public static class ManageNavPages
    {
        public static string Index => "Index";
        public static string Email => "Email";
        public static string ChangePassword => "ChangePassword";
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";
        public static string PersonalData => "PersonalData";

        //Link para o cadastro de cartão
        public static string AddCard => "AddCard";

        public static string IndexNavClass(ViewContext viewContext) => GetNavClass(viewContext, Index);
        public static string EmailNavClass(ViewContext viewContext) => GetNavClass(viewContext, Email);
        public static string ChangePasswordNavClass(ViewContext viewContext) => GetNavClass(viewContext, ChangePassword);
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => GetNavClass(viewContext, TwoFactorAuthentication);
        public static string PersonalDataNavClass(ViewContext viewContext) => GetNavClass(viewContext, PersonalData);
        public static string AddCardNavClass(ViewContext viewContext) => GetNavClass(viewContext, AddCard); // NOVO

        private static string GetNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return activePage == page ? "active" : "";
        }
    }
}