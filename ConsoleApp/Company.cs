using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Table("Company")]
    public class Company
    {
        public Company()
        {
            //Roles = new List<CompanyRole>();
            //CompanyLoanProgramGroups = new List<CompanyLoanProgramGroup>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The NMLS company ID, managed by an external system.
        /// </summary>
        [Index("IX_Company_NmlsCompanyId", IsUnique = true)]
        [Required]
        [MaxLength(254)]
        public string NmlsCompanyId { get; set; }

        [MaxLength(254)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Whether the company is active.
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Whom to send the Closing Connect email to.
        /// </summary>
        //public ClosingConnectEmailRecipient? ClosingConnectEmailRecipient { get; set; }

        /// <summary>
        /// Company roles.
        /// </summary>
        //public virtual ICollection<CompanyRole> Roles { get; set; }

        /// <summary>
        /// Company loan program groups.
        /// </summary>
        //public virtual ICollection<CompanyLoanProgramGroup> CompanyLoanProgramGroups { get; set; }

        /// <summary>
        ///Mandatory Delivery Net Worth 
        /// </summary>
        public decimal NetWorth { get; set; }

        /// <summary>
        ///Account Executive assigned to Company 
        /// </summary>
        [DefaultValue(0)]
        public int? AccountExecutiveId { get; set; }

        /// <summary>
        ///Customer Relationship Specialist assigned to Company 
        /// </summary>
        [DefaultValue(0)]
        public int? CustomerRelationshipSpecialistId { get; set; }
    }
}

