using Protsenko.TheoryEditor.Core.Types;
using System.Collections.Generic;

namespace Protsenko.TheoryEditor.Core.Events
{
    public delegate void Updated(object sender);
    public delegate void ContentUpdated(object added, object deleted);
    public delegate void Accepted();
    public delegate void Focused(List<ITheoryElement> elements);
}