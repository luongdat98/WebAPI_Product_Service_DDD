using System.Collections.Generic;

namespace App.Domain.Dtos
{
    public class TypeUserDto
    {
        public int TypeUserId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
    }
}
