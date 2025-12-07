using System;
using Microsoft.Maui.Controls;

namespace BumbAlexandruFlaviuLab7
{
    public class ValidationBehaviour : Behavior<Editor>
    {
        protected override void OnAttachedTo(Editor entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Editor entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var editor = (Editor)sender;
            editor.BackgroundColor = string.IsNullOrEmpty(args.NewTextValue)
                ? Color.FromRgba("#AA4A44")  
                : Color.FromRgba("#FFFFFF");  
        }
    }
}
