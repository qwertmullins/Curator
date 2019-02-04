using System;

namespace Curator
{
    public interface IPlayer
    {
        bool IsPlaying { get; }
        string Track { get; }
        event Action<string> OnTrackChange;
        event Action OnPausePlay;

        void Play(string track);
        void PausePlay();
    }
}