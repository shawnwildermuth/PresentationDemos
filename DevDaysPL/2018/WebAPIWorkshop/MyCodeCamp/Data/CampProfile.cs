using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Models;

namespace MyCodeCamp.Data
{
  public class CampProfile : Profile
  {
    public CampProfile()
    {
      CreateMap<Camp, CampModel>()
        .ReverseMap();

      CreateMap<Speaker, SpeakerModel>()
        .ReverseMap();

      CreateMap<Talk, TalkModel>()
        .ReverseMap();

    }
  }
}
