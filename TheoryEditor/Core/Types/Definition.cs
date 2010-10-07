using System;
using System.Collections.Generic;

using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core.Types
{
    [Serializable]
    class Definition : TheoryElement, ITheoryElement
    {
        protected Definition() { }

        public Definition(int theoryindex, string formulation, string template)
        {
            this.theoryindex = theoryindex;
            this.formulation = formulation;
            this.template = template;
            Updated += new Protsenko.TheoryEditor.Core.Events.Updated(Definition_Updated);
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
        }

        public void LanguageResource_Updated(object sender)
        {
            Updated(this);
        }

        void Definition_Updated(object sender)
        {

        }

        public TheoryTypes GetTheoryType()
        {
            return TheoryTypes.Definition;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is Definition))
                return false;

            Definition c = (Definition)obj;
            return ((c.theoryindex == theoryindex) && (c.formulation == formulation) && (c.template == template));
        }


        public override string ToString()
        {
            return LanguageResource.currentDictionary[Core.TheoryTypes.Definition.ToString()] + " " + theoryindex + " " + formulation;
        }

        #region Члены ITheoryElement

        public int Theoryindex
        {
            get { return theoryindex; }
            set { theoryindex = value; Updated(this); }
        }

        public string Formulation
        {
            get { return formulation; }
            set { formulation = value; Updated(this); }
        }

        public string Template
        {
            get { return template; }
            set { template = value; Updated(this); }
        }

        public int PositionX
        {
            get { return positionX; }
            set { positionX = value; Updated(this); }
        }

        public int PositionY
        {
            get { return positionY; }
            set { positionY = value; Updated(this); }
        }

        [field:NonSerialized()]
        public event Protsenko.TheoryEditor.Core.Events.Updated Updated;

        #endregion
    }
}
