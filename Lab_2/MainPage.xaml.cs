using Microsoft.Maui;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Xsl;
using static System.Net.Mime.MediaTypeNames;

public interface ICloseApplication
{
    bool ConfirmClose();
}

namespace Lab_2
{
    public partial class MainPage : ContentPage
    {
        private string _filePath = "";
        private string _error = "";
        private Dictionary<string, List<string>> _data = new Dictionary<string, List<string>>
        {
            { "AuthorName", new List<string>() },
            { "Faculty", new List<string>() },
            { "Department", new List<string>() },
            { "DateOfBirth", new List<string>() },
            { "AuthorPosition", new List<string>() },
            { "Gender", new List<string>() },
            { "Address", new List<string>() },
            { "Age", new List<string>() },
            { "Branch", new List<string>() }
            //{ "StartOnPosition", new List<string>() },
            //{ "LastonPosition", new List<string>() }
        };
        private ObservableCollection<ScientificWorker> _results = new ObservableCollection<ScientificWorker>(); 

        public MainPage()
        {
            InitializeComponent(); 
        }

        private void AddElemToPicker(XmlNode n)
        {
            foreach (var item in _data)
            {
                Pickers.AddPickerValue(n, item.Key, item.Value);
            }
            
        }

        private void ClearCriterias()
        {
            foreach (var list in _data.Values)
            {
                list.Clear();
            }
        }

        private void EditPage_Disappearing(object? sender, EventArgs e)
        {
            Shell.Current.Navigating -= Current_Navigating;
        }

        private void EditPage_Appearing(object? sender, EventArgs e)
        {
            Shell.Current.Navigating += Current_Navigating;
        }

        async private void Current_Navigating(object? sender, ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                e.Cancel(); // Cancel original back navigation to allow for prompt to show

                if (await DisplayAlert("Ignore changes", "Are you sure you want to leave without saving changes?", "Yes", "No"))
                {
                    Shell.Current.Navigating -= Current_Navigating; // Avoid infinite confirmation loop

                    await Shell.Current.Navigation.PopAsync(); // Go back
                }
            }
        }

        private void ClearPickersValues()
        {
            authorPicker.ItemsSource = null;
            facultyPicker.ItemsSource = null;
            departmentPicker.ItemsSource = null;
            dateOfBirthPicker.ItemsSource = null;
            positionPicker.ItemsSource = null;
            //startPicker.ItemsSource = null;
            //lastPicker.ItemsSource = null;
            genderPicker.ItemsSource = null;
            addressPicker.ItemsSource = null;
            agePicker.ItemsSource = null;
            branchPicker.ItemsSource = null;
        }

        private void SortPickersValues()
        {
            foreach (var list in _data.Values)
            {
                list.Sort();
            }
        }

        private void AddItemSourses()
        {
            SortPickersValues();
            authorPicker.ItemsSource = _data["AuthorName"];
            facultyPicker.ItemsSource = _data["Faculty"];
            departmentPicker.ItemsSource = _data["Department"];
            dateOfBirthPicker.ItemsSource = _data["DateOfBirth"];
            positionPicker.ItemsSource = _data["AuthorPosition"];
            genderPicker.ItemsSource = _data["Gender"];
            addressPicker.ItemsSource = _data["Address"];
            agePicker.ItemsSource = _data["Age"];
            branchPicker.ItemsSource = _data["Branch"];
            //startPicker.ItemsSource = _data["StartOnPosition"];
            //lastPicker.ItemsSource = _data["LastonPosition"];
        }

        private void FillPickers(string xmlPath)
        {
            ClearCriterias();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("ScientificWorker");

            for (int i = 0; i < childNodes.Count; i++) 
            {
                XmlNode childNode = childNodes.Item(i);
                AddElemToPicker(childNode);
            }
            //Pickers.FillYearPickers("StartOnPosition", _data["StartOnPosition"]);
            //Pickers.FillYearPickers("LastonPosition", _data["LastonPosition"]);
            ClearPickersValues();
            AddItemSourses();

        }

        private void UpdateFilters()
        {
            nameInput.Text = "";
            authorPicker.SelectedItem = null;
            facultyPicker.SelectedItem = null;
            departmentPicker.SelectedItem = null;
            dateOfBirthPicker.SelectedItem = null;
            positionPicker.SelectedItem = null;
            genderPicker.SelectedItem = null;
            addressPicker.SelectedItem = null;
            agePicker.SelectedItem = null;
            branchPicker.SelectedItem = null;
            DOMOPt.IsChecked = true;
            SAXOpt.IsChecked = false;
            LINQOpt.IsChecked = false;
            _results.Clear();
            notFoundLabel.IsVisible = false;
        } 

