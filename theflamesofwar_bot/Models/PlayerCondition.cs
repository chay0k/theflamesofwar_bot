using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace theflamesofwar_bot.Models;
public class PlayerCondition
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [NotMapped]
    public User User { get; set; }
    public Guid TableId { get; set; }
    [NotMapped]
    public Table Table { get; set; }
    public bool IsActive { get; set; }
    public int FullSpeed { get; set; }
    public int AvailableSpeed { get; set; }
    public int CurrentPosition { get; set; }

}
