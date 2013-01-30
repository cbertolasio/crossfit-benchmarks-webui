using System;
using System.Collections.Generic;
using System.Linq;

using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Models.Logger;
using AutoMapper;

namespace CrossfitBenchmarks.WebUi
{
    public static class AutomapBootstrap
    {
        public static void Initialize()
        {
            Mapper.CreateMap<AddLogEntryViewModel, LogEntryDto>()
                .ForMember(dest => dest.WorkoutLogId, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreatedAsString, opt => opt.MapFrom(src => src.DateCreated.Value.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz")));

            Mapper.CreateMap<WorkoutLogEntryDto, CrossfitBenchmarks.WebUi.Models.Logger.WodItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkoutId))
                .ForMember(dest => dest.LastAttemptDate, opt => opt.MapFrom(src => src.LastEntry != null ? src.LastEntry.DateCreated : DateTime.MinValue ))
                .ForMember(dest => dest.LastScore, opt => opt.MapFrom(src => src.LastEntry != null ? src.LastEntry.Score : null))
                .ForMember(dest => dest.LastPersonalRecordDate, opt => opt.MapFrom(src => src.LastPersonalRecord != null ? src.LastPersonalRecord.DateCreated : DateTime.MinValue ))
                .ForMember(dest => dest.PersonalRecordScore, opt => opt.MapFrom(src => src.LastPersonalRecord != null ? src.LastPersonalRecord.Score : null ))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WorkoutName));
        }
    }
}

