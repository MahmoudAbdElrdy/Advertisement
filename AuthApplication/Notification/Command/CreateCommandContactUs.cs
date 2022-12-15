using AutoMapper;
using Common.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApplication.Notification.Command
{
    public class AddContactUsCommand : IRequest<bool>
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
      

        class Handler : IRequestHandler<AddContactUsCommand, bool>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuditService _auditService; 
            public Handler(IAppDbContext context, IMapper mapper, IAuditService auditService)
            {
                _context = context;
                _mapper = mapper;
                  _auditService= auditService;
            }

            public async Task<bool> Handle(AddContactUsCommand request, CancellationToken cancellationToken)

            {
                if (_auditService.UserId == null)
                {
                    return false;
                }
                var UserId = _auditService.UserId;
                var ContactUs = new AuthDomain.Entities.ContactUs()
                { 
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    ClientId = UserId,
                    Content = request.Content,
                    Email = request.Email,
                    IsContact = request.IsContact,
                    Notes = request.Notes,
                    Phone = request.Phone,
                    ResponesAdmin = request.ResponesAdmin,
                    Title = request.Title,
                   
                };
                SendEmail(request.Email, request.ResponesAdmin, request.Title);
                await _context.Set<AuthDomain.Entities.ContactUs>().AddAsync(ContactUs, cancellationToken);
                return true;
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
