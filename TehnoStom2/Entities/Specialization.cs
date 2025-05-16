using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TehnoStom2.Entities;

public class Specialization
{
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Title { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<Order> Orders { get; set; }
}
