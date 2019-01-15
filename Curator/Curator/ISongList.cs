namespace Curator
{
    public interface ISongList
    {
        string GetPreviousTrack();
        string GetNextTrack();

        void ExcludeTrack(string track);
        void ApproveTrack(string track);
        void Undo();
    }
}