create proc GetDeptName
@DeptId int,
@DeptName nvarchar(50) output  --�������
as
begin
  select @DeptName=DeptName from DeptInfos
  where DeptId=@DeptId

end

--�����������
create proc GetDeptNameNew
@DeptName nvarchar(50) output  --�������
as
begin
  select @DeptName=DeptName from DeptInfos
  where DeptName like '%'+@DeptName+'%'

end

--����ֵ����
create proc GetUserAge
@UserId int
as
begin
  declare @age int
  select @age=Age from UserInfos 
  where UserId=@UserId
  return @age
end

--return ���ܷ���int���������ֵ
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