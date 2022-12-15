using AuthApplication.Notification.Dto;
using AutoMapper;
using Common;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.Notification.Command
{
  public  class EditContactUsCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ResponesAdmin { get; set; }
        public string Notes { get; set; }
        public bool? IsContact { get; set; }
        public string ClientId { get; set; }
        public string Id { get; set; }

        class EditContactUsCommandHandler : IRequestHandler<EditContactUsCommand, Result>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            public EditContactUsCommandHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result> Handle(EditContactUsCommand request1, CancellationToken cancellationToken)
            {

                var request = await _context.Set<AuthDomain.Entities.ContactUs>().FindAsync(request1.Id);
                request.ResponesAdmin = request1.ResponesAdmin;
                SendEmail(request.Email, request1.ResponesAdmin, request.Title);
                _context.Set<AuthDomain.Entities.ContactUs>().Update(request);

                return Result.Successed(_mapper.Map<ContactUsDto>(request));
            }
            private void SendEmail(string to, string body, string subject)
            {
               
                    System.Net.Mail.MailMessage MailMessageObj = new System.Net.Mail.MailMessage();
                    MailMessageObj.From = new System.Net.Mail.MailAddress("");
                    MailMessageObj.To.Add(to);
                    MailMessageObj.Subject = subject;
                    MailMessageObj.Body = body;
                    MailMessageObj.BodyEncoding = System.Text.Encoding.UTF8;
                    System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;//587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(MailMessageObj);

                
            }
        }
    }
}

