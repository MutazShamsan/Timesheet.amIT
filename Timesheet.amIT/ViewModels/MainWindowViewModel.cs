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

            _holidayApi = _container.Resolve<IHolidayProvider>();
            _container.RegisterType(typeof(ISheetGenerator), typeof(AmItSheetGenerator), new InjectionConstructor
            (
               _holidayApi,
                new DateTime(2018, 1, 1),
                new DateTime(2018, 12, 31)
            ));

            _generator = _container.Resolve<ISheetGenerator>();
            //(
            //    new ResolverOverride[]
            //    {
            //        new ParameterOverride(null, _holidayApi),
            //        new ParameterOverride(null, new DateTime(2018, 1, 1)),
            //        new ParameterOverride(null, new DateTime(2018, 12, 31))
            //    }
            //);

            Start();
            // _generator = generator;
            // _generator.Start("");

            //Interfaces.IHolidayProvider ss = new HolidayProvider.GoogleProvider();
            // holidays = ss.GetHolidays("My", "Kuala", ResourceManagement.GetResourceFileStream("Timesheet.amIT.credentials.json")).Result.ToList();
        }

        private void Start()
        {
            _generator.Start(Path.Combine(Common.ResourceManagement.GetCurrentExecution(), System.Configuration.ConfigurationManager.AppSettings["ExcelTemplateName"]));
        }

    }
}
