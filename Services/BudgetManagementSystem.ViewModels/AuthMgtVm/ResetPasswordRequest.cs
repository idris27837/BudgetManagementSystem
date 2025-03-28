﻿namespace BudgetManagementSystem.ViewModels.AuthMgtVm;

public class ResetPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    public string Code { get; set; }
}


public sealed class ConfirmEmailRequest
{
    [Required] public string UserId { get; set; }
    [Required] public string Code { get; set; }
}

public sealed class ResendConfirmationEmailRequest
{
    [Required] public string Email { get; set; }
    public string ClientHost { get; set; }
}