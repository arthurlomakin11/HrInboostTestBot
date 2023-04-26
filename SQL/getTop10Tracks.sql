CREATE PROCEDURE dbo.getTop10Tracks @IMEI varchar(50)
AS 
WITH numbered AS (
  SELECT 
    *,
    ROW_NUMBER() OVER (ORDER BY date_track) AS rn,
    LAG(date_track) OVER (ORDER BY date_track) AS prev_time
  FROM 
    TrackLocation
  WHERE IMEI = @IMEI
),
groupped AS (
  SELECT 
    *, 
    CASE 
      WHEN prev_time IS NULL OR DATEDIFF(MINUTE, prev_time, date_track) > 30 
      THEN rn 
      ELSE NULL 
    END AS grp
  FROM 
    numbered
),
walks AS (
  SELECT 
    *, 
    SUM(CASE WHEN grp IS NOT NULL THEN 1 ELSE 0 END) OVER (ORDER BY date_track) AS WalkNum
  FROM 
    groupped
),
locations AS (
  SELECT 
    *, 
    GEOGRAPHY::Point(Latitude, Longitude, 4326) AS location,
    LAG(GEOGRAPHY::Point(Latitude, Longitude, 4326)) OVER (PARTITION BY WalkNum ORDER BY date_track) AS prev_location
  FROM 
    walks
)
SELECT TOP 10
  WalkNum,
        MIN(date_track) AS StartTime,
       MAX(date_track) AS EndTime,
       DATEDIFF(MINUTE, MIN(date_track), MAX(date_track)) AS DurationMinutes,
       COALESCE(SUM(locations.prev_location.STDistance(locations.location)), 0) AS DistanceMeters
FROM locations
GROUP BY WalkNum
ORDER BY DurationMinutes DESC