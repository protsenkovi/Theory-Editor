using System.Collections.Generic;

namespace Protsenko.TheoryEditor.Core.Interfaces
{
    interface IEAllocator
    {
        void Allocate(List<EVisualInfo> elements);
        void Allocate();
    }
}
