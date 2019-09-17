using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Entity
{
    interface IEntity
    {
        [Key()]
        [Column("id")]
        int ID { get; set; }
    }
}
