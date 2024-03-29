﻿using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Triggers;
using Gamification.Core.ProjectSettings;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using IConfiguration = Gamification.Core.ProjectSettings.IConfiguration;

namespace Gamification.Web.AutoMapper.Mappings.ViewModels
{
    public class ActionTriggerViewModelConfiguration : IConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Project, ActionTriggerViewModel>()
                .ForMember(x => x.Statuses, opt => opt.MapFrom(x => x.Statuses))
                .ForMember(x => x.Achievements, opt => opt.MapFrom(x => x.Achievements))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.Ignore())
                ;


            Mapper.CreateMap<ActionTriggerViewModel, AchievementsTrigger>()
                .ForMember(x => x.Title, map => map.MapFrom(x => x.Title))
                .ForMember(x => x.ActionWithAchievement, map => map.MapFrom(x => x.AchievementAction))
                ;

            Mapper.CreateMap<ActionTriggerViewModel, AddOrRemoveStatusTrigger>()
                .ForMember(x => x.Title, map => map.MapFrom(x => x.Title))
                .ForMember(x => x.StatusAction, map => map.MapFrom(x => x.StatusAction))
                .ForMember(x => x.Status, map => map.Ignore())
                ;

            Mapper.CreateMap<ActionTriggerViewModel, ChangePointsTrigger>()
                .ForMember(x => x.Title, map => map.MapFrom(x => x.Title))
                .ForMember(x => x.Points, map => map.MapFrom(x => x.Points))
                .ForMember(x => x.PointsOperation, map => map.MapFrom(x => x.PointsOperation))
                ;
        }
    }
}