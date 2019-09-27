using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SATMovieBrowser.Controlls
{
    public class CustomStepper : StackLayout
    {

        Button PlusBtn;
        Button MinusBtn;
        Entry Entry;
        private EventHandler onValueChangedEvent = null;
        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 0,
              defaultBindingMode: BindingMode.TwoWay);

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public CustomStepper()
        {
            PlusBtn = new Button { WidthRequest = 30, HeightRequest = 30 };
            MinusBtn = new Button { WidthRequest = 30, HeightRequest = 30 };
            PlusBtn.Image = "exp20181029Artboard51";
            MinusBtn.Image = "exp20181029Artboard52";
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                case Device.Android:
                    {
                        PlusBtn.BackgroundColor = Color.Transparent;
                        MinusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
                case Device.iOS:
                    {
                        PlusBtn.BackgroundColor = Color.Transparent;
                        MinusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
            }
            Orientation = StackOrientation.Horizontal;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;
            Entry = new Entry { PlaceholderColor = Color.Gray, Keyboard = Keyboard.Numeric, WidthRequest = 30, BackgroundColor = Color.Transparent, FontSize = 15 };
            Entry.Keyboard = Keyboard.Numeric;
           // Entry.Behaviors.Add(new NumericValidationBehavior());
            Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
            Entry.HorizontalTextAlignment = TextAlignment.Center;
            Entry.TextChanged += Entry_TextChanged;
            Children.Add(MinusBtn);
            Children.Add(Entry);
            Children.Add(PlusBtn);
        }

        public event EventHandler OnValueChanged
        {
            add
            {
                onValueChangedEvent = null;
                onValueChangedEvent = value;
            }
            remove
            {
                // Will show a warning. You can ignore it.
                onValueChangedEvent = null;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue != ".")
                this.Text = int.Parse(e.NewTextValue);

            onValueChangedEvent?.Invoke(this, e);
        }
        //private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue != ".")
        //        this.Text = int.Parse(e.NewTextValue);
        //}

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text > 0)
                Text--;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            Text++;
        }

    }
}
