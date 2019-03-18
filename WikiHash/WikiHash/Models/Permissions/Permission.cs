using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiHash.Models.Permissions
{
    public class Permission
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; } = null;//null = everyone

        [Required]
        public PermissionTarget PermissionTarget { get; set; }
    }
}