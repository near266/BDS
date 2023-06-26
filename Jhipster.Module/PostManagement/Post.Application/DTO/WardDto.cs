using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.DTO
{
    public class WardDtos
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public string? Description { get; set; }
        public string? DistrictId { get; set; }
        public District? District { get; set; }
    }
}
