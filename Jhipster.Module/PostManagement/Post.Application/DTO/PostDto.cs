using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.DTO
{
    public class PostDto
    {
        public string Region { get; set; }
        public int Count { get; set; }
    }

    public class StatusDto
    {
        public int Status { get; set; }
        public int Count { get; set; }
    }
    public class SearchBoughtPostDTO
    {
        public string Id { get; set; } 
        public string? Titile { get; set; }
        public string? Description { get; set; }
        public List<string>? Image { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public double Price { get; set; }
        public bool? IsOpenFinance { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public string? Reason { get; set; }
        public DateTime? Order { get; set; }

        public DateTime? ApprovalDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? avatar { get; set; }
    }
    public class SearchSalePostDTO
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public string? Titile { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public double Price { get; set; }
        //Diện tích
        public double? Area { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public List<string>? Image { get; set; }
        public int IsOwner { get; set; }
        public string Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public string UserId { get; set; }
        public string? Reason { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? Order { get; set; }
        public string? MaxSale { get; set; }
        public string? MinSale { get; set; }
        public string? avatar { get; set; }
        public List<string>? Documents { get; set; }

    }

    public class WardDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public string? Description { get; set; }
        public DistrictDTO? District { get; set; }
    }

    public class DistrictDTO
    {
        public string? Name { get; set; }
    }
}
