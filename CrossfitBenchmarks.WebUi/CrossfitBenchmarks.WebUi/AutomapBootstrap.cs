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
                .ForMember(dest => dest.DateOfWodAsString, opt => opt.Ignore())
                .ForMember(dest => dest.UserInfo, opt => opt.Ignore());

                //.ForMember(dest => dest.DateOfWodAsString, opt => opt.MapFrom(src => src.DateOfWod.Value.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz")));

            Mapper.CreateMap<WorkoutLogEntryDto, CrossfitBenchmarks.WebUi.Models.Logger.WodItemViewModel>()
                .ForMember(dest => dest.LastPersonalRecordDateAsString, opt => opt.Ignore())
                .ForMember(dest => dest.LastAttemptDateAsString, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkoutId))
                .ForMember(dest => dest.LastAttemptDate, opt => opt.MapFrom(src => src.LastEntry != null ? src.LastEntry.DateOfWod : DateTime.MinValue ))
                .ForMember(dest => dest.LastScore, opt => opt.MapFrom(src => src.LastEntry != null ? src.LastEntry.Score : null))
                .ForMember(dest => dest.LastPersonalRecordDate, opt => opt.MapFrom(src => src.LastPersonalRecord != null ? src.LastPersonalRecord.DateOfWod : DateTime.MinValue ))
                .ForMember(dest => dest.PersonalRecordScore, opt => opt.MapFrom(src => src.LastPersonalRecord != null ? src.LastPersonalRecord.Score : null ))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WorkoutName));
        }
    }
}

