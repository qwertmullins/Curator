using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListControlView : ContentView
	{
		public ListControlView ()
		{
			InitializeComponent ();
		}

        private void OnThumbUp(object sender, System.EventArgs e)
        {
        }

        private void OnThumbDown(object sender, System.EventArgs e)
        {

        }

        private void OnUndo(object sender, System.EventArgs e)
        {

        }

        private void OnBack(object sender, System.EventArgs e)
        {

        }

        private void OnPausePlay(object sender, System.EventArgs e)
        {

        }

        private void OnSkip(object sender, System.EventArgs e)
        {

        }
    }
}