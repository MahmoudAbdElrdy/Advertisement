using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces;
using Common.Options;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace AuthApplication.Advertisement.Command
{
    public class AddCommercialRecordCommand : IRequest<bool>
    {
        public string[] Images { get; set; }

        class Handler : IRequestHandler<AddCommercialRecordCommand, bool>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
            private readonly IImageService _imageService;
            AppSettings _appSettings;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting)
            {
                this.auditService = auditService;
                _appSettings = systemSetting.Value;
                _context = context;
                _mapper = mapper;
                this._imageService = imageService;
            }

            public async Task<bool> Handle(AddCommercialRecordCommand request, CancellationToken cancellationToken)
            {
                var UserId = auditService.UserId;
                 if (request.Images.Any())
                {
                    foreach (var image in request.Images)
                    {
                        ClientDocument clientDocument = new ClientDocument() { ClientId = UserId }; 
                       // await _imageService.SaveImageAsync(clientDocument, image);
                        clientDocument.Imageurl = image;
                        // await _imageService.SaveImageAsync(user.Avatar, request.Avatar);
                      //  await _context.Set<UserAvatar>().AddAsync(user.Avatar);
                        await _context.Set<ClientDocument>().AddAsync(clientDocument, cancellationToken);

                    }
                }

                return true;
            }
        }
    }
}