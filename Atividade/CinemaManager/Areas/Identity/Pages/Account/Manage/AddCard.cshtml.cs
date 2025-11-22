using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CinemaManager.Models;

namespace CinemaManager.Areas.Identity.Pages.Account.Manage
{
    // Simplesmente herda do ProfileModel para ter acesso aos dados do usuário
    public class AddCardModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AddCardModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            return RedirectToPage(); 
        }
    }
}