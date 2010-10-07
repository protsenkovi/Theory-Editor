using System.Collections.Generic;
using System;

namespace Protsenko.TheoryEditor.Core
{
    public enum TheoryTypes
    {
        Corollary,
        Definition,
        Lemma,
        Postulate,
        Theorem
    }

    public static class TheoryTypesClass
    {
        readonly static List<TheoryTypes> types;

        static TheoryTypesClass()
        {
            types = new List<TheoryTypes>();
            types.Add(TheoryTypes.Corollary);
            types.Add(TheoryTypes.Definition);
            types.Add(TheoryTypes.Lemma);
            types.Add(TheoryTypes.Postulate);
            types.Add(TheoryTypes.Theorem);
        }

        public static List<TheoryTypes> GetTypes()
        {
            return types;
        }

        public static TheoryTypes GetType(string stype)
        {
            if (stype == TheoryTypes.Corollary.ToString())
            {
                return TheoryTypes.Corollary;
            }
            else if (stype == TheoryTypes.Definition.ToString())
            {
                return TheoryTypes.Definition;
            }
            else if (stype == TheoryTypes.Lemma.ToString())
            {
                return TheoryTypes.Lemma;
            }
            else if (stype == TheoryTypes.Postulate.ToString())
            {
                return TheoryTypes.Postulate;
            }
            else if (stype == TheoryTypes.Theorem.ToString())
            {
                return TheoryTypes.Theorem;
            }
            throw new Exception("Such type doesn't exists.");
        }
    }
}