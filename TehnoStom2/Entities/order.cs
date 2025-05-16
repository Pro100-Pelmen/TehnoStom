using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TehnoStom2.Entities;

public class Order
{
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Title { get; set; }

    [Required]
    [MaxLength(4096)]
    public string TechnicalRequirements { get; set; }

    public int SpecializationId { get; set; }
    public Specialization Specialization { get; set; }

    [MaxLength(16)]
    public string Status { get; set; } = "Ожидание";

    public int UserId { get; set; } = 1;
    public User User { get; set; }

    [Required]
    public int Score { get; set; }
}

