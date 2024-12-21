using Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DataExample
{
    public class ExampleDataAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            throw new NotImplementedException();
        }

        static Guid footballId = Guid.NewGuid();
        static Guid PadelId = Guid.NewGuid();

        static Guid footballPartAId = Guid.NewGuid();
        static Guid footballPartBId = Guid.NewGuid();
        static Guid padelPartAId = Guid.NewGuid();
        static Guid padelPartBId = Guid.NewGuid();

        static Dictionary<string, Guid> planningIds = new Dictionary<string, Guid>();
        static Dictionary<string, List<Guid>> timeRangeIds = new Dictionary<string, List<Guid>>();

        static void InitializeGuids()
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            foreach (var day in days)
            {
                // Assign a unique Guid for each day and part
                planningIds[day] = Guid.NewGuid();
                timeRangeIds[day] = Enumerable.Range(1, 9).Select(_ => Guid.NewGuid()).ToList();
            }
        }

        public static void Adding(MigrationBuilder migrationBuilder)
        {
            InitializeGuids();

            migrationBuilder.Sql($"INSERT INTO SportCategory VALUES ('{footballId}', NULL, 'Football', NULL, 'This is a description for football', '2024-12-21T18:18:05', NULL)");
            migrationBuilder.Sql($"INSERT INTO SportCategory VALUES ('{PadelId}', NULL, 'Padel', NULL, 'This is a description for padel', '2024-12-21T18:18:05', NULL)");

            // Football Part A and Part B insertions
            migrationBuilder.Sql($"INSERT INTO Sports VALUES ('{footballPartAId}', '{footballId}', 14, 14, 1, 'Condition for football part A', 'Football Part A', 'Description for football part A', NULL, '2024-12-21T18:18:05', NULL)");
            migrationBuilder.Sql($"INSERT INTO Sports VALUES ('{footballPartBId}', '{footballId}', 14, 14, 1, 'Condition for football part B', 'Football Part B', 'Description for football part B', NULL, '2024-12-21T18:18:05', NULL)");

            // Padel Part A and Part B insertions
            migrationBuilder.Sql($"INSERT INTO Sports VALUES ('{padelPartAId}', '{PadelId}', 14, 14, 1, 'Condition for padel part A', 'Padel Part A', 'Description for padel part A', NULL, '2024-12-21T18:18:05', NULL)");
            migrationBuilder.Sql($"INSERT INTO Sports VALUES ('{padelPartBId}', '{PadelId}', 14, 14, 1, 'Condition for padel part B', 'Padel Part B', 'Description for padel part B', NULL, '2024-12-21T18:18:05', NULL)");

            // Adding Plannings for Football and Padel
            foreach (var day in planningIds.Keys)
            {
                // Insert plannings for football part A and B with new unique GUIDs
                Guid footballPartAPlanningId = Guid.NewGuid();
                Guid footballPartBPlanningId = Guid.NewGuid();
                Guid padelPartAPlanningId = Guid.NewGuid();
                Guid padelPartBPlanningId = Guid.NewGuid();

                migrationBuilder.Sql($"INSERT INTO Plannings VALUES ('{footballPartAPlanningId}', '{footballPartAId}', {(Array.IndexOf(planningIds.Keys.ToArray(), day) + 1)}, '2024-12-21T18:18:05')");
                migrationBuilder.Sql($"INSERT INTO Plannings VALUES ('{footballPartBPlanningId}', '{footballPartBId}', {(Array.IndexOf(planningIds.Keys.ToArray(), day) + 1)}, '2024-12-21T18:18:05')");
                migrationBuilder.Sql($"INSERT INTO Plannings VALUES ('{padelPartAPlanningId}', '{padelPartAId}', {(Array.IndexOf(planningIds.Keys.ToArray(), day) + 1)}, '2024-12-21T18:18:05')");
                migrationBuilder.Sql($"INSERT INTO Plannings VALUES ('{padelPartBPlanningId}', '{padelPartBId}', {(Array.IndexOf(planningIds.Keys.ToArray(), day) + 1)}, '2024-12-21T18:18:05')");

                // Ensure unique GUIDs for TimeRanges for each planning entry
                for (int i = 0; i < 9; i++)
                {
                    string startTime = $"{13 + i}:00:00";
                    string endTime = $"{14 + i}:00:00";

                    // Generate unique GUIDs for each time range
                    Guid footballPartATimeRangeId = Guid.NewGuid();
                    Guid footballPartBTimeRangeId = Guid.NewGuid();
                    Guid padelPartATimeRangeId = Guid.NewGuid();
                    Guid padelPartBTimeRangeId = Guid.NewGuid();

                    migrationBuilder.Sql($"INSERT INTO TimeRanges VALUES ('{footballPartATimeRangeId}', '{startTime}', '{endTime}', '{footballPartAPlanningId}')");
                    migrationBuilder.Sql($"INSERT INTO TimeRanges VALUES ('{footballPartBTimeRangeId}', '{startTime}', '{endTime}', '{footballPartBPlanningId}')");
                    migrationBuilder.Sql($"INSERT INTO TimeRanges VALUES ('{padelPartATimeRangeId}', '{startTime}', '{endTime}', '{padelPartAPlanningId}')");
                    migrationBuilder.Sql($"INSERT INTO TimeRanges VALUES ('{padelPartBTimeRangeId}', '{startTime}', '{endTime}', '{padelPartBPlanningId}')");
                }
            }
        }

        public static void Removing(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM SportCategory WHERE Id = '{footballId}'");
            migrationBuilder.Sql($"DELETE FROM SportCategory WHERE Id = '{PadelId}'");

            foreach (var day in planningIds.Keys)
            {
                migrationBuilder.Sql($"DELETE FROM Plannings WHERE Id = '{planningIds[day]}'");
                foreach (var timeRangeId in timeRangeIds[day])
                {
                    migrationBuilder.Sql($"DELETE FROM TimeRanges WHERE Id = '{timeRangeId}'");
                }
            }
        }
    }
}
