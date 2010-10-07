using System;
using System.Collections.Generic;

using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core.Types
{
    [Serializable]
    class Postulate : TheoryElement, ITheoryElement
    {
        protected Postulate() { }

        public Postulate(int theoryindex, string formulation, string template)
        {
            this.theoryindex = theoryindex;
            this.formulation = formulation;
            this.template = template;
            Updated += new Protsenko.TheoryEditor.Core.Events.Updated(Postulate_Updated);
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
        }

        public void LanguageResource_Updated(object sender)
        {
            Updated(this);
        }

        void Postulate_Updated(object sender)
        {
        }

        public TheoryTypes GetTheoryType()
        {
            return TheoryTypes.Postulate;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is Postulate))
                return false;

            Postulate c = (Postulate)obj;
            return ((c.theoryindex == theoryindex) && (c.formulation == formulation) && (c.template == template));
        }

        public override string ToString()
        {
            return LanguageResource.currentDictionary[Core.TheoryTypes.Postulate.ToString()] + " " + Theoryindex + " " + Formulation;
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

        [field: NonSerialized()]
        public event Protsenko.TheoryEditor.Core.Events.Updated Updated;

        #endregion
    }
}
