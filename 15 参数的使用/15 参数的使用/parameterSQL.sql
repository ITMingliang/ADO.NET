create proc GetDeptName
@DeptId int,
@DeptName nvarchar(50) output  --输出参数
as
begin
  select @DeptName=DeptName from DeptInfos
  where DeptId=@DeptId

end

--输入输出参数
create proc GetDeptNameNew
@DeptName nvarchar(50) output  --输出参数
as
begin
  select @DeptName=DeptName from DeptInfos
  where DeptName like '%'+@DeptName+'%'

end

--返回值参数
create proc GetUserAge
@UserId int
as
begin
  declare @age int
  select @age=Age from UserInfos 
  where UserId=@UserId
  return @age
end

--return 不能返回int类型以外的值
create proc GetUserName
@UserId int
as
begin
  declare @name int
  select @name=UserName from UserInfos 
  where UserId=@UserId
  return @name
end

exec GetUserName 31