using AuthApplication.Advertisement.Dto;
using AuthDomain.Entities.Ads;
using AutoMapper;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Common;

namespace AuthApplication.Advertisement.Command
{
   public class AddFavouriteCommand : IRequest<AddFavouriteDto>
    {
        public string AdId { get; set; }
        public string ClientId { get; set; }
        public bool? IsFavorite { get; set; }
        class Handler : IRequestHandler<AddFavouriteCommand, AddFavouriteDto>
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

            public async Task<AddFavouriteDto> Handle(AddFavouriteCommand request, CancellationToken cancellationToken) {
                var UserId = _auditService.UserId;

                var AdFavourite = new AdFavourite()
                {
                    AdId = request.AdId,
                    Id = Guid.NewGuid().ToString(),
                    ClientId=UserId
                };
              //  var check = favouriteCheck(UserId, request.AdId);
                if (request.IsFavorite==true&&request.AdId!=null&&UserId!=null)
                {
                    await _context.Set<AdFavourite>().AddAsync(AdFavourite, cancellationToken);

                }
                if (request.IsFavorite == false && request.AdId != null && UserId != null)
                {
                    var service =  _context.Set<AdFavourite>().FirstOrDefault(x=>x.AdId==request.AdId&&x.ClientId==UserId);
                    if (service == null)
                    {
                        throw new ApiException(ApiExeptionType.NotFound);
                    }
                    _context.AdFavourites.Remove(service);

                }

                return _mapper.Map<AddFavouriteDto>(AdFavourite);
            }
            //private bool favouriteCheck(string UserId, string AdId)
            //{

            //    var favouriteList = _context.Set<AdFavourite>().Where(x=>x.AdId==AdId&&x.ClientId==UserId).ToList();
            //    //.(x => x.ApplicationUserId == ApplicationUserId && x.DiscountId == DiscountId).ToList();
            //    if (favouriteList != null && favouriteList.Count >= 1)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }

            //}


        }
    
}
}
