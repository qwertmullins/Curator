namespace Curator
{
    public interface ISongList
    {
        string GetPreviousTrack();
        string GetNextTrack();
        string CurrentTrack { get; }

        void IncludeTrack(string track);
        void ExcludeTrack(string track);

        void ApproveTrack(string track);
        void UnApproveTrack(string track);
    }
}