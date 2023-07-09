using Autofac;
using NUnit.Framework.Internal;

namespace IoC_Autofac.M2.ServiceLocator
{
    public class ClassWithServiceLocator {
        readonly ILogger _logger;
        readonly ISmartTracer _smartTracer;
        readonly INotifier _notifier;
        readonly IViewModelBase _viewModelBase;
        readonly Something.IRepository _repository;

        /*
         * Контейнер никогда не должен передаваться в методы
         */
        public ClassWithServiceLocator(IContainer container) {
            _logger = container.Resolve<ILogger>();
            _smartTracer = container.Resolve<ISmartTracer>();
            _notifier = container.Resolve<INotifier>();
            _viewModelBase = container.Resolve<IViewModelBase>();
            _repository = container.Resolve<Something.IRepository>();
        }
    }

    internal interface IRepository { }

    internal interface IViewModelBase { }

    internal interface INotifier { }

    internal interface ISmartTracer { }
}
