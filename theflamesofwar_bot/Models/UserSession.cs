using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace theflamesofwar_bot.Models;
public class UserSession
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    [NotMapped] 
    public User Player { get; set; }
    public int Location { get; set; }
    public Guid TableId { get; set; }
    [NotMapped]
    public Table Table { get; set; }
    public DateTime DateTime { get; set; }
}
