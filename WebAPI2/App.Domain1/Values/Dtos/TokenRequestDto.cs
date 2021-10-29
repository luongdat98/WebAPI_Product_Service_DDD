using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos
{
    public class TokenRequestDto
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
