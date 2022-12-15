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
using Common.Localization;
using Common.Options;
using Infrastructure;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthApplication.Advertisement.Command
{
    public class ApplyForAdvertisementCommand : IRequest<bool>
    {
        public string AdId { get; set; }
       //public int NumberOfShowTimes { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<string> ServicesIds { get; set; }

        class Handler : IRequestHandler<ApplyForAdvertisementCommand, bool>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService auditService;
            private readonly IImageService _imageService;
            AppSettings _appSettings;
            private readonly ILocalizationProvider _localizationProvider;
            private readonly INotificationService notificationService;

            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService, IImageService imageService, IOptions<AppSettings> systemSetting, ILocalizationProvider localizationProvider, INotificationService notificationService)
            {
                this.auditService = auditService;
                _appSettings = systemSetting.Value;
                _context = context;
                _mapper = mapper;
                this._imageService = imageService;
                this.notificationService = notificationService;
                this.auditService = auditService;
                _localizationProvider = localizationProvider;
            }

            public async Task<bool> Handle(ApplyForAdvertisementCommand request, CancellationToken cancellationToken)
            {
                HashSet<PaidService> PaidServices = new HashSet<PaidService>();

                AdInterval adInterval = new AdInterval();
                var UserId = auditService.UserId;
                var Ad = _context.Ads.Include(x=>x.SpaceInfo).ThenInclude(x=>x.Client).FirstOrDefault(x=>x.Id==request.AdId);                     
                adInterval.FromDate = request.FromDate;
                adInterval.ToDate = request.ToDate;
                adInterval.AdId = request.AdId;
                adInterval.ClientId = UserId;
                adInterval.Price = Ad.Price;
                adInterval.PaidServices = PaidServices;

                if (!string.IsNullOrWhiteSpace(request.ServicesIds.FirstOrDefault()))
                {
                    foreach (var serviceid in request.ServicesIds)
                    {
                        PaidService p = new PaidService { ServiceId = serviceid };
                        PaidServices.Add(p);
                    };
                }

                //}
                _context.AdIntervals.Add(adInterval);
                _context.Ads.Update(Ad);
                await _context.SaveChangesAsync();
                decimal amount=0 ;
                var duration = ((request.ToDate - request.FromDate).TotalDays);
                //transaction
                List<Transaction> transactionList = new List<Transaction>();
                Random rd = new Random();
                int rand_num = rd.Next(9999);
                var ad = _context.Ads.Where(c => c.Id == request.AdId).Include("SpaceInfo").FirstOrDefault();
                if (duration > 30)
                {
                    amount = ad.Price * 30;
                }
                else
                {
                    amount = ad.Price * ((Decimal)duration);
                }
                transactionList.Add(new Transaction
                {
                    AdId = request.AdId,
                    Amount = amount,
                    TranType = TranType.Paid,
                    Dir = TranDir.Out,
                    UserId = auditService.UserId,
                    RefNo = rand_num.ToString()
                });

                transactionList.Add(new Transaction
                {
                    AdId = request.AdId,
                    Amount = amount,
                    TranType = TranType.Paid,
                    Dir = TranDir.In,
                    RefNo = rand_num.ToString(),
                    UserId = ad.SpaceInfo.ClientId
                });
                if (!string.IsNullOrWhiteSpace(request.ServicesIds.FirstOrDefault()) && request.ServicesIds.Count > 0)
                {
                    foreach (var item in request.ServicesIds)
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


                //end transaction

                //installment
                DateTime dueDate = DateTime.Now;
                var userId = auditService.UserId;
                if (duration > 30)
                {
                    int AdDuration = Convert.ToInt32(duration) / 30;
                    for (int i = 0; i < AdDuration; i++)
                    {
                        var installment = new Installment()
                        {
                            Amount = amount,
                            DueDate = dueDate,
                            IsPaid = i == 0 ? true : false,
                            PaymentType = i == 0 ? PaymentType.Pay : PaymentType.Refund,
                            AdIntervalId = adInterval.Id //AdClientId = request.AdClientId,
                        };
                        await _context.Set<Installment>().AddAsync(installment);
                        dueDate = dueDate.AddDays(30);
                    }
                }
                //end intallment
                await notificationService.Save(new AuthDomain.Entities.Notification.Notification()
                {
                    BodyAr = string.Format(_localizationProvider.Localize("RequestAdBody", "ar"), Ad.Id),
                    BodyEn = string.Format(_localizationProvider.Localize("RequestAdBody", "en"), Ad.Id),
                    //  Subject = string.Format("Complain In Order Number {0}", request.OrderId)
                    SubjectEn = _localizationProvider.Localize("RequestAdTitle", "en"),
                    SubjectAr = _localizationProvider.Localize("RequestAdTitle", "ar"),
                    From = auditService.UserId,
                    To = Ad.SpaceInfo.ClientId
                }, Ad.SpaceInfo.Client.WebToken);

                return true;
            }
        }
    }
}