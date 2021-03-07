 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhyzrStore.Branches
{
    public class CreateBranchDto
    {
        public Guid? ParentId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }



    }
    //public class UploidImage
    //{
    //    public IFormFile File { get; set; }
    //}
}
