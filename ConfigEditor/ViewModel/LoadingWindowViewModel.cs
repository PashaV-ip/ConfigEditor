using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.IO;
using ConfigEditor.Model;
using System.Threading;
using System.Windows.Controls;
using ConfigEditor.View.Windows;

namespace ConfigEditor.ViewModel
{
    public class LoadingWindowViewModel : BaseViewModel
    {
        #region Fields
        private ImageBrush _backgroundWindow = new ImageBrush(GetBackground());
        private double _loadingValueMax = 100;
        private double _loadingValue = 0;
        #endregion

        #region Properties
        public ImageBrush BackgroundWindow
        {
            get => _backgroundWindow;
            set
            {
                _backgroundWindow = value;
                OnPropertyChanged(nameof(BackgroundWindow));
            }
        }
        public double LoadingValueMax
        {
            get => _loadingValueMax;
            set
            {
                _loadingValueMax = value;
                OnPropertyChanged(nameof(LoadingValueMax));
            }
        }
        public double LoadingValue
        {
            get => _loadingValue;
            set
            {
                _loadingValue = value;
                OnPropertyChanged(nameof(LoadingValue));
            }
        }

        #endregion

        #region Methods
        private async Task StartLoading()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i <= LoadingValueMax; i++)
                {
                    LoadingValue = i;
                    Thread.Sleep(50);
                }
            });
            if(LoadingValue == LoadingValueMax)
            {
                new MainWindow().Show();
                foreach (LoadingWindow w in App.Current.Windows) w.Close();
            }
            
        }
        private static BitmapImage GetBackground()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = new Uri("../../../Source/Images/Backgrounds/Background.png", UriKind.Relative);
            image.EndInit();

            return image;

        }
        #endregion

        public LoadingWindowViewModel()
        {
            StartLoading();
        }
    }
}
