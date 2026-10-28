using System.ComponentModel.DataAnnotations;

namespace TH_Lab01_QLSV.Models
{
    public class Learner
    {
        public Learner() { Enrollments = new HashSet<Enrollment>(); }

        public int LearnerID { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; } = "";

        [Display(Name = "Tên")]
        public string FirstMidName { get; set; } = "";

        [Display(Name = "Ngày nhập học")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Ngành")]
        public int MajorID { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}