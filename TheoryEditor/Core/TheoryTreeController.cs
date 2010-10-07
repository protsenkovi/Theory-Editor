using System;
using System.Collections.Generic;

using Protsenko.TheoryEditor.Core.Types;

namespace Protsenko.TheoryEditor.Core
{
    public class TheoryTreeController
    {
        private TheoryTree tree;


        public TheoryTreeController(TheoryTree tree)
        {
            this.tree = tree;
        }

        public void Add(ITheoryElement element)
        {
            tree.Add(element);
        }
        public void Add(ITheoryElement element, List<ITheoryElement> newbasis)
        {
            tree.Add(element, newbasis);
        }

        public void Add(TheoryTypes type, int theoryindex, string formulation, string template, string proof)
        {
            ITheoryElement element = null;
            switch (type)
            {
                case TheoryTypes.Corollary: element = new Corollary(theoryindex, formulation, template); break;
                case TheoryTypes.Definition: element = new Definition(theoryindex, formulation, template); break;
                case TheoryTypes.Lemma: element = new Lemma(theoryindex, formulation, template); break;
                case TheoryTypes.Postulate: element = new Postulate(theoryindex, formulation, template); break;
                case TheoryTypes.Theorem: element = new Theorem(theoryindex, formulation, template, proof); break;
            }
            tree.Add(element);
        }

        public void Add(TheoryTypes type, int theoryindex, string formulation, string template, string proof, List<ITheoryElement> newbasis)
        {
            ITheoryElement element = null;
            switch (type)
            {
                case TheoryTypes.Corollary: element = new Corollary(theoryindex, formulation, template); break;
                case TheoryTypes.Definition: element = new Definition(theoryindex, formulation, template); break;
                case TheoryTypes.Lemma: element = new Lemma(theoryindex, formulation, template); break;
                case TheoryTypes.Postulate: element = new Postulate(theoryindex, formulation, template); break;
                case TheoryTypes.Theorem: element = new Theorem(theoryindex, formulation, template, proof); break;
            }
            tree.Add(element, newbasis);
        }

        public void Edit(ITheoryElement element, int theoryindex, string formulation, string template, string proof)
        {
            ITheoryElement elementToEdit = tree.Search(element);
            elementToEdit.Theoryindex = theoryindex;
            elementToEdit.Formulation = formulation;
            elementToEdit.Template = template;
            if (elementToEdit is Theorem)
                ((Theorem)elementToEdit).Proof = proof;
        }

        public void Edit(ITheoryElement element,int theoryindex, string formulation, string template, string proof, List<ITheoryElement> newbasis)
        {
            ITheoryElement elementToEdit = tree.Search(element);
            elementToEdit.Theoryindex = theoryindex;
            elementToEdit.Formulation = formulation;
            elementToEdit.Template = template;
            if (elementToEdit is Theorem)
                ((Theorem)elementToEdit).Proof = proof;
            if (newbasis != null)
                tree.SetBasis(elementToEdit,newbasis);
        }

        public void Delete(ITheoryElement element)
        {
            tree.Remove(element);
        }

        public void Delete(TheoryTypes type, int theoryindex, string formulation, string template, string proof)
        {
            ITheoryElement element = null;
            switch (type)
            {
                case TheoryTypes.Corollary: element = new Corollary(theoryindex, formulation, template); break;
                case TheoryTypes.Definition: element = new Definition(theoryindex, formulation, template); break;
                case TheoryTypes.Lemma: element = new Lemma(theoryindex, formulation, template); break;
                case TheoryTypes.Postulate: element = new Postulate(theoryindex, formulation, template); break;
                case TheoryTypes.Theorem: element = new Theorem(theoryindex, formulation, template, proof); break;
            }
            tree.Remove(element);
        }

        public void SetTree(TheoryTree tree)
        {
            if (tree != null)
                this.tree = tree;
            else
                throw new Exception("Tree doesn't exists. tree = null;");
        }
    }
}
