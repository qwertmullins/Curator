namespace Curator
{
    public interface IPlayer
    {
        bool IsPlaying { get; }
        string Track { get; }

        void Play(string track);
        void PausePlay();
    }
}