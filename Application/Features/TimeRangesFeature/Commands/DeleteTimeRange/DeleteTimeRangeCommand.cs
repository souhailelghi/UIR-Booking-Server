﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeRangesFeature.Commands.DeleteTimeRange
{
    public class DeleteTimeRangeCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public DeleteTimeRangeCommand(Guid id)
        {
            Id = id;
        }
    }
}
