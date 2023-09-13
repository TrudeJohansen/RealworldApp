using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities;
public class Order
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public virtual required Article Article { get; set; }

    public int UserId { get; set; }
    public virtual required User User { get; set; }

    public bool Physical { get; set; }
    public string? SnailMail { get; set; }
    public decimal TotalPrice { get; set; }
}
