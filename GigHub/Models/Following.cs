using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Following
    {
        [Key]
        [Column(Order = 1)]
        public string FollowerID { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeID { get; set; }

        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}