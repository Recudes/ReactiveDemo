using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Media.Imaging;

namespace ReactiveDemo
{
    /// <summary>
    /// Interaktionslogik für NugetDetailsView.xaml
    /// </summary>
    public partial class NugetDetailsView : ReactiveUserControl<NugetDetailsViewModel>
    {
        public NugetDetailsView()
        {
            InitializeComponent();
            this.WhenActivated(disposableRegistration =>
            {
                // Our 4th parameter we convert from Url into a BitmapImage.
                // This is an easy way of doing value conversion using ReactiveUI binding.
                this.OneWayBind(ViewModel,
                        viewModel => viewModel.IconUrl,
                        view => view.iconImage.Source,
                        url => url == null ? null : new BitmapImage(url))
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.Title,
                        view => view.titleRun.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.Description,
                        view => view.descriptionRun.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                        viewModel => viewModel.OpenPage,
                        view => view.openButton)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}