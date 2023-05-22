using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IPC.Correios.Application.ViewModels;
using IPC.Correios.Core.Entities;

namespace IPC.Correios.Application.Mappers
{
    public class EnderecoToViewModelProfile : Profile
    {
        public EnderecoToViewModelProfile()
        {
            CreateMap<EnderecoViewModel, Endereco>();
        }
    }
}
