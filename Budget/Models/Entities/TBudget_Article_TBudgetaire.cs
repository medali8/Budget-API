namespace budget.Models.Entities
{
    public class TBudget_Article_TBudgetaire
    {
        public long id { get; set; }
        public string Article { get; set; }
        public string Paragraphe { get; set; }
        public long TBudget_titre { get; set; }
        public string DesignationAB { get; set; }
        public string TBudget_titre_libelle { get; set; }
        public string SousParagraphe { get; set; }
        public string Responsable { get; set; }
        public string year { get; set; }
    }
}
