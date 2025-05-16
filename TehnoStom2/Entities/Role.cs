using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TehnoStom2.Entities;

public class Role
{
    public int Id { get; set; }

    [Required]
    [MaxLength(16)]
    public string Title { get; set; }

    public ICollection<User> Users { get; set; }
}

