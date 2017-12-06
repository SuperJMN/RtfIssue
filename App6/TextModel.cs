using ReactiveUI;

namespace App6
{
    public class TextModel : ReactiveObject
    {
        private string text;

        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);
        }
    }
}