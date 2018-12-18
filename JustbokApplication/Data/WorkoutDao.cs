using JustbokApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Data
{
    public class WorkoutDao
    {
        public Result GetWorkouts(string description,int unitId,string orderBy, string direction,int startRowIndex, int maximumRows, int branchId)
        {
            Result objResult = new Result();
            try
            {
                var param = new DbParam[7];

                param[0] = new DbParam("@SortBy", orderBy, SqlDbType.VarChar);
                param[1] = new DbParam("@SortDirection", direction, SqlDbType.VarChar);
                param[2] = new DbParam("@StartRowIndex", startRowIndex, SqlDbType.Int);
                param[3] = new DbParam("@MaximumRows", maximumRows, SqlDbType.Int);
                param[4] = new DbParam("@BranchId", branchId, SqlDbType.Int);
                param[5] = new DbParam("@DescriptionSearch", description, SqlDbType.VarChar);
                param[6] = new DbParam("@UnitId", unitId, SqlDbType.Int);

                DataSet ds = Db.GetDataSet("SP_WORKOUT_GET", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Workout> workouts = new List<Workout>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Workout workout = new Workout();

                        workout.WorkoutId = Db.ToInteger(row["WorkoutId"]);
                        workout.Description = Db.ToString(row["Description"]);
                        workout.Unit = new Unit();
                        workout.Unit.Description = Db.ToString(row["Unit"]);
                        workouts.Add(workout);
                    }

                    objResult.Items = workouts;

                    objResult.ItemCount = 0;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResult.ItemCount = Db.ToInteger(ds.Tables[1].Rows[0]["WorkoutCount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return objResult;
        }

        public int WorkoutInsUpd(Workout workout, int StaffId)
        {
            int workoutId = 0;
            try
            {
                var param = new DbParam[6];

                param[0] = new DbParam("@WorkoutId", workout.WorkoutId, SqlDbType.Int);
                param[1] = new DbParam("@Description", workout.Description, SqlDbType.VarChar);
                param[2] = new DbParam("@UnitId", workout.Unit.UnitId, SqlDbType.Int);
                param[3] = new DbParam("@IsActive", workout.IsActive, SqlDbType.Bit);
                param[4] = new DbParam("@CreatedBy", StaffId, SqlDbType.Int);
                param[5] = new DbParam("@BranchId", workout.BranchId, SqlDbType.Int);

                workoutId = Db.Insert("SP_WORKOUT_INSUPD", param, true);
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return workoutId;
        }

        public Workout WorkoutById(int workoutId)
        {
            Workout workout = null;
            try
            {
                var param = new DbParam[1];

                param[0] = new DbParam("@WorkoutId", workoutId, SqlDbType.Int);

                DataRow row = Db.GetDataRow("SP_WORKOUT_BY_ID", param);

                if (row != null)
                {
                    workout = new Workout();


                    workout.WorkoutId = Db.ToInteger(row["WorkoutId"]);
                    workout.Description = Db.ToString(row["Description"]);
                    workout.Unit = new Unit();
                    workout.Unit.UnitId = Db.ToInteger(row["UnitId"]);
                    workout.Unit.Description = Db.ToString(row["Unit"]);
                    workout.IsActive = Db.ToBoolean(row["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return workout;
        }

        public int DeleteWorkoutById(int workoutId, int staffId)
        {
            int result = 0;
            try
            {
                var param = new DbParam[2];

                param[0] = new DbParam("@WorkoutId", workoutId, SqlDbType.Int);
                param[1] = new DbParam("@StaffId", staffId, SqlDbType.Int);

                result = Db.Insert("SP_WORKOUT_DELETE_BY_ID", param, false);

            }
            catch (Exception ex)
            {
                //CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return result;
        }

    }
}
