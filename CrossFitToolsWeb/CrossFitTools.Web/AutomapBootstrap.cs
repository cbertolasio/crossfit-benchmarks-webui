using System;
using System.Collections.Generic;
using System.Linq;
using CrossFitTools.Web.Models.Logger;
using CrossfitBenchmarks.Data.DataTransfer;

namespace CrossfitBenchmarks.WebUi
{
    public static class AutomapBootstrap
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.CreateMap<WorkoutLogEntryDto, BenchmarkItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkoutId))
                .ForMember(dest => dest.LastAttemptDate, opt => opt.MapFrom(src => src.LastEntry != null ? src.LastEntry.DateCreated : DateTime.MinValue ))
                .ForMember(dest => dest.LastScore, opt => opt.MapFrom(src => src.LastEntry != null ? src.LastEntry.Score : null))
                .ForMember(dest => dest.LastPersonalRecordDate, opt => opt.MapFrom(src => src.LastPersonalRecord != null ? src.LastPersonalRecord.DateCreated : DateTime.MinValue ))
                .ForMember(dest => dest.PersonalRecordScore, opt => opt.MapFrom(src => src.LastPersonalRecord != null ? src.LastPersonalRecord.Score : null ))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WorkoutName));
        }
    }
}

