using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.RecyclerView;
using StarWarsSample.Droid.Extensions;
using StarWarsSample.Core.Resources;
using StarWarsSample.Core.ViewModels;

namespace StarWarsSample.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("starWarSample.droid.views.SpeciesView")]
    public class SpeciesView : BaseFragment<SpeciesViewModel>
    {
        protected override int FragmentId => Resource.Layout.SpeciesView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = Strings.TargetSpecies;

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.species_recycler_view);
            if(recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);

                recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.FetchSpeciesTask, () => ViewModel.FetchSpecieCommand);
            }

            return view;
        }        
    }
}