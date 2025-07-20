public interface IScoreService
{
    void AddPoint(int pts = 1);
    int CurrentScore { get; }
}
