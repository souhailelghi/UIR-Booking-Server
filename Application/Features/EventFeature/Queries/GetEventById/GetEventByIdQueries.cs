﻿using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventFeature.Queries.GetEventById
{
    public class GetEventByIdQueries : IRequest<Event>
    {
        public Guid Id { get; set; }

        public GetEventByIdQueries(Guid id)
        {
            Id = id;

        }
    }
}
