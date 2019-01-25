namespace Curator
{
    public interface ISongList
    {
        string GetPreviousTrack();
        string GetNextTrack();
        string CurrentTrack { get; }
        void ExcludeTrack(string track);
        void ApproveTrack(string track);
        void IncludeTrack(string track);
    }
}