using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SAT.DATA.EF
{
    #region Course
    public class CoursMetaData
    {
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters.")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }

        [StringLength(250, ErrorMessage = "Maximum length is 250 characters.")]
        public string Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "Maximum length is 500 characters.")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CoursMetaData))]
    public partial class Cours { }
    #endregion

    #region Enrollment
    public class EnrollmentMetadata
    {
        [Display(Name = "Enrollment ID")]
        public int EnrollmentId { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Scheduled Class ID")]
        public int ScheduledClassId { get; set; }

        [Required]
        [Display(Name = "Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    #endregion

    #region ScheduledClass
    public class ScheduledClassMetadata
    {
        [Display(Name = "Scheduled Class ID")]
        public int ScheduledClassId { get; set; }

        [Required]
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Instructor Name")]
        [StringLength(40, ErrorMessage = "Maximum length is 40 characters.")]
        public string InstructorName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Maximum length is 20 characters.")]
        public string Location { get; set; }

        [Required]
        public int SCSID { get; set; }

        [Display(Name = "Class Info")]
        public string classInfo { get; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {

        public string classInfo
        {
            get { return String.Format($"Start Date: {StartDate:d} Course: {CourseId} Location: {Location}"); }
        }
    }
    #endregion
        
    #region Student
    public class StudentMetadata
    {
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Maximum length is 20 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Maximum length is 20 characters.")]
        public string LastName { get; set; }

        [StringLength(15, ErrorMessage = "Maximum length is 15 characters.")]
        public string Major { get; set; }

        [StringLength(50, ErrorMessage = "Maximum length is 50 characters.")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "Maximum length is 25 characters.")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Maximum length is 2 characters.")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, ErrorMessage = "Maximum length is 10 characters.")]
        public string ZipCode { get; set; }

        [StringLength(13, ErrorMessage = "Maximum length is 13 characters.")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(60, ErrorMessage = "Maximum length is 60 characters.")]
        public string Email { get; set; }

        [Display(Name = "Student Photo")]
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters.")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Student Name")]
        public string fullName { get; }

        [Required]
        public int SSID { get; set; }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        public string fullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion

    #region StudentStatus
    public class StudentStatusMetadata
    {
        public int SSID { get; set; }

        [Required]
        [Display(Name = "Status")]
        [StringLength(30, ErrorMessage = "Maximum length is 30 characters.")]
        public string SSName { get; set; }

        [Display(Name = "Description")]
        [StringLength(250, ErrorMessage = "Maximum length is 250 characters.")]
        public string SSDescription { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }
    #endregion
}