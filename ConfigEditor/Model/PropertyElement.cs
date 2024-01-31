using ConfigEditor.ViewModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConfigEditor.Model
{
    public class PropertyElement
    {
        private string _configName;
        private string _propertySectionConfig;


        private Grid _gridLine;
        private System.Windows.Controls.Label _labelPropertyName;
        private System.Windows.Controls.Button _buttonTransferProperty;

        private System.Windows.Controls.TextBox _textBoxValue;

        private MainMenuViewModel.DelegateTranferProperty _transferProperty;
        
        public string ConfigName
        {
            get => _configName;
            set
            {
                _configName = value;
            }
        }
        public string PropertySectionConfig
        {
            get => _propertySectionConfig;
            set
            {
                _propertySectionConfig = value;
            }
        }
        public Grid GridLine
        {
            get => _gridLine;
            set
            {
                _gridLine = value;
            }
        }
        public System.Windows.Controls.Label LabelPropertyName
        {
            get => _labelPropertyName; 
            set
            {
                _labelPropertyName = value;
            }
        }
        public System.Windows.Controls.Button ButtonTransferProperty
        {
            get => _buttonTransferProperty;
            set
            {
                _buttonTransferProperty = value;
            }
        }
        public System.Windows.Controls.TextBox TextBoxValue
        {
            get => _textBoxValue;
            set
            {
                _textBoxValue = value;
            }
        }

        public void TransferredElement(bool itsSelectedProperty)
        {
            GridLine.Children.Clear();
            if (itsSelectedProperty)
            {
                GridLine.ColumnDefinitions.Clear();
                GridLine.ColumnDefinitions.Add(new ColumnDefinition());
                GridLine.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(50)
                });

                ButtonTransferProperty = new System.Windows.Controls.Button
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButtonWithBackground"],
                    Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/right arrow.png", UriKind.Absolute))},
                        },
                    Command = new RelayCommand(() =>
                    {
                        _transferProperty(GridLine, false);
                    })
                };
                Grid.SetColumn(ButtonTransferProperty, 1);
                Grid.SetColumn(LabelPropertyName, 0);
                
            }
            else
            {
                GridLine.ColumnDefinitions.Clear();
                GridLine.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(50)
                });
                GridLine.ColumnDefinitions.Add(new ColumnDefinition());

                ButtonTransferProperty = new System.Windows.Controls.Button
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButtonWithBackground"],
                    Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/left arrow.png", UriKind.Absolute))},
                        },
                    Command = new RelayCommand(() =>
                    {
                        _transferProperty(GridLine, true);
                    })
                };
                Grid.SetColumn(ButtonTransferProperty, 0);
                Grid.SetColumn(LabelPropertyName, 1);
                
            }
            GridLine.Children.Add(ButtonTransferProperty);
            GridLine.Children.Add(LabelPropertyName);
        }

        public PropertyElement(string configName, string propertyName, string section, bool itsSelectedProperty, MainMenuViewModel.DelegateTranferProperty transfer )
        {
            _transferProperty = transfer;
            ConfigName = configName;
            PropertySectionConfig = section;
            GridLine = new Grid();
            LabelPropertyName = new System.Windows.Controls.Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Content = propertyName,
                Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20,
            };
            if (itsSelectedProperty)
            {
                GridLine.ColumnDefinitions.Add(new ColumnDefinition());
                GridLine.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(50)
                });

                ButtonTransferProperty = new System.Windows.Controls.Button
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButtonWithBackground"],
                    Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/right arrow.png", UriKind.Absolute))},
                        },
                    Command = new RelayCommand(() =>
                    {
                        _transferProperty(GridLine, false);
                    })
                };
                Grid.SetColumn(ButtonTransferProperty, 1);
                Grid.SetColumn(LabelPropertyName, 0);
            }
            else
            {
                GridLine.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(50)
                });
                GridLine.ColumnDefinitions.Add(new ColumnDefinition());

                ButtonTransferProperty = new System.Windows.Controls.Button
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Template = (ControlTemplate)System.Windows.Application.Current.Resources["SmallImageButtonWithBackground"],
                    Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/left arrow.png", UriKind.Absolute))},
                        },
                    Command = new RelayCommand(() =>
                    {
                        _transferProperty(GridLine, true);
                    })
                };
                Grid.SetColumn(ButtonTransferProperty, 0);
                Grid.SetColumn(LabelPropertyName, 1);
            }
            GridLine.Children.Add(ButtonTransferProperty);
            GridLine.Children.Add(LabelPropertyName);            

        }
        public PropertyElement(string configName, string propertyName, string section, string valueForKey)
        {
            ConfigName = configName;
            PropertySectionConfig = section;
            GridLine = new Grid();
            LabelPropertyName = new System.Windows.Controls.Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Content = propertyName,
                Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 25,
                Margin = new Thickness(30, 0, 30, 0)
            };      
            GridLine.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(500)
            });
            GridLine.ColumnDefinitions.Add(new ColumnDefinition());
            GridLine.Children.Add(new Border
            {
                BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                BorderThickness = new Thickness(0, 0, 1, 0),
                CornerRadius = new CornerRadius(0)
            });
            Border border = new Border
            {
                BorderBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(3)
            };
            Grid.SetColumnSpan(border, 2);
            GridLine.Children.Add(border);

            TextBoxValue = new System.Windows.Controls.TextBox
            {
                Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                CaretBrush = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                Padding = new Thickness(0, 5, 0, 5),
                Margin = new Thickness(3),
                FontSize = 25,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)System.Windows.Application.Current.Resources["TextBoxStyle"],
                Text = valueForKey
            };
            Grid.SetColumn(TextBoxValue, 1);
            Grid.SetColumn(LabelPropertyName, 0);
            GridLine.Children.Add(TextBoxValue);
            GridLine.Children.Add(LabelPropertyName);
        }
    }
}
