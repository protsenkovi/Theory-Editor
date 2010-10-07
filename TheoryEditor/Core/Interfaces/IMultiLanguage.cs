using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protsenko.TheoryEditor.Core.Interfaces
{
    /// <summary>
    /// Все элементы, имеющие содержимое, зависящее от языковой культуры, должны реализовывать
    /// этот интерфейс и присваивать содержимому значения в методе LanguageResource_Updated.
    /// </summary>
    public interface IMultiLanguage
    {
        void LanguageResource_Updated(object language);
    }
}
