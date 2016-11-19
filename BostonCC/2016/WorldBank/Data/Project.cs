using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldBank.Data
{
  public class Project
  {
    public int Id { get; set; }
    public DateTime ApprovalDate { get; set; }
    public string Borrower { get; set; }
    public DateTime ClosingDate { get; set; }
    public string Country { get; set; }
    public int GrantAmount { get; set; }
    public string LendingInstrument { get; set; }
    public long LendingProjectCost { get; set; }
    public string ProductLine { get; set; }
    public string Abstract { get; set; }
    public string ProjectName { get; set; }
    public string RegionName { get; set; }
    public string Source { get; set; }
    public string Status { get; set; }
    public int TotalAmount { get; set; }
    public int TotalCommitmentAmount { get; set; }
    public string Url { get; set; }

    public ICollection<ProjectDocument> Documents { get; set; }

  }

  public class ProjectDocument
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
  }
}
