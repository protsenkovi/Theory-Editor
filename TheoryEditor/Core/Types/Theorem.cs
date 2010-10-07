using System;
using System.Collections.Generic;

using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core.Types
{
    [Serializable]
    class Theorem : TheoryElement, ITheoryElement
    {
        private string proof;
        public string Proof 
        {
            get { return proof; }
            set { proof = value; Updated(this); }
        }

        protected Theorem() { }

        public Theorem(int theoryindex, string formulation, string template, string proof)
        {
            this.theoryindex = theoryindex;
            this.formulation = formulation;
            this.template = template;
            this.proof = proof;
            Updated += new Protsenko.TheoryEditor.Core.Events.Updated(Theorem_Updated);
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
        }

        public void LanguageResource_Updated(object sender)
        {
            Updated(this);
        }

        void Theorem_Updated(object sender)
        {
        }

        public TheoryTypes GetTheoryType()
        {
            return TheoryTypes.Theorem;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is Theorem))
                return false;

            Theorem c = (Theorem)obj;
            return ((c.theoryindex == theoryindex) && (c.formulation == formulation) && (c.template == template) && (c.proof == proof));
        }

        public override string ToString()
        {
            return LanguageResource.currentDictionary[Core.TheoryTypes.Theorem.ToString()] + " " + Theoryindex + " " + Formulation + " " + proof;
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
