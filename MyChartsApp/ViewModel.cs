using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace MyChartsApp
{
    class ViewModel:ViewModelBase
    {
        private string _filePath;
        private string _name;
        private TypeUserControl _windowType;
        private ICommand _switchWindowCommand;
        private ICommand _openFileCommand;
        public ObservableCollection<Model> _models;
        public ICommand SwitchWindowCommand => _switchWindowCommand ?? (_switchWindowCommand = new RelayCommands(arg => SwitchWindow()));
        public ICommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new RelayCommands(arg => GetPath()));

        public TypeUserControl WindowType
        {
            get { return _windowType; }
            set
            {
                _windowType = value;
                OnPropertyChanged(nameof(WindowType));
            }
        }
        private void SwitchWindow()
        {
            if (WindowType == TypeUserControl.Second)
            {
                WindowType = TypeUserControl.First;
            }
            else
            {
                WindowType = TypeUserControl.Second;
            }
        }
        public string FilePath
        {
            get { return _filePath; }
            set 
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public ObservableCollection<Model> Models
        {
            get { return _models; }
            set 
            {
                _models = value;
                OnPropertyChanged(nameof(Models));
            }
        }
        private void GetPath()
        {
            OpenFileDialog fn = new OpenFileDialog();
            fn.Filter = "CSV Files (*.csv)|*.csv";
            if (fn.ShowDialog() == true)
            {
                FilePath = fn.FileName;
                Models= new ObservableCollection<Model>(GetData(FilePath));
            }
            else
            {
                MessageBox.Show("No chosen file");
            }
        }
        private IEnumerable<Model> GetData(string filePath)
        {
            int c = 0;
            string[] lines = File.ReadAllLines(filePath);

            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                if(c==0)
                {
                    Name = Convert.ToString(data[0])+"/"+ Convert.ToString(data[1]);
            c++;
                    return new Model(null,null);
                }
                else
                { 
                return new Model(Convert.ToString(data[0]), Convert.ToString(data[1]));
                }
            });
        }
    }
}
