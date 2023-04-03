using Aveva.Core.Database;

namespace PsTrunniCalc
{
    public class Calculator
    {
        static DbAttribute StextAtt;
        static DbAttribute SctnStSupAtt;
        static DbAttribute MemDabDtxrAtt;
        static DbAttribute PsTrunniCalcAtt;
        public static void Start()
        {
            StextAtt = DbAttribute.GetDbAttribute(":STEXT");
            SctnStSupAtt = DbAttribute.GetDbAttribute(":SCTNSTSUP");
            MemDabDtxrAtt = DbAttribute.GetDbAttribute(":MEMDABDTXR");
            PsTrunniCalcAtt = DbAttribute.GetDbAttribute(":PsTrunniCalc");

            if (StextAtt == null || SctnStSupAtt == null || MemDabDtxrAtt == null || PsTrunniCalcAtt == null)
                return;


            DbPseudoAttribute.GetStringDelegate deleg = new DbPseudoAttribute.GetStringDelegate(Calculating);
            DbPseudoAttribute.AddGetStringAttribute(PsTrunniCalcAtt, DbElementTypeInstance.TRUNNION, deleg);
        }

        private static string Calculating(DbElement ele, DbAttribute pseudo, DbQualifier deleg)
        {
            var trancies = ele.Members(DbElementTypeInstance.TRANCILLARY);
            if (trancies == null || trancies.Length == 0)
            {
                return "unset";
            }

            var stext = trancies[0].GetAsString(DbAttributeInstance.STEX);
            ele.SetAttribute(StextAtt, stext);

            var sctnstsup = trancies[0].GetAsString(SctnStSupAtt);
            ele.SetAttribute(SctnStSupAtt, sctnstsup);

            var memDab = trancies[0].Members(DbElementTypeInstance.SSREFELEMENT);
            if (memDab == null || memDab.Length == 0)
                return "unset";

            var dtxrOfFirstMemdab = memDab[0].GetAsString(DbAttributeInstance.DTXR);
            ele.SetAttribute(MemDabDtxrAtt, dtxrOfFirstMemdab);

            return "unset";

        }

    }
}