using AuthApplication.Advertisement.Dto;
using AuthDomain.Entities.Ads;
using AutoMapper;
using Common;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.Advertisement.Command
{
 public   class AddRatingCommand : IRequest<RatingDto>
    {
        public string Id { get; set; }
        public string AdId { get; set; }
        public string ClientId { get; set; }
        public double? RatingValue { get; set; }
        public string RatingText { get; set; }

        class Handler : IRequestHandler<AddRatingCommand, RatingDto>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService _auditService;
            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService)
            {
                _context = context;
                _mapper = mapper;
                _auditService = auditService;
            }

            public async Task<RatingDto> Handle(AddRatingCommand request, CancellationToken cancellationToken)
            {
                var UserId = _auditService.UserId;

                var AdRating = new Rating()
                {
                    AdId = request.AdId,
                    Id = Guid.NewGuid().ToString(),
                    ClientId = UserId,
                    RatingValue=request.RatingValue,
                    RatingText=request.RatingText
                };

                var Rating = _context.Set<Rating>().FirstOrDefault(x => x.ClientId == UserId && x.AdId == request.AdId&&x.IsDeleted==false);
                if (Rating != null)
                {
                    Rating.RatingText = request.RatingText;
                    Rating.RatingValue = request.RatingValue;
                     _context.Set<Rating>().Update(Rating);
                }
                else
                {
                    await _context.Set<Rating>().AddAsync(AdRating, cancellationToken);

                }
              return _mapper.Map<RatingDto>(AdRating);
            }

           
        }

    }
}
