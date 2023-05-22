using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace IPC.Correios.Application.Mappers
{
    public class AutoMapperConfig 
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EnderecoToViewModelProfile>();
            });
        }
    }
}
