using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Plugins.Color.Droid;
using StarWarsSample.Core;
using StarWarsSample.Core.ViewModels;

namespace StarWarsSample.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("starWarsSample.droid.views.SpecieView")]
    public class SpecieView : BaseFragment<SpecieViewModel>
    {
        protected override int FragmentId => Resource.Layout.SpecieView;

        private Android.Support.V7.Widget.Toolbar _toolbar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            _toolbar.SetTitleTextColor(AppColors.AccentColor.ToAndroidColor());

            this.AddBindings(_toolbar, "Title Specie.Name");

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
        }
    }
}