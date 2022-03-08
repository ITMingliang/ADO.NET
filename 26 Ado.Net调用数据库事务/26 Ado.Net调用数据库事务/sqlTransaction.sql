--事务：一致性提交   回滚
--程序如何来调用事务？事务放在存储过程里
alter proc AddUserByTran
@UserName varchar(50),
@UserPwd varchar(50),
@Age int,
@DeptName nvarchar(50)
as
begin
begin tran
begin try
  --插入部门信息
  insert into DeptInfos(DeptName) values (@DeptName);
  declare @deptId int
  select @deptId=@@IDENTITY;
  --插入用户信息
  insert into UserInfos(UserName,UserPwd,Age,DeptId)
  values(@UserName,@UserPwd,@Age,@deptId)

  commit tran --提交
  return 1;
end try
begin catch
    rollback tran --回滚
	return 0;
end catch
end
go