using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GesColleges.Core;

namespace GesColleges.MVVM.ViewModel
{
    class MainViewModel : ObsevalObject
    {
        //Declaration des commandes pour les vues
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CollegeViewCommand { get; set; }
        public RelayCommand DepartemnetViewCommand { get; set; }
        public RelayCommand EnseignantViewCommand { get; set; }
        public RelayCommand EtudiantViewCommand { get; set; }
        public RelayCommand MatiereViewCommand { get; set; }
        public RelayCommand NoteViewCommand { get; set; }
        public RelayCommand SalleViewCommand { get; set; }
        // Declaration des ViewModel 
        public HomeViewModel HomeVm { get; set; }
        public CollegeViewModel CollegesVm { get; set; }
        public DepartementViewModel DepartementVm { get; set; }
        public EnseignantViewModel EnseignantVm { get; set; }
        public EtudiantViewModel EtudiantVm { get; set; }
        public MatiereViewModel MatiereVm { get; set; }
        public NoteViewModel NoteVm { get; set; }
        public SalleViewModel SalleVm { get; set; }

        private Object _CurrentView;

        public Object CurrentView
        {
            get { return _CurrentView; }
            set
            {
                _CurrentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            CollegesVm = new CollegeViewModel();
            DepartementVm = new DepartementViewModel();
            EnseignantVm = new EnseignantViewModel();
            EtudiantVm = new EtudiantViewModel();
            MatiereVm = new MatiereViewModel();
            NoteVm = new NoteViewModel();
            SalleVm = new SalleViewModel();
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVm; });
            CollegeViewCommand = new RelayCommand(o => { CurrentView = CollegesVm; });
            DepartemnetViewCommand = new RelayCommand(o => { CurrentView = DepartementVm; });
            EnseignantViewCommand = new RelayCommand(o => { CurrentView = EnseignantVm; });
            EtudiantViewCommand = new RelayCommand(o => { CurrentView = EtudiantVm; });
            MatiereViewCommand = new RelayCommand(o => { CurrentView = MatiereVm; });
            NoteViewCommand = new RelayCommand(o => { CurrentView = NoteVm; });
            SalleViewCommand = new RelayCommand(o => { CurrentView = SalleVm; });
        }
    }
}
