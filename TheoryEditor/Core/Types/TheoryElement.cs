using System;

namespace Protsenko.TheoryEditor.Core.Types
{
    [Serializable()]
    abstract class TheoryElement 
    {
        protected int theoryindex;
        protected string formulation;
        protected string template;
        protected int positionX;
        protected int positionY;
    }
}
