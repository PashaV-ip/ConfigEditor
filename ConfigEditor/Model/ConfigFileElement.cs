using ConfigEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.Windows.Forms;

namespace ConfigEditor.Model
{
    public class ConfigFileElement
    {
        private Grid _gridLine;
        private System.Windows.Controls.TextBox _textBoxConfigFilePath;
        private StackPanel _stackPanelControlButtonsForConfig;
        private System.Windows.Controls.Button _buttonBrowseConfigFile;
        private System.Windows.Controls.Button _buttonDeleteConfigFile;
        private System.Windows.Controls.Button _buttonCheckSaveConfigFile;

        private MainMenuViewModel.DelegateDeleteConfigFile _deleteConfig;
        public Grid GridLine
        {
            get => _gridLine;
            set
            {
                _gridLine = value;
            }
        }
        public System.Windows.Controls.TextBox TextBoxConfigFilePath
        {
            get => _textBoxConfigFilePath;
            set
            {
                _textBoxConfigFilePath = value;
            }
        }
        public StackPanel StackPanelControlButtonsForConfig
        {
            get => _stackPanelControlButtonsForConfig;
            set
            {
                _stackPanelControlButtonsForConfig = value;
            }
        }
        public System.Windows.Controls.Button ButtonBrowseConfigFile
        {
            get => _buttonBrowseConfigFile;
            set
            {
                _buttonBrowseConfigFile = value;
            }
        }
        public System.Windows.Controls.Button ButtonDeleteConfigFile
        {
            get => _buttonDeleteConfigFile;
            set
            {
                _buttonDeleteConfigFile = value;
            }
        }

        public ConfigFileElement(MainMenuViewModel.DelegateDeleteConfigFile delete)
        {
            _deleteConfig = delete;
            GridLine = new Grid
            {
                Margin = new Thickness(20, 10, 5, 0)
            };
            GridLine.ColumnDefinitions.Add(new ColumnDefinition
            {

            });
            GridLine.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(150)
            });
            TextBoxConfigFilePath = new System.Windows.Controls.TextBox
            {
                Name = "TextBoxFolderPath",
                TextAlignment = TextAlignment.Center,
                Text = "",
                Style = (Style)System.Windows.Application.Current.Resources["TextBoxStyle"],
                Margin = new Thickness(10, 0, 10, 0),
                Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                CaretBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                Padding = new Thickness(0, 5, 0, 5),
                MinWidth = 105,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20,
                IsReadOnly = false 
            };
            Grid.SetColumn(TextBoxConfigFilePath, 0);

            StackPanelControlButtonsForConfig = new System.Windows.Controls.StackPanel
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new Thickness(0, 3, 0, 3)
            };
            Grid.SetColumn(StackPanelControlButtonsForConfig, 1);

            ButtonBrowseConfigFile = new System.Windows.Controls.Button
            {
                Width = 30,
                Height = 30,
                Focusable = false,
                Margin = new Thickness(5, 0, 0, 0),
                Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButton"],
                Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/folder.png", UriKind.Absolute))},
                        },
                Command = new RelayCommand(() =>
                {
                    var dialog = new OpenFileDialog();
                    dialog.Filter = "INI (*.ini)|*.ini";
                    DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        TextBoxConfigFilePath.Text = dialog.FileName;
                    }
                })
            };
            ButtonDeleteConfigFile = new System.Windows.Controls.Button
            {
                Width = 30,
                Height = 30,
                Focusable = false,
                Margin = new Thickness(10, 0, 0, 0),
                Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButton"],
                Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/delete.png", UriKind.Absolute))},
                        },
                Command = new RelayCommand(() =>
                {
                    _deleteConfig(GridLine);
                })
            };

            StackPanelControlButtonsForConfig.Children.Add(ButtonBrowseConfigFile);
            StackPanelControlButtonsForConfig.Children.Add(ButtonDeleteConfigFile);
            GridLine.Children.Add(TextBoxConfigFilePath);
            GridLine.Children.Add(StackPanelControlButtonsForConfig);
        }



        /*public ConfigFileElement(string GamePath, MainMenuViewModel.DelegateDeleteConfigFile delete)
        {
            _deleteConfig = delete;
            GridLine = new Grid
            {
                Margin = new Thickness(20, 10, 5, 0)
            };
            GridLine.ColumnDefinitions.Add(new ColumnDefinition
            {

            });
            GridLine.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(150)
            });
            TextBoxConfigFilePath = new System.Windows.Controls.TextBox
            {
                Name = "TextBoxFolderPath",
                TextAlignment = TextAlignment.Center,
                Text = GamePath,
                Style = (Style)System.Windows.Application.Current.Resources["TextBoxStyle"],
                Margin = new Thickness(10, 0, 10, 0),
                Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                CaretBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                Padding = new Thickness(0, 5, 0, 5),
                MinWidth = 105,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20,
                IsReadOnly = true
            };
            Grid.SetColumn(TextBoxConfigFilePath, 0);


            StackPanelControlButtonsForConfig = new System.Windows.Controls.StackPanel
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new Thickness(0, 3, 0, 3)
            };
            Grid.SetColumn(StackPanelControlButtonsForConfig, 1);

            ButtonBrowseConfigFile = new System.Windows.Controls.Button
            {
                Width = 30,
                Height = 30,
                Focusable = false,
                Margin = new Thickness(5, 0, 0, 0),
                Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButton"],
                Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/folder.png", UriKind.Absolute))},
                },
                Command = new RelayCommand(() =>
                {
                    var dialog = new FolderBrowserDialog();
                    DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        TextBoxConfigFilePath.Text = dialog.SelectedPath;
                    }
                })
            };
            ButtonDeleteConfigFile = new System.Windows.Controls.Button
            {
                Width = 30,
                Height = 30,
                Focusable = false,
                Margin = new Thickness(10, 0, 0, 0),
                Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButton"],
                Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/delete.png", UriKind.Absolute))},
                },
                Command = new RelayCommand(() =>
                {

                })
            };


            StackPanelControlButtonsForConfig.Children.Add(ButtonBrowseConfigFile);
            StackPanelControlButtonsForConfig.Children.Add(ButtonDeleteConfigFile);

            GridLine.Children.Add(TextBoxConfigFilePath);
            GridLine.Children.Add(StackPanelControlButtonsForConfig);
        }*/
    }
}
