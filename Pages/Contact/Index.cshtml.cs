﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesBook.Models;
using RazorPagesBook.Data;
using System.ComponentModel.DataAnnotations;


namespace RazorPagesBook.Pages.Contact
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesBookContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public IndexModel(RazorPagesBookContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public string SupportEmail => "w6rkmail@gmail.com";
        public string Phone => "+380 (66) 605-22-09";


        public class ContactInput
        {
            [Required, StringLength(80)]
            [Display(Name = "Ваше ім’я")]
            public string Name { get; set; } = string.Empty;


            [Required, EmailAddress, StringLength(120)]
            [Display(Name = "Ел. пошта")]
            public string Email { get; set; } = string.Empty;


            [Required, StringLength(120)]
            [Display(Name = "Тема")]
            public string Subject { get; set; } = string.Empty;


            [Required, StringLength(4000)]
            [Display(Name = "Повідомлення")]
            public string Message { get; set; } = string.Empty;


            public string? Website { get; set; }
        }


        [BindProperty]
        public ContactInput Input { get; set; } = new();


        [TempData]
        public string? OkMessage { get; set; }


        public async Task OnGetAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    Input.Email = user.Email ?? Input.Email;
                    Input.Name = user.UserName ?? Input.Name;
                }
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();


            if (!string.IsNullOrWhiteSpace(Input.Website))
            {
                OkMessage = "Дякуємо, ваше повідомлення надіслано.";
                return RedirectToPage();
            }


            var feedback = new Feedback
            {
                Name = Input.Name.Trim(),
                Email = Input.Email.Trim(),
                Subject = Input.Subject.Trim(),
                Message = Input.Message.Trim(),
                UserId = User.Identity?.IsAuthenticated == true
                    ? _userManager.GetUserId(User)
                    : null
            };


            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();


            OkMessage = "Дякуємо! Ваше повідомлення надіслано.";
            return RedirectToPage();
        }
    }
}
