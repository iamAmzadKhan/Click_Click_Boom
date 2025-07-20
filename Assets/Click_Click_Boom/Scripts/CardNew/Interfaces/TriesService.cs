public interface ITriesService
{
    void AddTries(int pts = 1);
    int CurrentTries { get; }
}