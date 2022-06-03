using System.ComponentModel.DataAnnotations;

namespace AppleStore.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "User")]
        User = 0,

        [Display(Name = "Admin")]
        Admin = 1,
    }
}