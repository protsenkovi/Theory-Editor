using System.Collections.Generic;

namespace Protsenko.TheoryEditor.Core.Interfaces
{
    interface ITheoryContainer
    {
        Core.Types.ITheoryElement GetTheoryElement();
        List<Core.Types.ITheoryElement> GetTheoryElements();
    }
}
