using Microsoft.Practices.Unity;
using OfficeOpenXml;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Timesheet.BusinessLogic;
using Timesheet.Interfaces;

namespace Timesheet.amIT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        ISheetGenerator _generator;
        IHolidayProvider _holidayApi;
        IUnityContainer _container;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IUnityContainer container)
        {
            _container = container;

            _generator = _container.Resolve<ISheetGenerator>();

            //_holidayApi = holidayApi;
            _holidayApi.StartIn(new DateTime(2018, 1, 1)).EndIn(new DateTime(2018, 12, 31));

           // _generator = generator;
           // _generator.Start("");

            //Interfaces.IHolidayProvider ss = new HolidayProvider.GoogleProvider();
            // holidays = ss.GetHolidays("My", "Kuala", ResourceManagement.GetResourceFileStream("Timesheet.amIT.credentials.json")).Result.ToList();
        }

    }
}
