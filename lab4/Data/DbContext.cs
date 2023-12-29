using Classes.Accounts;
using Classes.Games;

namespace Classes.Data;

public class DbContext
{
    public List<GameAccountBase> GameAccounts { get; } = new();
    public List<GameBase> Games { get; } = new();
}