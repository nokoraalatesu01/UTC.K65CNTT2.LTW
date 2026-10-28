namespace TH_Lab01_QLSV.Models
{
    public class Major
    {
        public Major() { Learners = new HashSet<Learner>(); }

        public int MajorID { get; set; }
        public string MajorName { get; set; } = "";
        public virtual ICollection<Learner> Learners { get; set; }
    }
}