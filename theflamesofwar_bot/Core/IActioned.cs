using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Core;

public interface IActioned
{
    public void ShowMessage();
    public void AskQuestion();
    public User GetCurrentUser();
    public Table GetCurrentTable();
}