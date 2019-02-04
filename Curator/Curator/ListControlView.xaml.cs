using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListControlView : ContentView
    {
        private ISongList _currentList;
        private IPlayer _player;
        private Action _undo;
        private ObservableCollection<string> _pastSongs;
        public ListControlView()
        {
            InitializeComponent();
            _pastSongs = new ObservableCollection<string>();
            PreviousSongsList.ItemsSource = _pastSongs;
            var rawTracks = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                rawTracks.Add("track" + i);
            }

            var player = new DummyPlayer();
            player.OnPausePlay += () =>
            {
                CurrentTrackInfo.Text = $"{player.Track} {(player.IsPlaying ? "" : "(Paused)")}";
            };
            player.OnTrackChange += s =>
             {
                 _pastSongs.Add(player.Track);
                 CurrentTrackInfo.Text = s;
             };
            Init(new SongList(rawTracks, new List<string>(), new List<string>()), player);
        }

        public void Init(ISongList list, IPlayer player)
        {
            _currentList = list;
            _player = player;
            _player.Play(list.CurrentTrack);
        }

        public void OnThumbUp(object sender, EventArgs e)
        {
            _undo = () => { _currentList.UnApproveTrack(_currentList.CurrentTrack); };
            _currentList.ApproveTrack(_currentList.CurrentTrack);
        }

        public void OnThumbDown(object sender, EventArgs e)
        {
            var badTrack = _currentList.CurrentTrack;
            _undo = () =>
            {
                _currentList.IncludeTrack(badTrack);
                _player.Play(badTrack);
            };
            _currentList.ExcludeTrack(badTrack);
            _player.Play(_currentList.GetNextTrack());
        }

        public bool CanUndo => _undo != null;

        public void OnUndo(object sender, EventArgs e)
        {
            if (CanUndo)
            {
                _undo();
                _undo = null;
            }
        }

        public void OnBack(object sender, EventArgs e)
        {
            _player.Play(_currentList.GetPreviousTrack());
        }

        public void OnPausePlay(object sender, EventArgs e)
        {
            _player.PausePlay();
        }

        public void OnSkip(object sender, EventArgs e)
        {
            _player.Play(_currentList.GetNextTrack());
        }
    }

    public class DummyPlayer : IPlayer
    {
        public bool IsPlaying { get; set; }
        public string Track { get; set; }
        public event Action<string> OnTrackChange;
        public event Action OnPausePlay;

        public void Play(string track)
        {
            Track = track;
            IsPlaying = true;
            OnTrackChange?.Invoke(track);
        }

        public void PausePlay()
        {
            IsPlaying = !IsPlaying;
            OnPausePlay?.Invoke();
        }
    }
}