        async private void OnPickFileClicked(object sender, EventArgs e)
        {
            _error = "";
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    string resultPath = result.FullPath;

                    if (File.Exists(resultPath))
                    {
                        string extension = Path.GetExtension(resultPath);
                        if (extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            _filePath = resultPath;
                            UpdateFilters();
                            FillPickers(_filePath);
                            if(!string.IsNullOrEmpty(_filePath) && string.IsNullOrEmpty(_error))
                            {
                                filters.IsVisible = true;
                            }
                            else
                            {
                                filters.IsVisible = false;
                            }
                        }
                        else
                        {
                            _error = "error";
                            await DisplayAlert("Помилка", "Файл не є з XML-розширенням.", "ОК");
                        }
                    }
                    else
                    {
                        _error = "error";
                        await DisplayAlert("Помилка", "Файлу не існує.", "ОК");
                    }
                }
            }
            catch (Exception ex)
            {
                _error = "error";
                await DisplayAlert("Помилка", $"{ex.Message}", "ОК");
            }
        }
        
        private async void OnTransformBtnClicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_filePath) && string.IsNullOrEmpty(_error))
            {
                XslCompiledTransform xct = new XslCompiledTransform();
                string fHTML = @"C:\Users\User\Source\Repos\Lab_2\HTML.html";
                try
                {
                    xct.Load(@"C:\Users\User\Source\Repos\Lab_2\XSLT.xslt");
                    xct.Transform(_filePath, fHTML);
                    await DisplayAlert("HTML", "Трансформацію закінчено", "ОК");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Помилка", $"{ex.Message}", "ОК");
                }
                
            }
        }

        private SearchCriteria FormCriteria()
        {
            SearchCriteria criteria = new SearchCriteria();

            criteria.Name = nameInput.Text ?? string.Empty;
            criteria.AuthorName = authorPicker.SelectedItem != null ? authorPicker.SelectedItem as string : string.Empty;
            criteria.AuthorPosition = positionPicker.SelectedItem != null ? positionPicker.SelectedItem as string : string.Empty;
            criteria.Faculty = facultyPicker.SelectedItem != null ? facultyPicker.SelectedItem as string : string.Empty;
            criteria.Department = departmentPicker.SelectedItem != null ? departmentPicker.SelectedItem as string : string.Empty;
            criteria.DateOfBirth = dateOfBirthPicker.SelectedItem != null ? dateOfBirthPicker.SelectedItem as string : string.Empty;
            criteria.Gender = genderPicker.SelectedItem != null ? genderPicker.SelectedItem as string : string.Empty;
            criteria.Address = addressPicker.SelectedItem != null ? addressPicker.SelectedItem as string : string.Empty;
            criteria.Age = agePicker.SelectedItem != null ? agePicker.SelectedItem as string : string.Empty;
            criteria.Branch = branchPicker.SelectedItem != null ? branchPicker.SelectedItem as string : string.Empty;

            return criteria;
        }

        private void OnCleanBtnClicked(object sender, EventArgs e)
        {
            UpdateFilters();
        }

        private async void OnSearchBtnClicked(object sender, EventArgs e)
        {
            ResultsListView.ItemsSource = null;
            SearchCriteria criteria=FormCriteria();

            ISearch analizator = new DOM();

            if (DOMOPt.IsChecked)
            {
                analizator = new DOM();
            }

            if (SAXOpt.IsChecked)
            {
                analizator = new SAX();
            }

            if (LINQOpt.IsChecked)
            {
                analizator = new LINQ();
            }

            try
            {
                _results = analizator.Search(criteria, _filePath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"{ex.Message}", "ОК");
            }

            ResultsListView.ItemsSource = _results;

            if(_results.Count > 0 && !string.IsNullOrEmpty(_filePath))
            {
                ResultsContainer.IsVisible = true;
                notFoundLabel.IsVisible = false;
            }
            else
            {
                ResultsContainer.IsVisible = false;
                if (!string.IsNullOrEmpty(_filePath))
                {
                    notFoundLabel.IsVisible = true;
                }
            }
            
        }
    }
}
