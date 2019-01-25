using System;
using System.Collections.Generic;
using System.Linq;

namespace Curator
{
    public class SongList : ISongList
    {
        public List<string> Tracks;
        private int currentIndex;
        public List<string> ApprovedTracks;
        private List<string> _rawTracks;
        public List<string> ExcludedTracks;

        public SongList(List<string> rawTracks, List<string> excludedTracks, List<string> approvedTracks)
        {
            _rawTracks = rawTracks;
            ExcludedTracks = excludedTracks;
            ApprovedTracks = approvedTracks;
            Tracks = rawTracks.Where(a => !ExcludedTracks.Contains(a)).ToList();
        }

        void IncrementCurrentTrack()
        {
            currentIndex++;
            if (currentIndex >= Tracks.Count)
                currentIndex = 0;
        }

        void DecrementCurrentTrack()
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = Tracks.Count - 1;
        }
        public string GetNextTrack()
        {
            IncrementCurrentTrack();
            return Tracks[currentIndex];
        }

        public string GetPreviousTrack()
        {
            DecrementCurrentTrack();
            return Tracks[currentIndex];
        }

        public void ApproveTrack(string track)
        {
            ApprovedTracks.Add(track);
        }

        public void ExcludeTrack(string track)
        {
            ExcludedTracks.Add(track);
            Tracks.Remove(track);
        }

        public void IncludeTrack(string track)
        {
            Tracks.Add(track);
            ExcludedTracks.Remove(track);
        }

        public string CurrentTrack => Tracks[currentIndex];
    }
}
