using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;

namespace ReactiveDemo
{
    public partial class PasswordTextfield : IViewFor<PasswordTextfieldViewModel>
    {
        public PasswordTextfield()
        {
            InitializeComponent();
            
            Button.Click += ButtonOnClick;
            
            ViewModel = new PasswordTextfieldViewModel();
            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel,
                    model => model.EncodedPasswordText,
                    view => view.PasswordBox.Text)
                    .DisposeWith(disposableRegistration);
                
                this.Bind(ViewModel,
                    model => model.PasswordText,
                    view => view.TextBox.Text)
                    .DisposeWith(disposableRegistration);
            });
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowPassword = !ViewModel.ShowPassword;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (PasswordTextfieldViewModel)value;
        }

        public PasswordTextfieldViewModel ViewModel { get; set; }
    }
}