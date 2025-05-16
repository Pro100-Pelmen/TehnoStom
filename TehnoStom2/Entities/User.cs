using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TehnoStom2.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string FirstName { get; set; }

    [MaxLength(128)]
    public string MiddleName { get; set; }

    [Required]
    [MaxLength(128)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(128)]
    public string Login { get; set; }

    [Required]
    [MaxLength(128)]
    public string Password { get; set; }

    [Required]
    public string Phone { get; set; }

    public int SpecializationId { get; set; }
    public Specialization Specialization { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public int Score { get; set; } = 0;
    public ICollection<Order> Orders { get; set; }
}

