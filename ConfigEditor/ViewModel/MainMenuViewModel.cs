using ConfigEditor.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace ConfigEditor.ViewModel
{
    public class MainMenuViewModel : BaseViewModel
    {
        #region Delegates
        public delegate void DelegateDeleteConfigFile(UIElement element);
        public delegate void DelegateTranferProperty(UIElement element, bool selectedProperty);
        #endregion


        #region Fields
        private System.Windows.WindowState _stateWindow = System.Windows.WindowState.Normal;
        private ImageBrush _backgroundWindow = new ImageBrush(GetBackground());
        private IniFile _fileIni;

        #region SlideMenuFields
        private StackPanel _stackPanelSlideMenu;
        #endregion

        #region SettingsFields
        private string _pathToTheBackground = "";
        private System.Windows.Media.SolidColorBrush _colorPanels = new SolidColorBrush(GetColorPanels());
        private double _opacityPanels = GetOpacityPanels();
        #endregion

        #region VisibleFields
        private Visibility _referenceVisible = Visibility.Visible;
        private Visibility _infoVisible = Visibility.Hidden;
        private Visibility _optionsVisible = Visibility.Hidden;
        private Visibility _optionsConfigVisible = Visibility.Hidden;
        private Visibility _settingsPropertysVisible = Visibility.Hidden;
        private Visibility _workSpaceVisible = Visibility.Hidden;
        #endregion

        #region CreateConfigFields
        private string _configName;
        private ObservableCollection<ConfigFileElement> _configFileElementList = new ObservableCollection<ConfigFileElement>();
        private StackPanel _stackPanelConfigsList;
        #endregion

        #region ParametersConfigFields
        private ObservableCollection<PropertyElement> _selectedPropertiesList = new ObservableCollection<PropertyElement>();
        private ObservableCollection<PropertyElement> _filesPropertiesList = new ObservableCollection<PropertyElement>();
        private StackPanel _stackPanelSelectedPropertiesList;
        private StackPanel _stackPanelFilesPropertiesList;
        #endregion

        #region WorkSpaceFields
        private ObservableCollection<PropertyElement> _parametersConfigList = new ObservableCollection<PropertyElement>();
        private StackPanel _stackPanelParametersConfigList;


        #endregion


        #endregion

        #region Properties
        public System.Windows.WindowState StateWindow
        {
            get => _stateWindow;
            set
            {
                _stateWindow = value;
                OnPropertyChanged(nameof(StateWindow));
            }
        }
        public ImageBrush BackgroundWindow
        {
            get => _backgroundWindow;
            set
            {
                _backgroundWindow = value;
                OnPropertyChanged(nameof(BackgroundWindow));
            }
        }
        public IniFile FileIni
        {
            get => _fileIni;
            set
            {
                _fileIni = value;
            }
        }

        #region SlideMenuProperties
        public StackPanel StackPanelSlideMenu
        {
            get => _stackPanelSlideMenu;
            set
            {
                _stackPanelSlideMenu = value;
                StackPanelSlideMenu.Children.Clear();
                GetConfigs();
            }
        }
        #endregion

        #region SettingsProperties
        public string PathToTheBackground
        {
            get => _pathToTheBackground;
            set
            {
                _pathToTheBackground = value;
                OnPropertyChanged(nameof(PathToTheBackground));
            }
        }
        public System.Windows.Media.SolidColorBrush ColorPanels
        {
            get => _colorPanels;
            set
            {
                _colorPanels = value;
                OnPropertyChanged(nameof(ColorPanels));
            }
        }
        public double OpacityPanels
        {
            get => _opacityPanels;
            set
            {
                _opacityPanels = double.Parse(value.ToString("F2"));
                OnPropertyChanged(nameof(OpacityPanels));
            }
        }
        #endregion

        #region VisibleProperties
        public Visibility ReferenceVisible
        {
            get => _referenceVisible;
            set
            {
                _referenceVisible = value;
                OnPropertyChanged(nameof(ReferenceVisible));
            }
        }
        public Visibility InfoVisible
        {
            get => _infoVisible;
            set
            {
                _infoVisible = value;
                OnPropertyChanged(nameof(InfoVisible));
            }
        }
        public Visibility OptionsVisible
        {
            get => _optionsVisible;
            set
            {
                _optionsVisible = value;
                OnPropertyChanged(nameof(OptionsVisible));
            }
        }
        public Visibility OptionsConfigVisible
        {
            get => _optionsConfigVisible;
            set
            {
                _optionsConfigVisible = value;
                OnPropertyChanged(nameof(OptionsConfigVisible));
            }
        }
        public Visibility SettingsPropertysVisible
        {
            get => _settingsPropertysVisible;
            set
            {
                _settingsPropertysVisible = value;
                OnPropertyChanged(nameof(SettingsPropertysVisible));
            }
        }
        public Visibility WorkSpaceVisible
        {
            get => _workSpaceVisible;
            set
            {
                _workSpaceVisible = value;
                OnPropertyChanged(nameof(WorkSpaceVisible));
            }
        }
        #endregion

        #region CreateConfigProperties
        public string ConfigName
        {
            get => _configName;
            set
            {
                _configName = value;
                OnPropertyChanged(nameof(ConfigName));
            }
        }
        public ObservableCollection<ConfigFileElement> ConfigFileElementList
        {
            get => _configFileElementList;
            set
            {
                _configFileElementList = value;
            }
        }
        public StackPanel StackPanelConfigsList
        {
            get => _stackPanelConfigsList;
            set
            {
                _stackPanelConfigsList = value;
                OnPropertyChanged(nameof(StackPanelConfigsList));
            }
        }
        #endregion

        #region ParametersConfigProperties
        public ObservableCollection<PropertyElement> SelectedPropertiesList
        {
            get => _selectedPropertiesList;
            set
            {
                _selectedPropertiesList = value;
            }
        }
        public ObservableCollection<PropertyElement> FilesPropertiesList
        {
            get => _filesPropertiesList;
            set
            {
                _filesPropertiesList = value;
            }
        }
        public StackPanel StackPanelSelectedPropertiesList
        {
            get => _stackPanelSelectedPropertiesList;
            set
            {
                _stackPanelSelectedPropertiesList = value;
                OnPropertyChanged(nameof(StackPanelSelectedPropertiesList));
            }
        }
        public StackPanel StackPanelFilesPropertiesList
        {
            get => _stackPanelFilesPropertiesList;
            set
            {
                _stackPanelFilesPropertiesList = value;
                OnPropertyChanged(nameof(StackPanelFilesPropertiesList));
            }
        }
        #endregion

        #region WorkSpaceProperties
        public ObservableCollection<PropertyElement> ParametersConfigList
        {
            get => _parametersConfigList;
            set
            {
                _parametersConfigList = value;
                OnPropertyChanged(nameof(ParametersConfigList));
            }
        }

        public StackPanel StackPanelParametersConfigList
        {
            get => _stackPanelParametersConfigList;
            set
            {
                _stackPanelParametersConfigList = value;
                OnPropertyChanged(nameof(StackPanelParametersConfigList));
            }
        }


        #endregion

        #endregion







        #region Commands
        #region Controls commands

        public ICommand OpenQuestion
        {
            get
            {
                return new RelayCommand(() => {
                    OptionsConfigVisible = Visibility.Hidden;
                    WorkSpaceVisible = Visibility.Hidden;
                    SettingsPropertysVisible = Visibility.Hidden;
                    OptionsVisible = Visibility.Hidden;
                    InfoVisible = Visibility.Hidden;
                    ReferenceVisible = Visibility.Visible;
                });
            }
        }
        public ICommand OpenInfo
        {
            get
            {
                return new RelayCommand(() => {
                    OptionsConfigVisible = Visibility.Hidden;
                    WorkSpaceVisible = Visibility.Hidden;
                    SettingsPropertysVisible = Visibility.Hidden;
                    OptionsVisible = Visibility.Hidden;
                    ReferenceVisible = Visibility.Hidden;
                    InfoVisible = Visibility.Visible;
                });
            }
        }
        public ICommand OpenOptions
        {
            get
            {
                return new RelayCommand(() => {
                    OpacityPanels = GetOpacityPanels();
                    ColorPanels = new SolidColorBrush(GetColorPanels());
                    PathToTheBackground = "";
                    OptionsVisible = Visibility.Visible;
                });
            }
        }
        public ICommand MinimizeWindow
        {
            get
            {
                return new RelayCommand(() => { StateWindow = System.Windows.WindowState.Minimized; });
            }
        }
        public ICommand CloseApplication
        {
            get
            {
                return new RelayCommand(() => { foreach (System.Windows.Window w in App.Current.Windows) w.Close(); });
            }
        }
        #endregion

        #region SettingsCommands
        public ICommand SaveOptions
        {
            get
            {
                return new RelayCommand(() => {
                    FileIni.Write("ColorPanels", ColorPanels.ToString());
                    FileIni.Write("OpacityPanels", OpacityPanels.ToString());

                    if (PathToTheBackground != "" && new FileInfo(PathToTheBackground).Exists)
                    {

                        new FileInfo("../../../Source/Images/Backgrounds/Background.png").Delete();
                        File.Copy(PathToTheBackground, "../../../Source/Images/Backgrounds/Background.png");
                        BackgroundWindow = new ImageBrush(GetBackground());
                    }
                    OptionsVisible = Visibility.Hidden;
                });
            }
        }
        public ICommand CancleOptions
        {
            get
            {
                return new RelayCommand(() => {
                    OpacityPanels = GetOpacityPanels();
                    ColorPanels = new SolidColorBrush(GetColorPanels());
                    BackgroundWindow = new ImageBrush(GetBackground());
                    OptionsVisible = Visibility.Hidden;
                });
            }
        }


        public ICommand BrowseBackgroundImage
        {
            get
            {
                return new RelayCommand(() => {

                    var dialog = new OpenFileDialog();
                    dialog.Filter = "PNG (*.png)|*.png";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK && new FileInfo(dialog.FileName).Exists)
                    {
                        PathToTheBackground = dialog.FileName;
                        BitmapImage image = new BitmapImage(new Uri(PathToTheBackground, UriKind.Relative));
                        BackgroundWindow = new ImageBrush(image);
                    }
                });
            }
        }
        #endregion


        #region SlideMenuCommands
        public ICommand CreateConfig
        {
            get
            {
                return new RelayCommand(() => {
                    StackPanelConfigsList.Children.Clear();
                    ConfigFileElementList.Clear();
                    ConfigName = "";
                    OptionsConfigVisible = Visibility.Visible;
                });
            }
        }
        #endregion


        #region CreateConfigCommands
        public ICommand SaveOptionsConfig
        {
            get
            {
                return new RelayCommand(() => {
                    if (ConfigName == "")
                    {
                        System.Windows.MessageBox.Show("Имя конфига не может быть пустым!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor", ConfigName + ".ini")))
                    {
                        System.Windows.MessageBox.Show("Данное имя уже занято!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (ConfigFileElementList.Count > 0)
                        {
                            int coutConfigFiles = 0;
                            foreach (var element in ConfigFileElementList)
                            {
                                if (element.TextBoxConfigFilePath.Text != "" && File.Exists(element.TextBoxConfigFilePath.Text))
                                {
                                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor");
                                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                                    IniFile ini = new IniFile(Path.Combine(path, ConfigName + ".ini"));
                                    ini.Write(new FileInfo(element.TextBoxConfigFilePath.Text).Name, element.TextBoxConfigFilePath.Text, "ConfigsFiles");
                                    coutConfigFiles++;
                                }
                            }
                            if(coutConfigFiles > 0)
                            {
                                OptionsConfigVisible = Visibility.Hidden;
                                WorkSpaceVisible = Visibility.Hidden;
                                ReferenceVisible = Visibility.Hidden;
                                InfoVisible = Visibility.Hidden;
                                SettingsPropertysVisible = Visibility.Visible;
                                SelectedPropertiesList.Clear();
                                StackPanelSelectedPropertiesList.Children.Clear();
                                GetPropertysInSelectedConfig();
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Ни один добавленный файл не найден.\nУкажите корректный путь!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Добавьте минимум 1 конфиг файл!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Error);
                        } 
                    }
                });
            }
        }
        public ICommand CancleOptionsConfig
        {
            get
            {
                return new RelayCommand(() => {
                    OptionsConfigVisible = Visibility.Hidden;
                });
            }
        }
        public ICommand AddConfigFile
        {
            get
            {
                return new RelayCommand(() => {
                    AddConfigFileToList();
                });
            }
        }
        public ICommand SaveParametersConfig
        {
            get
            {
                return new RelayCommand(() => {
                    if(SelectedPropertiesList.Count > 0)
                    foreach(var element in SelectedPropertiesList)
                    {
                        IniFile iniConfig = new IniFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor", ConfigName + ".ini"));
                        iniConfig.Write(element.LabelPropertyName.Content.ToString(), element.PropertySectionConfig, element.ConfigName);
                    }
                    SettingsPropertysVisible = Visibility.Hidden;
                    WorkSpaceVisible = Visibility.Visible;
                    GetConfigs();
                    GetParametersForEdit();
                });
            }
        }
        #endregion

        #region WorkSpaceCommands
        public ICommand OpenConfig
        {
            get
            {
                return new RelayCommand<object>((parameter) => {
                    OptionsConfigVisible = Visibility.Hidden;
                    SettingsPropertysVisible = Visibility.Hidden;
                    OptionsVisible = Visibility.Hidden;
                    InfoVisible = Visibility.Hidden;
                    ReferenceVisible = Visibility.Hidden;

                    ConfigName = parameter.ToString();
                    WorkSpaceVisible= Visibility.Visible;
                    GetParametersForEdit();
                });
            }
        }
        public ICommand ApplyChang
        {
            get
            {
                return new RelayCommand<object>((parameter) => {
                    ApplyChangeInFiles();
                });
            }
        }
        public ICommand DeleteConfig
        {
            get
            {
                return new RelayCommand<object>((parameter) => {
                    WorkSpaceVisible = Visibility.Hidden;
                    StackPanelParametersConfigList.Children.Clear();
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor", ConfigName + ".ini"));
                    ConfigName = "";
                    GetConfigs();

                });
            }
        }

        #endregion

        #endregion

        #region Methods


        private void ApplyChangeInFiles()
        {
            foreach (var element in ParametersConfigList)
            {
                IniFile ini = new IniFile(element.ConfigName);
                ini.Write(element.LabelPropertyName.Content.ToString(), element.TextBoxValue.Text.ToString(), element.PropertySectionConfig);
            }
        }

        private void GetConfigs()
        {
            if(StackPanelSlideMenu.Children.Count > 0)
            {
                StackPanelSlideMenu.Children.Clear();
            }
            foreach (var config in new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor")).GetFiles().OrderBy(d => d.CreationTime))
            {
                System.Windows.Controls.Button button = new System.Windows.Controls.Button
                {
                    Template = (ControlTemplate)System.Windows.Application.Current.Resources["SlideMenuButton"],
                    Content = Path.GetFileNameWithoutExtension(config.FullName),
                    Command = OpenConfig,
                    CommandParameter = Path.GetFileNameWithoutExtension(config.FullName),
                    Resources = {
                            { "Img", new BitmapImage(new Uri("pack://application:,,,/Source/Images/config.png", UriKind.Absolute))},
                        }
                };

                StackPanelSlideMenu.Children.Add(button);
            }
        }
        private void GetParametersForEdit()
        {
            StackPanelParametersConfigList.Children.Clear();

            IniFile iniConfig = new IniFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor", ConfigName + ".ini"));
            foreach (var configFilesKey in iniConfig.GetKeys("ConfigsFiles"))
            {
                IniFile externalIniFile = new IniFile(iniConfig.Read(configFilesKey, "ConfigsFiles"));
                foreach (var key in iniConfig.GetKeys(iniConfig.Read(configFilesKey, "ConfigsFiles")))
                {
                    string text = externalIniFile.Read(key, iniConfig.Read(key, iniConfig.Read(configFilesKey, "ConfigsFiles")));

                    ParametersConfigList.Add(new PropertyElement(iniConfig.Read(configFilesKey, "ConfigsFiles"), key, iniConfig.Read(key, iniConfig.Read(configFilesKey, "ConfigsFiles")), text));
                    StackPanelParametersConfigList.Children.Add(ParametersConfigList[ParametersConfigList.Count - 1].GridLine);
                }
            }
        }
        private void GetPropertysInSelectedConfig()
        {
            StackPanelFilesPropertiesList.Children.Clear();
            
            IniFile iniConfig = new IniFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConfigEditor", ConfigName + ".ini"));
            if (iniConfig.GetSections().Count > 1)
            {
                foreach(var configFilesKey in iniConfig.GetKeys("ConfigsFiles"))
                {
                    foreach(var propertyKey in iniConfig.GetKeys(iniConfig.Read(configFilesKey, "ConfigsFiles")))
                    {
                        SelectedPropertiesList.Add(new PropertyElement(iniConfig.Read(configFilesKey, "ConfigsFiles"), propertyKey, iniConfig.Read(configFilesKey, "ConfigsFiles"), true, TranferProperty));
                        StackPanelSelectedPropertiesList.Children.Add(SelectedPropertiesList[SelectedPropertiesList.Count-1].GridLine);
                    }
                }
            }
            foreach (var configFilesKey in iniConfig.GetKeys("ConfigsFiles"))
            {
                StackPanelFilesPropertiesList.Children.Add(new System.Windows.Controls.Label
                {
                    FontSize = 20,
                    Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFC7C7C7"),
                    Content = configFilesKey,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                });
                IniFile externalIniFile = new IniFile(iniConfig.Read(configFilesKey, "ConfigsFiles"));
                foreach (var sectionInConfigsFile in externalIniFile.GetSections())
                {
                    foreach(var key in externalIniFile.GetKeys(sectionInConfigsFile))
                    {
                        if (!SelectedPropertiesList.Any(s => s.LabelPropertyName.Content.ToString() == key))
                        {
                            FilesPropertiesList.Add(new PropertyElement(iniConfig.Read(configFilesKey, "ConfigsFiles"), key, sectionInConfigsFile, false, TranferProperty));
                            StackPanelFilesPropertiesList.Children.Add(FilesPropertiesList[FilesPropertiesList.Count - 1].GridLine);
                        }
                    }
                    
                }
            }
        }








        private static void CreateMainSettingsINI()
        {
            IniFile ini = new IniFile("../../../Configs/Settings.ini");
            ini.Write("ColorPanels", "#FF000000");
            ini.Write("OpacityPanels", "0.75");
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
        private static double GetOpacityPanels()
        {
            if (!File.Exists("../../../Configs/Settings.ini"))
            {
                IniFile ini = new IniFile("../../../Configs/Settings.ini");
                CreateMainSettingsINI();
            }
            return double.Parse(new IniFile("../../../Configs/Settings.ini").Read("OpacityPanels").Replace('.', ','));
        }

        private static Color GetColorPanels()
        {
            if (!File.Exists("../../../Configs/Settings.ini"))
            {
                IniFile ini = new IniFile("../../../Configs/Settings.ini");
                CreateMainSettingsINI();
            }
            return (Color)ColorConverter.ConvertFromString(new IniFile("../../../Configs/Settings.ini").Read("ColorPanels"));
        }



        private void TranferProperty(UIElement element, bool selectedProperty)
        {
            if (selectedProperty)
            {
                var propertyElement = FilesPropertiesList.FirstOrDefault(s => s.GridLine == element);
                propertyElement.TransferredElement(selectedProperty);
                FilesPropertiesList.Remove(FilesPropertiesList.FirstOrDefault(s => s.GridLine == element));
                StackPanelFilesPropertiesList.Children.Remove(element);
                SelectedPropertiesList.Add(propertyElement);
                StackPanelSelectedPropertiesList.Children.Add(SelectedPropertiesList[SelectedPropertiesList.Count - 1].GridLine);
            }
            else
            {
                var propertyElement = SelectedPropertiesList.FirstOrDefault(s => s.GridLine == element);
                propertyElement.TransferredElement(selectedProperty);
                SelectedPropertiesList.Remove(SelectedPropertiesList.FirstOrDefault(s => s.GridLine == element));
                StackPanelSelectedPropertiesList.Children.Remove(element);
                FilesPropertiesList.Add(propertyElement);
                StackPanelFilesPropertiesList.Children.Add(FilesPropertiesList[FilesPropertiesList.Count - 1].GridLine);
                GetPropertysInSelectedConfig();
            }
            
            
        }


        private void AddConfigFileToList()
        {
            ConfigFileElementList.Add(new ConfigFileElement(DeleteConfigFileFromList));
            StackPanelConfigsList.Children.Add(ConfigFileElementList[ConfigFileElementList.Count - 1].GridLine);
        }
        private void DeleteConfigFileFromList(UIElement element)
        {
            ConfigFileElementList.RemoveAt(StackPanelConfigsList.Children.IndexOf(element));
            StackPanelConfigsList.Children.Remove(element);
        } 

        #endregion


        public MainMenuViewModel()
        {
            if (!File.Exists("../../../Configs/Settings.ini"))
            {
                IniFile ini = new IniFile("../../../Configs/Settings.ini");
                CreateMainSettingsINI();
            }
            FileIni = new IniFile("../../../Configs/Settings.ini");
        }
    }
}
