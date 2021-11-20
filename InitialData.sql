
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Initial_Data 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
	INSERT INTO testGymDB.[dbo].[Users] (FirstName, LastName, Email, Token, RoleId)
	VALUES ('Sean','Shaffer','sean@gmail.com','','1');

	INSERT INTO testGymDB.[dbo].[LoginCredentials](UserId, Username, PasswordHash, Attempts, LastAttempt)
	VALUES ('1','adminTest', '10000:QBN/urVo6+dJ7fYiW0DDPERv96SREHIH:ZWSxtyHXb8x829SF6hnwLf1t1vIcz9UYyRwZ0SDI5iY=', '0', '');

	INSERT INTO testGymDB.[dbo].[Users] (FirstName, LastName, Email, Token, RoleId)
	VALUES ('Ivy','Kelly','kelly@gmail.com','','2');

	INSERT INTO testGymDB.[dbo].[LoginCredentials](UserId, Username, PasswordHash, Attempts, LastAttempt)
	VALUES ('2','User', '10000:usH0/9D0I13AC850VU3vuxW6DSUYZ7Bv:OqWYZ6+BCPbl+yCx15WCLW9i5iemtgseRiwfclaf6vM=', '0', '');

	INSERT INTO testGymDB.[dbo].[Roles](Title)
	VALUES ('Admin');

	INSERT INTO testGymDB.[dbo].[Roles](Title)
	VALUES ('User');

	INSERT INTO testGymDB.[dbo].[Trainers](FirstName,LastName,Email)
	VALUES ('Eve', 'Trejo', 'trejo@gmail.com');

	INSERT INTO testGymDB.[dbo].[Gyms](StreetAdress, MaxPeople, OperationalHours, City, Email, Name, PhoneNumber, PostalCode)
	VALUES ('Drottninggatan','20', '6-22','Stockholm', 'gymexa@gmail.com', 'Gymex A', '123456789', '01345');

	INSERT INTO testGymDB.[dbo].[TrainingClasses](GymId,TrainerId, MaxPeople, Name, Description, Start, [End])
	VALUES ('1','1','20', 'Karate', 'Bjong', '2021-11-15 08:00:00.9620000', '2021-11-15 09:00:00.9620000');

	INSERT INTO testGymDB.[dbo].[TrainingClasses](GymId,TrainerId, MaxPeople, Name, Description, Start, [End])
	VALUES ('1','1','20', 'Taekwondo', 'Wuujah', '2021-11-16 10:00:00.9620000', '2021-11-16 11:00:00.9620000');

	INSERT INTO testGymDB.[dbo].[TrainingClasses](GymId,TrainerId, MaxPeople, Name, Description, Start, [End])
	VALUES ('1','1','20', 'Taekwondo', 'Wuujah', '2022-12-15 06:00:00.9620000', '2022-12-15 07:00:00.9620000');

	INSERT INTO testGymDB.[dbo].[TrainingClasses](GymId,TrainerId, MaxPeople, Name, Description, Start, [End])
	VALUES ('1','1','20', 'Box', 'Boom', '2022-12-15 08:00:00.9620000', '2022-12-15 09:00:00.9620000');

	INSERT INTO testGymDB.[dbo].[TrainingClasses](GymId,TrainerId, MaxPeople, Name, Description, Start, [End])
	VALUES ('1','1','20', 'Cardio', 'Phew', '2022-12-16 10:00:00.9620000', '2022-12-16 11:00:00.9620000');

	INSERT INTO testGymDB.[dbo].Bookings(GymId, UserId, Timestamp, Date, TrainerId, TrainingClassId)
	VALUES ('1','1', '2022-12-16 10:00:00.9620000', '2022-12-16 10:00:00.9620000', '1', '5');
END
GO

EXEC Initial_Data;