CREATE PROCEDURE sp_AddEmployee
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Username VARCHAR(50),
    @Password VARCHAR(50),
    @DepartmentId INT
AS
BEGIN
    INSERT INTO UserAccount
    (
        FirstName,
        LastName,
        Email,
        Username,
        Password,
        ConfirmPassword,
        Role,
        DepartmentId,
        IsActive
    )
    VALUES
    (
        @FirstName,
        @LastName,
        @Email,
        @Username,
        @Password,
        @Password,
        'Employee',
        @DepartmentId,
        1
    )
END