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
        public User Student { get; set; } // Assuming you have a User entity

        [ForeignKey("Sport")]
        public Guid SportId { get; set; }  // Remove any reference to SportId1
        public Sport Sport { get; set; }

        public DateTime ReservationDate { get; set; }

        public DayOfWeekEnum DayBooking { get; set; }
        public TimeSpan HourStart { get; set; }
        public TimeSpan HourEnd { get; set; }

        public DateOnly OnlyDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public List<Guid> StudentIdList { get; set; }

        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
    }
}
