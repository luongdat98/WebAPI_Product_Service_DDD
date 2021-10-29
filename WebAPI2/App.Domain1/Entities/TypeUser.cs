using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class TypeUser
    {
        public int TypeUserId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public  ICollection<User> Users { get; set; }
    }
}