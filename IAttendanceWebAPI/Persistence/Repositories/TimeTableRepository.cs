using AutoMapper;
using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class TimeTableRepository : Repository<TimeTable>, ITimeTableRepository
    {
        public TimeTableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TimeTable> GetTimeTable(int id)
        {
            return await Get(predicate: t => t.Id == id);
        }


        public async Task<IEnumerable<TeacherTimeTableDto>> GetTimeTablesForTeacherByDate(string teacherId,
            DateTime date)
        {
            var timeTables = await Get(t => t.Date.Day == date.Day &&
                                            t.Date.Month == date.Month &&
                                            t.Date.Year == date.Year &&
                                            t.TeacherId == teacherId,
                t => t.OrderBy(s => s.TimeSlotId),
                "TimeSlot, StudentGroup, Room, Campus, Course, StatusTimeTable, StatusTakeAttendance");

            var timeTablesList = timeTables.ToList();
            var validatedTimeTable = await ValidationTimeTable(timeTablesList);
            return Mapper.Map<IEnumerable<TimeTable>, IEnumerable<TeacherTimeTableDto>>(validatedTimeTable);
        }

        public async Task<IEnumerable<StudentTimeTableDto>> GetTimeTablesForStudentByDate(string studentId,
            DateTime date)
        {
            var query = from t in DbContext.TimeTables
                        join a in DbContext.Attendances on t.Id equals a.TimeTableId
                        where t.Date.Day == date.Day &&
                              t.Date.Month == date.Month &&
                              t.Date.Year == date.Year &&
                              a.StudentId == studentId
                        select t;

            var timeTables = await Get(query,
                t => t.OrderBy(s => s.TimeSlotId),
                "TimeSlot, StudentGroup, Room, Campus, Course, StatusTimeTable, StatusTakeAttendance, Attendances, Attendances.StatusAttendance");
            var timeTablesList = timeTables.ToList();
            var validatedTimeTable = await ValidationTimeTable(timeTablesList);
            return validatedTimeTable.Select(c => new StudentTimeTableDto
            {
                StudentId = studentId,
                Id = c.Id,
                Date = c.Date,
                Course = Mapper.Map<Course, CourseDto>(c.Course),
                Campus = Mapper.Map<Campus, CampusDto>(c.Campus),
                Room = Mapper.Map<Room, RoomDto>(c.Room),
                TeacherId = c.TeacherId,
                TimeSlot = Mapper.Map<TimeSlot, TimeSlotDto>(c.TimeSlot),
                StudentGroup = Mapper.Map<StudentGroup, StudentGroupDto>(c.StudentGroup),
                StatusTimeTable = Mapper.Map<StatusTimeTable, StatusTimeTableDto>(c.StatusTimeTable),
                StatusAttendance = Mapper.Map<StatusAttendance, StatusAttendanceDto>(c.Attendances
                    .FirstOrDefault(at => at.StudentId == studentId && at.TimeTableId == c.Id)?.StatusAttendance)
            });
        }

        private async Task<IEnumerable<TimeTable>> ValidationTimeTable(IEnumerable<TimeTable> timeTables)
        {
            var today = DateTime.Now;
            await Task.Delay(1000);
            var timeNow = today.ToString("HH:mm:ss tt zz", CultureInfo.CurrentCulture);
            var timeNowSplited = timeNow.Split(':');
            var hour = int.Parse(timeNowSplited[0]);
            var minutes = int.Parse(timeNowSplited[1]);

            var slotIdNow =
                (await DbContext.TimeSlots.FirstOrDefaultAsync(t =>
                    t.StartTime.Hours <= hour
                    && t.StartTime.Minutes <= minutes
                    && t.EndTime.Hours >= hour
                    && t.EndTime.Minutes >= minutes))?.Id;

            var statusTimeTablesInDb = await DbContext.StatusTimeTables.ToListAsync();
            var timeTablesValidated = timeTables.ToList();
            foreach (var t in timeTablesValidated)
            {
                switch (t.StatusTakeAttendanceId)
                {
                    case (int)StatusTakeAttendanceType.Taken when t.StatusTimeTableId != (int)StatusTimeTableType.Finished:
                        t.StatusTimeTableId = (int)StatusTimeTableType.Finished;
                        t.StatusTimeTable =
                            statusTimeTablesInDb.FirstOrDefault(c => c.Id == (int)StatusTimeTableType.Finished);
                        break;
                    case (int)StatusTakeAttendanceType.Waiting:
                        if ((today.Day >= t.Date.Day && today.Month >= t.Date.Month
                                                    && today.Year >= t.Date.Year
                                                    && t.TimeSlotId > slotIdNow) || (today.Day > t.Date.Day))
                        {
                            t.StatusTimeTableId = (int)StatusTimeTableType.Canceled;
                            t.StatusTimeTable =
                                statusTimeTablesInDb.FirstOrDefault(c => c.Id == (int)StatusTimeTableType.Canceled);
                        }

                        if (today.Day == t.Date.Day && today.Month == t.Date.Month
                                                    && today.Year == t.Date.Year && t.TimeSlotId == slotIdNow)
                        {
                            t.StatusTimeTableId = (int)StatusTimeTableType.Happening;
                            t.StatusTimeTable =
                                statusTimeTablesInDb.FirstOrDefault(c => c.Id == (int)StatusTimeTableType.Happening);
                        }
                        break;
                }
                Update(t);
            }

            await DbContext.SaveChangesAsync();
            return timeTablesValidated;
        }
    }
}