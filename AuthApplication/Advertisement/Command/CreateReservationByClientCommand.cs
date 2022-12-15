using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthApplication.Regions.Dto;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Enums;
using AutoMapper;
using Common.Interfaces;
using Common.Options;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthApplication.Advertisement.Command
{
    public class CreateReservationByClientCommand : IRequest<bool>
    {
        public string AdId { get; set; }
        public int Duration { get; set; }
        public string[] Services { get; set; }
        public decimal Amount { get; set; }
        class Handler : IRequestHandler<CreateReservationByClientCommand, bool>
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

            public async Task<bool> Handle(CreateReservationByClientCommand request, CancellationToken cancellationToken)
            {
                List<Transaction> transactionList = new List<Transaction>(); 
                Random rd = new Random();
                int rand_num = rd.Next(9999);
                var ad = _context.Ads.Where(c=>c.Id==request.AdId).Include("SpaceInfo").FirstOrDefault();
                transactionList.Add(new Transaction
                {
                    AdId = request.AdId,
                    Amount = request.Amount,
                    TranType = TranType.Paid,
                    Dir = TranDir.Out,
                    UserId = auditService.UserId,
                    RefNo = rand_num.ToString()
                });
                transactionList.Add(new Transaction
                {
                    AdId = request.AdId,
                    Amount = request.Amount,
                    TranType = TranType.Paid,
                    Dir = TranDir.In,
                    RefNo = rand_num.ToString(),
                    UserId = ad.SpaceInfo.ClientId
                });
                if (!string.IsNullOrWhiteSpace(request.Services.FirstOrDefault())) 
                {
                    foreach (var item in request.Services)
                    {
                        var service = _context.Services.Find(item); 
                            transactionList.Add(new Transaction
                            {
                                ServiceId = item,
                                Amount = service.Price,
                                TranType = TranType.Paid,
                                Dir = TranDir.Out,
                                UserId = auditService.UserId,
                                RefNo = rand_num.ToString()
                            });
                            transactionList.Add(new Transaction
                            {
                                ServiceId = item,
                                Amount = service.Price,
                                TranType = TranType.Paid,
                                Dir = TranDir.In,
                                RefNo = rand_num.ToString(),
                                UserId = service.UserId
                            }); 
                    }  
                }

                _context.Transactions.AddRange(transactionList); 
                DateTime dueDate = DateTime.Now;
                var userId = auditService.UserId; 
                    if (request.Duration > 30)
                    {
                        int duration = request.Duration / 30;
                        for (int i = 0; i < duration; i++)
                        {
                            var installment = new Installment()
                            {
                                Amount = request.Amount,
                                DueDate = dueDate,
                                IsPaid = i == 0 ? true : false,
                                PaymentType = i == 0 ? PaymentType.Pay : PaymentType.Refund,
                                AdIntervalId = request.AdId //AdClientId = request.AdClientId,
                            };
                            await _context.Set<Installment>().AddAsync(installment);
                            dueDate = dueDate.AddDays(30);
                        } 
                    }  
                return true;
            }
        }
    }
}