using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PTU_Tools_Web.Data
{
    public class SheetModel
    {
        [Key]
        public string SheetId { get; set; }
        public string SheetTitle { get; set; }
        public bool HasAccess { get; set; }
        public IdentityUser Owner { get; set; }
    }
}
