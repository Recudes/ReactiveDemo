using System;
using System.Reactive.Linq;
using System.Text;
using DynamicData.Binding;
using ReactiveUI;

namespace ReactiveDemo
{
    public class PasswordTextfieldViewModel :ReactiveObject
    {
        private string _passwordText;
        public string PasswordText
        {
            get => _passwordText;
            set => this.RaiseAndSetIfChanged(ref _passwordText, value);
        }

        private bool _showPassword;
        public bool ShowPassword
        {
            get => _showPassword;
            set => this.RaiseAndSetIfChanged(ref _showPassword, value);
        }

        private readonly ObservableAsPropertyHelper<string> _encodedPasswordText;

        public string EncodedPasswordText => _encodedPasswordText.Value;

        public PasswordTextfieldViewModel()
        {
            _encodedPasswordText = this
                .WhenAnyValue(model => model.PasswordText, model => model.ShowPassword)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Select(
                    s =>
                    {
                        var (passwordText, showPassword) = s;
                        if (string.IsNullOrEmpty(passwordText))
                            return "";
                        
                        return showPassword ? passwordText : new StringBuilder().Append('*', passwordText.Length).ToString();
                    })
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.EncodedPasswordText);
        }
    }
}