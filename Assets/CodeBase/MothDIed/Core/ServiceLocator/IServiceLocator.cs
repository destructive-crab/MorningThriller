using System;

namespace MothDIed.ServiceLocators
{
    public interface IServiceLocator
    {
        bool Contains(Type serviceType);

        object Get(Type serviceType);
    }
}