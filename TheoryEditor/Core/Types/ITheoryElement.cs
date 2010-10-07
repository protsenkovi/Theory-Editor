using System;
using System.Collections.Generic;

using Protsenko.TheoryEditor.Core.Interfaces;

namespace Protsenko.TheoryEditor.Core.Types
{
    
    public interface ITheoryElement: IMultiLanguage
    {
        int Theoryindex { get; set; }
        string Formulation { get; set; }
        string Template { get; set; }
        Core.TheoryTypes GetTheoryType();
        int PositionX { get; set; }
        int PositionY { get; set; }
        event Core.Events.Updated Updated;
    }
}
