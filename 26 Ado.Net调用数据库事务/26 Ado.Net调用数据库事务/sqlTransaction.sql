--����һ�����ύ   �ع�
--�����������������������ڴ洢������
alter proc AddUserByTran
@UserName varchar(50),
@UserPwd varchar(50),
@Age int,
@DeptName nvarchar(50)
as
begin
begin tran
begin try
  --���벿����Ϣ
  insert into DeptInfos(DeptName) values (@DeptName);
  declare @deptId int
  select @deptId=@@IDENTITY;
  --�����û���Ϣ
  insert into UserInfos(UserName,UserPwd,Age,DeptId)
  values(@UserName,@UserPwd,@Age,@deptId)

  commit tran --�ύ
  return 1;
end try
begin catch
    rollback tran --�ع�
	return 0;
end catch
end
go