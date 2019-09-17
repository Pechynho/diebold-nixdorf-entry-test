using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Entity
{
    [Table("user")]
    class User : IEntity
    {
        public int ID { get; set; }

        [Column("username")]
        [Required()]
        [StringLength(150)]
        public string Username { get; set; }

        [Column("password")]
        [Required()]
        [StringLength(150)]
        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"{ID} | {Username} | {Password}";
        }
    }
}
