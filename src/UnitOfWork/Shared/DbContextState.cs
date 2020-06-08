namespace UnitOfWork.Shared
{
    public enum DbContextState
    {
        Closed,
        Open,
        Committed,
        RolledBack
    }
}