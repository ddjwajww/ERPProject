using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Message;
using SASSTS.Model.Entities;
using System.Linq.Expressions;

namespace SASSTS.Business.Implementations
{
    public class MessageBs : BusinessMap, IMessageBs
    {
        private readonly IExceptionValidDto<Message, MessageGetDto, MessageBs, MessagePostDto, MessageGetDto> _ms;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        public MessageBs(IExceptionValidDto<Message, MessageGetDto, MessageBs, MessagePostDto, MessageGetDto> ms, IUnitWork unitWork,
            IMapper mapper)
        {
            _unitWork = unitWork;
            _ms = ms;
            _mapper = mapper;
        }

        //Admin görmesi için listelenecek mesajlar
        public async Task<ApiResponse<List<MessageGetDto>>> GetAllMessage()
        {
            List<MessageGetDto> dto = new List<MessageGetDto>();

            var messages = await _unitWork.GetRepository<Message>().GetAllAsync();

            var dtoList = _mapper.Map<List<MessageGetDto>>(messages);

            foreach(var d in dtoList)
            {
                dto.Add(d);
            }

            var collection = dto.OrderByDescending(x => x.MessageTime).ToList();
            if(collection != null)
                return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, collection);
            return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
        }

        //Kullanıcılar için listelenecek olan getAll operasyonu burada elemanın authorityId değeri gelmelidir. Sessiondan gelen.
        public async Task<ApiResponse<List<MessageGetDto>>> GetAllMessageEmployeebyAuthorityId(int authorityId)
        {
            List<MessageGetDto> dto = new List<MessageGetDto>();
            Expression<Func<Message, bool>> predicate;

            switch (authorityId)
            {
                case 2:
                    predicate = x => x.MessageReceiver.Request == true;
                    var messages = await _unitWork.GetRepository<Message>().GetAllAsync(predicate: predicate);

                    var dtoList = _mapper.Map<List<MessageGetDto>>(messages);

                    foreach (var d in dtoList)
                    {
                        dto.Add(d);
                    }

                    var collection = dto.OrderByDescending(x => x.MessageTime).ToList();
                    if(collection != null)
                    return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, collection);
                    return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
                case 3: 
                    predicate = x => x.MessageReceiver.Success == true;
                    var m = await _unitWork.GetRepository<Message>().GetAllAsync(predicate: predicate);

                    var dl = _mapper.Map<List<MessageGetDto>>(m);

                    foreach (var d in dl)
                    {
                        dto.Add(d);
                    }

                    var c = dto.OrderByDescending(x => x.MessageTime).ToList();
                    if(c != null)
                    return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, c);
                    return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
                case 4:
                    predicate = x => x.MessageReceiver.Buying == true;
                    var mw = await _unitWork.GetRepository<Message>().GetAllAsync(predicate: predicate);

                    var dls = _mapper.Map<List<MessageGetDto>>(mw);

                    foreach (var d in dls)
                    {
                        dto.Add(d);
                    }

                    var cs = dto.OrderByDescending(x => x.MessageTime).ToList();
                    if(cs != null)
                    return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, cs);
                    return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
                case 5:
                    predicate = x => x.MessageReceiver.Management == true;
                    var me = await _unitWork.GetRepository<Message>().GetAllAsync(predicate: predicate);

                    var dle = _mapper.Map<List<MessageGetDto>>(me);

                    foreach (var d in dle)
                    {
                        dto.Add(d);
                    }

                    var cee = dto.OrderByDescending(x => x.MessageTime).ToList();
                    if(cee != null)
                    return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, cee);
                    return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
                case 6:
                    predicate = x => x.MessageReceiver.Success == true;
                    var ma = await _unitWork.GetRepository<Message>().GetAllAsync(predicate: predicate);

                    var dal = _mapper.Map<List<MessageGetDto>>(ma);

                    foreach (var d in dal)
                    {
                        dto.Add(d);
                    }

                    var ca = dto.OrderByDescending(x => x.MessageTime).ToList();
                    if(ca != null)
                    return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, ca);
                    return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
                case 7:
                    predicate = x => x.MessageReceiver.Committee == true;
                    var mq = await _unitWork.GetRepository<Message>().GetAllAsync(predicate: predicate);

                    var dlq = _mapper.Map<List<MessageGetDto>>(mq);

                    foreach (var d in dlq)
                    {
                        dto.Add(d);
                    }

                    var cq = dto.OrderByDescending(x => x.MessageTime).ToList();
                    if(cq != null)
                    return ApiResponse<List<MessageGetDto>>.Success(StatusCodes.Status200OK, cq);
                    return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
            }
            return ApiResponse<List<MessageGetDto>>.Fail(StatusCodes.Status200OK, "Mesajlar bulunamadı");
        }

        public async Task<ApiResponse<NoData>> InsertAllMessage(MessagePostDto dto)
        {

            var k = await _unitWork.GetRepository<Message>().InsertAsync(new Message{Subject = dto.Subject,Text = dto.Text});

            await _unitWork.GetRepository<MessageReceiver>().InsertAsync(new MessageReceiver
            {
                MessageId = k.Id,
                Request = dto.Request,
                Success = dto.Success,
                Buying = dto.Buying,
                Accounting = dto.Accounting,
                Committee = dto.Committee,
                Management = dto.Management
            });
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}