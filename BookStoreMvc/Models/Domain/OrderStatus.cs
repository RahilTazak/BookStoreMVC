﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Models.Domain
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusName { get; set; }
    }
}
