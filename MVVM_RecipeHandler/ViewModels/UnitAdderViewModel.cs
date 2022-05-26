using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_EF6._0;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class UnitAdderViewModel : ViewModelBase
    {
        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAdderViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public UnitAdderViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // load ingredient data from db
            this.LoadUnits();

            // hookup command to assoiated methode
            this.AddUnitCommand = new ActionCommand(this.AddUnitCommandExecute, this.AddUnitCommandCanExecute);

            // subscribe to event
            EventAggregator.GetEvent<UnitDataChangedEvent>().Subscribe(this.OnUnitDataChanged, ThreadOption.UIThread);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all Unit data.
        /// </summary>
        public ObservableCollection<Unit> Units { get; set; }

        /// <summary>
        /// Gets the Add units button command.
        /// </summary>
        public ICommand AddUnitCommand { get; private set; }

        /// <summary>
        /// Gets or sets the NewUnit from Textbox
        /// </summary>
        public string NewUnit { get; set; }

        #endregion

        #region ------------- Events ----------------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current unit data.
        /// </summary>
        /// <param name="unit">Reference to the student data.</param>
        public void OnUnitDataChanged(Unit unit)
        {
            bool isNew = false;
            foreach (var item in this.Units)
            {
                if (unit.UnitName != item.UnitName)
                {
                    isNew = true;
                }
            }

            if (isNew)
            {
                this.Units.Add(unit);
            }
            else
            {
                var unitToUpdate = this.Units.FirstOrDefault(s => s.UnitName == unit.UnitName);
                unitToUpdate.UnitName = unit.UnitName;  
            }
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Generate Unit data from database.
        /// </summary>
        private void LoadUnits()
        {
            this.Units = new ObservableCollection<Unit>();
            using (var context = new RecipeContext())
            {
                var units = context.UnitsSet.SqlQuery("SELECT * FROM dbo.Units").ToList();
                foreach (var item in units)
                {
                    this.Units.Add(item);
                }
            }
        }
        #endregion

        #region ------------- Commands --------------------------------------------
        /// <summary>
        /// Determines, whether the add unit command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddUnitCommandCanExecute(object parameter)
        {
            Unit unit;
            unit = new Unit(this.NewUnit);
            Unit checkIfUnitExists = this.Units.FirstOrDefault(s => s.UnitName == unit.UnitName);

            if (checkIfUnitExists == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add unit" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddUnitCommandExecute(object parameter)
        {
            Unit unit;
            unit = new Unit(this.NewUnit);
            this.Units.Add(unit);
            EventAggregator.GetEvent<UnitDataChangedEvent>().Publish(unit);
            using (var context = new RecipeContext())
            {               
                    context.UnitsSet.Add(unit);
                    context.SaveChanges();
            }
        }
        #endregion
    } 
}
