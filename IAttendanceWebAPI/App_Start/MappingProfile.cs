using AutoMapper;
using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;

namespace IAttendanceWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<TimeTable, StudentTimeTableDto>();
            CreateMap<TimeTable, TeacherTimeTableDto>();
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<Campus, CampusDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<StudentGroup, StudentGroupDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<TimeSlot, TimeSlotDto>();
            CreateMap<StatusTimeTable, StatusTimeTableDto>();
            CreateMap<StatusAttendance, StatusAttendanceDto>();
            CreateMap<StatusTakeAttendance, StatusTakeAttendanceDto>();
            CreateMap<StatusMessage, StatusMessageDto>();
        }
    }
}