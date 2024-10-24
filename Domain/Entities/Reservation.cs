﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public User Student { get; set; } 

        [ForeignKey("Sport")]
        public Guid SportId { get; set; } 
        public Sport Sport { get; set; }

        public DateTime ReservationDate { get; set; }

        public DayOfWeekEnum DayBooking { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }

        public DateOnly OnlyDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public List<Guid>? StudentIdList { get; set; }

        // CodeUIRList property to store the list of CodeUIR strings
        public List<string> CodeUIRList { get; set; } // New property for CodeUIR

        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
    }
}
