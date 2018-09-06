using Timesheet.amIT.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Timesheet.Interfaces;
using Timesheet.HolidayProvider;
using Timesheet.BusinessLogic;
using System;

namespace Timesheet.amIT
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType(typeof(IHolidayProvider), typeof(GoogleProvider));
            //Container.RegisterType(typeof(ISheetGenerator), typeof(AmItSheetGenerator));

            //Container.Resolve<IHolidayProvider>();
            //Container.Resolve<ISheetGenerator>();

            base.ConfigureContainer();
            // this.RegisterTypeIfMissing(typeof(IHolidayProvider), typeof(GoogleProvider), false);
            // this.RegisterTypeIfMissing(typeof(ISheetGenerator), typeof(AmItSheetGenerator), true);
        }
    }
}
