using Xamarin.Forms;

namespace PushupChallenge
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new PushupsViewModel();
            InitializeComponent();
        }
    }
}
