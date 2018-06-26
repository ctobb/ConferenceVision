using ConferenceVision.Models;
using ConferenceVision.Services;
using ConferenceVision.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ConferenceVision
{
	public partial class App : Application
	{
		public const string APP_NAME = "ConferenceVision";

		public static DataStore DataStore;

		public App()
		{
#if DEBUG
			/*
             * Live Reload Preview is Windows only. On macOS it'll do nothing.
             * 1) Install the VS extension and follow the instructions here https://marketplace.visualstudio.com/items?itemName=Xamarin.XamarinLiveReload
            */
			try
			{
				LiveReload.Init();
			}
			catch { }
#endif
			InitializeComponent();

			InitDependencies();
			InitData();

			MainPage = new MasterView();
		}

		void InitDependencies()
		{
			DependencyService.Register<DataStoreService>();
			DependencyService.Register<VisionService>();
		}

		void InitData()
		{
			DependencyService.Get<DataStoreService>().Load(App.DataStore);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			AppCenter.Start("ios=fa5c7fbd-4460-4c50-8dc1-b27d3704ad57;" + "android=715e7104-de15-42eb-ab8c-a68f0e8a2214", typeof(Analytics), typeof(Crashes), typeof(Distribute));
		}

		protected override void OnSleep()
		{
			MessagingCenter.Send<App>(this, nameof(OnSleep));

		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
