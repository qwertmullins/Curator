using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListControlView : ContentView
    {
        private ISongList _currentList;
        private string _currentTrack;
        private IPlayer _player;

        public ListControlView()
        {
            InitializeComponent();
        }

        public void SetSongList(ISongList list)
        {
            _currentList = list;
        }

        private void OnThumbUp(object sender, EventArgs e)
        {
            _currentList.ApproveTrack(_currentTrack);
        }

        private void OnThumbDown(object sender, EventArgs e)
        {
            _currentList.ExcludeTrack(_currentTrack);
        }

        private void OnUndo(object sender, EventArgs e)
        {
            _currentList.Undo();
        }

        private void OnBack(object sender, EventArgs e)
        {
            _currentTrack = _currentList.GetPreviousTrack();
            _player.Play(_currentTrack);
        }

        private void OnPausePlay(object sender, EventArgs e)
        {
            _player.PausePlay();
        }

        private void OnSkip(object sender, EventArgs e)
        {
            _currentTrack = _currentList.GetNextTrack();
            _player.Play(_currentTrack);
        }
    }
}