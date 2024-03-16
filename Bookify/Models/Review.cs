﻿using Bookify.Models.Abstractions;

namespace Bookify.Models
{
    public class Review : Entity
    {
        public static Review Create(Guid apartID, Guid bookingId, Guid userId, int rating, string comment, DateTime date)
        {
            return new Review(Guid.NewGuid(), apartID, bookingId, userId, rating, comment, date);
        }

        public Review() { }

        private Review(
            Guid id,
            Guid apartmentId, 
            Guid bookingId, 
            Guid userId, 
            int rating, 
            string comment, 
            DateTime createdOnUtc) : base(id)
        {
            ApartmentId = apartmentId;
            BookingId = bookingId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid ApartmentId { get; private set; }
        public Guid BookingId { get; private set; }
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
    }
}