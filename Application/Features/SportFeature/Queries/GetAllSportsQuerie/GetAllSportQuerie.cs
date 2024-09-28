﻿using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SportFeature.Queries.GetAllSportsQuerie
{
    public class GetAllSportQuerie : IRequest<List<Sport>>
    {
    }
}
