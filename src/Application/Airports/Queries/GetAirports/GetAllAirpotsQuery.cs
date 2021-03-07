using AutoMapper;
using AutoMapper.QueryableExtensions;
using LowCostFligtsBrowser.Application.Common.Interfaces;
using LowCostFligtsBrowser.Application.Common.Security;
using LowCostFligtsBrowser.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Airports.Queries.GetAirports
{
   public class GetAllAirpotsQuery : IRequest<AirportVM>
    {

    }
    public class GetAllAirportsQueryHandler : IRequestHandler<GetAllAirpotsQuery, AirportVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAirportsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<AirportVM> Handle(GetAllAirpotsQuery request, CancellationToken cancellationToken)
        {
            var k = _context.Airports
                .AsNoTracking()
                .ProjectTo<AirportDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken);
            var toReturn = new AirportVM
            {
                Airports = k.Result
            };
            return Task.FromResult(toReturn);
           
            
        }
    }
}
