using SharedUI_Lib.Images;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SharedUI_Lib.Controls
{
    [TemplatePart(Name = "PART_Image", Type = typeof(Image))]
    public class AppButton : Button
    {

        #region Private Fields

        private Image image;

        #endregion

        #region Constructors

        public AppButton()
        {
            DefaultStyleKey = typeof(AppButton);
        }

        #endregion

        #region Properties

        public AppButtonType ButtonType
        {
            get { return (AppButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(AppButtonType), typeof(AppButton),
            new PropertyMetadata(AppButtonType.Default, OnButtonTypePropertyChanged));

        private static void OnButtonTypePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((AppButton)sender).ApplyButtonType();
        }

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            image = GetTemplateChild("PART_Image") as Image;
            base.OnApplyTemplate();
            ApplyButtonType();
        }

        #endregion

        #region Private Methods

        private void ApplyButtonType()
        {
            if (image == null)
                return;
            switch (ButtonType)
            {
                case AppButtonType.Default:
                    break;
                case AppButtonType.Ok:
                    Content = "OK";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Ok));
                    break;
                case AppButtonType.Yes:
                    Content = "Ja";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Ok));
                    break;
                case AppButtonType.Cancel:
                    Content = "Abbrechen";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Cancel));
                    break;
                case AppButtonType.No:
                    Content = "Nein";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Cancel));
                    break;
                case AppButtonType.Close:
                    Content = "Schließen";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Cancel));
                    break;
                case AppButtonType.Add:
                    Content = "Neu";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Add));
                    break;
                case AppButtonType.Delete:
                    Content = "Löschen";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Delete));
                    break;
                case AppButtonType.Edit:
                    Content = "Bearbeiten";
                    image.SetValue(Image.SourceProperty, GetImage(ImageType.Edit));
                    break;
            }
            if (ButtonType != AppButtonType.Default)
                image.Visibility = Visibility.Visible;
        }

        private BitmapImage GetImage(ImageType imagetype)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var uriString = $"pack://application:,,,/{assemblyName};component/Images/{imagetype.ToString()}.png";
            var uri = new Uri(uriString, UriKind.Absolute);
            return new BitmapImage(uri);
        }

        #endregion

    }

    #region AppButtonType

    public enum AppButtonType
    {
        Default,
        Ok,
        Yes,
        Cancel,
        No,
        Close,
        Add,
        Delete,
        Edit
    }

    #endregion
}
