-- BEGIN TRANSACTION

-- Delete all the data
DELETE FROM reservation;
DELETE FROM site;
DELETE FROM campground;
DELETE FROM park;



print 'Rows were removed'

-- Insert 2 parks
INSERT INTO park (name, location, establish_date, area, visitors, description)
VALUES ('Test', 'Ohio', '1919-02-26', 47389, 2563129, 'Test desc')
DECLARE @park1 int = (SELECT @@IDENTITY);
INSERT INTO park (name, location, establish_date, area, visitors, description)
VALUES ('Test2', 'Ohio', '2019-03-29', 20345, 200000, 'test desc');
DECLARE @park2 int = (SELECT @@IDENTITY);
-- Insert 1 campground into each park
-- Test
INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@park1, 'Blackwoods', 1, 12, 35.00);
DECLARE @camp1 int = (SELECT @@IDENTITY);
-- TestPark 2
INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@park2, 'Devil''s Garden', 1, 12, 25.00);
DECLARE @camp2 int = (SELECT @@IDENTITY);
-- Insert site into each campground
-- Test
INSERT INTO site (site_number, campground_id) VALUES (1, @camp1);
DECLARE @site int = (SELECT @@IDENTITY);
-- TestPark 2
INSERT INTO site (site_number, campground_id) VALUES (1, @camp2);
DECLARE @site2 int = (SELECT @@IDENTITY);
-- Insert reservations into each site
-- TestPark
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@site, 'Smith Family Reservation', '2019-06-21', '2019-06-30');
-- TestPark 2
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@site2, 'TEST', '2019-06-21', '2019-06-30');

-- ROLLBACK TRANSACTION